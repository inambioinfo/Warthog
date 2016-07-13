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
                    com.CommandText = "SELECT mzList FROM Spectrum CROSS JOIN Pairs WHERE Spectrum.ID = Pairs.id_scan1";// select the mzList where the id from the pairs table matches the id from the spectrum table
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //store mzList values into a list
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
                        //store mzList1 and mzList2 into an object holding those values
                        inputs.Add(new PairsForComparison(mzList1List[i], mzList2List[i], i));
                        //inputs.Add(new PairsForComparison(mzList1List[i], mzList2List[i], i).toDatabase());
                        //putObjectsIntoNewDB(inputs Join);
                        //inputs = new List<string>(); //this is data dumping instead of storing all into memory
                    }
                }
                con.Close(); // Close the connection to the database
            }
            //do something with list of inputs now
            //for (int i =0; i< inputs.Count; i++) { Console.WriteLine("the values are: " + inputs[i].numberOfPeakMatches);}
        }

        public static void putObjectsIntoNewDB(string valuesToInsert)
        {
            //you will get a string and then input to the database every 800-1000 or so
        }
    }
}

