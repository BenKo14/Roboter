
using Microsoft.Win32;
using Roboter.Function;
using Roboter.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.ComponentModel;
using System.Reflection;

namespace Roboter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //#####################  Instanzen  ###################################################################
        SetStartUpFolder startLocation = new SetStartUpFolder();
        LoadData loadData = new LoadData();
        SaveDataToCsv saveData = new SaveDataToCsv();
        FilePath filePath = new FilePath();
        LoaadFromXml loadXml = new LoaadFromXml();
        SaveRoboDataInXml saveRobo = new SaveRoboDataInXml();
        DatagridFunction datagridFunction = new DatagridFunction();
        WriteCfg writeCfg = new WriteCfg();
        public XmlWriter Writer { get; set; } = null;
        //#####################  Felder  ######################################################################

        private List<string> ListProjectnames = new List<string>();

        //#####################  Propertys  ###################################################################
        public string Projectname { get; set; }
        public  string PathUri { get; set; }
        public string StarUriPath { get; set; }
        //#####################################################################################################
        public MainWindow()
        {
            InitializeComponent();
            startLocation.BuildStartLocation();
        }

        
        //#####################################################################################################
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //#####################################################################################################
            //lese Pfade aus Ordner aus, Selectiere Projektnamen für Cmb, fürge Projektnamen zu Cmb
            filePath.GetFilePath();
            filePath.SeperateFileName(ListProjectnames); //Combobox Anzeigename
            filePath.SetProjectNames(cmb_Projectnamen, ListProjectnames); // setzt Combobox Items
            //#####################################################################################################
            //Combobox Signal, Acceslevel, safetyleve
            loadData.SetComboboxItems(Cmb_TypeOfSignal, Cmb_AccessLevel, Cmb_SafeLevel, Cmb_Invert /* DeviceMapStart, DeviceMapEnd*/);
            //Datatable für xml
            loadData.CreateDatatableCol(cmb_Projectnamen);
            //Lade Xml für Datagrid
            loadXml.loadDataFromXml(cmb_Projectnamen, loadData, datagrid);
        }
        //#####################################################################################################

        private void Btn_Click_Uebernehmen(object sender, RoutedEventArgs e)
        {
            try { 
            loadData.SetDataToDatatable(tbName, Cmb_TypeOfSignal, tbAssingnedToDevice, tbSignalIdentificationLabel,
            tbDeviceMapping, /*DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive,
            tbFilterTimeActive, Cmb_SafeLevel, Cmb_Invert, datagrid);

            datagrid.ItemsSource = loadData.Datatable.DefaultView;

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result,resultXml, cmb_Projectnamen);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //#####################################################################################################

        private void Btn_Click_WindowBrowser(object sender, RoutedEventArgs e)
        {
            var FilePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Config Files (*.cfg)|*cfg|Csv Files (*.csv)|*csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\RoboXmlData";
            FilePath = openFileDialog.FileName;


            if (openFileDialog.ShowDialog() == true)
            {
                TextBox tb = new TextBox();

                tb.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

                Window ViewWindow = new Window() { Height = 800, Width = 800 };
                ViewWindow.Content = tb;
                tb.Text = File.ReadAllText(openFileDialog.FileName);
               
                //result.Text = File.ReadAllText(openFileDialog.FileName);

                ViewWindow.Show();
            }
        }
        //#####################################################################################################

        private void Btn_Click_Csv(object sender, RoutedEventArgs e)
        {
            Projectname = cmb_Projectnamen.SelectedItem.ToString();
            string strFilePath = @"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\RoboCsvData\" + Projectname + ".csv";
            saveData.ExportToCsv(loadData.Datatable, strFilePath);
        }
        //#####################################################################################################

        private void Btn_Click_Add(object sender, RoutedEventArgs e)
        {
            string Path = @"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\RoboXmlData\" + ProjectName.Text.ToString() + ".xml";
            if (ProjectName.Text.ToString() != string.Empty && !File.Exists(Path))
            {
                try
                {
                    Writer = XmlWriter.Create(Path, saveRobo.CreateXmlWriterSettings());
                    // XML-Validierungs-Kopf
                    Writer.WriteStartDocument(true);    // true = standalone="yes"
                    Writer.WriteStartElement("Root");
                    Writer.WriteEndElement();
                    Writer.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Writer.Close();
                }

                cmb_Projectnamen.Items.Clear(); // cmb leeren
                filePath.GetFilePath();
                filePath.SeperateFileName(ListProjectnames); // erstellt Filename für cmb
                filePath.SetProjectNames(cmb_Projectnamen, ListProjectnames); // set cmb Items
            }
            else { MessageBox.Show("Bitte geben sie eine Projektbezeichnung an!"); }
        }
        //#####################################################################################################

        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            //saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result);
        }
        //#####################################################################################################

        #region SaveRoboData

        //#####################################################################################################


        #endregion

        private void Cmb_Projectname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadData.CreateDatatableCol(cmb_Projectnamen);

            loadXml.loadDataFromXml(cmb_Projectnamen, loadData, datagrid);
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_RemoveProject(object sender, RoutedEventArgs e)
        {
            string Path = @"C:\Users\BKohlstedt\Desktop\Projekte\Roboter\ProjectNamespace\" + cmb_Projectnamen.SelectedItem.ToString() + ".xml";
            if (File.Exists(Path))
            {

                MessageBoxResult result = MessageBox.Show(this, "Soll das Projekt gelöscht werden?", "Achtung!!!", MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                {
                    File.Delete(Path);
                    ListProjectnames.Clear();
                    cmb_Projectnamen.Items.Clear();

                    filePath.GetFilePath();
                    filePath.SeperateFileName(ListProjectnames);
                    filePath.SetProjectNames(cmb_Projectnamen, ListProjectnames);
                }
            }
            else
            {
                cmb_Projectnamen.Items.Remove(cmb_Projectnamen.SelectedItem);
            }
        }

        #region ButtonSpalte
        private void Btn_Click_Name(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "Name", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_TypeOfSignal(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "TypeOfSignal", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_AssingedToDevice(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "AssingnedToDevice", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_SignalIdentLabel(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "SignalIdentificationLabel", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_DeviceMapping(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "DeviceMapping", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_Category(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "Category", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_AccessLevel(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "AccessLevel", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_DefaultValue(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "DefaultValue", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_FilterPassive(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "FilterTimePassive", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_FilterActive(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "FilterTimeActive", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        private void Btn_Click_InvertPhysicalVal(object sender, RoutedEventArgs e)
        {
            datagridFunction.SetColumn(loadData.Datatable, "InvertPhysicalValue", tbName, Cmb_TypeOfSignal, tbAssingnedToDevice,
                tbSignalIdentificationLabel, tbDeviceMapping,/* DeviceMapStart, DeviceMapEnd,*/ tbCategory, Cmb_AccessLevel, tbDefaultValue, tbFilterTimePassive, tbFilterTimeActive, Cmb_SafeLevel);

            saveRobo.SaveRoboData(cmb_Projectnamen, loadData, result, resultXml); //schreibe xml
            saveRobo.SetXmlContent(result, resultXml, cmb_Projectnamen);
        }

        #endregion

        private void Btn_Click_CreateCfg(object sender, RoutedEventArgs e)
        {
            string ProjektVal = cmb_Projectnamen.SelectedItem.ToString();
            writeCfg.CreateCfgData(loadData.Datatable, ProjektVal);
        }

        private void Btn_Click_Reaload(object sender, RoutedEventArgs e)
        {
            cmb_Projectnamen.Items.Clear(); // cmb leeren
            filePath.GetFilePath();
            filePath.SeperateFileName(ListProjectnames); // erstellt Filename für cmb
            filePath.SetProjectNames(cmb_Projectnamen, ListProjectnames); // set cmb Items
        }
    }
}
