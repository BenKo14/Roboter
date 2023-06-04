using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace Roboter.Function
{
    public class SaveRoboDataInXml
    {
        WriteCfg writer = new WriteCfg();
        public string Path { get; set; }
        public string Path2 { get; set; }
        public XmlWriter Writer { get; set; } = null;
        public XmlWriterSettings WriterSettings { get; set; } = new XmlWriterSettings();

        public XmlWriterSettings CreateXmlWriterSettings()
        {
            // Eigenschaften / Einstellungen festlegen
            WriterSettings.Indent = true;
            WriterSettings.IndentChars = "  ";
            WriterSettings.NewLineChars = "\r\n";

            return WriterSettings;
        }

        public void SetXmlContent(TextBox result,TextBox resultXml, ComboBox cmb_Projectnamen)
        {
            if(cmb_Projectnamen.Items.Count != 0 )
            {
                Path = SetStartUpFolder.DirectoryPath + @"\RoboXmlData\" + cmb_Projectnamen.SelectedItem.ToString() + ".xml";
                Path2 = SetStartUpFolder.DirectoryPath + @"\RoboCfgData\" + cmb_Projectnamen.SelectedItem.ToString() + ".cfg";
                try
                {
                    if (File.Exists(Path))
                    {
                        result.Text = File.ReadAllText(Path);
                    }
                    else { result.Text = string.Empty; }

                    if (File.Exists(Path2))
                    {
                        resultXml.Text = File.ReadAllText(Path2);
                    }
                    else { resultXml.Text = string.Empty; }
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            
        }

        public void SaveRoboData(ComboBox cmb_Projectnamen, LoadData loadData, TextBox result, TextBox resultXml)
        {
            CreateXmlWriterSettings();

            Path = SetStartUpFolder.DirectoryPath + @"\RoboXmlData\" + cmb_Projectnamen.SelectedItem.ToString() + ".xml";

            string Projektname = cmb_Projectnamen.SelectedItem.ToString();

            try
            {
                Writer = XmlWriter.Create(Path, WriterSettings);
                // XML-Validierungs-Kopf
                Writer.WriteStartDocument(true);    // true = standalone="yes"
                loadData.Datatable.WriteXml(Writer); //Write XML

                writer.CreateCfgData(loadData.Datatable, Projektname);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Writer.Close();
                SetXmlContent(result, resultXml, cmb_Projectnamen);
            }
        }
    }
}
