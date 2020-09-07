using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace NDV4Sharp
{
    class OpenFile
    {

        public SQLiteConnection DbConn { get; set; }

        public SQLiteCommand SqlCmd { get; set; }

        //public string NameProject { get; set; }
        public string PathLocation { get; set; }

        //public string PathSourcesFolder { get; set; }

        public string DbFileName { get; set; }
        public OpenFile() {
            DbConn = new SQLiteConnection();
            SqlCmd = new SQLiteCommand();
        }

        public void openFileDB()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "sqlite files (*.sqlite)|*.sqlite|All files (*.*)|*.*";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                DbFileName = Path.GetFileName(ofd.FileName);
                PathLocation = Path.GetDirectoryName(ofd.FileName);

                
                DbConn = new SQLiteConnection("Data Source=" + PathLocation + "\\" + DbFileName + ";Version=3;");
                DbConn.Open();
                SqlCmd.Connection = DbConn;

                if (DbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Open connection with database");
                    return;
                }
            }
            else
            {
                return;
            }

            InfoOpenProject.DbConn = DbConn;
            InfoOpenProject.SqlCmd = SqlCmd;
            InfoOpenProject.PathLocation = Path.GetDirectoryName(ofd.FileName);
            MessageBox.Show("Open DB - OK");
        }
    }
}
