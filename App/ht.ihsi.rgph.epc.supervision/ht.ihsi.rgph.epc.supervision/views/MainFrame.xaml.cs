using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using ht.ihsi.rgph.epc.supervision.utils;
using System.Diagnostics;
using ht.ihsi.rgph.epc.supervision.models;


namespace ht.ihsi.rgph.epc.supervision.views
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainFrame : DXRibbonWindow
    {
        public MainFrame()
        {
            InitializeComponent();

            BarButtonItem btnConnexion = new BarButtonItem();
            btnConnexion.Content = "Deconnexion";
            btnConnexion.Glyph = new BitmapImage(new Uri(@"/images/signout.png", UriKind.RelativeOrAbsolute));
            btnConnexion.ItemClick += btnConnexion_ItemClick;

            BarButtonItem btnInfo = new BarButtonItem();
            btnInfo.Glyph = new BitmapImage(new Uri(@"/images/user.png", UriKind.RelativeOrAbsolute));
            btnInfo.Content = Users.users.Nom + " " + Users.users.Prenom + " (Superviseur)";
            bsiIsConnecter.Items.Add(btnInfo);
            bsiIsConnecter.Items.Add(btnConnexion);

            TextModel model = null;
            if (Users.users.Profile == ((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG).ToString())
            {
                page_configuration.IsEnabled = false;
                bbi_avances.IsVisible = false;
                bbi_agents.IsVisible = false;
                txt_connecteduser.Text = "" + Users.users.Nom + " " + Users.users.Prenom + " (Superviseur)";
            }
            if (Users.users.Profile == ((int)Constant.ProfileUtilisateur.PROFIL_ASTIC).ToString())
            {
                rpc_transfert.IsEnabled = false;
                rpc_sdes.IsEnabled = false;
                txt_connecteduser.Text = "" + Users.users.Nom + " " + Users.users.Prenom + " (Agent de Support TIC)";
            }
            model = new TextModel();
            model.Username = "" + Users.users.Nom + " " + Users.users.Prenom;
            model.Deconnexion = "Deconnexion";
            stConnexion.ToolTip = model.Username + " s'est connecté";
            stConnexion.DataContext = model;
            biConnexion.ToolTip = model.Username + " s'est connecté";
            
        }

        void btnConnexion_ItemClick(object sender, ItemClickEventArgs e)
        {
            MessageBoxResult confirm = MessageBox.Show("Eske ou vle dekonekte?", Constant.WINDOW_TITLE, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm == MessageBoxResult.Yes)
            {
                Process[] procs = Process.GetProcessesByName("adb");
                Utilities.killProcess(procs);

                frm_connexion connexion = new frm_connexion();
                connexion.Show();
                this.Close();

            }

        }
        public void deselectedBarItem()
        {
            bbc_affichage.Dispatcher.BeginInvoke((Action)(() => bbc_affichage.IsChecked = false));
            bbc_synchronisation.Dispatcher.BeginInvoke((Action)(() => bbc_synchronisation.IsChecked = false));
            bbc_verification.Dispatcher.BeginInvoke((Action)(() => bbc_verification.IsChecked = false));
        }

        private void bbc_synchronisation_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_pop_up_transfert popupTransfert = new frm_pop_up_transfert(Constant.TRANSFERT_MOBILE);
            deselectedBarItem();
            bbc_synchronisation.Dispatcher.BeginInvoke((Action)(() => bbc_synchronisation.IsChecked = true));
            popupTransfert.Closing+=popupTransfert_Closing;
            if (popupTransfert.ShowDialog() == true)
            {

            }
            bbc_synchronisation.Focus();
        }
        void popupTransfert_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
        }
        
        private void bbi_avances_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            deselectedBarItem();
            bbi_avances.Dispatcher.BeginInvoke((Action)(() => bbi_avances.IsChecked = true));
            main_grid_1.IsSplashScreenShown = true;
            frm_configuration conf = new frm_configuration();
            Utilities.showControl(conf, main_grid);
            main_grid_1.IsSplashScreenShown = false;
            bbi_avances.Focus();
        }
        private void bbi_rapports_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            deselectedBarItem();
            bbi_rapports.Dispatcher.BeginInvoke((Action)(() => bbi_rapports.IsChecked = true));
            main_grid_1.IsSplashScreenShown = true;
            frm_rpt_personnel conf = new frm_rpt_personnel();
            Utilities.showControl(conf, main_grid);
            main_grid_1.IsSplashScreenShown = false;
            bbi_rapports.Focus();
        }
        private void bbi_agents_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            deselectedBarItem();
            bbi_agents.Dispatcher.BeginInvoke((Action)(() => bbi_agents.IsChecked = true));
            main_grid_1.IsSplashScreenShown = true;
            frm_view_agents frm_agents = new frm_view_agents();
            Utilities.showControl(frm_agents, main_grid);
            main_grid_1.IsSplashScreenShown = false;
            bbi_agents.Focus();
        }
        private void main_ribbon_SelectedPageChanged(object sender, RibbonPropertyChangedEventArgs e)
        {
            if (main_ribbon.SelectedPage == page_sde)
            {
                if (Users.users.Profile == ((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG).ToString())
                {
                    main_grid_1.Dispatcher.BeginInvoke((Action)(() => main_grid_1.IsSplashScreenShown = true));
                    deselectedBarItem();
                    //frm_view_verification verification = new frm_view_verification();
                    //Utilities.showControl(verification, main_grid);
                    //bbi_verification.Dispatcher.BeginInvoke((Action)(() => bbi_verification.IsChecked = true));
                    main_grid_1.Dispatcher.BeginInvoke((Action)(() => main_grid_1.IsSplashScreenShown = false));
                }
            }
            if (main_ribbon.SelectedPage == page_transfret)
            {
                if (Users.users.Profile == ((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG).ToString())
                {
                    main_grid_1.Dispatcher.BeginInvoke((Action)(() => main_grid_1.IsSplashScreenShown = true));
                    MessageBox.Show("En cours de developpement", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                    deselectedBarItem();
                    //Frm_view_transfert frm_transfert = new Frm_view_transfert(this);
                    //Utilities.showControl(frm_transfert, main_grid);
                    //deselectedBarItem();
                    //bbi_transfert.Dispatcher.BeginInvoke((Action)(() => bbi_transfert.IsChecked = true));
                    main_grid_1.Dispatcher.BeginInvoke((Action)(() => main_grid_1.IsSplashScreenShown = false));
                }
            }
            if (main_ribbon.SelectedPage == page_configuration)
            {
                if (Users.users.Profile == ((int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG).ToString())
                {
                    main_grid.Dispatcher.BeginInvoke((Action)(() => main_grid.Children.Clear()));
                }
            }
        }

        private void bbc_affichage_ItemClick(object sender, ItemClickEventArgs e)
        {
            deselectedBarItem();
            bbc_affichage.Dispatcher.BeginInvoke((Action)(() => bbc_affichage.IsChecked = true));
            main_grid_1.IsSplashScreenShown = true;
            frm_visualisation frm_visualisation = new frm_visualisation();
            Utilities.showControl(frm_visualisation, main_grid);
            main_grid_1.IsSplashScreenShown = false;
            bbc_affichage.Focus();
        }

        private void bbc_verification_ItemClick(object sender, ItemClickEventArgs e)
        {
            deselectedBarItem();
            frm_view_verification frm_verification = new views.frm_view_verification();
            Utilities.showControl(frm_verification,main_grid);
            main_grid_1.IsSplashScreenShown = false;
            bbc_verification.Focus();
        }
        private void rpc_rpt_deroulement_collecte_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            deselectedBarItem();
            rpc_rpt_deroulement_collecte.Dispatcher.BeginInvoke((Action)(() => rpc_rpt_deroulement_collecte.IsChecked = true));
            main_grid_1.IsSplashScreenShown = true;
            frm_rpt_deroulement rpt = new frm_rpt_deroulement();
            Utilities.showControl(rpt, main_grid);
            main_grid_1.IsSplashScreenShown = false;
        }
      

    }
}
