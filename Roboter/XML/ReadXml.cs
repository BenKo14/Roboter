using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Roboter.XML
{
    public class ReadXml
    {
        public List<string> ReadProjektnamenListeXML(List<string> ListProjectnames, string URLString)
        {
            if (File.Exists(URLString))
            {
                XmlTextReader reader = new XmlTextReader(URLString);
                try {
                    while (reader.Read())
                    {
                        if (reader.Name != "ProjektListe" && reader.Name != "xml" && reader.Name != string.Empty)
                        {
                            ListProjectnames.Add(reader.Name);
                            Console.WriteLine(reader.Name);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ListProjectnames;
        }
    }
}
