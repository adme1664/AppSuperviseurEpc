using DevExpress.Xpf.Charts;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
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
    /// Logique d'interaction pour frm_verification.xaml
    /// </summary>
    public partial class frm_verification : UserControl
    {
        #region  DECLARATIONS
        private static string MAIN_DATABASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\RgphData\Data\Databases\EPC\";
        ISqliteReader reader;
        bool IsAllDistrict = false;
        List<BatimentModel> listOfBatiments;
        private SdeModel sdeSelected = null;
        frm_view_verification main;
        Logger log;
        ThreadStart ths = null;
        Thread t = null;
        bool isTabCouvertureLoad = false;
        #endregion
        public frm_verification(SdeModel sde, frm_view_verification mainFrame)
        {
            InitializeComponent();
            main = mainFrame;
            log = new Logger();
            sdeSelected = sde;
            tabIndCouverture.Focus();
        }
        public frm_verification(bool isAllDistrict, frm_view_verification mainFrame)
        {
            InitializeComponent();
            InitializeComponent();
            main = mainFrame;
            tabGestionNotes.Visibility = Visibility.Hidden;
            this.IsAllDistrict = isAllDistrict;
            tabIndCouverture.Focus();
        }
        public void createGraphicSocioControls()
        {
            //Initialiser les controls
            initializeChartControls();
            //
            int nbrePersonnes = 0;
            int nbreFemmesRecenses = 0;
            int nbreEnfantMoins1Ans = 0;
            int nbrePersonnes18Ans = 0;
            int nbrePersonnes15_64Ans = 0;
            int nbreBatiment = 0;
            int nbreLogementInd = 0;
            int nbrHommeChefeMenage = 0;
            int nbreFemmeChefMenage = 0;
            int nbreMenageUniPersonnel = 0;
            int nbreMenage6Personnes = 0;
            double tailleMoyenneMenage = 0;
            int nbreTotalMenage = 0;
            int nbreTotalAncienMembre = 0;
            try
            {
                IConfigurationService configuration = new ConfigurationService();
                //Test pour voir si le calcul doit se faire pour tout le district ou pr un SDE
                if (IsAllDistrict == true)
                {
                    foreach (SdeModel sde in configuration.searchAllSdes())
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                        //Indicateurs socio-demographiques
                        nbreEnfantMoins1Ans = nbreEnfantMoins1Ans + reader.getTotalEnfantDeMoinsDe1Ans();
                        nbrePersonnes = nbrePersonnes + reader.getTotalHomme();
                        nbreFemmesRecenses = nbreFemmesRecenses + reader.getTotalFemme();
                        nbrePersonnes18Ans = nbrePersonnes18Ans + reader.getTotalPersonnes18EtPlusAns();
                        nbrePersonnes15_64Ans += reader.getTotalPersonnes15_64Ans();
                        tailleMoyenneMenage += reader.tailleMoyenneMenage();
                        //

                        //                      
                        nbrHommeChefeMenage = nbrHommeChefeMenage + reader.getTotalHommeChefMenage();
                        nbreTotalMenage = nbreTotalMenage + reader.getTotalMenages();
                        nbreFemmeChefMenage += reader.getTotalFemmeChefMenage();
                        nbreMenageUniPersonnel += reader.getTotalMenageUnipersonnel();
                        nbreMenage6Personnes += nbreMenage6Personnes + reader.getTotalMenageDe6IndsEtPlus();
                        nbreTotalAncienMembre = nbreTotalAncienMembre + reader.getTotalAncienMembre();

                    }
                }
                else
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    //Indicateurs de couverture
                    nbreBatiment = nbreBatiment + reader.GetAllBatimentModel().Count();
                    nbreLogementInd = nbreLogementInd + reader.getTotalIndividus();
                    nbrHommeChefeMenage = nbrHommeChefeMenage + reader.getTotalHommeChefMenage();
                    //

                    //Indicateurs socio-demographiques
                    nbreEnfantMoins1Ans = nbreEnfantMoins1Ans + reader.getTotalEnfantDeMoinsDe1Ans();
                    nbrePersonnes = nbrePersonnes + reader.getTotalHomme();
                    nbreFemmesRecenses = nbreFemmesRecenses + reader.getTotalFemme();
                    nbrePersonnes18Ans = nbrePersonnes18Ans + reader.getTotalPersonnes18EtPlusAns();
                    nbrePersonnes15_64Ans += reader.getTotalPersonnes15_64Ans();
                    tailleMoyenneMenage += reader.tailleMoyenneMenage();
                    //

                    //
                    nbreFemmeChefMenage += reader.getTotalFemmeChefMenage();
                    nbrHommeChefeMenage = nbrHommeChefeMenage + reader.getTotalHommeChefMenage();
                    nbreMenageUniPersonnel += reader.getTotalMenageUnipersonnel();
                    nbreTotalMenage = nbreTotalMenage + reader.getTotalMenages();
                    nbreTotalAncienMembre = nbreTotalAncienMembre + reader.getTotalAncienMembre();
                }
                pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Add(new SeriesPoint("% d'hommes dans la population recensée", nbrePersonnes))));
                pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Add(new SeriesPoint("% de femmes dans la population recensée", nbreFemmesRecenses))));

                barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%) d´enfants de moins d´un (1) an", nbreEnfantMoins1Ans))));
                barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 18 ans et plus", nbrePersonnes18Ans))));
                barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() =>
                    barSeriesProportionsDetails.Points.Add(new SeriesPoint("Proportion (%)  de personnes de 15-64 ans", nbrePersonnes15_64Ans))));


                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesTailleMenage.Points.Add(new SeriesPoint("Nombre de ménages enquêtés", nbreTotalMenage))));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                        barSeriesTailleMenage.Points.Add(new SeriesPoint("Taille moyenne des ménages", tailleMoyenneMenage))));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                            barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages unipersonnels (1 personne au plus)", nbreMenageUniPersonnel))));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                            barSeriesTailleMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages de grande taille (6  et plus)", nbreMenage6Personnes))));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() =>
                           barSeriesTailleMenage.Points.Add(new SeriesPoint("Nombre de personnes décédées ou ayant laissé le ménage après le DP", nbreTotalAncienMembre))));

                pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() =>
                       pieSeriesNbreMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages dont le chef est un homme", nbrHommeChefeMenage))));
                pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() =>
                           pieSeriesNbreMenage.Points.Add(new SeriesPoint("Proportion (%)  de ménages dont le chef est une femme. ", nbreFemmeChefMenage))));
                

                waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = false));

            }
            catch (Exception)
            {

            }
        }

        public void initializeChartControls()
        {
            try
            {
                pieSeriesNbreFemmes.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreFemmes.Points.Clear()));
                barSeriesProportionsDetails.Dispatcher.BeginInvoke((Action)(() => barSeriesProportionsDetails.Points.Clear()));
                barSeriesTailleMenage.Dispatcher.BeginInvoke((Action)(() => barSeriesTailleMenage.Points.Clear()));
                pieSeriesNbreMenage.Dispatcher.BeginInvoke((Action)(() => pieSeriesNbreMenage.Points.Clear()));               
            }
            catch (Exception)
            {

            }
        }

        public void createGraphicTabCouverture()
        {

            int nbrePersonnes = 0;
            int nbrePersonneTermine = 0;
            int nbrePersonneNonTermine = 0;
            int nbrePersonneVerifie = 0;
            int nbrePersonneNonVerifie = 0;
            int nbreBatimentTotal = 0;
            int nbreBatimentTotalTermine = 0;
            int nbreBatimentTotalNonTermine = 0;
            int nbreBatimentTotalVerifie = 0;
            int nbreBatimentTotalNonVerife = 0;
            int nbreLogementIndTotal = 0;
            int nbreLogementIndTotalTermine = 0;
            int nbreLogementIndTotalNonTermine = 0;
            int nbreLogementIndTotalVerifie = 0;
            int nbreLogementIndTotalNonVerifie = 0;       
            int nbreMenageTotal = 0;
            int nbreMenageTotalTermine = 0;
            int nbreMenageTotalNonTermine = 0;
            int nbreMenageTotalVerifie = 0;
            int nbreMenageTotalNonVerife = 0;
            int nbreActualisation = 0;
            int nbreAncienMembreTotal = 0;
            int nbreAncienMembreTotalTermine = 0;
            int nbreAncienMembreTotalNonTermine = 0;
            try
            {


                IConfigurationService configuration = new ConfigurationService();
                List<CouvertureModel> couvertures = new List<CouvertureModel>();

                #region CONSTRUCTION DE LA GRAPHE
                //Test pour verifier si la table st deja telechargee
                if (isTabCouvertureLoad == false)
                {
                    //Test pour voir si le calcul doit se faire pour tout le district ou pr une SDE
                    if (IsAllDistrict == true)
                    {
                        foreach (SdeModel sde in configuration.searchAllSdes())
                        {
                            reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));

                            //Indicateurs de couverture
                            //Batiments
                            nbreBatimentTotal = nbreBatimentTotal + reader.GetAllBatimentModel().Count();
                            nbreBatimentTotalNonTermine = nbreBatimentTotalNonTermine + reader.getTotalBatRecenseNTermine();
                            nbreBatimentTotalTermine = nbreBatimentTotalTermine + reader.getTotalBatRecenseNTermine();
                            nbreBatimentTotalVerifie = nbreBatimentTotalVerifie + 0;
                            nbreBatimentTotalNonVerife = nbreBatimentTotalNonVerife + 0;
                             //
                            //Logements Individuels
                            nbreLogementIndTotal = nbreLogementIndTotal + reader.getTotalLogement();
                            nbreLogementIndTotalNonTermine = nbreLogementIndTotalNonTermine + reader.getTotalLogeIRecenseNTermine();
                            nbreLogementIndTotalTermine = nbreLogementIndTotalTermine + reader.getTotalLogeIRecenseTermine();
                            nbreLogementIndTotalVerifie = nbreLogementIndTotalVerifie +0;
                            nbreLogementIndTotalNonVerifie = nbreLogementIndTotalNonVerifie + 0;
                            //                          
                            //
                            //Menages
                            nbreMenageTotal = nbreMenageTotal + reader.getTotalMenages();
                            nbreMenageTotalNonTermine = nbreMenageTotalNonTermine + reader.getTotalMenageRecenseNTermine();
                            nbreMenageTotalNonVerife = nbreMenageTotalNonVerife + 0;
                            nbreMenageTotalTermine = nbreMenageTotalTermine + reader.getTotalMenageRecenseTermine();
                            nbreMenageTotalVerifie = nbreMenageTotalVerifie +0;
                            //
                            //Personnes
                            nbrePersonnes = nbrePersonnes + reader.getTotalIndividus();
                            nbrePersonneNonTermine = nbrePersonneNonTermine + reader.getTotalIndRecenseNTermine();
                            nbrePersonneTermine = nbrePersonneTermine + reader.getTotalIndRecenseTermine();
                            nbrePersonneVerifie = nbrePersonneVerifie + 0;
                            nbrePersonneNonVerifie = nbrePersonneNonVerifie +0;
                            nbreActualisation = nbreActualisation + 0;
                            //
                            //Ancien Membre
                            nbreAncienMembreTotal = nbreAncienMembreTotal + reader.getTotalAncienMembre();
                            nbreAncienMembreTotalNonTermine = nbreAncienMembreTotalNonTermine + reader.getTotalAncienMembreNTermine();
                            nbreAncienMembreTotalTermine = nbreAncienMembreTotalTermine + reader.getTotalAncienMembreTermine();
                            nbreActualisation = nbreActualisation + 0;
                            //
                        }
                    }
                    else
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));

                        //Indicateurs de couverture
                        //Batiments
                        nbreBatimentTotal = nbreBatimentTotal + reader.GetAllBatimentModel().Count();
                        nbreBatimentTotalNonTermine = nbreBatimentTotalNonTermine + reader.getTotalBatRecenseNTermine();
                        nbreBatimentTotalTermine = nbreBatimentTotalTermine + reader.getTotalBatRecenseNTermine();
                        nbreBatimentTotalVerifie = nbreBatimentTotalVerifie + 0;
                        nbreBatimentTotalNonVerife = nbreBatimentTotalNonVerife + 0;
                        //
                        //Logements Individuels
                        nbreLogementIndTotal = nbreLogementIndTotal + reader.getTotalLogement();
                        nbreLogementIndTotalNonTermine = nbreLogementIndTotalNonTermine + reader.getTotalLogeIRecenseNTermine();
                        nbreLogementIndTotalTermine = nbreLogementIndTotalTermine + reader.getTotalLogeIRecenseTermine();
                        nbreLogementIndTotalVerifie = nbreLogementIndTotalVerifie + 0;
                        nbreLogementIndTotalNonVerifie = nbreLogementIndTotalNonVerifie + 0;
                        //                          
                        //
                        //Menages
                        nbreMenageTotal = nbreMenageTotal + reader.getTotalMenages();
                        nbreMenageTotalNonTermine = nbreMenageTotalNonTermine + reader.getTotalMenageRecenseNTermine();
                        nbreMenageTotalNonVerife = nbreMenageTotalNonVerife + 0;
                        nbreMenageTotalTermine = nbreMenageTotalTermine + reader.getTotalMenageRecenseTermine();
                        nbreMenageTotalVerifie = nbreMenageTotalVerifie + 0;
                        //
                        //Personnes
                        nbrePersonnes = nbrePersonnes + reader.getTotalIndividus();
                        nbrePersonneNonTermine = nbrePersonneNonTermine + reader.getTotalIndRecenseNTermine();
                        nbrePersonneTermine = nbrePersonneTermine + reader.getTotalIndRecenseTermine();
                        nbrePersonneVerifie = nbrePersonneVerifie + 0;
                        nbrePersonneNonVerifie = nbrePersonneNonVerifie + 0;
                        nbreActualisation = nbreActualisation + 0;
                        //
                        //Ancien Membre
                        nbreAncienMembreTotal = nbreAncienMembreTotal + reader.getTotalAncienMembre();
                        nbreAncienMembreTotalNonTermine = nbreAncienMembreTotalNonTermine + reader.getTotalAncienMembreNTermine();
                        nbreAncienMembreTotalTermine = nbreAncienMembreTotalTermine + reader.getTotalAncienMembreTermine();
                        nbreActualisation = nbreActualisation + 0;
                        //
                    }
                    //On cree les graphes
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                            barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Batiments", nbreBatimentTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Logements Individuels", nbreLogementIndTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Menages", nbreMenageTotal))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                       barSeriesIndCouverture.Points.Add(new SeriesPoint("Nombre de Personnes", nbrePersonnes))));
                    barSeriesIndCouverture.Dispatcher.BeginInvoke((Action)(() =>
                      barSeriesIndCouverture.Points.Add(new SeriesPoint("Ancien Membre", nbreAncienMembreTotal))));
                #endregion

                    #region CONSTRUCTION DU TABLEAU
                    CouvertureModel model1 = new CouvertureModel();
                    model1.Couverture = "BATIMENTS";
                    model1.Actualisation = nbreActualisation;
                    model1.Total = nbreBatimentTotal;
                    model1.NonTermine = nbreBatimentTotalNonTermine.ToString();
                    model1.Termine = nbreBatimentTotalTermine.ToString();
                    model1.NonVerifie = nbreBatimentTotalNonVerife.ToString();
                    model1.Verifie = nbreBatimentTotalVerifie.ToString();
                    couvertures.Add(model1);

                    //Nombre de logements Individuels
                    model1 = new CouvertureModel();
                    model1.Couverture = "LOGEMENTS INDIVIDUELS";
                    model1.Actualisation = 0;
                    model1.Total = nbreLogementIndTotal;
                    model1.NonTermine = nbreLogementIndTotalNonTermine.ToString();
                    model1.Termine = nbreLogementIndTotalTermine.ToString();
                    model1.NonVerifie = nbreLogementIndTotalNonVerifie.ToString();
                    model1.Verifie = nbreLogementIndTotalVerifie.ToString();
                    couvertures.Add(model1);
                    //
            
                    // Nombre de meanges
                    model1 = new CouvertureModel();
                    model1.Couverture = "MENAGES";
                    model1.Actualisation = 0;
                    model1.Total = nbreMenageTotal;
                    model1.Termine = nbreMenageTotalTermine.ToString();
                    model1.NonTermine = nbreMenageTotalNonTermine.ToString();
                    model1.NonVerifie = nbreMenageTotalNonVerife.ToString();
                    model1.Verifie = nbreMenageTotalVerifie.ToString();
                    couvertures.Add(model1);

                    //Nombre de personnes
                    model1 = new CouvertureModel();
                    model1.Couverture = "PERSONNES";
                    model1.Actualisation = 0;
                    model1.Total = nbrePersonnes;
                    model1.NonTermine = nbrePersonneNonTermine.ToString();
                    model1.NonVerifie = nbrePersonneNonVerifie.ToString();
                    model1.Termine = nbrePersonneTermine.ToString();
                    model1.Verifie = nbrePersonneVerifie.ToString();
                    couvertures.Add(model1);
                    //Nombre Ancien membre
                    model1 = new CouvertureModel();
                    model1.Couverture = "ANCIEN MEMBRE";
                    model1.Actualisation = 0;
                    model1.Total = nbreAncienMembreTotal;
                    model1.NonTermine = nbreAncienMembreTotalNonTermine.ToString();
                    //model1.NonVerifie = nbrePersonneNonVerifie.ToString();
                    model1.Termine = nbreAncienMembreTotalTermine.ToString();
                    //model1.Verifie = nbrePersonneVerifie.ToString();
                    couvertures.Add(model1);
                    dataGridCouverture.Dispatcher.BeginInvoke((Action)(() => dataGridCouverture.ItemsSource = couvertures));
                    #endregion

                    isTabCouvertureLoad = true;
                }
            }
            catch (Exception)
            {

            }
        }

        private void tabSocioDemographiques_GotFocus(object sender, RoutedEventArgs e)
        {
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            try
            {
                ths = new ThreadStart(() => createGraphicSocioControls());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception)
            {
                //log.Info("ERREUR:<>===================<>" + ex.Message);
            }
        }

        private void tabIndCouverture_GotFocus(object sender, RoutedEventArgs e)
        {
            if (t != null)
            {
                if (t.IsAlive)
                {
                    t.Abort();
                }
            }
            waitIndicator.Dispatcher.BeginInvoke((Action)(() => waitIndicator.DeferedVisibility = true));
            try
            {
                ths = new ThreadStart(() => createGraphicTabCouverture());
                t = new Thread(ths);
                t.Start();
            }
            catch (Exception)
            {
                //log.Info("ERREUR:<>===================<>" + ex.Message);
            }
        }

        public void createIndicateursPerformances(bool isForSde)
        {
            try
            {
                if (isForSde == true)
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    double nbreParJourBatiment = reader.getTotalBatRecenseParJourV();
                    double nbreParJourLogement = reader.getTotalLogeRecenseParJourV();
                    double nbreParJourMenage = reader.getTotalMenageRecenseParJourV();
                    double nbreParJourPersonnes = reader.getTotalIndRecenseParJourV();
                    List<KeyValue> list = new List<KeyValue>();
                    list.Add(new KeyValue(nbreParJourBatiment, "Nombre de questionnaires par jour de recensement"));
                    list.Add(new KeyValue(nbreParJourLogement, "Nombre de logements par jour de recensement"));
                    list.Add(new KeyValue(nbreParJourMenage, "Nombre de menages par jour de recensement"));
                    list.Add(new KeyValue(nbreParJourPersonnes, "Nombre d'individus par jour de recensement"));
                    dataGridIndPerformance.ItemsSource = list;
                    table_view.Dispatcher.BeginInvoke((Action)(() => table_view.BestFitColumns()));
                }
            }
            catch (Exception)
            {

            }
        }

        private void tabPagePerformance_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sdeSelected != null)
            {
                createIndicateursPerformances(true);
            }
        }

    }
}
