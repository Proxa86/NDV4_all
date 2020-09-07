using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDV4Sharp
{
    class MarkerBin
    {
        public MarkerBin() { }
        public MarkerBin(string p, List <string> l)
        {
            Path = p;
            ListNumberMarker = l;
        }
        public string Path {get; set;}
        public List<string> ListNumberMarker { get; set; }
    }
}
