using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace GenealogySoftwareV3.Types
{
    class Individual
    {
        //public int ID { get; private set; }
        public List<string> Names { get; set; }
        public List<string> Surnames { get; set; }
        public Sex Sex { get; set; }
        public List<int> MarriageIDs { get; set; }
        //public Family Family { get; set; }
        public LifeEvent Birth { get; private set; }
        public Death Death { get; private set; }
        public List<AttachedFile> AttachedFiles { get; private set; }

        //public Individual(string indTag)
        //{
        //    Regex IDexpr = new Regex("\\d+");
        //    ID = int.Parse(IDexpr.Match(indTag).Value);

        //    Names = new List<string>();
        //    Surnames = new List<string>();
        //    Birth = new LifeEvent();
        //    Death = new Death();
        //    AttachedFiles = new List<AttachedFile>();
        //}

        public Individual()
        {
            Names = new List<string>();
            Surnames = new List<string>();
            MarriageIDs = new List<int>();
            Birth = new LifeEvent();
            Death = new Death();
            AttachedFiles = new List<AttachedFile>();
        }

        public override string ToString()
        {
            StringBuilder fullName = new StringBuilder();
            StringBuilder fullSurname = new StringBuilder();
            //string birthDate = null;
            //string deathDate = null;
      
            foreach(string name in Names)
                fullName.Append(string.Format(" {0}", name));
            foreach(string surname in Surnames)
                fullSurname.Append(string.Format(" {0}", surname));

            //if (Birth.Date != null)
            //    birthDate = Birth.Date.Dates[0].ToShortDateString();
            //if (Death.Date != null)
            //    deathDate = Death.Date.Dates[0].ToShortDateString();

            return string.Format("[{0}],[{1}],[{2}],[{3}]", GetNames(), GetSurnames(), Birth.Date, Death.Date);
        }

        public string GetNames()
        {
            if (Names.Count > 0)
                return string.Join(" ", Names);
            else
                return string.Empty;
        }

        public string GetSurnames()
        {
            if (Surnames.Count > 0)
                return string.Join(" ", Surnames);
            else
                return string.Empty;
        }
    }
}
