using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace NDV4Sharp
{
    class InfoOpenProject
    {
        public InfoOpenProject() { }

        //public static string NameProject { get; set; }
        public static string PathLocation { get; set; }

        //public static string PathSourcesFolder { get; set; }

        //public static string DbFileName { get; set; }
        public static SQLiteConnection DbConn { get; set; }

        public static SQLiteCommand SqlCmd { get; set; }
    }
}
