﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Roboter
{
    public class LoadData
    {
        public string Name { get; set; } = string.Empty;
        private DataRow row;

        public DataTable Datatable { get; set; }

        public void SetComboboxItems(ComboBox Cmb_TypeOfSignal, ComboBox Cmb_AccessLevel, ComboBox Cmb_SafeLevel, ComboBox Cmb_Invert)
        {
            // Combobox TypeOfSignal 
            Cmb_TypeOfSignal.Items.Add("AI");
            Cmb_TypeOfSignal.Items.Add("DI");
            Cmb_TypeOfSignal.Items.Add("GI");
            Cmb_TypeOfSignal.Items.Add("AO");
            Cmb_TypeOfSignal.Items.Add("DO");
            Cmb_TypeOfSignal.Items.Add("GO");

            // Combobox AccessLevel 
            Cmb_AccessLevel.Items.Add("Default");
            Cmb_AccessLevel.Items.Add("All");
            Cmb_AccessLevel.Items.Add("ReadOnly");

            // Combobox SafeLevel 
            Cmb_Invert.Items.Add("-- None --");
            Cmb_Invert.Items.Add("DefaultSafeLevel");
            Cmb_Invert.Items.Add("SafetySafeLevel");
            Cmb_Invert.Items.Add("StoreSignal");

            // SafeLevel 
            Cmb_SafeLevel.Items.Add("No");
            Cmb_SafeLevel.Items.Add("Yes");
        }

        public void CreateDatatableCol(ComboBox cmb_Projectnamen)
        {
            if (cmb_Projectnamen.Items.Count != 0)
            {
                try
                {
                    Name = cmb_Projectnamen.SelectedItem.ToString();

                    //Eingänge
                    Datatable = new DataTable(Name);

                    // Create Columns for Datatable
                    Datatable.Columns.Add("Name", typeof(string));
                    Datatable.Columns.Add("TypeOfSignal", typeof(string));
                    Datatable.Columns.Add("AssingnedToDevice", typeof(string));
                    Datatable.Columns.Add("SignalIdentificationLabel", typeof(string));
                    Datatable.Columns.Add("DeviceMapping", typeof(string));
                    Datatable.Columns.Add("Category", typeof(string));
                    Datatable.Columns.Add("AccessLevel", typeof(string));
                    Datatable.Columns.Add("DefaultValue", typeof(string));
                    Datatable.Columns.Add("FilterTimePassive", typeof(string));
                    Datatable.Columns.Add("FilterTimeActive", typeof(string));
                    Datatable.Columns.Add("Safety", typeof(string));
                    Datatable.Columns.Add("InvertPhysicalValue", typeof(string));

                    // Set default values for Datatable
                    Datatable.Columns["Name"].DefaultValue = "Name";
                    Datatable.Columns["TypeOfSignal"].DefaultValue = "TypeOfSignal";
                    Datatable.Columns["AssingnedToDevice"].DefaultValue = "AssingnedToDevice";
                    Datatable.Columns["SignalIdentificationLabel"].DefaultValue = "SignalIdentificationLabel";
                    Datatable.Columns["DeviceMapping"].DefaultValue = "DeviceMapping";
                    Datatable.Columns["Category"].DefaultValue = "Category";
                    Datatable.Columns["AccessLevel"].DefaultValue = "AccessLevel";
                    Datatable.Columns["DefaultValue"].DefaultValue = "DefaultValue";
                    Datatable.Columns["FilterTimePassive"].DefaultValue = "FilterTimePassive";
                    Datatable.Columns["FilterTimeActive"].DefaultValue = "FilterTimeActive";
                    Datatable.Columns["Safety"].DefaultValue = "No";
                    Datatable.Columns["InvertPhysicalValue"].DefaultValue = "InvertPhysicalValue";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Create new Row on Datatable  /*ComboBox DeviceMapStart,ComboBox DeviceMapEnd,*/
        public DataTable SetDataToDatatable(TextBox tbName, ComboBox Cmb_TypeOfSignal, TextBox tbAssingnedToDevice, TextBox tbSignalIdentificationLabel,
            TextBox tbDeviceMapping, TextBox tbCategory, ComboBox Cmb_AccessLevel, TextBox tbDefaultValue, TextBox tbFilterTimePassive,
            TextBox tbFilterTimeActive, ComboBox Cmb_Invert, ComboBox Cmb_SafeLevel, DataGrid datagrid)
        {
            try
            {
                //Eingänge
                row = Datatable.NewRow();
                row["Name"] = tbName.Text.ToString();
                row["TypeOfSignal"] = Cmb_TypeOfSignal.SelectedValue.ToString();
                row["AssingnedToDevice"] = tbAssingnedToDevice.Text.ToString();
                row["SignalIdentificationLabel"] = tbSignalIdentificationLabel.Text.ToString();
                row["DeviceMapping"] = tbDeviceMapping.Text.ToString(); 
                row["Category"] = tbCategory.Text.ToString();
                row["AccessLevel"] = Cmb_AccessLevel.SelectedValue.ToString();
                row["DefaultValue"] = tbDefaultValue.Text.ToString();
                row["FilterTimePassive"] = tbFilterTimePassive.Text.ToString();
                row["FilterTimeActive"] = tbFilterTimeActive.Text.ToString();
                row["Safety"] = Cmb_SafeLevel.SelectedValue.ToString();
                row["InvertPhysicalValue"] = Cmb_Invert.SelectedValue.ToString(); 
                Datatable.Rows.Add(row);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Datatable;
        }
    }
}

