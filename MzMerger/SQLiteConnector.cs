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
    class SQLiteConnector // here you will query the database and collect the matching pairs with their mzLists to send to the bin counter
    {
        //property accessors
        //public static List<string> inputs = new List<string>();
        public static List<double[]> mzList1List = new List<double[]>();
        public static List<double[]> mzList2List = new List<double[]>();
        public static List<string> aggregateValuesToDraw = new List<string>();//this is for drawing random values all at once
        public static List<int> editDistList = new List<int>();
        public static List<int> pairIDList = new List<int>();
        public static int numberOfRandomPairsToAdd = 100000;
        public static string inputDir { get; set; }
        public static string outputFilename { get; set; }

        //constructor
        public SQLiteConnector(ParseCommandLine options)
        {
            inputDir = options.InputDir;
            outputFilename = options.outputFileName;
            GetItemsFromDatabase(); // this opens the database and grabs all of our actual data
        }

        public static void GetRandomItemsFromDatabase()
        {
            Random rnd = new Random();
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {   //select the pair of mzLists, the edit distance for the pair, and the pair ID from the database
                    int counter = 0;
                    for (int p = 0; p < numberOfRandomPairsToAdd; p++)
                    {
                        int randint1 = rnd.Next(1, 20000);
                        int randint2 = rnd.Next(1, 20000);
                        string valuesToRandomlyDraw = "(" + randint1 + "," + randint2 + ")";
                        com.CommandText = "SELECT mzList from Spectrum WHERE ID IN " + valuesToRandomlyDraw;

                        using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                counter++;
                                if (counter % 2 != 0) //the counter is odd
                                {
                                    mzList1List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());
                                }
                                else if (counter % 2 == 0)//the counter is even 
                                {
                                    mzList2List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());//converts object into string and then into a double array
                                    editDistList.Add(1000);
                                }
                            }
                        }

                    }
                }
                con.Close(); // Close the connection to the database
            }
        }
        

        public static void GetItemsFromDatabase()
        //07-12-2016 need to update so that not reading all lines, only maybe 1000 at a time
        {
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {   //select the pair of mzLists, the edit distance for the pair, and the pair ID from the database
                    com.CommandText = "SELECT mzList, editDist, Pairs.ID from Spectrum, Pairs WHERE Spectrum.ID in (Pairs.id_scan1, Pairs.id_scan2)";

                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        int counter = 0;
                        while (reader.Read())
                        {
                            counter++;
                            //store mzList values into a list and store the pair edit distance value to a separate list, we can iterate through all three together 
                            if (counter % 2 == 0) //the counter is even 
                            {
                                mzList2List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());//converts object into string and then into a double array
                                editDistList.Add(Convert.ToInt32(reader["editDist"]));
                                pairIDList.Add(Convert.ToInt32(reader["ID"]));
                            }
                            else // the counter is odd
                            {
                                mzList1List.Add(reader["mzList"].ToString().Split(',').Select(n => Convert.ToDouble(n)).ToArray());
                            }
                        }
                        GetRandomItemsFromDatabase(); // this adds random values to the list before sending to the peak matcher
                        sendToFile(mzList1List, mzList2List, editDistList, pairIDList);
                    }
                }
                con.Close(); // Close the connection to the database 
            }
        }

        public static void sendToFile(List<double[]> mzList1List, List<double[]> mzList2List, List<int> editDistList, List<int> pairIdList)
        {   List<int> numberOfPeakMatchesList = new List<int>();
            int counterForLowScorePair = 0;
            for (int i = 0; i < mzList1List.Count; i++)
            {
                //THREE THINGS: PairID and , editDist, and MatchScore ------------------------------------------
                double[] mergedList = new MergeSort().mergeSort(mzList1List[i].OfType<double>().ToList(),
                    mzList2List[i].OfType<double>().ToList());
                int numberOfPeakMatches = new MergeSort().calculatePeakMatches(mergedList);
                //if (numberOfPeakMatches <=4 && editDistList[i] == 0)
                //{
                //    Console.WriteLine("low scoring Pair ID is: " + pairIdList[i]);
                //    counterForLowScorePair++;
                //}
                numberOfPeakMatchesList.Add(numberOfPeakMatches);
            }
            //Console.WriteLine("The total number of low scoring pairs is: " + counterForLowScorePair);
            //sending to the bin counter to create a histogram
            BinsForHistogramCreator.createBins(editDistList, numberOfPeakMatchesList, outputFilename);
        }
    }   
}



