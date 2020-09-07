using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDV4Sharp
{
    class ResultMarkersInBin
    {
        public ResultMarkersInBin() { }
        public ResultMarkersInBin(string p, List<MarkerSrc> l)
        {
            Path = p;
            LMarkerInBin = l;
        }
        public string Path { get; set; }
        public List<MarkerSrc> LMarkerInBin { get; set; }
    }
}
