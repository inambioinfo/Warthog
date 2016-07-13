using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzMerger
{
    class PairsForComparison // this is an object containing the intial two sorted lists, one merged sorted list, and the number of peaks that match with a tolerance of 1
    {   public int IdofPairsTable { get; set; }
        public double[] mzListOne { get; set; }
        public double[] mzListTwo { get; set; }
        public double[] mergedList { get; set; }
        public int numberOfPeakMatches {get;set;}

        public PairsForComparison(string mzList1, string mzList2, int ID)
        {   
            mzListOne = mzList1.Split(',').Select(n => Convert.ToDouble(n)).ToArray();// convert the string from the database into an array of type double
            mzListTwo = mzList2.Split(',').Select(n => Convert.ToDouble(n)).ToArray();
            mergedList = new MergeSort().mergeSort(mzListOne.OfType<double>().ToList(), mzListTwo.OfType<double>().ToList());
            numberOfPeakMatches = new MergeSort().calculatePeakMatches(mergedList);
            IdofPairsTable = ID;
        }

        public void toDatabase()
        {
            string valuesForInputIntoDB = numberOfPeakMatches.ToString();
        }
    }
}
