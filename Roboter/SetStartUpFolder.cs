using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace Roboter
{
   public class SetStartUpFolder
    {
        public static string DirectoryPath { get; set; }
        public void BuildStartLocation()
        {
            DirectoryPath = Directory.GetCurrentDirectory();
            int resultPath = DirectoryPath.LastIndexOf('\\');
            DirectoryPath = DirectoryPath.Substring(0, resultPath - 1);
            resultPath = DirectoryPath.LastIndexOf('\\');
            DirectoryPath = DirectoryPath.Substring(0, resultPath);
            resultPath = DirectoryPath.LastIndexOf('\\');
            DirectoryPath = DirectoryPath.Substring(0, resultPath);
            

            if (!Directory.Exists(DirectoryPath + "\\RoboCfgData"))
            {
                Directory.CreateDirectory(DirectoryPath + "\\RoboCfgData");
            }
            if (!Directory.Exists(DirectoryPath + "\\RoboXmlData"))
            {
                Directory.CreateDirectory(DirectoryPath + "\\RoboXmlData");
            }
            if (!Directory.Exists(DirectoryPath + "\\RoboCsvData"))
            {
                Directory.CreateDirectory(DirectoryPath + "\\RoboCsvData");
            }



        }


    }
}
