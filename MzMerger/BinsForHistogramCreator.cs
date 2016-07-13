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
        public static int[] binsForEdit2 = new int[21];
        public static int[] binsForRandom = new int[21];

        public static void createBins(List<PairsForComparison> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].editDist == 0)
                {
                    binsForEdit0[inputs[i].numberOfPeakMatches] ++;
                }
                if (inputs[i].editDist == 1)
                {
                    binsForEdit1[inputs[i].numberOfPeakMatches]++;
                }
                if (inputs[i].editDist == 2)
                {
                    binsForEdit2[inputs[i].numberOfPeakMatches]++;
                }
                if (inputs[i].editDist >= 3)
                {
                    binsForRandom[inputs[i].numberOfPeakMatches]++;
                }
            }
         
            string text = @"C:\Users\sieb277\Desktop\bins0.txt";
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
                //foreach (var i in binsForEdit0)
                //{
                //    writer.WriteLine(i);
                //}
            }
            //string text2 = @"C:\Users\sieb277\Desktop\bins1.txt";
            //using (TextWriter writer = File.CreateText(text2))
            //{

            //    foreach (var i in binsForEdit1)
            //    {
            //        writer.WriteLine(i);
            //    }
            //}
            //string text3 = @"C:\Users\sieb277\Desktop\bins2.txt";
            //using (TextWriter writer = File.CreateText(text3))
            //{

            //    foreach (var i in binsForEdit2)
            //    {
            //        writer.WriteLine(i);
            //    }
            //}
            //string text4 = @"C:\Users\sieb277\Desktop\binsRand.txt";
            //using (TextWriter writer = File.CreateText(text4))
            //{

            //    foreach (var i in binsForRandom)
            //    {
            //        writer.WriteLine(i);
            //    }
            //}

        }
    }
}
