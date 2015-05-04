using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GenealogySoftwareV3.Types
{
    interface IGEDCOMInterpreter
    {
        DataTable LoadDataIntoDataTable(string filePath);
        void InterpretFileContents(DataTable data);
        //Dictionary<int, Individual> GetIndividuals(DataTable data);
        //Dictionary<int, Marriage> GetFamilies(DataTable data);
    }
}
