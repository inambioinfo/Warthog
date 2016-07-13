using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzMerger
{
    class MergeSort // this will contain a method for merge sorting the two arrays and also a method to check how many duplicates there are
    {
        public static List<double> emptyList = new List<double>();
        public static List<double> mergedList = new List<double>();
        public int numberOfPeakMatches = 0;
        public int tolerance = 1;

        public double[] mergeSort(List<double> mzListOne, List<double> mzListTwo)
        { 
            while (mzListOne.Count > 0 &&  mzListTwo.Count > 0)//mzListOne != null || mzListTwo != null ||
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
            while (mzListOne != null && mzListOne.Count >0)
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

        public int calculatePeakMatches(double[] mergedArray)
        {   
            for (int i = 0; i < mergedArray.Length-1; i++)
            {
                if (Math.Abs(mergedArray[i] - mergedArray[i + 1]) <= tolerance)
                {
                    numberOfPeakMatches ++;
                }
            }
            return numberOfPeakMatches;
        }
    }
}




