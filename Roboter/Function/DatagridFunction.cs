using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Roboter.Function
{
    class DatagridFunction
    {
        public string Val { get; set; } = string.Empty;
        public string Col { get; set; } = string.Empty;

        public void SetColumn(DataTable datatable, string Col, TextBox tbName, ComboBox Cmb_TypeOfSignal, TextBox tbAssingnedToDevice,
            TextBox tbSignalIdentificationLabel, TextBox tbDeviceMapping, TextBox tbCategory, ComboBox Cmb_AccessLevel,
            TextBox tbDefaultValue, TextBox tbFilterTimePassive, TextBox tbFilterTimeActive, ComboBox Cmb_SafeLevel)
        {
            switch (Col)
            {
                case "Name":
                    Col = "Name";
                    Val = tbName.Text.ToString();
                    break;
                case "TypeOfSignal":
                    Col = "TypeOfSignal";
                    Val = Cmb_TypeOfSignal.SelectedItem.ToString();
                    break;
                case "AssingnedToDevice":
                    Col = "AssingnedToDevice";
                    Val = tbAssingnedToDevice.Text.ToString();
                    break;
                case "SignalIdentificationLabel":
                    Col = "SignalIdentificationLabel";
                    Val = tbSignalIdentificationLabel.Text.ToString();
                    break;
                case "DeviceMapping":
                    Col = "DeviceMapping";
                    Val = tbDeviceMapping.Text.ToString();
                    break;
                case "Category":
                    Col = "Category";
                    Val = tbCategory.Text.ToString();
                    break;
                case "AccessLevel":
                    Col = "AccessLevel";
                    Val = Cmb_AccessLevel.SelectedItem.ToString();
                    break;
                case "DefaultValue":
                    Col = "DefaultValue";
                    Val = tbDefaultValue.Text.ToString();
                    break;
                case "FilterTimePassive":
                    Col = "FilterTimePassive";
                    Val = tbFilterTimePassive.Text.ToString();
                    break;
                case "FilterTimeActive":
                    Col = "FilterTimeActive";
                    Val = tbFilterTimeActive.Text.ToString();
                    break;
                case "InvertPhysicalValue":
                    Col = "InvertPhysicalValue";
                    Val = Cmb_SafeLevel.SelectedItem.ToString();
                    break;
            }

            foreach (DataRow row in datatable.Rows)
            {
                row.SetField<string>(Col, Val);
                Console.WriteLine(row.Field<string>(Col));
            }
        }
    }
}
