﻿using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ht.ihsi.rgph.epc.supervision.views
{
    /// <summary>
    /// Logique d'interaction pour frm_configuration.xaml
    /// </summary>
    public partial class frm_configuration : UserControl
    {
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RgphData\Configuration\";
        string pathDefaultConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"App_data\configuration\";
        string file = "";
        XmlUtils configuration = null;
        public frm_configuration()
        {
            InitializeComponent();
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            string[] files = Directory.GetFiles(pathDefaultConfigurationFile);
            if (files.Count() != 0)
            {
                foreach (string f in files)
                {
                    string cFile = basePath + "configuration.xml";
                    if (!System.IO.File.Exists(cFile))
                    {
                        System.IO.File.Copy(f, cFile, true);
                    }
                }
            }
            file = basePath + "configuration.xml";
            configuration = new XmlUtils(file);
            txt_adr_server.Text = configuration.getAdrServer();
            Users.users.AppExecutionPath = AppDomain.CurrentDomain.BaseDirectory;
            lbl_variable.Text = Users.users.AppExecutionPath + @"\adb";
        }
        private void btn_save_adr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string adrServer = txt_new_adr_server.Text;
                char split = '.';
                string[] adr = adrServer.Split(split);
                StringBuilder adresse = new StringBuilder();
                int range = Convert.ToInt32(adr[0]);
                if (range > 256 || range < 0)
                {
                    MessageBox.Show("L'adreses IP n'est pas valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    adresse.Append(""+range);
                    range = Convert.ToInt32(adr[1]);
                    if (range > 256 || range < 0)
                    {
                        MessageBox.Show("L'adreses IP n'est pas valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        adresse.Append("." + range);
                        range = Convert.ToInt32(adr[2]);
                        if (range > 256 || range < 0)
                        {
                            MessageBox.Show("L'adreses IP n'est pas valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            adresse.Append("." + range);
                            range = Convert.ToInt32(adr[3]);
                            if (range > 256 || range < 0)
                            {
                                MessageBox.Show("L'adreses IP n'est pas valide", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                adresse.Append("." + range);
                                string ipAdress = adresse.ToString();
                                configuration.UpdateAdrServer(ipAdress);
                                MessageBox.Show("Mise à effectuée avec succès", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);

                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string variableConf = configuration.getString(Constant.XML_ELEMENT_VARIABLE);
                if (variableConf != null && variableConf != "")
                {
                    MessageBox.Show("La variable est deja configuree:" + variableConf, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string pathVar = System.Environment.GetEnvironmentVariable("ANDROID_ROOT");
                    string variable = "ANDROID_ROOT";
                    string path = @"" + Users.users.AppExecutionPath + "adb";
                    if (pathVar == null)
                    {
                        System.Environment.SetEnvironmentVariable(variable, path, EnvironmentVariableTarget.Machine);
                        string mainVar = System.Environment.GetEnvironmentVariable("Path");
                        string mainPath = mainVar + @";" + path;
                        System.Environment.SetEnvironmentVariable("Path", mainPath, EnvironmentVariableTarget.Machine);
                        configuration.updateEnvironnementVariable(path);
                        MessageBox.Show("Variable configurée avec succès", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBoxResult result = MessageBox.Show("Veuillez redemarrer l'ordinateur.", Constant.WINDOW_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            Process proc = new Process();
                            proc.StartInfo.FileName = "shutdown.exe";
                            proc.StartInfo.UseShellExecute = false;
                            proc.StartInfo.CreateNoWindow = true;
                            proc.StartInfo.RedirectStandardInput = true;
                            proc.StartInfo.RedirectStandardOutput = true;
                            proc.StartInfo.RedirectStandardError = true;
                            proc.StartInfo.Arguments = "/r /t 0";
                            proc.Start();
                        }
                        else
                        {
                            MessageBox.Show("La variable n'est pas configuree", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("La variable est deja configuree/Variable:" + pathVar, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La variable n'est pas configuree: Error=>" + ex.Message);
            }
        }
    }
}
