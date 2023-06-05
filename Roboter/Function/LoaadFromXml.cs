using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Roboter.Function
{
    class LoaadFromXml
    {
        public void loadDataFromXml(ComboBox cmb_Projectnamen, LoadData loadData, DataGrid datagrid)
        {
            if (cmb_Projectnamen.Items.Count != 0)
            {
                string path = SetStartUpFolder.DirectoryPath + @"\RoboXmlData\" + cmb_Projectnamen.SelectedItem.ToString() + ".xml";
                Stream stream = new FileStream(path, FileMode.Open); // , FileMode.Open, FileAccess.Read
                try
                {
                    loadData.Datatable.ReadXml(stream);
                    datagrid.ItemsSource = loadData.Datatable.DefaultView;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    stream.Close();
                }
            }
        }
    }
}
