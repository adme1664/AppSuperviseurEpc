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
            deselectedBarItem();
        }

        private void bbc_affichage_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_visualisation frm_vue = new frm_visualisation();
            deselectedBarItem();
            Utilities.showControl(frm_vue, main_grid);
        }

    }
}
