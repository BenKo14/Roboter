using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Roboter.XML
{
    public class XmlWriterSaveData
    {
        public void WriteXml(List<string> ProjektnamenListe) // string Projektname
        {
            XmlWriter oXmlWriter = null;
            XmlWriterSettings oXmlWriterSettings = new XmlWriterSettings();

            try
            {
                // Eigenschaften / Einstellungen festlegen
                oXmlWriterSettings.Indent = true;
                oXmlWriterSettings.IndentChars = "  ";
                oXmlWriterSettings.NewLineChars = "\r\n";

                oXmlWriter = XmlWriter.Create(@"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\RoboXmlData\Projectnames.xml", oXmlWriterSettings);

                // XML-Validierungs-Kopf
                oXmlWriter.WriteStartDocument(true);    // true = standalone="yes"

                // Wurzel-Element
                oXmlWriter.WriteStartElement("ProjektListe");

                // Kommentar
                oXmlWriter.WriteComment("ProjektListe");

                foreach (string s in ProjektnamenListe)
                {
                        // ProjektnamenTest
                        oXmlWriter.WriteStartElement(s); //Projektname

                        oXmlWriter.WriteEndElement(); //Element schließen
                }

                // Wurzel-Element schließen
                oXmlWriter.WriteEndElement();

                MessageBox.Show("Die XML-Datei wurde geschrieben!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Daten in Datei schreiben und Stream schließen
                oXmlWriter.Close();
            }


        }
    }
}
