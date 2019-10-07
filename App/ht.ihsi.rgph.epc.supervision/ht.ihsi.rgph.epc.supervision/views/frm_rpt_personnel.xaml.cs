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
    /// Logique d'interaction pour frm_rpt_personnel.xaml
    /// </summary>
    public partial class frm_rpt_personnel : UserControl
    {

        IConfigurationService configurationService;
        IRapportService rapportService;
        AgentModel agent;

        /// <summary>
        /// Constructor
        /// </summary>
        public frm_rpt_personnel()
        {
            InitializeComponent();
            configurationService = new ConfigurationService();
            lbAgents.ItemsSource = configurationService.searchAllAgents();
            rapportService = new RapportService();
            agent = new AgentModel();
        }
        private void lbAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lbRprts.ItemsSource = new List<RapportPersonnelModel>();
                ListBox ltb = e.OriginalSource as ListBox;
                this.agent = ltb.SelectedItems.OfType<AgentModel>().FirstOrDefault();
                string sde = configurationService.getSdeByAgent(agent.AgentId).SdeId;
                List<RapportPersonnelModel> listOfRpt = new List<RapportPersonnelModel>();
                listOfRpt = rapportService.searchRapportPersonnelByAgent(Convert.ToInt32(agent.CodeUtilisateur));
                if (listOfRpt.Count == 0)
                {
                    RapportPersonnelModel rpt = new RapportPersonnelModel();
                    rpt.RapportName = "Nouveau";
                    listOfRpt.Add(rpt);
                    lbRprts.ItemsSource = listOfRpt;
                }
                else
                {
                    lbRprts.ItemsSource = listOfRpt;
                }
            }
            catch (Exception)
            {

            }
        }
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            frm_questions_agents frm = new frm_questions_agents(this.agent, this);
            Utilities.showControl(frm, grd_details);
        }

        private void lbRprts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox ltb = e.OriginalSource as ListBox;
                RapportPersonnelModel rpt = ltb.SelectedItems.OfType<RapportPersonnelModel>().FirstOrDefault();
                if (rpt == null)
                {
                    frm_questions_agents frm = new frm_questions_agents(this.agent, this);
                    Utilities.showControl(frm, grd_details);
                }
                else
                {
                    if (rpt.RapportName == "Nouveau")
                    {
                        frm_questions_agents frm = new frm_questions_agents(this.agent, this);
                        Utilities.showControl(frm, grd_details);
                    }
                    else
                    {
                        frm_questions_agents frm = new frm_questions_agents(this.agent, rpt);
                        Utilities.showControl(frm, grd_details);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
