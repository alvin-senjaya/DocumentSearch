using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSearch.Stopping
{
    class SimpleStopperModule :IStopperModule
    {
        HashSet<string> stopWords = new HashSet<string>();
        string stopFile = "";

        public SimpleStopperModule(string stopFilePath)
        {
            stopFile = stopFilePath;

            populateStopWord(stopFile);
        }
        public void populateStopWord(string stopFilePath)
        {
            try
            {
                StreamReader stopListFile = new StreamReader(stopFilePath);
                string word = "";
                while ((word = stopListFile.ReadLine()) != null)
                {
                    stopWords.Add(word.ToLower());
                }

                stopListFile.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.Error.WriteLine("File not found. Please specify a correct path.");
                Console.WriteLine(e.StackTrace);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("Invalid stoplist format.");
                Console.WriteLine(e.StackTrace);
            }
        }

        public bool isStopWord(string word)
        {
            if (stopWords.Contains(word))
            {
                return true;
            }
            return false;
        }
    }
}
