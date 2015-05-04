using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenealogySoftwareV3.Types
{
    enum DateType { Between, Range, Exact, About }
    enum CauseOfDeath { Illness, Natural, Accident, Murder }
    enum AttachedFileType { Document, Image }
    enum DocumentType { Birth, Marriage, Death }
    enum GEDCOMTags { NAME, GIVN, SURN, SEX, BIRT, DATE, PLAC, DEAT, CAUS, FAMS, FAMC, NICK, MARR, HUSB, WIFE, CHIL, Unknown}
    enum Sex { M, F }
}
