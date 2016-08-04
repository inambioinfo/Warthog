﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MzMerger
{
    class UnitTestsForMzMerger
    {
        [Test]
        public void testMergeSort()
        {
            List<double> testOneList = new List<double>() { 129.1021, 204.1342, 216.134, 315.2028, 420.2077, 497.3074, 515.3183, 576.2971, 614.3826, 705.3383, 814.9211, 815.4234, 833.432, 932.4996, 933.5035, 1047.5243, 1160.6065, 1291.6455, 1292.6493, 1390.7148 };
            List<double> testTwoList = new List<double>() { 129.1023, 204.1343, 216.1342, 315.2031, 420.2088, 497.3077, 515.3183, 576.2971, 614.3839, 705.3389, 815.4231, 833.433, 932.5014, 1047.5259, 1291.6454, 1369.1514, 1369.6582, 1370.1592, 1370.6724, 1390.7145 };
            List<double> checkList = new List<double>() { 129.1021, 129.1023, 204.1342, 204.1343, 216.134, 216.1342, 315.2028, 315.2031, 420.2077, 420.2088, 497.3074, 497.3077, 515.3183, 515.3183, 576.2971, 576.2971, 614.3826, 614.3839, 705.3383, 705.3389, 814.9211, 815.4231, 815.4234, 833.432, 833.433, 932.4996, 932.5014, 933.5035, 1047.5243, 1047.5259, 1160.6065, 1291.6454, 1291.6455, 1292.6493, 1369.1514, 1369.6582, 1370.1592, 1370.6724, 1390.7145, 1390.7148 };
            Assert.That(MainProgram.mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }

        [Test]
        public void testMergeSort2()
        {
            List<double> testOneList = new List<double>() { 129.1021, 204.1342, 216.134, 315.2028, 420.2077, 497.3074, 515.3183, 576.2971, 614.3826, 705.3383, 814.9211, 815.4234, 833.432, 932.4996, 933.5035, 1047.5243, 1160.6065, 1291.6455, 1292.6493, 1390.7148 };
            List<double> testTwoList = new List<double>() { 120.0807, 129.1021, 186.1234, 204.1341, 216.1339, 244.0926, 262.118, 343.161, 391.1608, 399.1982, 420.2074, 515.3185, 576.2958, 614.3903, 705.338, 833.4318, 932.5006, 1047.5251, 1291.6463, 1390.7162 };
            List<double> checkList = new List<double>() { 120.0807, 129.1021, 129.1021, 186.1234, 204.1341, 204.1342, 216.1339, 216.134, 244.0926, 262.118, 315.2028, 343.161, 391.1608, 399.1982, 420.2074, 420.2077, 497.3074, 515.3183, 515.3185, 576.2958, 576.2971, 614.3826, 614.3903, 705.338, 705.3383, 814.9211, 815.4234, 833.4318, 833.432, 932.4996, 932.5006, 933.5035, 1047.5243, 1047.5251, 1160.6065, 1291.6455, 1291.6463, 1292.6493, 1390.7148, 1390.7162 };
            Assert.That((MainProgram.mergeSort(testOneList, testTwoList)), Is.EqualTo(checkList));
        }

        [Test]
        public void testMergeSort3()
        {
            List<double> testOneList = new List<double>() { 115.0866, 129.1022, 136.0756, 143.0813, 205.1005, 226.1183, 234.1445, 244.129, 349.1711, 512.2333, 731.3385, 731.8417, 882.8914, 883.3919, 883.8919, 932.4258, 932.9235, 933.4252, 981.9614, 982.4613 };
            List<double> testTwoList = new List<double>() { 102.0553, 104.0532, 115.0868, 120.081, 129.1024, 136.0758, 143.0815, 147.1129, 175.1191, 205.0997, 226.1184, 234.1445, 244.1292, 346.1235, 512.2339, 731.3389, 882.8903, 883.3907, 932.4248, 932.9239 };
            List<double> checkList = new List<double>() { 102.0553, 104.0532, 115.0866, 115.0868, 120.081, 129.1022, 129.1024, 136.0756, 136.0758, 143.0813, 143.0815, 147.1129, 175.1191, 205.0997, 205.1005, 226.1183, 226.1184, 234.1445, 234.1445, 244.129, 244.1292, 346.1235, 349.1711, 512.2333, 512.2339, 731.3385, 731.3389, 731.8417, 882.8903, 882.8914, 883.3907, 883.3919, 883.8919, 932.4248, 932.4258, 932.9235, 932.9239, 933.4252, 981.9614, 982.4613 };
            Assert.That((MainProgram.mergeSort(testOneList, testTwoList)), Is.EqualTo(checkList));
        }

        [Test]
        public void testCalculatePeakMatchesZeroPull1()//sometimes you might be missing one peak, but you do have the others, so this tests the first two matching
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 129.1021, 129.1023, 204.1342, 204.1343, 216.134, 216.1342, 315.2028, 315.2031, 420.2077, 420.2088, 497.3074, 497.3077, 515.3183, 515.3183, 576.2971, 576.2971, 614.3826, 614.3839, 705.3383, 705.3389, 814.9211, 815.4231, 815.4234, 833.432, 833.433, 932.4996, 932.5014, 933.5035, 1047.5243, 1047.5259, 1160.6065, 1291.6454, 1291.6455, 1292.6493, 1369.1514, 1369.6582, 1370.1592, 1370.6724, 1390.7145, 1390.7148 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(16));
        }

        [Test]
        public void testCalculatePeakMatchesZeroPull2()//sometimes you might be missing one peak, but you do have the others, so this tests the first two matching
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 120.0807, 129.1021, 129.1021, 186.1234, 204.1341, 204.1342, 216.1339, 216.134, 244.0926, 262.118, 315.2028, 343.161, 391.1608, 399.1982, 420.2074, 420.2077, 497.3074, 515.3183, 515.3185, 576.2958, 576.2971, 614.3826, 614.3903, 705.338, 705.3383, 814.9211, 815.4234, 833.4318, 833.432, 932.4996, 932.5006, 933.5035, 1047.5243, 1047.5251, 1160.6065, 1291.6455, 1291.6463, 1292.6493, 1390.7148, 1390.7162 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(13));
        }

        [Test]
        public void testCalculatePeakMatchesZeroPull3()//sometimes you might be missing one peak, but you do have the others, so this tests the first two matching
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 102.0553, 104.0532, 115.0866, 115.0868, 120.081, 129.1022, 129.1024, 136.0756, 136.0758, 143.0813, 143.0815, 147.1129, 175.1191, 205.0997, 205.1005, 226.1183, 226.1184, 234.1445, 234.1445, 244.129, 244.1292, 346.1235, 349.1711, 512.2333, 512.2339, 731.3385, 731.3389, 731.8417, 882.8903, 882.8914, 883.3907, 883.3919, 883.8919, 932.4248, 932.4258, 932.9235, 932.9239, 933.4252, 981.9614, 982.4613 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(14));
        }

        //--------------------Edit Distance of 1----------------------------
        [Test]
        public void testMergeSortOne1()//Spectrum IDs:8942, 103
        {
            List<double> testOneList = new List<double>() { 104.053, 110.0713, 120.0807, 129.1022, 136.0756, 147.1126, 157.1333, 173.1281, 185.1283, 201.1232, 251.1025, 260.1966, 279.0974, 300.1549, 449.2359, 622.3544, 1001.5563, 1002.5616, 1100.6265, 1387.6953 };
            List<double> testTwoList = new List<double>() { 110.0711, 136.0753, 157.1332, 175.1184, 185.1282, 229.1179, 251.1025, 279.0975, 300.1554, 301.1584, 362.1354, 643.4247, 859.5001, 987.5576, 1157.6617, 1158.6645, 1256.7299, 1257.7325, 1300.7061, 1369.8097 };
            List<double> checkList = new List<double>() { 104.053, 110.0711, 110.0713, 120.0807, 129.1022, 136.0753, 136.0756, 147.1126, 157.1332, 157.1333, 173.1281, 175.1184, 185.1282, 185.1283, 201.1232, 229.1179, 251.1025, 251.1025, 260.1966, 279.0974, 279.0975, 300.1549, 300.1554, 301.1584, 362.1354, 449.2359, 622.3544, 643.4247, 859.5001, 987.5576, 1001.5563, 1002.5616, 1100.6265, 1157.6617, 1158.6645, 1256.7299, 1257.7325, 1300.7061, 1369.8097, 1387.6953 };
            Assert.That(MainProgram.mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }
        [Test]
        public void testCalculatePeakMatchesOnePull1()//sometimes you might be missing one peak, but you do have the others, so this tests the first two matching
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 104.053, 110.0711, 110.0713, 120.0807, 129.1022, 136.0753, 136.0756, 147.1126, 157.1332, 157.1333, 173.1281, 175.1184, 185.1282, 185.1283, 201.1232, 229.1179, 251.1025, 251.1025, 260.1966, 279.0974, 279.0975, 300.1549, 300.1554, 301.1584, 362.1354, 449.2359, 622.3544, 643.4247, 859.5001, 987.5576, 1001.5563, 1002.5616, 1100.6265, 1157.6617, 1158.6645, 1256.7299, 1257.7325, 1300.7061, 1369.8097, 1387.6953 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(7));
        }

        [Test]
        public void testMergeSortOne2()//83,28034
        {
            List<double> testOneList = new List<double>() { 143.1178, 171.1126, 181.0971, 199.1076, 213.0869, 231.0976, 254.1498, 272.1603, 328.1621, 383.1919, 488.3056, 851.4489, 932.4659, 1031.5328, 1177.6372, 1178.6394, 1314.6951, 1364.6927, 1463.7149, 1514.8089 };
            List<double> testTwoList = new List<double>() { 110.0716, 120.081, 129.1025, 136.0759, 143.1181, 147.1129, 171.113, 173.1288, 180.1021, 199.1081, 231.0968, 254.1501, 261.1244, 272.1606, 383.1932, 828.4752, 828.9772, 829.4727, 1163.6329, 1164.6385 };
            List<double> checkList = new List<double>() { 110.0716, 120.081, 129.1025, 136.0759, 143.1178, 143.1181, 147.1129, 171.1126, 171.113, 173.1288, 180.1021, 181.0971, 199.1076, 199.1081, 213.0869, 231.0968, 231.0976, 254.1498, 254.1501, 261.1244, 272.1603, 272.1606, 328.1621, 383.1919, 383.1932, 488.3056, 828.4752, 828.9772, 829.4727, 851.4489, 932.4659, 1031.5328, 1163.6329, 1164.6385, 1177.6372, 1178.6394, 1314.6951, 1364.6927, 1463.7149, 1514.8089 };
            Assert.That(MainProgram.mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }
        [Test]
        public void testCalculatePeakMatchesOnePull2()
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 110.0716, 120.081, 129.1025, 136.0759, 143.1178, 143.1181, 147.1129, 171.1126, 171.113, 173.1288, 180.1021, 181.0971, 199.1076, 199.1081, 213.0869, 231.0968, 231.0976, 254.1498, 254.1501, 261.1244, 272.1603, 272.1606, 328.1621, 383.1919, 383.1932, 488.3056, 828.4752, 828.9772, 829.4727, 851.4489, 932.4659, 1031.5328, 1163.6329, 1164.6385, 1177.6372, 1178.6394, 1314.6951, 1364.6927, 1463.7149, 1514.8089 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(7));
        }

        //------------------Random Selection--------------------------------

        [Test]
        public void testMergeSortRandom1()//6324,8430
        {
            List<double> testOneList = new List<double>() { 102.055, 110.0712, 129.102, 145.0603, 175.1185, 183.1489, 211.1436, 230.1263, 287.1475, 315.2017, 322.1862, 412.2544, 525.335, 526.3399, 573.2868, 637.7699, 646.2794, 646.7815, 676.2944, 763.3268 };
            List<double> testTwoList = new List<double>() { 126.0548, 129.1019, 175.1188, 226.0817, 244.0923, 269.1239, 297.1187, 315.1291, 368.1555, 487.3337, 687.4118, 800.4964, 821.9374, 822.4349, 929.5351, 1000.5723, 1001.5726, 1128.6274, 1129.6319, 1256.6856 };
            List<double> checkList = new List<double>() { 102.055, 110.0712, 126.0548, 129.1019, 129.102, 145.0603, 175.1185, 175.1188, 183.1489, 211.1436, 226.0817, 230.1263, 244.0923, 269.1239, 287.1475, 297.1187, 315.1291, 315.2017, 322.1862, 368.1555, 412.2544, 487.3337, 525.335, 526.3399, 573.2868, 637.7699, 646.2794, 646.7815, 676.2944, 687.4118, 763.3268, 800.4964, 821.9374, 822.4349, 929.5351, 1000.5723, 1001.5726, 1128.6274, 1129.6319, 1256.6856 };
            Assert.That(MainProgram.mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }
        [Test]
        public void testCalculatePeakMatchesRandom1()
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 102.055, 110.0712, 126.0548, 129.1019, 129.102, 145.0603, 175.1185, 175.1188, 183.1489, 211.1436, 226.0817, 230.1263, 244.0923, 269.1239, 287.1475, 297.1187, 315.1291, 315.2017, 322.1862, 368.1555, 412.2544, 487.3337, 525.335, 526.3399, 573.2868, 637.7699, 646.2794, 646.7815, 676.2944, 687.4118, 763.3268, 800.4964, 821.9374, 822.4349, 929.5351, 1000.5723, 1001.5726, 1128.6274, 1129.6319, 1256.6856 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(2));
        }

        [Test]
        public void testMergeSortRandom2()//111,4223
        {
            List<double> testOneList = new List<double>() { 102.055, 129.102, 130.0649, 136.0754, 159.0912, 199.071, 216.0975, 218.1495, 226.0817, 244.0922, 298.1032, 315.1292, 459.3279, 922.3692, 1329.1099, 1329.607, 1422.6464, 1447.6745, 1448.6753, 1515.1708 };
            List<double> testTwoList = new List<double>() { 110.0714, 120.0808, 129.1022, 136.0756, 147.1126, 200.1391, 226.1181, 228.1339, 244.1289, 261.1553, 343.1599, 374.2386, 475.2859, 584.762, 585.2638, 588.3688, 1035.7957, 1036.1309, 1036.4625, 1168.5145 };
            List<double> checkList = new List<double>() { 102.055, 110.0714, 120.0808, 129.102, 129.1022, 130.0649, 136.0754, 136.0756, 147.1126, 159.0912, 199.071, 200.1391, 216.0975, 218.1495, 226.0817, 226.1181, 228.1339, 244.0922, 244.1289, 261.1553, 298.1032, 315.1292, 343.1599, 374.2386, 459.3279, 475.2859, 584.762, 585.2638, 588.3688, 922.3692, 1035.7957, 1036.1309, 1036.4625, 1168.5145, 1329.1099, 1329.607, 1422.6464, 1447.6745, 1448.6753, 1515.1708 };
            Assert.That(MainProgram.mergeSort(testOneList, testTwoList), Is.EqualTo(checkList));
        }
        [Test]
        public void testCalculatePeakMatchesRandom2()
        {
            var binner = new Binner();
            List<double> checkList = new List<double>() { 102.055, 110.0714, 120.0808, 129.102, 129.1022, 130.0649, 136.0754, 136.0756, 147.1126, 159.0912, 199.071, 200.1391, 216.0975, 218.1495, 226.0817, 226.1181, 228.1339, 244.0922, 244.1289, 261.1553, 298.1032, 315.1292, 343.1599, 374.2386, 459.3279, 475.2859, 584.762, 585.2638, 588.3688, 922.3692, 1035.7957, 1036.1309, 1036.4625, 1168.5145, 1329.1099, 1329.607, 1422.6464, 1447.6745, 1448.6753, 1515.1708 };
            Assert.That(binner.calculatePeakMatches(checkList.ToArray()), Is.EqualTo(2));
        }
        //--------------------For Peak Shifting------------------------------
        [Test]
        public void testCalculateMassDifference()
        {
            double precursor1 = 1712.95235265891;
            double precursor2 = 1585.862142;
            double massDiff = precursor1 - precursor2;
            //Assert.That(MassCalculator(), Is.EqualTo(massDiff));

        }
        [Test]
        public void testCalculateMzDifference()
        {
            //double precursor1 = 545.102;
            //double precursor2 = 1585.862142;
            //double massDiff = precursor1 - precursor2;
            //Assert.That(MassCalculator(), Is.EqualTo(massDiff));

        }
        [Test]
        public void updatingLibraryMzList()
        {

        }
        [Test]
        public void testIfPeaksWereShifted()
        {

        }
        [Test]
        public void testIfTheCorrectPeaksWereShifted()
        {

        }
        [Test]
        public void testFullExamplePeakListForEachPeptide()
        {
            int peptide = 10;
            string[] bPeaks = { "b2-ls", "b2", "b2-us", "b3-ls", "b3", "b3-us", "b4-ls", "b4", "b4-up", "b5-ls", "b5", "b5-us", "b6-ls", "b6", "b6-us", "b7-ls", "b7", "b7-us", "b8-ls", "b8", "b8-us", "b9-ls", "b9", "b9-us" };
            string[] yPeaks = { "y1-ls", "y1", "y1-us", "y2-ls", "y2", "y2-us", "y3-ls", "y3", "y3-us", "y4-ls", "y4", "y4-us", "y5-ls", "y5", "y5-us", "y6-ls", "y6", "y6-us", "y7-ls", "y7", "y7-us", "y8-ls", "y8", "y8-us", "y9-ls", "y9", "y9-us", "y10-ls", "y10", "y10-us" };
            int thing1 = 0;
            int secondthing1 = 0;
            int thing2 = peptide - 1;

            for (int k = 0; k < peptide; k++)
            {
                if (k == 0)
                {
                    string[] nine = yPeaks.ToList<string>().GetRange(secondthing1, ((thing2 + 1) * 3)).ToArray(); foreach (var i in nine) { Console.WriteLine("this thing: " + i); }
                    Console.WriteLine();
                    //thing1++;
                    thing2--;
                    Console.ReadKey();
                }
                else if (k == (peptide - 1))
                {
                    Console.WriteLine(" the value of k is: " + k);
                    string[] nine = yPeaks.ToList<string>().GetRange(secondthing1, 3).ToArray(); foreach (var i in nine) { Console.WriteLine("this thing: " + i); }
                    Console.WriteLine();
                    thing2--;
                    Console.ReadKey();
                }
                //else if (k==1) { string[] nine = yPeaks.ToList<string>().GetRange(secondthing1, ((thing2 + 1) * 3)).ToArray(); foreach (var i in nine) { Console.WriteLine("this thing: " + i); } Console.ReadKey(); Console.WriteLine(); }
                else
                {
                    string[] nine = yPeaks.ToList<string>().GetRange(secondthing1, ((thing2 + 1) * 3)).Concat(bPeaks.ToList<string>().GetRange(0, (thing1 + 1) * 3)).ToArray();
                    foreach (var i in nine) { Console.WriteLine("this thing: " + i); }
                    thing1++;
                    thing2--;
                    Console.WriteLine(" the value of k is: " + k);
                    Console.ReadKey();
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
