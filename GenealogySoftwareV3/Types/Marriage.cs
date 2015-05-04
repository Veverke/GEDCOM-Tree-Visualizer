using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenealogySoftwareV3.Types
{
    class Marriage : LifeEvent
    {
        public int Husband { get; set; }
        public int Wife { get; set; }
        public List<int> Children { get; set; }
        public LifeEvent Divorce { get; set; }
        public int ID { get; set; }

        public Marriage()
        {
            Children = new List<int>();
        }

        public override string ToString()
        {
            StringBuilder children = new StringBuilder();
            foreach (int childID in Children)
                children.Append(string.Format(" {0}", childID));

            return string.Format("H: {0}, W: {1}, D: {2}, P: {3}, C: {4}", Husband, Wife, Date, Place, children.ToString().Trim());
        }
    }
}
