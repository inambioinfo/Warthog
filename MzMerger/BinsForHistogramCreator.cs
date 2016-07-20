using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzMerger
{
    class BinsForHistogramCreator
    {
        //creates bins to store the peak matches at each edit distance
        public static int[] binsForEdit0 = new int[21];
        public static int[] binsForEdit1 = new int[21];
        public static int[] binsForEdit2 = new int[21];
        public static int[] binsForRandom = new int[21];

        public static void createBins(List<int> editDist, List<int> numberOfPeakMatches, string outputFilename)
        {
            for (int i = 0; i < editDist.Count; i++)
                if (editDist[i] == 0)
                {
                    if (numberOfPeakMatches[i] > 20)
                    {
                        Console.WriteLine("greater than 20, at 0 edit: " + numberOfPeakMatches[i]);
                    }
                    else { binsForEdit0[numberOfPeakMatches[i]]++; }
                }
                else if (editDist[i] == 1)
                {
                    if (numberOfPeakMatches[i] > 20)
                    {
                        Console.WriteLine("greater than 20, at 1 edit: " + numberOfPeakMatches[i]);
                    }
                    else { binsForEdit1[numberOfPeakMatches[i]]++; }
                }
                else if (editDist[i] == 2)
                {
                    if (numberOfPeakMatches[i] > 20)
                    {
                        Console.WriteLine("greater than 20, at 2 edit: " + numberOfPeakMatches[i]);
                    }
                    else { binsForEdit2[numberOfPeakMatches[i]]++; }
                }
                else if (editDist[i] == 1000) //1000 will specify random
                {
                    if (numberOfPeakMatches[i] > 20)
                    {
                        Console.WriteLine("greater than 20, random: " + numberOfPeakMatches[i]);
                    }
                    else { binsForRandom[numberOfPeakMatches[i]]++; }
                }
            
            string text = outputFilename;
            using (TextWriter writer = File.CreateText(text))
            {
                for (int i = 0; i < binsForEdit0.Length; i++)
                {
                    writer.Write(binsForEdit0[i]);
                    writer.Write("\t");
                    writer.Write(binsForEdit1[i]);
                    writer.Write("\t");
                    writer.Write(binsForEdit2[i]);
                    writer.Write("\t");
                    writer.Write(binsForRandom[i]);
                    writer.WriteLine();
                }
            }
            Console.WriteLine("Finished. Press any key to continue.");
            Console.ReadKey();
        }
    }
}
