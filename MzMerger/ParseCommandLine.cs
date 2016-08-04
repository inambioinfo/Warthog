using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace MzMerger
{   
    public class ParseCommandLine
    {
            [Option('r', "read", Required = true,
              HelpText = "Input the path of the Query database.")]
            public string QueryDBPath { get; set; }

            [Option('v', "verbose", DefaultValue = true,
              HelpText = "Prints all messages to standard output.")]
            public bool Verbose { get; set; }

            [Option('d', "dir", Required = true,
              HelpText = "Input the path of the Library database.")]
            public string LibraryDBPath { get; set; }

            [Option('p', "path", Required = true,
              HelpText = "Input the path of the LibraryQueryPair database.")]
            public string QueryLibraryPairsDBPath { get; set; }

            [Option('o', "outputFileName", DefaultValue = "RenameYourOutput.csv",
              HelpText = "The name of your output file.")]
            public string outputFileName { get; set; }

           

        [ParserState]
            public IParserState LastParserState { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this,
                  (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
     
        
    }
}
