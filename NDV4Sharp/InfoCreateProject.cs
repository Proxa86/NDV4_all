using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace NDV4Sharp
{
    class InfoCreateProject
    {
        public InfoCreateProject() { }

        public InfoCreateProject(string nameProj, string pathLoc, string pathsrc, string dbFileName,
            SQLiteConnection dbConn, SQLiteCommand sqlcmd)
        {
            NameProject = nameProj;
            PathNewLocation = pathLoc;
            PathSourcesFolder = pathsrc;
            DbFileName = dbFileName;
            DbConn = dbConn;
            SqlCmd = sqlcmd;
        }

        public static string NameProject { get; set; }
        public static string PathNewLocation { get; set; }

        public static string PathSourcesFolder { get; set; }

        public static string DbFileName { get; set; }
        public static SQLiteConnection DbConn { get; set; }

        public static SQLiteCommand SqlCmd { get; set; }

    }
}
