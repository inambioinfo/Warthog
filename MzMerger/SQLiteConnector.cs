using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
        //public static List<string> inputs = new List<string>();
        public static List<double[]> mzList1List = new List<double[]>();
        public static List<double[]> mzList2List = new List<double[]>();//public static List<string> mzList2List = new List<string>();
        public static List<int> editDistList = new List<int>();
        public static List<int> pairIDList = new List<int>();
        public static string inputDir { get; set; }

        //constructor
        public SQLiteConnector(ParseCommandLine options)
        {
            inputDir = options.InputDir;
            GetItemsFromDatabase();
        }

        public static void GetItemsFromDatabase()
            //07-12-2016 need to update so that not reading all lines, only maybe 1000 at a time ***cannot update until I know where the data is going, what needs to be stored, etc. 
        {
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {   //select the pair of mzLists, the edit distance for the pair, and the pair ID from the database
                    com.CommandText = "SELECT mzList, editDist ,Pairs.ID from Spectrum, Pairs WHERE Spectrum.ID in (Pairs.id_scan1, Pairs.id_scan2)";
                    
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter ++;
                            //store mzList values into a list and store the pair edit distance value to a separate list, we can iterate through all three together 
                            if (counter%2 == 0) //the counter is even 
                            {
                                mzList2List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());//converts object into string and then into a double array
                                editDistList.Add(Convert.ToInt32(reader["editDist"]));
                                pairIDList.Add(Convert.ToInt32(reader["ID"]));
                            }
                            else // the counter is odd
                            {
                                mzList1List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());
                            }
                            //sendToFile(mzList1List, mzList2List, editDistList, pairIDList);
                            //if (editDistList.Count >= 1000)
                            //{
                            //    //here storing only 1000 pairs and then sending off to the bin creator
                            //    sendToFile(mzList1List, mzList2List, editDistList, pairIDList);
                            //    mzList1List = new List<double[]>();
                            //    mzList2List = new List<double[]>();
                            //    editDistList = new List<int>();
                            //    pairIDList = new List<int>();
                            //}
                        }
                        sendToFile(mzList1List, mzList2List, editDistList, pairIDList);
                    }
                }
                con.Close(); // Close the connection to the database
            }
        }

        public static void sendToFile(List<double[]> mzList1List, List<double[]> mzList2List, List<int> editDistList, List<int> pairIdList)
        {   List<int> numberOfPeakMatchesList = new List<int>();

            for (int i = 0; i < mzList1List.Count; i++)
            {
                //THREE THINGS: PairID and , editDist, and MatchScore ------------------------------------------
                double[] mergedList = new MergeSort().mergeSort(mzList1List[i].OfType<double>().ToList(),
                    mzList2List[i].OfType<double>().ToList());
                int numberOfPeakMatches = new MergeSort().calculatePeakMatches(mergedList);
                //Console.WriteLine("the number of peak matches is: " + numberOfPeakMatches);
                //Console.ReadKey();
                numberOfPeakMatchesList.Add(numberOfPeakMatches);
            }

            BinsForHistogramCreator.createBins(editDistList, numberOfPeakMatchesList);

            //send each of the elements to the bin creator
            //BinsForHistogramCreator.createBins(inputs);
            //inputs = new List<PairsForComparison>(); //clear out list before next round

        }
    }   
}



