using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenealogySoftwareV3.Types
{
    class Date : PieceOfData
    {
        public DateType Type { get; private set; }
        public List<DateTime> Dates {get; private set;}

        public Date()
        {
            Dates = new List<DateTime>();
        }

        public override string ToString()
        {
            if (Dates.Count > 0)
                return Dates[0].ToShortDateString();
            else
                return string.Empty;
        }
    }
}
