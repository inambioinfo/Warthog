using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MzMerger
{
    class MainProgram
    {
        //create constructors
        static void Main(string[] args)
        {
            var options = new ParseCommandLine();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {  
                // Values are available here
                Console.WriteLine("Starting Program.");
                new SQLiteConnector(options); //.ToFile(); // this creates a new SortPairs and outputs the info to file/'
                Console.ReadKey();
                Console.WriteLine("Finished.");
            }
        }

    }

}
