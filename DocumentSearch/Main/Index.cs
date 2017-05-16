using DocumentSearch.Indexing;
using DocumentSearch.Stopping;
using Iveonik.Stemmers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSearch.Main
{
    class Index
    {
        static void Main(string[] args)
        {
        
            // Variable for storing command-line arguments
            bool printTerms = false;
            bool useStopFile = false;
            string dataFilePath = "";
            string stopFilePath = "";

            // Procesing command-line arguments
            if (args.Length <= 0)
            {
                printErrorMessageWithUsage("Please specify the necessery files.");
                Environment.Exit(0);
            }

            for (int i = 0; i < args.Length; i++)
            {
                String arg = args[i];

                if (arg.Equals("-p"))
                {
                    printTerms = true;
                }
                else if (arg.Equals("-s"))
                {
                    useStopFile = true;
                    i++;
                    if (i == args.Length)
                    {
                        break;
                    }
                    else
                    {
                        stopFilePath = args[i];
                    }
                }
                else
                {
                    dataFilePath = arg;
                }
            }

            // Check the source file exists or not.
            if (!File.Exists(dataFilePath))
            {
                printErrorMessageWithUsage("Please specify a valid source file path. Cannot find the source file at \"" + dataFilePath + "\"");
                Environment.Exit(0);
            }

            // Check the stoplist file exists or not.
            IStopperModule stopper;
            if (useStopFile)
            {
                if (!File.Exists(stopFilePath))
                {
                    printErrorMessageWithUsage("Please specify a valid stoplist file path. Cannot find the stoplist file at \"" + stopFilePath + "\"");
                    Environment.Exit(0);
                }
                stopper = new SimpleStopperModule(stopFilePath);
            }

            // Process with indexing
            Indexer indexer = new Indexer(printTerms, dataFilePath);

            // Preventing the command prompt to close automatically after running the program.
            Console.Write("Press any key to exit. ");
            Console.Read();
        }

        private static void printErrorMessageWithUsage(string error)
        {
            Console.Error.WriteLine(error);
            Console.Error.WriteLine("USAGE: index [-p] <sourcefile> [-s <stoplist>]");
        }
    }
}
