using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ht.ihsi.rgph.epc.supervision.views
{
    /// <summary>
    /// Logique d'interaction pour frm_pop_up_transfert.xaml
    /// </summary>
    public partial class frm_pop_up_transfert : Window
    {
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\EPC\";
        private static string TEMP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Temp\EPC\";
        private static string BACKUP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Backup\EPC\";
        private static string APP_DIRECTORY_PATH = AppDomain.CurrentDomain.BaseDirectory;
        private static string CLASSNAME = "frm_pop_up_transfert";
        private DeviceManager device;
        BackgroundWorker bckw;
        SqliteDataReaderService service;
        ISqliteReader reader = null;
        Logger log;
        ConfigurationService settings = null;
        ThreadStart ths = null;
        Thread t = null;
        int typeTransfert = 0;
        bool copied = false;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public frm_pop_up_transfert(int type)
        {
            InitializeComponent();
            device = new DeviceManager();
            bckw = new BackgroundWorker();
            service = new SqliteDataReaderService();
            log = new Logger();
            typeTransfert = type;
            if (typeTransfert == Constant.TRANSFERT_MOBILE)
                grpTransfert.Dispatcher.BeginInvoke((Action)(() => grpTransfert.Header = "Transfè k ap fèt sou odinatè sipèvizè a."));
            else
            {
                grpTransfert.Dispatcher.BeginInvoke((Action)(() => grpTransfert.Header = "Transfè k ap fèt sou tablèt ajan an. "));
            }
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
        }

        public void pullFile()
        {
            string methodName = "pullFile";
            string sdeId = null;
            MaterielModel mat = null;
            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
            try
            {

                if (device.IsConnected == true)
                {
                    lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Tablèt la konekte."));
                    DeviceManager dev = null;
                    DeviceInformation info = null;
                    dev = new DeviceManager();
                    info = dev.getDeviceInformation();
                    Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
                    settings = new ConfigurationService();
                    mat = settings.getMateriel(info.Serial);
                    sdeId = settings.getSdeByAgent(mat.AgentId.GetValueOrDefault()).SdeId;
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 30));
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfert des fichiers... "));
                        if (!Directory.Exists(TEMP_DATABASE_PATH))
                        {
                            Directory.CreateDirectory(TEMP_DATABASE_PATH);
                        }
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a ap fèt... "));
                        copied = device.pullFile(TEMP_DATABASE_PATH);
                        Process[] procs = Process.GetProcessesByName("adb");
                        if (procs.Length != 0)
                        {
                            foreach (var proc in procs)
                            {
                                if (!proc.HasExited)
                                {
                                    proc.Kill();
                                }
                            }
                        }
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 40));
                        if (copied == true)
                        {
                            string db_backup = BACKUP_DATABASE_PATH + "" + sdeId + "\\";
                            if (!Directory.Exists(db_backup))
                            {
                                Directory.CreateDirectory(db_backup);
                            }
                            //
                            if (Directory.GetDirectories(TEMP_DATABASE_PATH).Length != 0)
                            {
                                TEMP_DATABASE_PATH = TEMP_DATABASE_PATH + "rgph_epc_db";
                            }
                            //
                            string[] files = Directory.GetFiles(TEMP_DATABASE_PATH);
                            foreach (string f in files)
                            {
                                string fileName = "temp";
                                //Creation d'un repoertoire data dans temp
                                string tempData = TEMP_DATABASE_PATH + @"\data\";
                                if (!Directory.Exists(tempData))
                                {
                                    Directory.CreateDirectory(tempData);
                                }
                                //
                                string tempFileName = System.IO.Path.Combine(tempData, fileName + ".SQLITE");
                                System.IO.File.Copy(f, tempFileName, true);

                                string destFileName = System.IO.Path.Combine(db_backup, fileName + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".SQLITE");
                                //Get all the data in the temporary database 
                                reader = new SqliteReader(Utilities.getConnectionString(tempData, fileName));
                                List<BatimentModel> batimentsFromMobile = reader.GetAllBatimentModel();
                                //BatimentDataMobile data = reader.GetAllBatiments();
                                ISqliteReader readerFromSde = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeId));
                                System.IO.File.Copy(f, destFileName, true);
                                if (!Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    Directory.CreateDirectory(MAIN_DATABASE_PATH);
                                }
                                destFileName = System.IO.Path.Combine(MAIN_DATABASE_PATH, sdeId + ".SQLITE");
                                FileInfo file = new FileInfo(destFileName);
                                log.Info("Length:" + file.Length);
                                if (!System.IO.File.Exists(destFileName))
                                {
                                    System.IO.File.Move(f, destFileName);
                                }
                                else
                                {
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                break;
                            }
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                            img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Visible));
                            MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);                            
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                            lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a fini. "));
                            this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
                        }
                        else
                        {
                            MessageBox.Show(Constant.MSG_FICHIER_PAS_COPIE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                            this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
                        }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    Process[] procs = Process.GetProcessesByName("adb");
                    if (procs.Length != 0)
                    {
                        foreach (var proc in procs)
                        {
                            if (!proc.HasExited)
                            {
                                proc.Kill();
                            }
                        }
                    }
                    prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                    this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
                }
            }
            catch (Exception ex)
            {
                log.Info("Classname/" + CLASSNAME + "/Method/" + methodName + "=>Error:" + ex.Message);
            }
            finally
            {
                Process[] procs = Process.GetProcessesByName("adb");
                if (procs.Length != 0)
                {
                    foreach (var proc in procs)
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                }
                prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            if (btn_start.Content.ToString() == "Demarrer")
            {
                copied = false;
                if (t != null)
                {
                    if (t.IsAlive)
                    {
                        t.Abort();
                    }
                }
                prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 0));
                img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
                img_finish.Dispatcher.BeginInvoke((Action)(() => img_finish.Visibility = Visibility.Hidden));
                btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
                btn_annuler.Dispatcher.BeginInvoke((Action)(() => btn_annuler.IsEnabled = false));
                try
                {
                    if (typeTransfert == Constant.TRANSFERT_PC)
                    {
                        ths = new ThreadStart(() => pushFile());
                        t = new Thread(ths);
                        t.Start();
                    }
                    else
                    {
                        ths = new ThreadStart(() => pullFile());
                        t = new Thread(ths);
                        t.Start();
                    }

                }
                catch (Exception ex)
                {
                    log.Info("ERREUR:<>===================<>" + ex.Message);
                }
                finally
                {
                    Process[] procs = Process.GetProcessesByName("adb");
                    if (procs.Length != 0)
                    {
                        foreach (var proc in procs)
                        {
                            if (!proc.HasExited)
                            {
                                proc.Kill();
                            }
                        }
                    }
                    img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = true));
                    btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.Content = "Fermer"));
                    btn_annuler.Dispatcher.BeginInvoke((Action)(() => btn_annuler.IsEnabled = true));
                }
            }
            else
            {
                this.Close();
                Process[] procs = Process.GetProcessesByName("adb");
                if (procs.Length != 0)
                {
                    foreach (var proc in procs)
                    {
                        if (!proc.HasExited)
                        {
                            proc.Kill();
                        }
                    }
                }
            }
        }

        public void pushFile()
        {
            string sdeId = null;
            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Visible));
            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
            btn_start.Dispatcher.BeginInvoke((Action)(() => btn_start.IsEnabled = false));
            if (device.IsConnected == true)
            {
                lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Tablèt la konekte."));
                DeviceManager dev = null;
                DeviceInformation info = null;
                MaterielModel mat = null;
                dev = new DeviceManager();
                info = dev.getDeviceInformation();
                Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
                settings = new ConfigurationService();
                mat = settings.getMateriel(info.Serial);
                if (mat != null)
                {
                    sdeId = settings.getSdeByAgent(mat.AgentId.GetValueOrDefault()).SdeId;
                    prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 50));
                    string sourceFileName = "";
                    if (Directory.Exists(MAIN_DATABASE_PATH))
                    {
                        string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                        foreach (string f in files)
                        {
                            if (f.Contains(sdeId))
                            {
                                sourceFileName = System.IO.Path.GetFileName(f);
                                string destFileName = System.IO.Path.Combine(TEMP_DATABASE_PATH, Constant.DB_NAME + ".db");
                                if (!System.IO.File.Exists(destFileName))
                                {
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                else
                                {
                                    System.IO.File.Copy(f, destFileName, true);
                                }
                                break;
                            }
                        }
                        files = Directory.GetFiles(TEMP_DATABASE_PATH);
                        bool pushed = false;
                        foreach (string f in files)
                        {
                            pushed = device.pushFile(f);
                            if (pushed == true)
                            {
                                System.IO.File.Delete(f);
                            }
                            Process[] procs = Process.GetProcessesByName("adb");
                            if (procs.Length != 0)
                            {
                                foreach (var proc in procs)
                                {
                                    if (!proc.HasExited)
                                    {
                                        proc.Kill();
                                    }
                                }
                            }
                            break;
                        }
                        prgb_trans_pda.Dispatcher.BeginInvoke((Action)(() => prgb_trans_pda.Value = 100));
                        MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                        lbl_trans.Dispatcher.BeginInvoke((Action)(() => lbl_trans.Content = "Transfè a fini. "));
                        this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
                }
            }
            else
            {
                MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
            }
        }
        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Process[] procs = Process.GetProcessesByName("adb");
            if (procs.Length != 0)
            {
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
        }
    }
}
