using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace GenealogySoftwareV3.Types
{
    class FileReader : IGEDCOMInterpreter
    {
        public Dictionary<int, Individual> Individuals { get; set; }
        public Dictionary<int, Marriage> Marriages { get; set; }

        public FileReader()
        {
            Individuals = new Dictionary<int, Individual>();
            Marriages = new Dictionary<int, Marriage>();
        }

        struct TagAnalyzer
        {
            public GEDCOMTags Tag;
            public int childrenTags;
        }

        public DataTable LoadDataIntoDataTable(string filePath)
        {
            DataTable data = new DataTable("GEDCOM Original File");
            data.Columns.Add("ID", typeof(String));
            data.Columns.Add("TAG", typeof(String));
            data.Columns.Add("VALUE", typeof(String));

            string[] fileContents;
            int firstDelimiterIndex = 0;
            int secondDelimiterIndex = 0;
            char fieldDelimiter = ' ';

            fileContents = File.ReadAllLines(filePath);

            foreach (string line in fileContents)
            {
                string columnValue1 = string.Empty;
                string columnValue2 = string.Empty;
                string columnValue3 = string.Empty;

                firstDelimiterIndex = line.IndexOf(fieldDelimiter);
                if (firstDelimiterIndex > -1)
                {
                    columnValue1 = line.Substring(0, firstDelimiterIndex);
                    columnValue2 = line.Substring(firstDelimiterIndex + 1);

                    secondDelimiterIndex = line.IndexOf(fieldDelimiter, firstDelimiterIndex + 1);
                    if (secondDelimiterIndex > -1)
                    {
                        columnValue2 = line.Substring(firstDelimiterIndex + 1, secondDelimiterIndex - firstDelimiterIndex);
                        columnValue3 = line.Substring(secondDelimiterIndex + 1);
                    }

                    data.Rows.Add(columnValue1, columnValue2, columnValue3);

                    columnValue1 = string.Empty;
                    columnValue2 = string.Empty;
                    columnValue3 = string.Empty;
                }
            }

            return data;

        }

        private int GetRecordID(Regex exp, string record)
        {
            int result = 0;
            try
            {
                result = int.Parse(exp.Match(record).Value);
            }
            catch (Exception)
            {
                result = -1;
            }

            return result;
        }



        public void InterpretFileContents(DataTable data)
        {

            TagAnalyzer mainTag;
            mainTag.Tag = GEDCOMTags.Unknown;
            mainTag.childrenTags = 0;
            //Dictionary<int, Individual> individuals = new Dictionary<int, Individual>();
            //Dictionary<int, Marriage> marriages = new Dictionary<int, Marriage>();
            Individual newIndividual = null;
            Individual foundIndividual = null;
            int individualID = 0;
            int familyID = 0;
            Marriage newMarriage = null;
            Regex exp = new Regex("\\d+");


            for (int i = 0; i < data.Rows.Count; i++)
            {
                string tag = data.Rows[i]["TAG"].ToString().Trim();
                string[] values = null;

                try
                {
                    if (!string.IsNullOrEmpty(data.Rows[i]["VALUE"].ToString()))
                        values = data.Rows[i]["VALUE"].ToString().Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (tag.StartsWith("@I"))
                    {
                        individualID = GetRecordID(exp, tag);
                        newIndividual = new Individual();
                        Individuals.Add(individualID, newIndividual);
                    }
                    else if (tag == GEDCOMTags.GIVN.ToString())
                    {
                        foreach (string name in values)
                        {
                            newIndividual.Names.Add(name);
                        }
                    }
                    else if (tag == GEDCOMTags.SURN.ToString())
                    {
                        foreach (string surname in values)
                        {
                            newIndividual.Surnames.Add(surname);
                        }
                    }
                    else if (tag == GEDCOMTags.NICK.ToString())
                    {
                    }
                    else if (tag == GEDCOMTags.SEX.ToString())
                    {
                        newIndividual.Sex = (Sex)Enum.Parse(typeof(Sex), values[0].Trim());
                    }
                    else if (tag == GEDCOMTags.BIRT.ToString() || tag == GEDCOMTags.DEAT.ToString())
                    {
                        mainTag.Tag = (GEDCOMTags)Enum.Parse(typeof(GEDCOMTags), tag);
                        mainTag.childrenTags = 0;
                    }

                    else if (tag == GEDCOMTags.DATE.ToString())
                    {
                        if (mainTag.childrenTags == 0)
                        {
                            mainTag.childrenTags++;
                            if (values.Length == 3)
                            {
                                try
                                {
                                    DateTime date = new DateTime(int.Parse(values[2]), Utils.GetMonthInteger(values[1]), int.Parse(values[0]));
                                    switch (mainTag.Tag)
                                    {
                                        case GEDCOMTags.BIRT: newIndividual.Birth.Date.Dates.Add(date);
                                            break;
                                        case GEDCOMTags.MARR: newMarriage.Date.Dates.Add(date);
                                            break;
                                        case GEDCOMTags.DEAT: newIndividual.Death.Date.Dates.Add(date);
                                            break;
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                    else if (tag == GEDCOMTags.PLAC.ToString())
                    {
                        if (mainTag.childrenTags == 0)
                        {
                            mainTag.childrenTags++;
                            if (values.Length == 3)
                            {
                                Place place = new Place { City = values[0], State = values[1], Country = values[2] };
                                switch (mainTag.Tag)
                                {
                                    case GEDCOMTags.BIRT: newIndividual.Birth.Place = place;
                                        break;
                                    case GEDCOMTags.MARR: newMarriage.Place = place;
                                        break;
                                    case GEDCOMTags.DEAT: newIndividual.Death.Place = place;
                                        break;
                                }
                            }
                        }
                    }
                    else if (tag.StartsWith("@F"))
                    {
                        familyID = GetRecordID(exp, tag);
                        newMarriage = new Marriage();
                        Marriages.Add(familyID, newMarriage);
                    }
                    else if (tag == GEDCOMTags.HUSB.ToString() || tag == GEDCOMTags.WIFE.ToString() || tag == GEDCOMTags.CHIL.ToString())
                    {
                        individualID = GetRecordID(exp, values[0]);

                        foundIndividual = null;
                        if ((tag != GEDCOMTags.CHIL.ToString()) && (Individuals.TryGetValue(individualID, out foundIndividual)))
                        {
                            foundIndividual.MarriageIDs.Add(familyID);
                        }

                        if (tag == GEDCOMTags.HUSB.ToString())
                            newMarriage.Husband = individualID;
                        else if (tag == GEDCOMTags.WIFE.ToString())
                            newMarriage.Wife = individualID;
                        else if (tag == GEDCOMTags.CHIL.ToString())
                            newMarriage.Children.Add(individualID);
                    }
                    else if (tag == GEDCOMTags.MARR.ToString())
                    {
                        mainTag.Tag = GEDCOMTags.MARR;
                        mainTag.childrenTags = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Error in handling tag [{0}]. {1}", tag, ex.Message));
                }
            }

        }

        public void GenerateTree(TreeNode node, Marriage marriage)
        {
            Individual individualMatch;
            string husbandName = string.Empty;
            string wifeName = string.Empty;
            string familyName = string.Empty;

            if (Individuals.TryGetValue(marriage.Husband, out individualMatch))
            {
                husbandName = individualMatch.GetNames();
                familyName = individualMatch.GetSurnames();
            }

            if (Individuals.TryGetValue(marriage.Wife, out individualMatch))
                wifeName = individualMatch.GetNames();

            TreeNode newNode = new TreeNode(string.Format("[{0}] ({1} + {2})", familyName.ToUpper(), husbandName, wifeName));

            node.Nodes.Add(newNode);
            node = newNode;
            foreach (int child in marriage.Children)
            {
                if (Individuals[child].MarriageIDs.Count > 0)
                    foreach (int m in Individuals[child].MarriageIDs)
                    {
                        Marriage match;
                        if (Marriages.TryGetValue(m, out match))
                            GenerateTree(node, match);
                    }
                else
                    node.Nodes.Add(Individuals[child].GetNames());
            }
        }

        //public IEnumerable<KeyValuePair<int, Individual>> GetFamiliesRoots()
        //{
        //    //Marriages.Where( m => m.
        //    return Individuals.Where(i =>
        //    {
        //        Marriage match;
        //        bool hasParents = false;
        //        foreach (int marriageID in i.Value.MarriageIDs)
        //        {
        //            hasParents = (Marriages.TryGetValue(marriageID, out match) && match.Children.Contains(i.Key));
        //        }

        //        return !hasParents;
        //    });


        //}
        public List<KeyValuePair<int, Individual>> GetFamiliesRoots()
        {
            List<KeyValuePair<int, Individual>> familiesRoots = new List<KeyValuePair<int, Individual>>();
            foreach (KeyValuePair<int, Individual> i in Individuals)
            {
                foreach (int marriageID in i.Value.MarriageIDs)
                {
                    Marriage match;
                    if ((Marriages.TryGetValue(marriageID, out match) && !match.Children.Contains(i.Key)))
                        familiesRoots.Add(i);
                }
            }

            return familiesRoots;
        }

        public List<string> GetFamiliesCsv()
        {
            List<string> familyNames = new List<string>();
            foreach (var individual in Individuals)
            {
                var surnames = individual.Value.GetSurnames();
                if (!familyNames.Contains(surnames))
                    familyNames.Add(surnames);
            }

            familyNames = familyNames?.Distinct()?.OrderBy(f => f)?.ToList();
            familyNames.Insert(0, "Family");

            return familyNames;
        }
    }
}
