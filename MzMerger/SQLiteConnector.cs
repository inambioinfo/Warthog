using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.AccessControl;
using NUnit.Framework.Constraints;

namespace MzMerger
{
    class SQLiteConnector // here you will query the database with a join and collect the matching pairs with their mzLists
    {
        //property accessors
        public static List<PairsForComparison> inputs = new List<PairsForComparison>();
        //public static List<string> inputs = new List<string>();
        public static List<string> mzList1List = new List<string>();
        public static List<string> mzList2List = new List<string>();
        public static List<int> editDistList = new List<int>();
        public static string inputDir { get; set; }

        //constructor
        public SQLiteConnector(ParseCommandLine options)
        {
            inputDir = options.InputDir;
            GetItemsFromDatabase();
        }

        public static void GetItemsFromDatabase() //07-12-2016 need to update so that not reading all lines, only maybe 1000 at a time ***cannot update until I know where the data is going, what needs to be stored, etc. 
        {   
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    com.CommandText = "SELECT mzList, editDist FROM Spectrum CROSS JOIN Pairs WHERE Pairs.id_scan1 = Spectrum.ID";
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                       int counter = 0;
                        while (reader.Read())
                        {
                            counter ++;
                            Console.WriteLine("Counter for first query: " + counter);
                            //store mzList values into a list and store the pair edit distance value to a separate list, we can iterate through all three together 
                            editDistList.Add(Convert.ToInt32(reader["editDist"]));
                            mzList1List.Add(reader["mzList"].ToString());
                            if (editDistList.Count >= 1000)
                            {
                                
                            }
                            //reader.Read() not exiting loop, so temporarily putting in this break to move on.  Will update after histogram creation 07-13-2016
                            if (counter == 11967)
                            {
                                break;
                            }
                        }
                    }

                    com.CommandText = "SELECT mzList FROM Spectrum CROSS JOIN Pairs WHERE Spectrum.ID = Pairs.id_scan2";
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter ++;
                            //store the mzList into a list
                            mzList2List.Add(reader["mzList"].ToString());
                            Console.WriteLine("Counter for second query: " + counter);

                            if (counter == 11967)
                            {
                                break;
                            }
                        }
                    }
                }
                con.Close(); // Close the connection to the database
            }
            for (int i = 0; i  < mzList1List.Count; i++)
            {   Console.WriteLine("making it inside of input creator");
                //store the two lists and then the ID for the pair and the edit distance
                inputs.Add(new PairsForComparison(mzList1List[i], mzList2List[i], editDistList[i], i));
                if (inputs.Count >= 1000)// send over 1000 at a time 
                {
                    Console.WriteLine("making it to 1000 pairs inside of the inputs list");
                    BinsForHistogramCreator.createBins(inputs);
                    inputs = new List<PairsForComparison>(); //clear out list before next round
                }
                
            }
                
            //    //change to dump only at maybe 800-1000 list size
            //for (int i = 0; i < inputs.Count; i++) 
            //{
            //    Console.WriteLine("final" + inputs[i].numberOfPeakMatches);  
            //} 
        }   
    }
}


