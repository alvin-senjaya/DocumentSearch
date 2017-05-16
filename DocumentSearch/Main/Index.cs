using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSearch.Main
{
    class Index
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                printErrorMessageWithUsage("Please specify the necessery files.");
                Environment.Exit(0);
            }
            
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
