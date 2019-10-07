using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Grid;
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
        bool tabFlagCounterFocus = false;
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
            #region Ajout des donnees dans les tableaux
            reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotal(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;

            List<TableVerificationModel> verificationsNonReponsePartielle = Utilities.getVerificationNonReponsePartielle(MAIN_DATABASE_PATH, sde.SdeId);
            dtg_non_reponse_partielle.ItemsSource = verificationsNonReponsePartielle;

            List<VerificationFlag> verficationFlags = Utilities.getVerificationNonReponseParVariable(MAIN_DATABASE_PATH, sde.SdeId);
            #endregion

            #region Ajout des images dans les nodes
            //Expand the node in level 1
            foreach (TreeListNode node in treeListView1.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Batiman adding image Icon
                    foreach (TreeListNode batimanNode in childNode.Nodes)
                    {
                        batimanNode.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                    }
                }
            }
            //
            foreach (TreeListNode node in treeListView_partielle.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node logement
                    foreach (TreeListNode logementChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = logementChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            logementChild.Image = new BitmapImage(new Uri(@"/images/logement.png", UriKind.Relative));
                        }
                        //a l'interieur du node Logement
                        foreach (TreeListNode menageChild in logementChild.Nodes)
                        {
                            TableVerificationModel niveau4 = menageChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                menageChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                            }
                        }
                    }
                    //Node Menage
                    foreach (TreeListNode menageChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = menageChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "5")
                        {
                            menageChild.Image = new BitmapImage(new Uri(@"/images/menage.png", UriKind.Relative));
                        }
                        if (niveau3.Niveau == "6")
                        {
                            menageChild.Image = new BitmapImage(new Uri(@"/images/individu1.png", UriKind.Relative));
                        }
                    }

                }
            }
            #endregion
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
            List<TableVerificationModel> verificationsNonReponseTotal = Utilities.getVerificatoinNonReponseTotalForAllSdes(MAIN_DATABASE_PATH);
            List<TableVerificationModel> verificationsNonReponsePartielle = Utilities.getVerificationNonReponsePartielleForAllSdes(MAIN_DATABASE_PATH);
            dtg_non_reponse_totale.ItemsSource = verificationsNonReponseTotal;
            dtg_non_reponse_partielle.ItemsSource = verificationsNonReponsePartielle;
            //Expand the node in level 2
            foreach (TreeListNode node in treeListView1.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Sde
                    foreach (TreeListNode sdeChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = sdeChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            sdeChild.Image = new BitmapImage(new Uri(@"/images/database.png", UriKind.Relative));
                        }
                        //Node batiment
                        foreach (TreeListNode batimanChild in sdeChild.Nodes)
                        {
                            TableVerificationModel niveau4 = batimanChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                batimanChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                            }
                        }
                    }
                }
            }
            //treeListView_partielle
            //
            foreach (TreeListNode node in treeListView_partielle.Nodes)
            {
                node.IsExpanded = true;
                node.Image = new BitmapImage(new Uri(@"/images/report3.png", UriKind.Relative));
                foreach (TreeListNode childNode in node.Nodes)
                {
                    TableVerificationModel model = childNode.Content as TableVerificationModel;
                    if (model.Niveau == "2")
                    {
                        childNode.IsExpanded = true;
                        childNode.Image = new BitmapImage(new Uri(@"/images/malrempli.png", UriKind.Relative));
                    }
                    //Node Sde
                    foreach (TreeListNode sdeChild in childNode.Nodes)
                    {
                        TableVerificationModel niveau3 = sdeChild.Content as TableVerificationModel;
                        if (niveau3.Niveau == "3")
                        {
                            sdeChild.Image = new BitmapImage(new Uri(@"/images/database.png", UriKind.Relative));
                        }
                        //Node batiment
                        foreach (TreeListNode logementChild in sdeChild.Nodes)
                        {
                            TableVerificationModel niveau4 = logementChild.Content as TableVerificationModel;
                            if (niveau4.Niveau == "4")
                            {
                                logementChild.Image = new BitmapImage(new Uri(@"/images/menu-image.png", UriKind.Relative));
                            }
                            foreach (TreeListNode batimanChild in logementChild.Nodes)
                            {
                                TableVerificationModel niveau5 = batimanChild.Content as TableVerificationModel;
                                if (niveau5.Niveau == "5")
                                {
                                    batimanChild.Image = new BitmapImage(new Uri(@"/images/home.png", UriKind.Relative));
                                }
                            }
                        }
                    }
                }
            }
            //
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

        private void dtg_non_reponse_partielle_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Color")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Niveau")
                e.Column.Visible = false;
            if (e.Column.FieldName == "Image")
                e.Column.Visible = false;
        }
        private void dtg_non_reponse_totale_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
                {
                    if (e.Column.FieldName == "Color")
                        e.Column.Visible = false;
                    if (e.Column.FieldName == "Niveau")
                        e.Column.Visible = false;
                    if (e.Column.FieldName == "Image")
                        e.Column.Visible = false;
                }
        private void chartControlCompteur_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);
            ChartHitInfo info = chartControlCompteur.CalcHitInfo(point);
            if (info.Series != null)
            {
                MessageBox.Show("" + info.Series.Name);
            }

        }
        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);
            ChartHitInfo info = chartControlCompteur.CalcHitInfo(point);
            if (info.Series != null)
            {
                MessageBox.Show("" + info.Series.Name);
            }
        }

        private void gridFlag_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (e.Column.FieldName == "Individu")
                e.Column.Visible = false;

        }
        private void treeListView2_RowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            TreeListView listView = sender as TreeListView;
            if (listView != null)
            {
                TreeListNode node = listView.FocusedNode;
                if (node != null)
                {
                    if (node.HasChildren == false)
                    {
                        RapportFlagModel flag = node.Content as RapportFlagModel;
                        frm_reponses frm = new frm_reponses(flag.Individu, flag.Individu.sdeId);
                        main.listBox_sde.SelectedIndex = -1;
                        Utilities.showControl(frm, main.grd_details);
                    }
                }
            }

        }
        private void tabFlagMenage_GotFocus(object sender, RoutedEventArgs e)
        {
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            initializeChartControls();
            int nbreTotal = 0;
            try
            {
                reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                nbreTotal = reader.GetAllMenagesModel().Count;
                Flag menage_1_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_1_Personne());
                Flag menage_2_3_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_2_3_Personnes());
                Flag menage_4_5_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_4_5_Personnes());
                Flag menage_6_Personne = reader.compteurFlagParMenages(reader.GetAllMenage_6_Personnes());
                Flag all_menage = reader.compteurFlagParMenages(reader.GetAllMenagesModel());

                //Creation des barres
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_Aucun, nbreTotal)))));
                aucunFlag.Dispatcher.BeginInvoke((Action)(() =>
                            aucunFlag.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_Aucun, nbreTotal)))));

                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_1_4, nbreTotal)))));
                flag_1_4.Dispatcher.BeginInvoke((Action)(() =>
                            flag_1_4.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_1_4, nbreTotal)))));

                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_5_14, nbreTotal)))));
                flag_5_14.Dispatcher.BeginInvoke((Action)(() =>
                            flag_5_14.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_5_14, nbreTotal)))));

                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_15_26, nbreTotal)))));
                flag_15_26.Dispatcher.BeginInvoke((Action)(() =>
                            flag_15_26.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_15_26, nbreTotal)))));

                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_27_47, nbreTotal)))));
                flag_27_47.Dispatcher.BeginInvoke((Action)(() =>
                            flag_27_47.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_27_47, nbreTotal)))));

                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_48_70, nbreTotal)))));

                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_48_70, nbreTotal)))));
                flag_48_70.Dispatcher.BeginInvoke((Action)(() =>
                            flag_48_70.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_48_70, nbreTotal)))));

                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Menage Unipersonnel (1)", Utilities.getPourcentage(menage_1_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de deux (2) à trois (3) individus", Utilities.getPourcentage(menage_2_3_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de quatre (4) à cinq (5) individus", Utilities.getPourcentage(menage_4_5_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Ménage de six (6) individus ou plus", Utilities.getPourcentage(menage_6_Personne.Flag_71_130, nbreTotal)))));
                flag_71_130.Dispatcher.BeginInvoke((Action)(() =>
                            flag_71_130.Points.Add(new SeriesPoint("Tous les menages", Utilities.getPourcentage(all_menage.Flag_71_130, nbreTotal)))));


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur:" + ex.Message, Constant.WINDOW_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }

        private void tabCompteurFlag_GotFocus(object sender, RoutedEventArgs e)
        {
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = true));
            tabFlagCounterFocus = true;
            initializeChartControls();
            try
            {
                //ouvrir le decorateur
                //decoratorTab.Dispatcher.BeginInvoke((Action)(() => decoratorTab.IsSplashScreenShown = true));
                IConfigurationService configuration = new ConfigurationService();
                int nbreTotal = 0;
                Flag flagPopulationParDistrict = new Flag();
                Flag flagAgeDateNaissanceParDistrict = new Flag();
                Flag flagFeconditeParDistrict = new Flag();
                Flag flagEmploiParDistrict = new Flag();
                if (IsAllDistrict == true)
                {
                    foreach (SdeModel sde in configuration.searchAllSdes())
                    {
                        reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sde.SdeId));
                        List<IndividuModel> individus = reader.GetAllIndividusModel();
                        nbreTotal += individus.Count;
                        Flag flagPopulation = reader.CountTotalFlag(individus);
                        Flag flagAgeDateNaissance = reader.Count2FlagAgeDateNaissance();
                        Flag flagFecondite = reader.CountFlagFecondite();
                        //Flag flagEmploi = reader.CountFlagEmploi();
                        flagPopulationParDistrict.Flag0 += flagPopulation.Flag0;
                        flagPopulationParDistrict.Flag1 += flagPopulation.Flag1;
                        flagPopulationParDistrict.Flag2 += flagPopulation.Flag2;
                        flagPopulationParDistrict.Flag3 += flagPopulation.Flag3;
                        flagPopulationParDistrict.Flag4 += flagPopulation.Flag4;
                        flagPopulationParDistrict.Flag5 += flagPopulation.Flag5;
                        flagPopulationParDistrict.Flag6 += flagPopulation.Flag6;
                        flagPopulationParDistrict.Flag7 += flagPopulation.Flag7;
                        flagPopulationParDistrict.Flag8 += flagPopulation.Flag8;
                        flagPopulationParDistrict.Flag9 += flagPopulation.Flag9;
                        flagPopulationParDistrict.Flag10 += flagPopulation.Flag10;
                        flagPopulationParDistrict.Flag11 += flagPopulation.Flag11;
                        flagPopulationParDistrict.Flag12 += flagPopulation.Flag12;

                        flagAgeDateNaissanceParDistrict.Flag0 += flagAgeDateNaissance.Flag0;
                        flagAgeDateNaissanceParDistrict.Flag1 += flagAgeDateNaissance.Flag1;
                        flagAgeDateNaissanceParDistrict.Flag2 += flagAgeDateNaissance.Flag2;
                        flagAgeDateNaissanceParDistrict.Flag3 += flagAgeDateNaissance.Flag3;
                        flagAgeDateNaissanceParDistrict.Flag4 += flagAgeDateNaissance.Flag4;
                        flagAgeDateNaissanceParDistrict.Flag5 += flagAgeDateNaissance.Flag5;
                        flagAgeDateNaissanceParDistrict.Flag6 += flagAgeDateNaissance.Flag6;

                        flagFeconditeParDistrict.Flag0 += flagFecondite.Flag0;
                        flagFeconditeParDistrict.Flag1 += flagFecondite.Flag1;
                        flagFeconditeParDistrict.Flag2 += flagFecondite.Flag2;
                        flagFeconditeParDistrict.Flag3 += flagFecondite.Flag3;
                        flagFeconditeParDistrict.Flag4 += flagFecondite.Flag4;
                        flagFeconditeParDistrict.Flag5 += flagFecondite.Flag5;
                        flagFeconditeParDistrict.Flag6 += flagFecondite.Flag6;

                        //flagEmploiParDistrict.Flag0 += flagEmploi.Flag0;
                        //flagEmploiParDistrict.Flag1 += flagEmploi.Flag1;
                        //flagEmploiParDistrict.Flag2 += flagEmploi.Flag2;
                        //flagEmploiParDistrict.Flag3 += flagEmploi.Flag3;
                        //flagEmploiParDistrict.Flag4 += flagEmploi.Flag4;
                        //flagEmploiParDistrict.Flag5 += flagEmploi.Flag5;
                        //flagEmploiParDistrict.Flag6 += flagEmploi.Flag6;
                    }
                }
                else
                {
                    reader = new SqliteReader(Utilities.getConnectionString(MAIN_DATABASE_PATH, sdeSelected.SdeId));
                    List<IndividuModel> individus = reader.GetAllIndividusModel();
                    nbreTotal += individus.Count;
                    flagPopulationParDistrict = reader.CountTotalFlag(individus);
                    flagAgeDateNaissanceParDistrict = reader.Count2FlagAgeDateNaissance();
                    flagFeconditeParDistrict = reader.CountFlagFecondite();
                    //flagEmploiParDistrict = reader.CountFlagEmploi();
                    //Fill the grid
                    gridFlag.Dispatcher.BeginInvoke((Action)(() => gridFlag.ItemsSource = Utilities.getListOfIndividuWithFlag(MAIN_DATABASE_PATH, sdeSelected.SdeId)));
                }

                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag0.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag0, nbreTotal)))));
                chartFlag0.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag0.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag0, nbreTotal)))));

                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag1.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag1, nbreTotal)))));
                chartFlag1.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag1.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag1, nbreTotal)))));

                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag2.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag2, nbreTotal)))));
                chartFlag2.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag2.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag2, nbreTotal)))));

                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag3.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag3, nbreTotal)))));
                chartFlag3.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag3.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag3, nbreTotal)))));

                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag4.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag4, nbreTotal)))));
                chartFlag4.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag4.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag4, nbreTotal)))));

                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag5.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag5, nbreTotal)))));
                chartFlag5.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag5.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag5, nbreTotal)))));

                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                            chartFlag6.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Population Totale (2 Flags au total)", Utilities.getPourcentage(flagAgeDateNaissanceParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Femmes de 13 ans et plus", Utilities.getPourcentage(flagFeconditeParDistrict.Flag6, nbreTotal)))));
                chartFlag6.Dispatcher.BeginInvoke((Action)(() =>
                             chartFlag6.Points.Add(new SeriesPoint("Population de 10 ans et plus avec un emploi", Utilities.getPourcentage(flagEmploiParDistrict.Flag6, nbreTotal)))));
                chartFlag7.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag7.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag7, nbreTotal)))));
                chartFlag8.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag8.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag8, nbreTotal)))));
                chartFlag9.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag9.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag9, nbreTotal)))));
                chartFlag10.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag10.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag10, nbreTotal)))));
                chartFlag11.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag11.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag11, nbreTotal)))));
                chartFlag12.Dispatcher.BeginInvoke((Action)(() =>
                           chartFlag12.Points.Add(new SeriesPoint("Population Totale (13 Flags au total)", Utilities.getPourcentage(flagPopulationParDistrict.Flag12, nbreTotal)))));


            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
            //fermer le decorateur
            decorator.Dispatcher.BeginInvoke((Action)(() => decorator.IsSplashScreenShown = false));
        }

    }
}
