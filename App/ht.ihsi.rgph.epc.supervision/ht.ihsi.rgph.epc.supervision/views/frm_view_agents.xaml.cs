using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour frm_view_agents.xaml
    /// </summary>
    public partial class frm_view_agents : UserControl
    {
        //public string ConfDir = "" + System.Windows.Forms + "\\Data\\Configuration";
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\EPC\";
        private static string TEMP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Temp\EPC\";
        private static string BACKUP_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Backup\EPC\";
        private static string APP_DIRECTORY_PATH = AppDomain.CurrentDomain.BaseDirectory;
        AgentModel agentModel = null;
        SdeModel sdeModel = null;
        IConfigurationService service = null;
        UtilisateurService utilisateurService;
        bool copied = false;
        public frm_view_agents()
        {
            InitializeComponent();
            service = new ConfigurationService();
            DataContext = this;
            lbAgents.ItemsSource = service.searchAllAgents();
            btn_save_tab.IsEnabled = false;
            utilisateurService = new UtilisateurService();
        }

        private void lbAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                AgentModel agent = ltb.SelectedItems.OfType<AgentModel>().FirstOrDefault();
                agentModel = agent;
                sdeModel = service.getSdeByAgent(agent.AgentId);
                List<SdeModel> sdes = new List<SdeModel>();
                sdes.Add(sdeModel);
                lbSdes.ItemsSource = sdes;
                List<AgentModel> agents = new List<AgentModel>();
                agents.Add(agent);
                List<MaterielModel> materielForAgent = new List<MaterielModel>();
                MaterielModel materiel = service.getMaterielByAgent(agent.AgentId);
                if (materiel.MaterielId != 0)
                {
                    //Activer ou desactiver le bouton configurer si le materiel est deja configurer
                    if (materiel.IsConfigured.GetValueOrDefault() == 1)
                    {
                        btn_synch.IsEnabled = false;
                    }
                    else
                        btn_synch.IsEnabled = true;
                    //

                    //Desactiver le bouton save si le materiel est deja enregistre
                    btn_save_tab.IsEnabled = false;
                    //
                    materiel.Agent = agentModel.AgentName;
                    materielForAgent.Add(materiel);
                    gridTablette.ItemsSource = materielForAgent;

                }
                else
                {
                    //Activer le bouton save
                    btn_save_tab.IsEnabled = true;
                    //Desactiver le botuon configurer
                    btn_synch.IsEnabled = false;
                    //Efface le grid
                    gridTablette.ItemsSource = new List<MaterielModel>();
                }

            }
            catch (Exception)
            {

            }
        }

        private void btn_synch_Click(object sender, RoutedEventArgs e)
        {
            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => configureTab());
            t = new Thread(ths);
            t.Start();
        }
        public void configureTab()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {

                            if (service.isMaterielConfigure(devInfo.Serial))
                            {
                                MessageBox.Show("Tablet sa konfigire deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {
                                tbl_personnel person = new tbl_personnel();
                                person.persId = agentModel.AgentId;
                                person.sdeId = sdeModel.SdeId;
                                SdeModel sde = new SdeModel();
                                sde = Utilities.getSdeInformationForTabletConf(sdeModel.SdeId);
                                person.prenom = agentModel.Prenom;
                                person.nom = agentModel.Nom;
                                person.nomUtilisateur = agentModel.CodeUtilisateur;
                                person.motDePasse = agentModel.MotDePasse;
                                person.ProfileId = (int)Constant.ProfileUtilisateur.PROFIL_AGENT_RECENSEUR_MOBILE;
                                person.estActif = 1;
                                person.sexe = agentModel.Sexe;
                                person.email = agentModel.Email;
                                person.comId = sde.ComId;
                                person.deptId = sde.DeptId;
                                person.vqseId = sde.VqseId;
                                person.zone = sde.Zone;
                                person.codeDistrict = sde.CodeDistrict;
                                person.motDePasse = "passpass";
                                service.savePersonne(person);
                                Tbl_Materiel mat = ModelMapper.MapToEntity(service.getMateriel(devInfo.Serial));
                                mat.IsConfigured = 1;
                                mat.LastSynchronisation = DateTime.Now.ToString();
                                service.updateMateriel(mat);
                                string sourceFileName = "";
                                if (Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                                    foreach (string f in files)
                                    {
                                        if (f.Contains(sdeModel.SdeId))
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
                                        //Arretez le processus ADB
                                        Utilities.killProcess(procs);
                                        //
                                        break;
                                    }
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                MessageBox.Show("Tablet sa a byen konfigire.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = service.SearchMateriels()));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe anrejistre tablet sa a avan ou konfigire li.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_save_tab_Click(object sender, RoutedEventArgs e)
        {
            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => saveTab());
            t = new Thread(ths);
            t.Start();
        }

        public void saveTab()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {
                            MessageBox.Show("Tablet sa anregistre deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                        else
                        {
                            if (service.isAgentExist(Convert.ToInt32(this.agentModel.AgentId)))
                            {
                                MessageBox.Show("Tablet sa anregistre deja pou yon lot ajan resanse.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {
                                Tbl_Materiel mat = new Tbl_Materiel();
                                mat.Model = devInfo.Model;
                                mat.Serial = devInfo.Serial;
                                mat.Version = devInfo.OsVersion;
                                mat.DateAssignation = DateTime.Now.ToString();
                                mat.LastSynchronisation = DateTime.Now.ToString();
                                mat.IsConfigured = 0;
                                mat.Imei = devInfo.Imei;
                                mat.AgentId = this.agentModel.AgentId;
                                bool result = service.saveMateriel(mat);

                                if (!Directory.Exists(TEMP_DATABASE_PATH))
                                {
                                    Directory.CreateDirectory(TEMP_DATABASE_PATH);
                                }
                                copied = device.pullFile(TEMP_DATABASE_PATH);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                if (copied == true)
                                {
                                    string db_backup = BACKUP_DATABASE_PATH + @"" + sdeModel.SdeId + "\\";
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
                                        string fileName = System.IO.Path.GetFileName(f);
                                        string destFileName = System.IO.Path.Combine(db_backup, fileName + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".SQLITE");
                                        System.IO.File.Copy(f, destFileName, true);
                                        if (!Directory.Exists(MAIN_DATABASE_PATH))
                                        {
                                            Directory.CreateDirectory(MAIN_DATABASE_PATH);
                                        }
                                        destFileName = System.IO.Path.Combine(MAIN_DATABASE_PATH, sdeModel.SdeId + ".SQLITE");
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
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show(Constant.MSG_FICHIER_PAS_COPIE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                MessageBox.Show("Tablèt sa byen anregistre", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = service.SearchMateriels()));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONNECTE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);

                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_add_sup_Click(object sender, RoutedEventArgs e)
        {

            ThreadStart ths = null;
            Thread t = null;
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            ths = new ThreadStart(() => save_sup());
            t = new Thread(ths);
            t.Start();
        }
        public void save_sup()
        {
            try
            {
                DeviceManager device = new DeviceManager();
                Process[] procs = Process.GetProcessesByName("adb");
                if (device.IsConnected == true)
                {
                    DeviceInformation devInfo = device.getDeviceInformation();
                    if (devInfo != null)
                    {
                        IConfigurationService service = new ConfigurationService();
                        if (service.isMaterielExist(devInfo.Serial))
                        {

                            if (!service.isMaterielConfigure(devInfo.Serial))
                            {
                                MessageBox.Show("Tablet sa a poko konfigire deja pou yon ajan. Ou pa ka ajoute yon sipevize.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                            else
                            {

                                tbl_personnel person = new tbl_personnel();
                                UtilisateurModel user = utilisateurService.getSuperviseur((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_MOBILE);
                                if (user.CodeUtilisateur == "")
                                {
                                    Utilities.killProcess(procs);
                                    //
                                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                    return;
                                }
                                //Verifier si il ya deja un superviseur sur la tablette
                                if (service.ifSuperviseurExist((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_MOBILE) == true)
                                {
                                    MessageBox.Show("Gentan gen yon sipevize ki konfigire sou tablet sa a deja.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                                    //Arretez le processus ADB
                                    Utilities.killProcess(procs);
                                    //
                                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                                    return;
                                }
                                person.persId = user.UtilisateurId;
                                person.sdeId = sdeModel.SdeId;
                                SdeModel sde = new SdeModel();
                                sde = Utilities.getSdeInformationForTabletConf(sdeModel.SdeId);

                                person.prenom = user.Prenom;
                                person.nom = user.Nom;
                                person.nomUtilisateur = user.CodeUtilisateur;
                                person.motDePasse = user.MotDePasse;
                                person.ProfileId = (int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_MOBILE;
                                person.estActif = 1;
                                //person.sexe = agentModel.Sexe;
                                //person.email = agentModel.Email;
                                person.comId = sde.ComId;
                                person.deptId = sde.DeptId;
                                person.vqseId = sde.VqseId;
                                person.zone = sde.Zone;
                                person.codeDistrict = sde.CodeDistrict;
                                person.motDePasse = "passpass";

                                service.savePersonne(person);
                                //Tbl_Materiels mat = service.getMateriels(devInfo.Serial);
                                //mat.IsConfigured = 1;
                                //mat.LastSynchronisation = DateTime.Now.ToString();
                                //service.updateMateriels(mat);
                                string sourceFileName = "";
                                if (Directory.Exists(MAIN_DATABASE_PATH))
                                {
                                    string[] files = Directory.GetFiles(MAIN_DATABASE_PATH);
                                    foreach (string f in files)
                                    {
                                        if (f.Contains(sdeModel.SdeId))
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
                                        //Arretez le processus ADB
                                        Utilities.killProcess(procs);
                                        //
                                        break;
                                    }
                                    MessageBox.Show(Constant.MSG_TRANSFERT_TERMINE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                MessageBox.Show("Tablet sa a byen konfigire.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                gridTablette.Dispatcher.BeginInvoke((Action)(() => gridTablette.ItemsSource = service.SearchMateriels()));
                                //Arretez le processus ADB
                                Utilities.killProcess(procs);
                                //
                                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ou dwe anrejistre tablet sa a avan ou konfigire li.", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            //Arretez le processus ADB
                            Utilities.killProcess(procs);
                            //
                            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Constant.MSG_TABLET_PAS_CONFIGURE, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Arretez le processus ADB
                    Utilities.killProcess(procs);
                    //
                    waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void gridTablette_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "MaterielId")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Imei")
                e.Column.Visible = false;
        }
    }
}
