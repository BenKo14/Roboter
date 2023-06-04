using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roboter
{
    public class SaveDataToCsv
    {

        private DataTable _datatable = new DataTable("Projektnamen");
        

        public DataTable Datatable
        {
            get { return _datatable; }
            set { _datatable = value; }
        }

       

        public void ExportToCsv(DataTable dt, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            int iColumnCount = dt.Columns.Count;

            for(int i = 0; i < iColumnCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if(i < iColumnCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach(DataRow row in dt.Rows)
            {
                for (int i = 0; i < iColumnCount; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        sw.Write(row[i].ToString());
                    }
                    if( i < iColumnCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
