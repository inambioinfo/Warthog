using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandLine;
using CommandLine.Text;


namespace MzMerger
{
    class MainProgram
    {
        //public string options.outputFilename { get; set;}
        //create constructors
        static void Main(string[] args)
        {
            var options = new ParseCommandLine();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {  
                // Values are available here
                Console.WriteLine("Starting Program.");
                new SQLiteConnector(options); //.ToFile(); // this creates a new SortPairs and outputs the info to file/'

                Console.WriteLine("Finished.");
            }
        }
        
        // This is the main program that calls all of the other functions
        public static void sendToFile(List<double[]> mzList1List, List<double[]> mzList2List, List<int> editDistList, List<int> pairIdList, string outputFilename)
        {
            List<int> numberOfPeakMatchesList = new List<int>();
            for (int i = 0; i < mzList1List.Count; i++)
            {
                //THREE THINGS: PairID and , editDist, and MatchScore ------------------------------------------
                double[] mergedList = new MergeSort().mergeSort(mzList1List[i].OfType<double>().ToList(),
                    mzList2List[i].OfType<double>().ToList());
                int numberOfPeakMatches = new MergeSort().calculatePeakMatches(mergedList);
                numberOfPeakMatchesList.Add(numberOfPeakMatches);
            }
            //Console.WriteLine("The total number of low scoring pairs is: " + counterForLowScorePair);
            //sending to the bin counter to create a histogram
            BinsForHistogramCreator.createBins(editDistList, numberOfPeakMatchesList, outputFilename);
        }
    }
}
