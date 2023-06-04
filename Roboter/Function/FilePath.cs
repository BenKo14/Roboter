using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Roboter.Function
{
    class FilePath
    {
        public static string docPath = SetStartUpFolder.DirectoryPath + @"\Roboter\RoboXmlData\";
        public List<string> Files { get; set; } 


        public List<string> GetFilePath()
        {
            
            return Files = new List<string>(Directory.GetFiles(docPath)); 
        }

        public void SeperateFileName(List<string> ListProjectnames)
        {
            ListProjectnames.Clear();

            // Seperate Filename from Path
            if (Files != null)
            {
                foreach (string s in Files)
                {
                    int anfang = s.LastIndexOf(@"\");
                    int ende = s.LastIndexOf(".");
                    string index = s.Substring(anfang + 1, ((s.Length - 4) - anfang - 1));

                    ListProjectnames.Add(index);
                }
            }
        }

        public void SetProjectNames(ComboBox cmb_ProjectNames, List<string> ListProjectnames)
        {
            

            foreach (string s in ListProjectnames)
            {
                cmb_ProjectNames.Items.Add(s);
            }
                cmb_ProjectNames.SelectedIndex = (cmb_ProjectNames.HasItems) ? 0 : -1;
        }
    }
}
