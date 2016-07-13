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
        public int editDist { get; set; }
        public int numberOfPeakMatches {get;set;}

        public PairsForComparison(string mzList1, string mzList2, int edit_Dist,int ID)
        {   
            mzListOne = mzList1.Split(',').Select(n => Convert.ToDouble(n)).ToArray();// convert the string from the database into an array of type double
            mzListTwo = mzList2.Split(',').Select(n => Convert.ToDouble(n)).ToArray();
            editDist = edit_Dist;
            mergedList = new MergeSort().mergeSort(mzListOne.OfType<double>().ToList(), mzListTwo.OfType<double>().ToList());
            numberOfPeakMatches = new MergeSort().calculatePeakMatches(mergedList);
            IdofPairsTable = ID;
        }

        //public void toHistBin()
        //{
        //    string valuesForInputIntoHist = numberOfPeakMatches.ToString() + "," + editDist;// instead of this you need to create bins that contain the count of the 0-20 
        //}
    }
}
