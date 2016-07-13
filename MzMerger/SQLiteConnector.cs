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
            Console.WriteLine("Getting items from database.");
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();

                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    com.CommandText = "SELECT mzList, editDist FROM Spectrum CROSS JOIN Pairs WHERE Spectrum.ID = Pairs.id_scan1";// select the mzList where the id from the pairs table matches the id from the spectrum table
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {   
                            //store mzList values into a list and store the pair edit distance value to a separate list, we can iterate through all three together 
                            editDistList.Add(Convert.ToInt32(reader["editDist"]));
                            mzList1List.Add(reader["mzList"].ToString());
                        }
                    }
                    com.CommandText = "SELECT mzList FROM Spectrum CROSS JOIN Pairs WHERE Spectrum.ID = Pairs.id_scan2"; 
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //store the mzList into a list
                            mzList2List.Add(reader["mzList"].ToString());
                        }
                    }
                    for (int i = 0; i  < mzList1List.Count; i++)//
                    {   
                        //store the two lists and then the ID for the pair
                        inputs.Add(new PairsForComparison(mzList1List[i], mzList2List[i], editDistList[i], i));
                    }
                }
                con.Close(); // Close the connection to the database
            }
        }

        public static void putObjectsIntoHist(string valuesToInsert)
        {
            //you will get a string and then input to the database every 800-1000 or so
        }
    }
}

