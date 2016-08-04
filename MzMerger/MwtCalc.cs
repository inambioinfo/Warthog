using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MzMerger
{   //this class is a subset of the items in the MwtWinDllTestCS example provided on the Molecular Weight Caclulator website
    public class MwtCalc
    {
        public static double[] bPeakHolder { get; set; }
        public static double[] yPeakHolder { get; set; }
        MwtWinDll.MolecularWeightCalculator mMwtWin;


        public MwtCalc()
        {
            mMwtWin = new MwtWinDll.MolecularWeightCalculator();
        }

        public MwtCalc(ref MwtWinDll.MolecularWeightCalculator objMwtWin, string sequence)
        {
            mMwtWin = objMwtWin;
            bPeakHolder = null;
            yPeakHolder = null;
            TestAccessFunctions(sequence);
        }

        private void TestAccessFunctions(string sequence)//public or private
        {
            string strNewSeq = sequence;
            // Set the element mode (average, monoisotopic, or integer)
            mMwtWin.SetElementMode(MwtWinDll.MWElementAndMassRoutines.emElementModeConstants.emIsotopicMass);

            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumOptionsType udtFragSpectrumOptions = default(MwtWinDll.MWPeptideClass.udtFragmentationSpectrumOptionsType);
            udtFragSpectrumOptions.Initialize();

            // Initialize udtFragSpectrumOptions with the defaults
            udtFragSpectrumOptions = mMwtWin.Peptide.GetFragmentationSpectrumOptions();

            // Customize the options
            udtFragSpectrumOptions.DoubleChargeIonsShow = false; //false
            udtFragSpectrumOptions.TripleChargeIonsShow = false;

            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itAIon].ShowIon = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].ShowIon = true;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].ShowIon = true;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].NeutralLossAmmonia = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].NeutralLossAmmonia = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].NeutralLossPhosphate = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].NeutralLossPhosphate = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].NeutralLossWater = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].NeutralLossWater = false;

            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itCIon].ShowIon = false;
            udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itZIon].ShowIon = false;

            // Obtain the fragmentation spectrum for a peptide

            // First define the peptide sequence
            // Need to pass "false" to parameter blnIs3LetterCode since strNewSeq is in one-letter notation
            mMwtWin.Peptide.SetSequence(strNewSeq,
                                        MwtWinDll.MWPeptideClass.ntgNTerminusGroupConstants.ntgHydrogen,
                                        MwtWinDll.MWPeptideClass.ctgCTerminusGroupConstants.ctgHydroxyl,
                                        false);

            // Update the options
            mMwtWin.Peptide.SetFragmentationSpectrumOptions(ref udtFragSpectrumOptions);

            // Get the fragmentation masses
            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumDataType[] udtFragSpectrum = null;
            mMwtWin.Peptide.GetFragmentationMasses(ref udtFragSpectrum);
            //store b and y ions in separate lists
            List<double> bList = new List<double>();
            List<double> yList = new List<double>();
            for (int i = 0; i < udtFragSpectrum.Length; i++)
            {
                if (udtFragSpectrum[i].Symbol != null && udtFragSpectrum[i].Symbol.Contains("y"))
                {
                    yList.Add(Convert.ToDouble(udtFragSpectrum[i].Mass.ToString("0.0000")));
                }
                else
                {
                    bList.Add(Convert.ToDouble(udtFragSpectrum[i].Mass.ToString("0.0000")));
                }
            //byPeakList.Add(Convert.ToDouble(udtFragSpectrum[i].Mass.ToString("0.0000")));
            }
            bPeakHolder = bList.ToArray();
            yPeakHolder = yList.ToArray();
        }
    }
}
    
