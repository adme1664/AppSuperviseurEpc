using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class frm_rpt_deroulement : UserControl
    {
        #region DECLARATIONS....
        IRapportService rapportService;
        IConfigurationService configurationService;
        SdeModel sde = null;
        #endregion

        #region CONSTRUCTORS...
        public frm_rpt_deroulement()
        {
            InitializeComponent();
            rapportService = new RapportService();
            configurationService = new ConfigurationService();
            sde = configurationService.searchAllSdes().First();
            List<SdeModel> sdes = new List<SdeModel>();
            sdes.Add(sde);
            lbCodeDistrict.ItemsSource = sdes;
        }
        #endregion

        #region EVENTS...
        private void btn_add_report_Click(object sender, RoutedEventArgs e)
        {
            if (sde.CodeDistrict != null)
            {
                RapportDeroulementModel rptDeroulement = new RapportDeroulementModel();
                rptDeroulement.CodeDistrict = sde.CodeDistrict;
                frm_rpt_deroulement_details frm = new frm_rpt_deroulement_details(rptDeroulement, this);
                Utilities.showControl(frm, grd_details);
            }
            else
            {
                MessageBox.Show("Vous devez selectionner le code du district", Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void lbCodeDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox ltb = e.OriginalSource as ListBox;
            sde = new SdeModel();
            sde = ltb.SelectedItems.OfType<SdeModel>().FirstOrDefault();
            List<RapportDeroulementModel> listOf = new List<RapportDeroulementModel>();
            listOf = rapportService.searchRapportDeroulement();
            if (listOf.Count() == 0)
            {
                RapportDeroulementModel rpt = new RapportDeroulementModel();
                rpt.RapportName = "Nouveau";
                rpt.CodeDistrict = sde.CodeDistrict;
                listOf.Add(rpt);
                lbRprts.ItemsSource = listOf;
                btn_add_report.IsEnabled = true;
            }
            else
            {
                btn_add_report.IsEnabled = true;
                lbRprts.ItemsSource = listOf;
            }
        }

        private void lbRprts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = sender as ListBox;
                RapportDeroulementModel rpt = ltb.SelectedItems.OfType<RapportDeroulementModel>().FirstOrDefault();
                if (rpt != null)
                {
                    if (rpt.RapportName == "Nouveau")
                    {
                        frm_rpt_deroulement_details frm = new frm_rpt_deroulement_details(rpt, this);
                        Utilities.showControl(frm, grd_details);
                    }
                    else
                    {
                        frm_rpt_deroulement_details frm = new frm_rpt_deroulement_details(rpt, null);
                        Utilities.showControl(frm, grd_details);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR=>" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
