using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MzMerger
{
    class MainProgram
    {
        private static List<double> emptyList = new List<double>();
        private static List<double> mergedList = new List<double>();

        static void Main(string[] args)
        {
            var options = new ParseCommandLine();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                // Values are available here
                Console.WriteLine("Starting Program.");
                new SQLiteConnector(options);
                Console.WriteLine("Finished.");
            }
        }
        public static double[] mergeSort(List<double> mzListOne, List<double> mzListTwo)
        {
            while (mzListOne.Count > 0 && mzListTwo.Count > 0)//mzListOne != null || mzListTwo != null ||
            {
                if (mzListOne.First() <= mzListTwo.First())
                {
                    emptyList.Add(mzListOne.First());
                    mzListOne.Remove(mzListOne.First()); //drop the head of mzListOne
                }
                else
                {
                    emptyList.Add(mzListTwo.First());
                    mzListTwo.Remove(mzListTwo.First()); //drop the head of mzListTwo
                }
            }
            while (mzListOne != null && mzListOne.Count > 0)
            {
                emptyList.Add(mzListOne.First());
                mzListOne.Remove(mzListOne.First()); //drop the head of mzListOne
            }
            while (mzListTwo != null && mzListTwo.Count > 0)
            {
                emptyList.Add(mzListTwo.First());
                mzListTwo.Remove(mzListTwo.First()); //drop the head of mzListTwo
            }
            mergedList = emptyList;
            emptyList = new List<double>();
            return mergedList.ToArray();
        }
    }
}
