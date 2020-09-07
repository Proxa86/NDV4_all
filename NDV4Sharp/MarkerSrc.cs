using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDV4Sharp
{
    class MarkerSrc
    {
        public MarkerSrc() {}
        public MarkerSrc(string n, string p)
        {
            Number = n;
            Path = p;
        }

        public string Number { get; set; }
        public string Path { get; set; }
    }
}
