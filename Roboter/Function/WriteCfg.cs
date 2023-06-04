using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Roboter.Function
{
    class WriteCfg
    {
        private string WritePath;
        public string result { get; set; }
        public void CreateCfgData(DataTable Datatable, string Projektname)
        {
            
            WritePath = @"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\RoboCfgData\" + Projektname + ".cfg";

            if(File.Exists(WritePath))
            {
                File.Delete(WritePath);
            }

            
            FileStream fs = new FileStream(WritePath, FileMode.CreateNew);
            StreamWriter stream = new StreamWriter(fs);

            string val0, val1, val2, val3, val4, val5, val6, val7, val8, val9, val10, val11;

            stream.WriteLine("EIO_SIGNAL:\n");
            foreach (DataRow row in Datatable.Rows)
            {
                val0 = row.Field<string>("Name");
                val1 = row.Field<string>("TypeOfSignal");
                val2 = row.Field<string>("AssingnedToDevice");
                val3 = row.Field<string>("SignalIdentificationLabel");
                val4 = row.Field<string>("DeviceMapping");
                val5 = row.Field<string>("Category");
                val6 = row.Field<string>("AccessLevel");
                val7 = row.Field<string>("DefaultValue");
                val8 = row.Field<string>("FilterTimePassive");
                val9 = row.Field<string>("FilterTimeActive");
                val10 = row.Field<string>("Safety");
                val11 = row.Field<string>("InvertPhysicalValue");


                if (row.Field<string>("TypeOfSignal") == "Analog Input" || row.Field<string>("TypeOfSignal") == "Digital Input" || row.Field<string>("TypeOfSignal") == "Group Input")
                {
                    if (val0 != ""){ result = $"\t -Name \"{val0}\" "; }
                    if (val1 != ""){ result = result + $"-SignalType \"{val1}\"\\\n\t "; }
                    if (val2 != ""){ result = result + $"-Device \"{val2}\" "; }
                    if (val3 != ""){ result = result + $"-Label \"{val3}\" "; }
                    if (val4 != ""){ result = result + $"-DeviceMap \"{val4}\"\\ \n\t ";}
                    if (val5 != "") { result = result + $"-Category  \"{val5}\" "; }
                    if (val6 != "") { result = result + $"-Access  \"{val6}\" "; }
                    if (val7 != "") { result = result + $"-Default \"{val7}\"\\ \n\t "; }
                    if (val8 != "") { result = result + $"-FillPas \"{val8}\" "; }
                    if (val9 != "") { result = result + $"-FillAct \"{val9}\" "; }
                    if (val10 == "") { result = result + $"-SafeLevel "; }
                    if(val11 != "No") { result = result + $"-Invert \"{val11}\" \n"; }
                    else { result = result + "\n"; }

                    stream.WriteLine(result);
                }
                else if (row.Field<string>("TypeOfSignal") == "Analog Output" || row.Field<string>("TypeOfSignal") == "Digital Output" || row.Field<string>("TypeOfSignal") == "Group Output")
                {
                    if (val0 != "") { result = $"\t -Name \"{val0}\" "; }
                    if (val1 != "") { result = result + $"-SignalType \"{val1}\"\\\n\t "; }
                    if (val2 != "") { result = result + $"-Device \"{val2}\" "; }
                    if (val3 != "") { result = result + $"-Label \"{val3}\" "; }
                    if (val4 != "") { result = result + $"-DeviceMap \"{val4}\"\\ \n\t "; }
                    if (val5 != "") { result = result + $"-Category \"{val5}\" "; }
                    if (val6 != "") { result = result + $"-Access \"{val6}\" "; }
                    if (val7 != "") { result = result + $"-Default \"{val7}\"\\ \n\t "; }
                    if (val8 != "") { result = result + $"-FillPas \"{val8}\" "; }
                    if (val9 != "") { result = result + $"-FillAct \"{val9}\" "; }
                    if (val10 != "" & val10 != "-- None --") { result = result + $"-SafetyLevel \"{val10}\""; }
                    else if (val10 != "") { result = result + $"-SafetyLevel"; }
                    if (val11 != "No") { result = result + $" -Invert \"{val11}\"\n"; }
                    else { result = result + "\n"; }

                    stream.WriteLine(result);
                }


                // stream.WriteLine($"\t-Name \"{val0}\" -SignalType \"{val1}\"\\\n\t-Unit \"{2}\" -UnitMap \"{val3}\" -Access \"{val4}\" \n", val0, val1, val2, val3, val4, val5);


                //else if (row.Field<string>("TypeOfSignal") == "Digital Input" || row.Field<string>("TypeOfSignal") == "Digital Output")
                //{
                //    stream.WriteLine(string.Format($"\t-Name \"{0}\" -SignalType \"{1}\"\\\n\t-Unit \"{2}\" -UnitMap \"{3}\" -Access \"{4}\"", val0, val1, val2, val3, val4), val5);
                //}
                //else if(row.Field<string>("TypeOfSignal") == "Group Input" || row.Field<string>("TypeOfSignal") == "Group Output")
                //{
                //    stream.WriteLine(string.Format($"\t-Name \"{0}\" -SignalType \"{1}\"\\\n\t-Unit \"{2}\" -UnitMap \"{3}\" -Access \"{4}\"", val0, val1, val2, val3, val4), val5);
                //}


            }
            stream.Close();
            fs.Close();
        }

    }
}
