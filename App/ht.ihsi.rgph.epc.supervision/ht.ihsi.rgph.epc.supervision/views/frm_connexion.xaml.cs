using ht.ihsi.rgph.epc.supervision.beans;
using ht.ihsi.rgph.epc.supervision.jsons;
using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ht.ihsi.rgph.epc.supervision.views
{
    /// <summary>
    /// Logique d'interaction pour frm_connexion.xaml
    /// </summary>
    public partial class frm_connexion : Window
    {
        private IUtilisateurService service;
        private IConfigurationService configuration;
        private ConsumeApiService apiService;
        private bool state = false;
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\EPC\";
        string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Configuration\";
        string pathDefaultConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"App_data\configuration\";
        XmlUtils conf = null;
        string serverAdress;
        Logger log;
        public frm_connexion()
        {
            InitializeComponent();
            Users.users = new Users();
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            service = new UtilisateurService();
            configuration = new ConfigurationService();
            log = new Logger();
            try
            {
                if (!Directory.Exists(MAIN_DATABASE_PATH))
                {
                    Directory.CreateDirectory(MAIN_DATABASE_PATH);
                }
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                string fileName = basePath + "configuration.xml";
                string[] files = Directory.GetFiles(pathDefaultConfigurationFile);
                foreach (string f in files)
                {
                    System.IO.File.Copy(f, fileName, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void getAdressServer()
        {
            string file = basePath + "configuration.xml";
            conf = new XmlUtils(file);
            serverAdress = conf.getAdrServer();
        }
        private void btn_annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            App.Current.Shutdown();
        }

        private void btn_connexion_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                getAdressServer();
                initializeConnexion(t_username.Text, t_password.Password);
                if (state == true)
                {
                    MainFrame m = new MainFrame();
                    this.Close();
                    m.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Info("Error:" + ex.Message);
            }
        }
        private void chk_isAstic_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        public bool pingTheServer(string adrIp)
        {
            try
            {
                var ping = new Ping();
                var reply = ping.Send(adrIp);
                return reply != null && reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {

            }
            return false;
        }
        public async Task initializeConnexion(string username, string password)
        {
            bool pingStatus = true;
            Dispatcher.Invoke(new Action(() =>
            {
                busyIndicator.IsBusy = true;
                busyIndicator.BusyContent = "Tentative de connexion...";
                img_loading.Visibility = Visibility.Visible;
            }
            ));

            if (username.Length == 0 || password.Length == 0)
            {
                await busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.IsBusy = false));
                await img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                MessageBox.Show("Veuillez saisir un nom utilisateur et un mot de passe", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UtilisateurModel user = null;
                await busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion avec la base de donnéees..."));

                //Tester si c'est l'astic qui se connecte
                if (chk_isAstic.IsChecked == true)
                {
                    if (service.isAsticAccountExist() == true)
                    {
                        user = service.authenticateUserLocally(username, password);
                        await img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                    }

                }
                else
                {
                    //Authentification au niveau sans passer par l'API
                    if (service.isSuperviseurAccountExist() == true)
                    {
                        try
                        {
                            //string[] tab = username.Split('.');
                            //username = tab[0] + "" + tab[1];
                            user = service.authenticateUserLocally(username, password);
                            await img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                        }
                        catch (Exception ex)
                        {
                            log.Info("Error:" + ex.Message);
                            img_loading.Dispatcher.BeginInvoke((Action)(() => img_loading.Visibility = Visibility.Hidden));
                        }
                    }
                    //
                    //Atuhentification a partir de l'API
                    else
                    {

                        await busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Application non encore configuree. Connexion avec le serveur..."));
                        //
                        //On fait ping de l'addresse du server
                        pingStatus = pingTheServer(serverAdress);
                        if (pingStatus == false)
                        {
                            MessageBox.Show("Serveur indisponible", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);

                        }

                        await busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion avec le serveur en cours..."));
                        try
                        {
                            //Point de Contact avec l'API 
                            apiService = new ConsumeApiService();
                            Utilisateur userApi = new Utilisateur();
                            //string[] tab = username.Split('.');
                            //username = tab[0] + "" + tab[1];
                            userApi.username = username;
                            userApi.password = password;
                            //Authentification de du superviseur et recuperation de son identifiant
                            Utilisateur test = await apiService.authenticateUser(userApi);
                            //Si l'utilisateur existe
                            if (test != null)
                            {
                                user = new UtilisateurModel();
                                user.CodeUtilisateur = test.username;
                                user.MotDePasse = test.password;
                                user.ProfileId = Convert.ToInt32(test.profileId);
                                user.Nom = test.nom;
                                user.Prenom = test.prenom;

                                //Enregistrement du superviseur
                                service.insertUser(user);

                                //Recherche des Sdes assignees au superviseur
                                List<SdeBean> sdes = await apiService.listOfSde(test);
                                if (sdes != null)
                                {
                                    foreach (SdeBean s in sdes)
                                    {
                                        SdeModel sde = new SdeModel();
                                        sde.CodeDistrict = s.codeDistrict;
                                        sde.SdeId = s.SdeId;
                                        //Enregistrements
                                        configuration.saveSdeDetails(ModelMapper.MapTo(sde));
                                    }
                                }
                                //
                                //Recherche des agents
                                List<AgentJson> agents = await apiService.listOfAgent(test);
                                if (agents != null)
                                {
                                    foreach (AgentJson a in agents)
                                    {
                                        AgentModel agent = new AgentModel();
                                        agent.CodeUtilisateur = a.agentId;
                                        agent.Nom = a.nom;
                                        agent.Prenom = a.prenom;
                                        agent.Email = a.email;
                                        //Ajout des agents
                                        AgentModel agentSaved = configuration.insertAgentSde(agent);
                                        //Modification de la sde en attribuant un agent a une sde
                                        SdeModel s = configuration.getSdeDetails(a.sde);
                                        if (s != null)
                                        {
                                            s.AgentId = agentSaved.AgentId;
                                            //configuration.updateSdeDetails(ModelMapper.MapToSde(s));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Ce superviseur n'existe pas sur le serveur ", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        catch (Exception ex)
                        {
                            log.Info("ERROR:===============>" + ex.Message);
                        }

                        busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Connexion reussie"));
                    }
                }

                if (Utils.IsNotNull(user))
                {
                    //Creation d'une variable statique User accessible n'importe dans le programme
                    Users.users.CodeUtilisateur = user.CodeUtilisateur;
                    Users.users.Prenom = user.Prenom;
                    Users.users.Nom = user.Nom;
                    Users.users.Profile = "" + user.ProfileId;
                    state = true;
                    Users.users.Utilisateur = new UtilisateurModel();
                    Users.users.Utilisateur = user;
                    Users.users.DatabasePath = MAIN_DATABASE_PATH;
                    //
                    //Affichage de la fenetre mere
                    MainFrame m = new MainFrame();
                    this.Close();
                    m.ShowDialog();

                }
                else
                {
                    await busyIndicator.Dispatcher.BeginInvoke((Action)(() => busyIndicator.BusyContent = "Erreur de connexion."));
                    await Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                    {
                        busyIndicator.IsBusy = false;
                        lbl_error.Content = "Erreur- Nom Utilisateur ou mot de passe errone.";
                        lbl_error.Visibility = Visibility.Visible;
                        img_loading.Visibility = Visibility.Hidden;
                    }, null);
                    state = false;
                }
            }
        }

        private void t_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {

                    initializeConnexion(t_username.Text, t_password.Password);
                }
                catch (Exception ex)
                {
                    log.Info("Error:" + ex.Message);
                }
            }
        }
    }

}
