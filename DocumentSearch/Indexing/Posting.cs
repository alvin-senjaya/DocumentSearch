using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentSearch.Indexing
{
    public class Posting
    {
        private int documentID;
        private int frequency;

        public Posting(int documentID) : this(documentID, 1)
        {

        }
        public Posting(int documentID, int frequency)
        {
            this.DocumentID = documentID;
            this.Frequency = frequency;
        }

        public int DocumentID
        {
            get
            {
                return documentID;
            }

            set
            {
                documentID = value;
            }
        }

        public int Frequency
        {
            get
            {
                return frequency;
            }

            set
            {
                frequency = value;
            }
        }

        public void incrementFrequency()
        {
            frequency++;
        }
    }
}
