using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MzMerger
{
    class Binner
        
        //create constructors
    {   //creates bins to store the peak matches at each edit distance
        private int[] binsForHistogram;
        private int numberOfPeakMatches = 0;
        private int numberOfPeakMatchesAfterShifting = 0;
        
        private double tolerance = 0.01;// the m/z value for which we could consider peaks to be the same thing. Updated to 0.01, getting matches of over 20

        public Binner()
        {
            binsForHistogram = new int[21];
        }

        public  void processor(List<Query> listOfAllQueries, List<Library> listOfAllLibraries)
        {
            List<int> numberOfPeakMatchesList = new List<int>();
            Stopwatch time = new Stopwatch();
            time.Start();
            int counter = 0;
            // you will need to iterate through this list to process the pairs
            for (int i = 0; i < listOfAllQueries.Count; i++)
            {
                counter++;
                if (counter % 1000000 == 0) { Console.WriteLine("Processed: " + counter + ", Elapsed Time: " + time.ElapsedMilliseconds); }//only output a signal every million pairs processed
                for (int k = 0; k < listOfAllLibraries[i].Sequence.Length; k++)
                {
                    double massDiff = calculateTheMassDiff(listOfAllLibraries[i], listOfAllQueries[i]);
                    numberOfPeakMatches = 0;
                    Library newLibAfterShifting = Library.peakShifter(listOfAllLibraries[i], massDiff, k);
                    double[] mergedListAfterShifting = MainProgram.mergeSort(listOfAllLibraries[i].MzList.ToList<double>(), listOfAllQueries[i].MzList.ToList<double>()); // this needs to send the original Library and Query mzLists to be checked for similarity
                    numberOfPeakMatches = calculatePeakMatches(mergedListAfterShifting);
                }
                numberOfPeakMatchesList.Add(numberOfPeakMatches);

            }
            //sending to the bin counter to create a histogram
            createBins(numberOfPeakMatchesList); // this will no longer have an edit distance
        }
        
        private double calculateTheMassDiff(Library newLib, Query newQuery)
        {
            double libraryMass = (newLib.Precursor * newLib.Charge) + newLib.Charge;
            double queryMass = (newQuery.Precursor * newQuery.Charge) + newQuery.Charge;
            return libraryMass - queryMass;
        }

        public int calculatePeakMatches(double[] mergedArray)
        {
            numberOfPeakMatches = 0;
            for (int i = 0; i < mergedArray.Length - 1; i++)
            {
                if (Math.Abs(mergedArray[i] - mergedArray[i + 1]) <= tolerance)
                {
                    numberOfPeakMatches++;
                }
            }
            return numberOfPeakMatches;
        }

        public void createBins(List<int> numberOfPeakMatches)
        {
            for (int i = 0; i < numberOfPeakMatches.Count; i++)
            {
                if (numberOfPeakMatches[i] > 20)
                {   //at this time, not storing matches over 20, because they are invalid, most likely due to two or more values in a single m/z list being the same by coincidence
                    Console.WriteLine("greater than 20, at 0 edit: " + numberOfPeakMatches[i]);
                }
                else { binsForHistogram[numberOfPeakMatches[i]]++; }
            }
        }
        public void sendToFile(string outputFileName)
        {
            
            using (TextWriter writer = File.CreateText(outputFileName))
            {
                for (int i = 0; i < binsForHistogram.Length; i++)
                {
                    writer.Write(binsForHistogram[i] + "\n");
                }
            }
            Console.WriteLine("Finished. Press any key to continue.");
            Console.ReadKey();

        }
    }
}
