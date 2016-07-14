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

        public static int[] binsForEdit0 = new int[21];
        public static int[] binsForEdit1 = new int[21];
        //public static int[] binsForEdit2 = new int[20];
        public static int[] binsForRandom = new int[21];

        public static void createBins(List<int> editDist, List<int> numberOfPeakMatches)
            //List<PairsForComparison> inputs
        {
            for (int i = 0; i < editDist.Count; i++)
                if (editDist[i] == 0)
                {
                    Console.WriteLine("error here: " + (numberOfPeakMatches[i]));
                    binsForEdit0[numberOfPeakMatches[i]] ++;
                }
                else if (editDist[i] == 1)
                {
                    binsForEdit1[numberOfPeakMatches[i]]++;
                }
                else if (editDist[i] == 1000) //1000 will specify random
                {
                    binsForRandom[numberOfPeakMatches[i]]++;
                }
                //if (inputs[i].editDist == 2)
                //{
                //    binsForEdit2[inputs[i].numberOfPeakMatches]++;
                //}
                //if (inputs[i].editDist >= 3)
                //{
                //    binsForRandom[inputs[i].numberOfPeakMatches]++;
                //}


            string text = @"C:\Users\sieb277\Desktop\bins0.txt";
            using (TextWriter writer = File.CreateText(text))
            {
                for (int i = 0; i < binsForEdit0.Length; i++)
                {
                    writer.Write(binsForEdit0[i]);
                    writer.Write("\t");
                    writer.Write(binsForEdit1[i]);
                    writer.Write("\t");
                    writer.Write(binsForRandom[i]);
                    writer.WriteLine();
                }

            }
        }
    }
}


//for (int i = 0; i<inputs.Count; i++)
//            {
//                if (inputs[i].editDist == 0)
//                {
//                    binsForEdit0[inputs[i].numberOfPeakMatches] ++;
//                }
//                if (inputs[i].editDist == 1)
//                {
//                    binsForEdit1[inputs[i].numberOfPeakMatches]++;
//                }
//                //if (inputs[i].editDist == 2)
//                //{
//                //    binsForEdit2[inputs[i].numberOfPeakMatches]++;
//                //}
//                //if (inputs[i].editDist >= 3)
//                //{
//                //    binsForRandom[inputs[i].numberOfPeakMatches]++;
//                //}
//            }