using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GenealogySoftwareV3.Types
{
    class AttachedFile
    {
        public AttachedFileType Type { get; private set; }
        public FileInfo File { get; private set; }
    }
}
