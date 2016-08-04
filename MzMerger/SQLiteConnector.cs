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
        public static List<Library> listOfAllLibraries = new List<Library>();
        public static List<Query> listOfAllQueries = new List<Query>();
        public static List<string> listOfLibraryIDs = new List<string>();
        public static int numberOfRandomPairsToAdd =10000;
        public static string QueryDBPath { get; set; }
        public static string LibraryDBPath { get; set; }
        public static string QueryLibraryPairsDB { get; set; }
        public static string outputFilename { get; set; }
        public static string aggregateLibraryIDs { get; set; }

        //constructor
        public SQLiteConnector(ParseCommandLine options)
        {
            QueryDBPath = options.QueryDBPath;
            LibraryDBPath = options.LibraryDBPath;
            outputFilename = options.outputFileName;
            QueryLibraryPairsDB = options.QueryLibraryPairsDBPath;
            GetItemsFromDatabase(); // this opens the database and grabs all of our actual data
        }

        private static void GetItemsFromDatabase()
        {
            List<int> numberOfPeakMatchesList = new List<int>();
            List<string> toDatabaseList = new List<string>();
            var Binner = new Binner();
            using (
                System.Data.SQLite.SQLiteConnection conn =
                    new System.Data.SQLite.SQLiteConnection("data source=" + LibraryDBPath + ".db3"))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "ATTACH '" + QueryDBPath + ".db3' AS db2;";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "ATTACH '" + QueryLibraryPairsDB + ".db3' AS dbPair;";
                    cmd.ExecuteNonQuery();


                    //This select statement is equivalent to if I had used inner joins between the tables.  This version is just in an older style.
                    cmd.CommandText = "SELECT main.Spectrum.mzList, main.Spectrum.charge, main.Spectrum.precursor, main.Spectrum.peptide, main.Files.path, " +
                                       "db2.QuerySpectrum.mzList, db2.QuerySpectrum.charge, db2.QuerySpectrum.precursor, db2.Files.path " +
                                       "FROM main.Spectrum, main.Files, db2.Files, db2.QuerySpectrum, dbPair.QueryLibraryPair " +
                                       "WHERE main.Spectrum.ID = dbPair.QueryLibraryPair.id_Library AND db2.QuerySpectrum.ID = dbPair.QueryLibraryPair.id_Query " +
                                       "AND db2.QuerySpectrum.codex = db2.Files.codex AND main.Spectrum.codex = main.Files.codex " +
                                       "AND dbPair.QueryLibraryPair.PeakMatchNumber<11;";
                    
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        while (reader.Read())
                        {
                            Library oneLibrary = new Library(Convert.ToInt32(reader.GetString(1)), reader.GetString(3), reader.GetString(0).Split(',').Select(n => Convert.ToDouble(n)).ToArray(), reader.GetString(4), Convert.ToDouble(reader.GetString(2)));
                            Query oneQuery = new Query(Convert.ToInt32(reader.GetString(6)), reader.GetString(5).Split(',').Select(n => Convert.ToDouble(n)).ToArray(), reader.GetString(8), Convert.ToDouble(reader.GetString(7)));
                            listOfAllLibraries.Add(oneLibrary);
                            listOfAllQueries.Add(oneQuery);
                            if(listOfAllLibraries.Count %50 == 0) { Console.WriteLine("processed: " + listOfAllLibraries.Count); }
                            if (listOfAllLibraries.Count >= 500)
                            //if(listOfAllLibraries.Count %1000 ==0)
                            {
                                Binner.processor(listOfAllQueries, listOfAllLibraries);
                                listOfAllLibraries = new List<Library>();
                                listOfAllQueries = new List<Query>();
                               // break;
                            }
                        }
                    }
                    Binner.sendToFile(outputFilename);
                }
                conn.Close();
            }
        }   
    }
}


