using Iveonik.Stemmers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentSearch.Indexing
{
    class Indexer
    {
        Dictionary<int, string> map = new Dictionary<int, string>();
        HashSet<string> terms = new HashSet<string>();

        bool printTerm = false;
        string dataSourceFile = "";

        public Indexer(bool printTerm, string sourceFilePath)
        {
            this.printTerm = printTerm;
            dataSourceFile = sourceFilePath;

            tokenising(dataSourceFile);
        }

        private void tokenising(string dataSourceFile)
        {
            // Read the file and display it line by line.
            StreamReader file = new StreamReader(dataSourceFile);

            string content = "";
            string tempContent = "";
            int docID = 0;

            while ((content = file.ReadLine()) != null)
            {
                //if (content.Contains("<DOCNO>"))
                //{
                //    // Get the actual document ID.
                //    content = content.Substring(7, content.Length - 8).Trim();

                //    if (!map.ContainsKey(docID))
                //    {
                //        map[docID] = content;
                //    }
                //}
                // End of document, and increase the docID.
                //else if (content.Contains("</DOCNO>"))
                //{
                //    docID++;
                //}
                //Read lines between <HEADLINE> and </HEADLINE> and between <TEXT> and </TEXT>
                if (content.StartsWith("<HEADLINE>") || content.StartsWith("<TEXT>"))
                {
                    while (!(content.Equals("</HEADLINE>")) && !(content.Equals("</TEXT>")))
                    {

                        tempContent += content.Trim() + " ";

                        content = file.ReadLine();
                    }
                }
            }

            file.Close();

            // Replace hyphen with whitespace.
            tempContent = tempContent.Replace('-', ' ');
            Console.WriteLine(ScrubHtml(tempContent));
            Console.WriteLine("----------------");

            // Remvoing excess markups tags.
            string noMarkupString = ScrubHtml(tempContent);
            string[] cleanContent = noMarkupString.Split(' ');

            int i = 0;
            foreach (var item in cleanContent)
            {
                Console.WriteLine(trim(cleanContent[i]));
                i++;
            }
        }

        public string trim(string word)
        {
            char[] bothsidestrimchar = { '\'', '<', '>', '/', ':', ';', '"', '{', '}', '|', '\\', '[', ']', '.', ',', '~', '`', '!', '?', '@', '#', '%', '^', '&', '*', '(', ')', '_', '-', '+', '=' };
            char[] endtrimchar = { '$' };

            word = word.Trim();
            word = word.Trim(bothsidestrimchar);
            word = word.TrimEnd(endtrimchar);

            // Stemming word after tokenization.
            IStemmer englishStemmer = new EnglishStemmer();
            word = englishStemmer.Stem(word);

            // Cleaning-up extra characters after tokenization.
            

            return word;
        }

        /*
         * Method for removing excess markup tags
         * @param value - string to be checked and removed if its markup tag.
         */ 
        public static string ScrubHtml(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }
    }
}
