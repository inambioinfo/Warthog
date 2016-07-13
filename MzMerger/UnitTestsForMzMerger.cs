using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MzMerger
{
    class UnitTestsForMzMerger
    {
        //Visual checks 5 random spectra collected in database have passed 07-12-2016
        //{a-37511:pass,

        [Test]
        public void testMergeSort()
        {
            List<double> testOneList = new List<double>(){12.5082,185.0801,1510.5461,2000.1215};
            List<double> testTwoList = new List<double>() { 13.5082, 186.0801, 1512.5461, 2022.1215};
            List<double> checkList = new List<double>(){ 12.5082, 13.5082, 185.0801, 186.0801, 1510.5461, 1512.5461, 2000.1215, 2022.1215 };
            Assert.That(new MergeSort().mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }

        [Test]
        public void testCalculatePeakMatchesZeroStart()//sometimes you might be missing one peak, but you do have the others, so this tests the first two matching
        {
            List<double> checkList = new List<double>() { 12.5082, 13.5082, 185.0801, 186.0801, 1510.5461, 1512.5461, 2000.1215, 2022.1215 };
            Assert.That(new MergeSort().calculatePeakMatches(checkList.ToArray()), Is.EqualTo(2));
        }

        [Test]
        public void testCalculatePeakMatchesFirstStartStart()//sometimes you might be missing one peak, but you do have the others, so this tests the first one being by itself
        {
            List<double> checkList = new List<double>() {1.000, 12.5082, 13.5082, 185.0801, 186.0801, 1510.5461, 1512.5461, 2000.1215, 2022.1215 };
            Assert.That(new MergeSort().calculatePeakMatches(checkList.ToArray()), Is.EqualTo(2));
        }

    }
}
