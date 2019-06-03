using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using ht.ihsi.rgph.epc.supervision.viewmodels;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour frm_visualisation.xaml
    /// </summary>
    public partial class frm_visualisation : UserControl
    {
        Logger log;
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\";
        TreeViewModel model;
        SqliteDataReaderService sqliteService = null;
        IConfigurationService configurationService;
        TreeViewItem getTreeviewItem;
        ISqliteReader reader = null;
        SdeViewModel _sde = null;
        ThreadStart ths = null;
        Thread t = null;
        public frm_visualisation()
        {
            InitializeComponent();
            log = new Logger();
            sqliteService = new SqliteDataReaderService();
            configurationService = new ConfigurationService();
            //Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            SdeModel[] sdes = configurationService.searchAllSdes().ToArray();
            model = new TreeViewModel(sdes);
            base.DataContext = model;

        }

        #region PROPERTIES
        public TreeViewItem GetTreeviewItem
        {
            get { return getTreeviewItem; }
            set { getTreeviewItem = value; }
        }
        #endregion
        #region AFFICHAGE
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem currentContainer = e.OriginalSource as TreeViewItem;
                this.GetTreeviewItem = currentContainer;
                SdeViewModel _sde;
                BatimentViewModel _batiment;
                LogementViewModel _logement;
                MenageViewModel _menage;
                MenageDetailsViewModel _menageDetails;
                //IndividuViewModel _individu;
                scrl_bar_1.Visibility = Visibility.Visible;
                string name = "";
                while (currentContainer != null)
                {
                    //if (currentContainer.DataContext.ToString() == Constant.DATACONTEXT_INDIVIDUVIEWMODEL)
                    //{
                    //    _individu = currentContainer.DataContext as IndividuViewModel;
                    //}
                    if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.SdeViewModel))
                    {
                        _sde = currentContainer.DataContext as SdeViewModel;
                        //frm_details_sdes fr_sde = new frm_details_sdes(_sde.Sde);
                        //fr_sde.lbl_details_sde.Text = Utilities.getGeoInformation(_sde.SdeName);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            wInd.DeferedVisibility = true;
                        }));
                        //Utilities.showControl(fr_sde, grd_details);
                    }
                    else if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.BatimentViewModel))
                    {
                            _batiment = currentContainer.DataContext as BatimentViewModel;
                            frm_reponses fr_bat = new frm_reponses(_batiment.Batiment, _batiment.SdeName);
                            Utilities.showControl(fr_bat, grd_details);
                    }
                    else if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.LogementViewModel))
                    {
                        _logement = currentContainer.DataContext as LogementViewModel;
                        frm_reponses fr_logement = new frm_reponses(_logement.Logement, _logement.Logement.sdeId);
                        Utilities.showControl(fr_logement, grd_details);
                    }
                    else if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.MenageViewModel))
                    {
                        _menage = currentContainer.DataContext as MenageViewModel;
                        frm_reponses fr_menage = new frm_reponses(_menage.Model, _menage.NumSde);
                        Utilities.showControl(fr_menage, grd_details);
                    }
                    else if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.MenageDetailsViewModel))
                    {
                        _menageDetails = currentContainer.DataContext as MenageDetailsViewModel;
                        if (_menageDetails.Menage.Type ==(int) Constant.CodeMenageDetails.AncienMembre)
                        {
                            frm_reponses frm_emigre = new frm_reponses(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                            Utilities.showControl(frm_emigre, grd_details);
                        }
                        else if (_menageDetails.Menage.Type == (int)Constant.CodeMenageDetails.RapportFinal)
                        {
                            frm_reponses frm_deces = new frm_reponses(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                            Utilities.showControl(frm_deces, grd_details);
                        }
                        else
                        {
                            frm_reponses frm_individu = new frm_reponses(_menageDetails.Menage, _menageDetails.Menage.SdeId);
                            Utilities.showControl(frm_individu, grd_details);
                        }
                        name = _menageDetails.MenageDetailsId;
                    }
                    currentContainer = getParent(currentContainer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {

            TreeViewItem currentContainer = e.OriginalSource as TreeViewItem;
            SdeViewModel _sde;
            if (currentContainer.DataContext.ToString() == Constant.GetStringValue(Constant.DataContext.SdeViewModel))
            {
                _sde = currentContainer.DataContext as SdeViewModel;
                if (_sde.AllreadyLoad == false)
                {
                    _sde.IsLoading = true;
                }
            }
        }
        private TreeViewItem getParent(TreeViewItem container)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(container);
            while (parent != null && (parent is TreeViewItem))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as TreeViewItem;
        }

        private void scrl_view_tree_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void scrl_bar_1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }


        #endregion
    }
}
