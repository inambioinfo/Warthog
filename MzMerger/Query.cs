using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MzMerger
{
    class Query
    {
        private string _organismName;
        private double[] _mzList;
        private int _charge;
        private double _precursor;

        //Property accessors
        public int Charge { get { return _charge; } }
        public double[] MzList { get { return _mzList; } }
        public string OrganismName { get { return _organismName; } }
        public double Precursor { get { return _precursor; } }

        //constructor
        public Query(int charge, double[] mzList, string path, double precursor)
        { 
            this._charge = charge;
            this._mzList = mzList;
            this._organismName = GetOrganism(path);
            this._precursor = precursor; // this is where the precursor m/z is assigned. 
        }

        //this method parses out the name of the organsim from the filename provided
        public static string GetOrganism(string path)
        {
            string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
            string[] lines = Regex.Split(filenameNoExtension, "_");
            return lines[1] + "_" + lines[2].Substring(0, 4);
        }
    }
}
