using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public class Utilities
    {
        #region ConnectionString SQLITE Database
        /// <summary>
        /// get ConnectionString SQLITE Database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sdeID"></param>
        /// <returns></returns>
        public static string getConnectionString(string path, string sdeID)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/entities.EpcEntities.csdl|res://*/entities.EpcEntities.ssdl|res://*/entities.EpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + sdeID + ".SQLITE"
                }.ConnectionString


            }.ConnectionString;
            return connectionString;

        }
        public static string getConnectionString2(string path, string fileName)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/entities.EpcEntities.csdl|res://*/entities.EpcEntities.ssdl|res://*/entities.EpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + fileName
                }.ConnectionString

            }.ConnectionString;
            return connectionString;
        }

        public static string getConnectionString(string path)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = @"res://*/entities.SupEpcEntities.csdl|res://*/entities.SupEpcEntities.ssdl|res://*/entities.SupEpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + Constant.SUPDATABASE_FILE_NAME,
                }.ConnectionString
            }.ConnectionString;
            return connectionString;
        }

        public static void killProcess(Process[] procs)
        {
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

   #endregion

        #region UTILITIES
        public static SdeModel getSdeInformation(string sdeId)
        {
            SdeModel sde = new SdeModel();
            try
            {
                if (Utils.IsNotNull(sdeId))
                {
                    sde.DeptId = sdeId.Substring(0, 2);
                    sde.ComId = sdeId.Substring(0, 4);
                    sde.VqseId = sdeId.Substring(0, 7);
                }
             }
            catch (Exception)
            {
            }
            return sde;
        }

        public static SdeModel getSdeInformationForTabletConf(string sdeId)
        {
            SdeModel sde = new SdeModel();
            try
            {
                if (Utils.IsNotNull(sdeId))
                {
                    sde.DeptId = "0" + sdeId.Substring(0, 1);
                    sde.ComId = "0" + sdeId.Substring(0, 3);
                    sde.VqseId = "0" + sdeId.Substring(0, 6);
                    if (sdeId.Substring(4, 2) == "90")
                    {
                        sde.Zone = ""+(int)Constant.Zone.SDE_ZONE_URBAINE;
                    }
                    else
                        sde.Zone = "" + (int)Constant.Zone.SDE_ZONE_RURAL;
                    return sde;
                }
             }
            catch (Exception)
            {

            }
            return null;
        }

        public static string getSdeFormatWithDistrict(string sdeId)
        {
            if (sdeId.Length <= 14)
            {
                string[] splits = sdeId.Split('-');
                string convertSde = "0" + splits[0] + "-" + splits[1] + "-" + splits[2] + "-" + splits[3];
                return convertSde;
            }
            else
            {
                return sdeId;
            }
         }

        public static void showControl(Control control, Grid grid)
        {
            control.Dispatcher.BeginInvoke((Action)(() => control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch));
            control.Dispatcher.BeginInvoke((Action)(() => control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Clear()));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Add(control)));
        }
        public static bool isCategorieExist(List<string> list, string categorie)
        {
            foreach (string cat in list)
            {
                if (cat == categorie)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Retourne le score en fonction de la question
        /// </summary>
        /// <param name="score"></param>
        /// <param name="reponse"></param>
        /// <param name="type_question"></param>
        /// <returns></returns>
        public static int getScore(int score, ReponseModel reponse, int type_question)
        {
            int note = 0;
            if (reponse != null)
            {
                if (type_question == (int)Constant.RapportTypeQuestion.question_4)
                {
                    if (Convert.ToInt32(reponse.CodeReponse) == 1)
                        note = score + 2;
                    if (Convert.ToInt32(reponse.CodeReponse) == 2)
                        note = score + 0;
                    if (Convert.ToInt32(reponse.CodeReponse) == 3)
                        note = score + 1;
                    if (Convert.ToInt32(reponse.CodeReponse) == 4)
                        note = score - 1;
                }
                else
                {
                    if (Convert.ToInt32(reponse.CodeReponse) == 1)
                        note = score + 1;
                    if (Convert.ToInt32(reponse.CodeReponse) == 2)
                        note = score;
                    if (Convert.ToInt32(reponse.CodeReponse) == 3)
                        note = score + 2;
                }
                return note;
            }
            return 0;
        }
        #region THICKNESS...
        public static Thickness getThickness(Thickness t)
        {
            t.Left = 10;
            t.Top = t.Top + 35;
            t.Right = 0;
            t.Bottom = 0;
            return t;
        }

        public static Thickness getRightThickness(Thickness t)
        {
            t.Left = t.Left + 80;
            t.Top = t.Top;
            t.Right = 0;
            t.Bottom = 0;
            return t;
        }
        #endregion

        public static bool isIndividuExist(List<IndividuModel> individus, IndividuModel ind)
        {
            foreach (IndividuModel individu in individus)
            {
                if (individu.individuId == ind.individuId &&
                    individu.batimentId == ind.batimentId &&
                    individu.logeId == ind.logeId &&
                    individu.menageId == ind.menageId &&
                    individu.sdeId == ind.sdeId)
                {
                    return true;
                }
            }
            return false;
        }

        //Retourne le pourcentage entre 2 nombres
        /// <summary>
        /// Retourne le pourcentage de 2 nombres
        /// </summary>
        /// <param name="nbreACalculer"></param>
        /// <param name="nbreTotal"></param>
        /// <returns></returns>
        public static double getPourcentage(double nbreACalculer, int nbreTotal)
        {
            double percent = 0;
            try
            {
                if (nbreACalculer == 0 || nbreTotal == 0)
                {
                    return percent;
                }
                else
                {
                    double number = Math.Round(((nbreACalculer * 100) / nbreTotal), 2);
                    percent = Convert.ToDouble(number);
                }

            }
            catch (Exception)
            {

            }
            return percent;
        }

        #region VERIFICATION
        public static List<RapportFlagModel> getListOfIndividuWithFlag(string path, string sdeId)
        {
            List<RapportFlagModel> rapports = new List<RapportFlagModel>();
            RapportFlagModel rpt = null;
            int lastParentId = 0;
            int lastId = 0;
            int firstParentId = 0;
            try
            {
                ISqliteReader reader = new SqliteReader(getConnectionString(path, sdeId));
                //Ajout du premier noeud
                rpt = new RapportFlagModel();
                rpt.ID = 1;
                rpt.ParentID = 0;
                rpt.Type = "Ensemble d'individus possedant des flags";
                firstParentId = rpt.ParentID;
                lastId = rpt.ID;
                lastParentId = rpt.ParentID;
                rapports.Add(rpt);
                Flag individusWith2Flags = reader.Count2FlagAgeDateNaissance();

                //Ajout flag pour la population totale
                rpt = new RapportFlagModel();
                rpt.ID = lastId + 1;
                rpt.ParentID = lastParentId;
                rpt.Type = "1- Population Totale (2 Flags au total/Date de Naissance)";
                rpt.Total = individusWith2Flags.Individus.Count;
                rapports.Add(rpt);
                lastId = rpt.ID;
                lastParentId = rpt.ID;
                foreach (IndividuModel ind in individusWith2Flags.Individus)
                {
                    rpt = new RapportFlagModel();
                    rpt.ID = lastId + 1;
                    rpt.Type = reader.locateIndividu(ind);
                    rpt.Individu = ind;
                    rpt.ParentID = lastParentId;
                    lastId = rpt.ID;
                    rapports.Add(rpt);
                }
                //
                //Ajout population de 13 flags et plus
                rpt = new RapportFlagModel();
                rpt.ID = lastId + 1;
                rpt.ParentID = firstParentId;
                rpt.Type = "2- Population Totale (13 Flags au total)";
                lastId = rpt.ID;
                lastParentId = rpt.ID;
                individusWith2Flags = new Flag();
                individusWith2Flags = reader.CountTotalFlag(reader.GetAllIndividusModel());
                rpt.Total = individusWith2Flags.Individus.Count;
                rapports.Add(rpt);
                //Ajout des individus se trouvant dans cette categorie
                foreach (IndividuModel ind in individusWith2Flags.Individus)
                {
                    rpt = new RapportFlagModel();
                    rpt.ID = lastId + 1;
                    rpt.Individu = ind;
                    rpt.Type = reader.locateIndividu(ind);
                    rpt.ParentID = lastParentId;
                    lastId = rpt.ID;
                    rapports.Add(rpt);
                }
                //Ajout flag fecondite
                rpt = new RapportFlagModel();
                rpt.ID = lastId + 1;
                rpt.ParentID = firstParentId;
                rpt.Type = "3- Femmes de 13 ans et plus";
                lastId = rpt.ID;
                lastParentId = rpt.ID;
                individusWith2Flags = new Flag();
                individusWith2Flags = reader.CountFlagFecondite();
                rpt.Total = individusWith2Flags.Individus.Count;
                rapports.Add(rpt);
                //Ajout des individus se trouvant dans cette categorie
                foreach (IndividuModel ind in individusWith2Flags.Individus)
                {
                    rpt = new RapportFlagModel();
                    rpt.ID = lastId + 1;
                    rpt.Individu = ind;
                    rpt.Type = reader.locateIndividu(ind);
                    rpt.ParentID = lastParentId;
                    lastId = rpt.ID;
                    rapports.Add(rpt);
                }
                ////Ajout flag emploi
                //rpt = new RapportFlagModel();
                //rpt.ID = lastId + 1;
                //rpt.ParentID = firstParentId;
                //rpt.Type = "4- Population de 10 ans et plus avec un emploi";
                //lastId = rpt.ID;
                //lastParentId = rpt.ID;
                //individusWith2Flags = new Flag();
                //individusWith2Flags = reader.CountFlagEmploi();
                //rpt.Total = individusWith2Flags.Individus.Count;
                //rapports.Add(rpt);
                ////Ajout des individus se trouvant dans cette categorie
                //foreach (IndividuModel ind in individusWith2Flags.Individus)
                //{
                //    rpt = new RapportFlagModel();
                //    rpt.ID = lastId + 1;
                //    rpt.Individu = ind;
                //    rpt.Type = reader.locateIndividu(ind);
                //    rpt.ParentID = lastParentId;
                //    lastId = rpt.ID;
                //    rapports.Add(rpt);
                //}

            }
            catch (Exception ex)
            {

            }
            return rapports;
        }

        public static List<TableVerificationModel> getVerificatoinNonReponseTotal(string path, string sdeId)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            SqliteDataReaderService service = new SqliteDataReaderService(getConnectionString(path, sdeId));
            List<BatimentModel> batiments = new List<BatimentModel>();
            int lastParentId = 0;
            int lastId = 0;
            int firstParentId = 0;
            int totalBatiments = service.getAllBatiments().Count();
            int nbreBatimentPasRempli = service.sr.GetAllBatimentsInobservables().Count() + service.sr.GetAllBatimentsWithAtLeastOneBlankObject().Count();
            if (nbreBatimentPasRempli != 0)
            {
                TableVerificationModel report = new TableVerificationModel();
                report.Type = "I.-STATUT DE REMPLISSAGE INITIAL (AR).";
                report.ID = 1;
                report.ParentID = 0;
                report.Indicateur = "Uniquement les questionnaires en premier passage.";
                report.Niveau = "1";
                report.Total = "" + totalBatiments;
                rapports.Add(report);
                lastId = report.ID;
                firstParentId = report.ID;

                report = new TableVerificationModel();
                report.Type = "1-NOMBRE QUESTIONNAIRES PAS DU TOUT REMPLIS ";
                report.Indicateur = "";
                report.Total = "" + nbreBatimentPasRempli;
                report.ID = lastId + 1;
                report.ParentID = lastId;
                report.Niveau = "2";
                report.Taux = "" + Utilities.getPourcentage(nbreBatimentPasRempli, totalBatiments) + "%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                batiments = service.sr.GetAllBatimentsInobservables().ToList();
                foreach (BatimentModel bat in service.sr.GetAllBatimentsWithAtLeastOneBlankObject())
                {
                    batiments.Add(bat);
                }
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }
                //Branche pour les batiments inobservables
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "2- BÂTIMENTS INOBSERVABLES / TAUX DE NON-RÉPONSE TOTALE NRT 1 (%)";
                report.ID = lastId + 1;
                report.ParentID = firstParentId;
                report.Niveau = "2";
                report.Indicateur = "BÂTIMENTS INOBSERVABLES (B1=5)";
                batiments = new List<BatimentModel>();
                batiments = service.sr.GetAllBatimentsInobservables().ToList();
                report.Total = "" + batiments.Count;
                report.Taux = "" + getPourcentage(batiments.Count(), totalBatiments) + "%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }
                //Branche pour les batiments ayant au moins un logement pas rempli
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "3-TAUX DE NON-RÉPONSE TOTALE (%)";
                report.ID = lastId + 1;
                report.Niveau = "2";
                report.ParentID = firstParentId;
                report.Indicateur = "Objet Logement pas rempli du tout";
                batiments = new List<BatimentModel>();
                batiments = service.sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList();
                report.Total = "" + batiments.Count;
                report.Taux = "" + getPourcentage(batiments.Count(), totalBatiments) + "%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                foreach (BatimentModel batiment in batiments)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = lastParentId;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }
                //Bracnhe Distribution des questonnaires selon la raison indiquee dans le rapport de l'agent recenseuur
                List<RapportArModel> rapportsAgentsRecenseurs = new List<RapportArModel>();
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "4-DISTRIBUTION DES QUESTIONNAIRES EN NRT2 SELON LA RAISON (%)";
                report.ID = lastId + 1;
                report.ParentID = firstParentId;
                report.Niveau = "2";
                report.Indicateur = "";
                rapportsAgentsRecenseurs = service.sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList();
                report.Total = "" + rapportsAgentsRecenseurs.Count;
                report.Taux = "" + getPourcentage(rapportsAgentsRecenseurs.Count, totalBatiments) + "%";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                //Ensemble des batiments inobservables et au moins un objet vide
                batiments = service.sr.GetAllBatimentsInobservables().ToList();
                foreach (BatimentModel bat in service.sr.GetAllBatimentsWithAtLeastOneBlankObject())
                {
                    batiments.Add(bat);
                }
                //Nombre de refus total
                int nonbreRefusTotal = 0;
                List<BatimentModel> batimentsEnRefus = new List<BatimentModel>();
                string raisonRefus = "";

                //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<BatimentModel> batimentsEnIndAvecRendezVous = new List<BatimentModel>();
                string raisonAvecRendezVous = "";

                //Indisponibilite
                int nbreIndisponible = 0;
                List<BatimentModel> batimentsIndisponible = new List<BatimentModel>();
                string raisonIndisponible = "";

                //Autre
                int nbreAutre = 0;
                List<BatimentModel> batimentsEnAutre = new List<BatimentModel>();
                string raisonAutre = "";

                foreach (BatimentModel batiment in batiments)
                {
                    //On recherche les rapports AR par batiment et on fait le total
                    List<RapportArModel> rars = service.sr.GetAllRptAgentRecenseurByBatiment(batiment.batimentId);
                    if (rars != null)
                    {
                        foreach (RapportArModel rar in rars)
                        {
                            //Indisponibilité avec rendez-vous raison=17
                            if (rar.RaisonActionId == 17)
                            {
                                NbreIndAvecRendezVous++;
                                batimentsEnIndAvecRendezVous.Add(batiment);
                                raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Refus
                            if (rar.RaisonActionId == 16)
                            {
                                nonbreRefusTotal++;
                                batimentsEnRefus.Add(batiment);
                                raisonRefus = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Indisponibilité
                            if (rar.RaisonActionId == 18)
                            {
                                nbreIndisponible++;
                                batimentsIndisponible.Add(batiment);
                                raisonIndisponible = Constant.getRaison(rar.RaisonActionId).Value;
                            }
                            //Autre
                            if (rar.RaisonActionId == 19)
                            {
                                nbreAutre++;
                                batimentsEnAutre.Add(batiment);
                                raisonAutre = rar.AutreRaisonAction;
                            }
                        }

                    }
                }
                //On definit les parents parents des bracnhes refus, indisponible etc...
                int refusParent = 0;
                int indAvecRDParent = 0;
                int indParent = 0;
                int autreParent = 0;

                //On ajoute la branche REFUS avec le total
                report = new TableVerificationModel();
                report.Type = "Refus";
                report.ID = lastId + 1;
                refusParent = report.ID;
                report.Indicateur = raisonRefus;
                report.ParentID = lastParentId;
                report.Total = "" + nonbreRefusTotal;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnRefus)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = refusParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID + 1;
                }

                //On ajoute la branche Indisponibilité avec rendez-vous
                report = new TableVerificationModel();
                report.Type = "Indisponibilité avec rendez-vous";
                report.ID = lastId + 1;
                indAvecRDParent = report.ID;
                report.Indicateur = raisonAvecRendezVous;
                report.ParentID = lastParentId;
                report.Total = "" + NbreIndAvecRendezVous;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = indAvecRDParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }

                //On ajoute la branche Indisponibilité
                report = new TableVerificationModel();
                report.Type = "Indisponibilité";
                report.ID = lastId + 1;
                indParent = report.ID;
                report.Indicateur = raisonIndisponible;
                report.ParentID = lastParentId;
                report.Total = "" + nbreIndisponible;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsIndisponible)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = indParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }
                //On ajoute la branche Autre
                report = new TableVerificationModel();
                report.Type = "Autre";
                report.ID = lastId + 1;
                autreParent = report.ID;
                report.Indicateur = raisonAutre;
                report.ParentID = lastParentId;
                report.Total = "" + nbreAutre;
                rapports.Add(report);
                lastId = report.ID;
                //On ajoute les batiments dans la branche
                foreach (BatimentModel batiment in batimentsEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                    report.ID = lastId + 1;
                    report.ParentID = autreParent;
                    report.Niveau = "3";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            return rapports;
        }
        public static List<TableVerificationModel> getVerificatoinNonReponseTotalForAllSdes(string path)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            IConfigurationService configurationService = new ConfigurationService();
            List<SdeModel> listOfSdes = configurationService.searchAllSdes();

            #region CONTRUCTION DU RAPPORT
            List<BatimentModel> batiments = new List<BatimentModel>();
            int lastParentId = 0;
            int lastId = 0;
            int firstParentId = 0;
            int nbreBatimentPasRempli = 0;
            int totalBatiments = 0;
            int totalBatimentParSde = 0;
            SqliteDataReaderService sqliteService = null;

            #region BRANCHE BATIMENTS MAL REMPLI
            //
            //Somme des batiments mal rempli pour le district du superviseur
            foreach (SdeModel sde in listOfSdes)
            {
                sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                totalBatiments += sqliteService.getAllBatiments().Count();
                nbreBatimentPasRempli = nbreBatimentPasRempli + (sqliteService.sr.GetAllBatimentsInobservables().Count() + sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject().Count());
            }
            if (nbreBatimentPasRempli != 0)
            {
                TableVerificationModel report = new TableVerificationModel();
                report.Type = "I.-STATUT DE REMPLISSAGE INITIAL (AR).";
                report.ID = 1;
                report.ParentID = 0;
                report.Indicateur = "Uniquement les questionnaires en premier passage.";
                report.Niveau = "1";
                report.Total = "" + totalBatiments;
                rapports.Add(report);
                lastId = report.ID;
                firstParentId = report.ID;

                report = new TableVerificationModel();
                int batimentPasRempliId = 0;
                report.Type = "1-NOMBRE QUESTIONNAIRES PAS DU TOUT REMPLIS ";
                report.Indicateur = "";
                report.Total = "" + nbreBatimentPasRempli;
                report.Taux = "" + getPourcentage(nbreBatimentPasRempli, totalBatiments) + "%";
                report.ID = lastId + 1;
                report.ParentID = lastId;
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                batimentPasRempliId = report.ID;
                lastParentId = report.ID;
                //
                //Compilation des batiments sur l'ensemble des Sdes
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    totalBatimentParSde += sqliteService.getAllBatiments().Count();
                    batiments = sqliteService.sr.GetAllBatimentsInobservables().ToList();
                    foreach (BatimentModel bat in sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    {
                        batiments.Add(bat);
                    }
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    //report.Image = "/images/database.png";
                    report.ID = lastId + 1;
                    report.Niveau = "3";
                    report.ParentID = batimentPasRempliId;
                    report.Total = "" + batiments.Count();
                    report.Taux = "" + getPourcentage(batiments.Count(), totalBatimentParSde) + "%";
                    lastId = report.ID;
                    lastParentId = report.ID;
                    rapports.Add(report);
                    //
                    //On ajoute les batiments dans La SDE 
                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
            #endregion

                #region BRANCHE BATIMENTS INOBSERVABLES
                //Branche pour les batiments inobservables
                int nbreBatimentInobservables = 0;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentInobservables = nbreBatimentInobservables + sqliteService.sr.GetAllBatimentsInobservables().ToList().Count();
                }
                lastParentId = lastId;
                int batimentInobservableId = 0;
                report = new TableVerificationModel();
                report.Type = "2- BÂTIMENTS INOBSERVABLES / TAUX DE NON-RÉPONSE TOTALE NRT 1 (%)";
                report.ID = lastId + 1;
                report.ParentID = firstParentId;
                report.Indicateur = "BÂTIMENTS INOBSERVABLES (B1=5)";
                report.Total = "" + nbreBatimentInobservables;
                report.Taux = "" + getPourcentage(nbreBatimentInobservables, totalBatiments) + "%";
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                batimentInobservableId = report.ID;
                lastParentId = report.ID;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    batiments = sqliteService.sr.GetAllBatimentsInobservables().ToList();
                    totalBatimentParSde += sqliteService.getAllBatiments().Count();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    report.ParentID = batimentInobservableId;
                    report.Total = "" + batiments.Count();
                    report.Taux = "" + Utilities.getPourcentage(batiments.Count(), totalBatimentParSde) + "%";
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                    //
                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
                #endregion

                #region BRANCHE BATIMENT AYANT UN OBJET VIDE
                //
                //Branche pour les batiments ayant au moins un logement pas rempli
                lastParentId = lastId;
                int nbreBatimentObjetVide = 0;
                int BatimentObjetVideId = 0;
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentObjetVide = nbreBatimentObjetVide + sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList().Count();
                }
                report = new TableVerificationModel();
                report.Type = "3-TAUX DE NON-RÉPONSE TOTALE (%)";
                report.ID = lastId + 1;
                BatimentObjetVideId = report.ID;
                report.ParentID = firstParentId;
                report.Indicateur = "Objet Logement pas rempli du tout";
                report.Total = "" + nbreBatimentObjetVide;
                report.Taux = "" + getPourcentage(nbreBatimentObjetVide, totalBatiments) + "%";
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;
                //
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    batiments = sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject().ToList();
                    totalBatimentParSde += sqliteService.getAllBatiments().Count();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    report.ParentID = BatimentObjetVideId;
                    report.Total = "" + batiments.Count();
                    report.Taux = "" + Utilities.getPourcentage(batiments.Count(), totalBatimentParSde) + "%";
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                    //

                    foreach (BatimentModel batiment in batiments)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = lastParentId;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                }
                #endregion

                #region BRANCHE BATIMENT AVEC RAISON
                //
                //Bracnhe Distribution des questonnaires selon la raison indiquee dans le rapport de l'agent recenseuur
                List<RapportArModel> rapportsAgentsRecenseurs = new List<RapportArModel>();
                int nbreBatimentInobservableEtVide = 0;
                int sdeParentId = 0;
                int BatimentInobservableEtVideParentId = 0;
                batiments = new List<BatimentModel>();
                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    nbreBatimentInobservableEtVide = nbreBatimentInobservableEtVide + sqliteService.sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList().Count();
                    ////Ensemble des batiments inobservables et au moins un objet vide
                    //batiments = sqliteService.sr.GetAllBatimentsInobservables().ToList();
                    //foreach (BatimentModel bat in sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    //{
                    //    batiments.Add(bat);
                    //}
                    ////
                }
                lastParentId = lastId;
                report = new TableVerificationModel();
                report.Type = "4-DISTRIBUTION DES QUESTIONNAIRES EN NRT2 SELON LA RAISON (%)";
                report.ID = lastId + 1;
                BatimentInobservableEtVideParentId = report.ID;
                report.ParentID = firstParentId;
                report.Indicateur = "";
                report.Total = "" + nbreBatimentInobservableEtVide;
                report.Taux = "" + Utilities.getPourcentage(nbreBatimentInobservableEtVide, totalBatiments) + "%";
                report.Niveau = "2";
                rapports.Add(report);
                lastId = report.ID;
                lastParentId = report.ID;

                foreach (SdeModel sde in listOfSdes)
                {
                    sqliteService = new SqliteDataReaderService(getConnectionString(path, sde.SdeId));
                    batiments = new List<BatimentModel>();
                    int BatimentInobservableEtVideParSde = sqliteService.sr.GetAllRptAgentRecenseurForNotFinishedObject().ToList().Count();
                    totalBatimentParSde += sqliteService.getAllBatiments().Count();
                    //
                    //Ajout de la branche SDE pour identifier dans quelle Sde se trouve les batiments concernes
                    report = new TableVerificationModel();
                    report.Type = "" + sde.SdeId;
                    report.ID = lastId + 1;
                    sdeParentId = report.ID;
                    report.ParentID = BatimentInobservableEtVideParentId;
                    report.Total = "" + BatimentInobservableEtVideParSde;
                    report.Taux = "" + Utilities.getPourcentage(BatimentInobservableEtVideParSde, totalBatimentParSde) + "%";
                    lastId = report.ID;
                    lastParentId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);

                    //////////////////////
                    //Ensemble des batiments inobservables et au moins un objet vide
                    batiments = sqliteService.sr.GetAllBatimentsInobservables().ToList();
                    foreach (BatimentModel bat in sqliteService.sr.GetAllBatimentsWithAtLeastOneBlankObject())
                    {
                        batiments.Add(bat);
                    }
                    //Nombre de refus total
                    int nonbreRefusTotal = 0;
                    List<BatimentModel> batimentsEnRefus = new List<BatimentModel>();
                    string raisonRefus = "";

                    //Nombre Indisponibilité avec rendez-vous
                    int NbreIndAvecRendezVous = 0;
                    List<BatimentModel> batimentsEnIndAvecRendezVous = new List<BatimentModel>();
                    string raisonAvecRendezVous = "";

                    //Indisponibilite
                    int nbreIndisponible = 0;
                    List<BatimentModel> batimentsIndisponible = new List<BatimentModel>();
                    string raisonIndisponible = "";

                    //Autre
                    int nbreAutre = 0;
                    List<BatimentModel> batimentsEnAutre = new List<BatimentModel>();
                    string raisonAutre = "";

                    foreach (BatimentModel batiment in batiments)
                    {
                        //On recherche les rapports AR par batiment et on fait le total
                        List<RapportArModel> rars = sqliteService.sr.GetAllRptAgentRecenseurByBatiment(batiment.batimentId);
                        if (rars != null)
                        {
                            foreach (RapportArModel rar in rars)
                            {
                                //Indisponibilité avec rendez-vous raison=17
                                if (rar.RaisonActionId == 17)
                                {
                                    NbreIndAvecRendezVous++;
                                    batimentsEnIndAvecRendezVous.Add(batiment);
                                    raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Refus
                                if (rar.RaisonActionId == 16)
                                {
                                    nonbreRefusTotal++;
                                    batimentsEnRefus.Add(batiment);
                                    raisonRefus = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Indisponibilité
                                if (rar.RaisonActionId == 18)
                                {
                                    nbreIndisponible++;
                                    batimentsIndisponible.Add(batiment);
                                    raisonIndisponible = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                                //Autre
                                if (rar.RaisonActionId == 19)
                                {
                                    nbreAutre++;
                                    batimentsEnAutre.Add(batiment);
                                    raisonAutre = rar.AutreRaisonAction;
                                }
                            }

                        }
                    }
                    //On definit les parents parents des bracnhes refus, indisponible etc...
                    int refusParent = 0;
                    int indAvecRDParent = 0;
                    int indParent = 0;
                    int autreParent = 0;

                    //On ajoute la branche REFUS avec le total
                    report = new TableVerificationModel();
                    report.Type = "Refus";
                    report.ID = lastId + 1;
                    refusParent = report.ID;
                    report.Indicateur = raisonRefus;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nonbreRefusTotal;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnRefus)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = refusParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }

                    //On ajoute la branche Indisponibilité avec rendez-vous
                    report = new TableVerificationModel();
                    report.Type = "Indisponibilité avec rendez-vous";
                    report.ID = lastId + 1;
                    indAvecRDParent = report.ID;
                    report.Indicateur = raisonAvecRendezVous;
                    report.ParentID = sdeParentId;
                    report.Total = "" + NbreIndAvecRendezVous;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnIndAvecRendezVous)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = indAvecRDParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }

                    //On ajoute la branche Indisponibilité
                    report = new TableVerificationModel();
                    report.Type = "Indisponibilité";
                    report.ID = lastId + 1;
                    indParent = report.ID;
                    report.Indicateur = raisonIndisponible;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nbreIndisponible;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsIndisponible)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = indParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                    //On ajoute la branche Autre
                    report = new TableVerificationModel();
                    report.Type = "Autre";
                    report.ID = lastId + 1;
                    autreParent = report.ID;
                    report.Indicateur = raisonAutre;
                    report.ParentID = sdeParentId;
                    report.Total = "" + nbreAutre;
                    rapports.Add(report);
                    lastId = report.ID;
                    //On ajoute les batiments dans la branche
                    foreach (BatimentModel batiment in batimentsEnAutre)
                    {
                        report = new TableVerificationModel();
                        report.Type = "Batiman-" + batiment.batimentId + " /REC-" + batiment.qrec;
                        report.ID = lastId + 1;
                        report.ParentID = autreParent;
                        report.Niveau = "4";
                        rapports.Add(report);
                        lastId = report.ID;
                    }
                    ////////////////////////
                }
                #endregion

            }
            #endregion
            return rapports;
        }
        public static List<TableVerificationModel> getVerificationNonReponsePartielle(string path, string sdeId)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            SqliteDataReaderService service = new SqliteDataReaderService(getConnectionString(path, sdeId));
            int lastParentId = 0;
            int lastId = 0;
            //Ajout des nomres de logements, menages et individus remplis partiellement
            int nbreTotalLogement = service.sr.GetAllLogementsModel().Count();
            int nbreTotalMenages = service.sr.GetAllMenagesModel().Count();
            int nbreTotalIndividus = service.sr.GetAllIndividusModel().Count();
            int totalBatiments = service.getAllBatiments().Count();
            int totalBatimentsPasFini = service.sr.GetAllBatimentNonTermine().Count();

            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            #region STATUT DE REMPLISSAGE INITIAL
            //Ajout de la branche STATUT DE REMPLISSAGE INITIAL
            TableVerificationModel report = new TableVerificationModel();
            report.Type = "STATUT DE REMPLISSAGE INITIAL";
            report.Indicateur = "";
            report.ParentID = 0;
            report.ID = 1;
            report.Niveau = "1";
            report.Total = "" + totalBatiments;
            lastId = report.ID;
            lastParentId = report.ID;
            parent_0 = lastParentId;
            rapports.Add(report);

            #region NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS
            //Ajout de l'entete dans le rapport de verification
            report = new TableVerificationModel();
            report.Type = "I-NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS";
            report.Indicateur = "";
            report.ParentID = lastParentId;
            report.ID = lastId + 1;
            parent_1 = report.ID;
            report.Niveau = "2";
            report.Total = "" + totalBatimentsPasFini;
            report.Taux = "" + getPourcentage(totalBatimentsPasFini, totalBatiments) + "%";
            lastId = report.ID;
            lastParentId = report.ID;
            rapports.Add(report);
            //


            //Ajout de la branche logement
            List<LogementModel> logementsPartiellesRemplis = service.sr.GetAllLogementNonTermines();
            int nbreLogementTotalPR = logementsPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Niveau = "3";
            report.Type = "Nombre de logements individuels";
            report.Total = "" + logementsPartiellesRemplis.Count();
            report.Taux = "" + getPourcentage(nbreLogementTotalPR, nbreTotalLogement) + "%";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des logements a l'interieur dans la branche logements
            if (nbreLogementTotalPR != 0)
            {
                foreach (LogementModel logement in logementsPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + logement.batimentId + "/Lojman-" + logement.logeId;
                    report.ParentID = lastParentId;
                    report.Niveau = "4";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            //
            //Ajout de la branche Menage
            List<MenageModel> menagesPartiellesRemplis = service.sr.GetAllMenageNonTermine();
            int nbreMenagesPartiellesRemplis = menagesPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Type = "Nombre de menages";
            report.Total = "" + nbreMenagesPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreMenagesPartiellesRemplis, nbreTotalMenages) + "%";
            report.Niveau = "5";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des menages a l'interieur dans la branche Menage
            if (nbreLogementTotalPR != 0)
            {
                foreach (MenageModel men in menagesPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "/Menaj-" + men.menageId;
                    report.ParentID = lastParentId;
                    report.Niveau = "";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            //
            //Ajout de la branche INDIVIDUS
            List<IndividuModel> individusPartiellesRemplis = service.sr.GetAllIndividuNonTermine();
            int nbreindividusPartiellesRemplis = individusPartiellesRemplis.Count();
            report = new TableVerificationModel();
            report.ID = lastId + 1;
            report.ParentID = parent_1;
            report.Type = "Nombre d'individus";
            report.Total = "" + nbreindividusPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreindividusPartiellesRemplis, nbreTotalIndividus) + "%";
            report.Niveau = "6";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            //Ajout des menages a l'interieur dans la branche Menage
            if (nbreLogementTotalPR != 0)
            {
                foreach (IndividuModel ind in individusPartiellesRemplis)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "/Menaj-" + ind.menageId + "/Envidivi-" + ind.individuId;
                    report.ParentID = lastParentId;
                    report.Niveau = "6";
                    rapports.Add(report);
                    lastId = report.ID;
                }
            }
            #endregion

            #region NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS
            //Ajout de la branche principale
            report = new TableVerificationModel();
            List<LogementModel> logementsOccupantAbsent = service.sr.GetAllLogementOccupantAbsent();
            int nbreLogOccupantAbsent = logementsOccupantAbsent.Count();
            report.Type = "2-NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreLogOccupantAbsent;
            report.Taux = "" + getPourcentage(nbreLogOccupantAbsent, nbreTotalLogement) + "%";
            lastId = report.ID;
            lastParentId = report.ID;
            rapports.Add(report);
            //
            //Ajout des branches enfants 
            if (nbreLogOccupantAbsent != 0)
            {
                foreach (LogementModel logement in logementsOccupantAbsent)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.Type = "Batiman-" + logement.batimentId + "/Lojman-" + logement.logeId;
                    report.ParentID = lastParentId;
                    lastId = report.ID;
                    report.Niveau = "3";
                    rapports.Add(report);
                }
            }
            #endregion

            #region MENAGES PARTIELLEMENT REMPLIS
            report = new TableVerificationModel();
            report.Type = "3- %  DE MÉNAGES PARTIELLEMENT REMPLIS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreMenagesPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreMenagesPartiellesRemplis, nbreTotalMenages) + "%";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            if (nbreMenagesPartiellesRemplis > 0)
            {
                //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<MenageModel> menageEnIndAvecRendezVous = new List<MenageModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<MenageModel> menageAbandon = new List<MenageModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<MenageModel> menageEnAutre = new List<MenageModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (MenageModel menage in menagesPartiellesRemplis)
                {
                    List<RapportArModel> rarForMenage = service.sr.GetAllRptAgentRecenseurByMenage(menage.menageId);
                    foreach (RapportArModel rar in rarForMenage)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            nbreAbandon = nbreAbandon + 1;
                            menageAbandon.Add(menage);
                            raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 8)
                        {
                            NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                            menageEnIndAvecRendezVous.Add(menage);
                            raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 10)
                        {
                            nbreAutre = nbreAutre + 1;
                            menageEnAutre.Add(menage);
                            raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;
                        }

                    }
                }
                //
                //Ajout des ces branches dans lea branche Menage
                int parentAbandon = 0;
                int parentRendezVous = 0;
                int parentAutre = 0;

                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                parentAbandon = report.ID;
                report.Niveau = "2";
                lastId = report.ID;
                report.Total = "" + menageAbandon.Count;
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (MenageModel men in menageAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Interruption avec Rendez-vous";
                report.Total = "" + menageEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Autre";
                report.Total = "" + menageEnAutre.Count;
                parentAutre = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    lastId = report.ID;
                    rapports.Add(report);
                }

            }
            #endregion

            #region INDIVIDUS PARTIELLEMENT REMPLIS
            report = new TableVerificationModel();
            report.Type = "4- % D´INDIVIDUS PARTIELLEMENT REMPLIS";
            report.ParentID = 1;
            report.ID = lastId + 1;
            parent_2 = report.ID;
            report.Niveau = "2";
            report.Total = "" + nbreindividusPartiellesRemplis;
            report.Taux = "" + getPourcentage(nbreindividusPartiellesRemplis, nbreTotalIndividus) + "%";
            rapports.Add(report);
            lastId = report.ID;
            lastParentId = report.ID;
            if (nbreindividusPartiellesRemplis > 0)
            {
                //Nombre Indisponibilité avec rendez-vous
                int NbreIndAvecRendezVous = 0;
                List<IndividuModel> individuEnIndAvecRendezVous = new List<IndividuModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<IndividuModel> indviduAbandon = new List<IndividuModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<IndividuModel> individuEnAutre = new List<IndividuModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (IndividuModel ind in individusPartiellesRemplis)
                {
                    List<RapportArModel> rarForIndividu = service.sr.GetAllRptAgentRecenseurByIndividu(ind.individuId);
                    foreach (RapportArModel rar in rarForIndividu)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            if (indviduAbandon.Count > 0)
                            {
                                if (Utilities.isIndividuExist(indviduAbandon, ind) == false)
                                {
                                    nbreAbandon = nbreAbandon + 1;
                                    indviduAbandon.Add(ind);
                                    raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                            }
                            else
                            {
                                nbreAbandon = nbreAbandon + 1;
                                indviduAbandon.Add(ind);
                                raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                            }

                        }

                        if (rar.RaisonActionId == 8)
                        {
                            if (individuEnIndAvecRendezVous.Count > 0)
                            {
                                if (Utilities.isIndividuExist(individuEnIndAvecRendezVous, ind) == false)
                                {
                                    NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                                    individuEnIndAvecRendezVous.Add(ind);
                                    raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                            }
                            else
                            {
                                NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                                individuEnIndAvecRendezVous.Add(ind);
                                raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                            }

                        }
                        if (rar.RaisonActionId == 10)
                        {
                            if (individuEnAutre.Count > 0)
                            {
                                if (Utilities.isIndividuExist(individuEnAutre, ind) == false)
                                {
                                    nbreAutre = nbreAutre + 1;
                                    individuEnAutre.Add(ind);
                                    raisonAutre = rar.AutreRaisonAction;
                                }
                            }
                            else
                            {
                                nbreAutre = nbreAutre + 1;
                                individuEnAutre.Add(ind);
                                raisonAutre = rar.AutreRaisonAction;
                            }

                        }

                    }
                }
                //
                //Ajout des ces branches dans lea branche Menage
                int parentAbandon = 0;
                int parentRendezVous = 0;
                int parentAutre = 0;

                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                parentAbandon = report.ID;
                report.Niveau = "6";
                lastId = report.ID;
                report.Total = "" + indviduAbandon.Count;
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (IndividuModel ind in indviduAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "Menaj-" + ind.menageId + "/Envidivi-" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Interruption avec Rendez-vous";
                report.Niveau = "6";
                report.Total = "" + individuEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individuEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "'Menaj-" + ind.menageId + "/Envidivi-" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = lastParentId;
                report.Type = "Autre";
                report.Niveau = "6";
                report.Total = "" + individuEnAutre.Count;
                parentAutre = report.ID;
                lastId = report.ID;
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individuEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "'Menaj-" + ind.menageId + "/Envidivi-" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    report.Indicateur = raisonAutre;
                    lastId = report.ID;
                    rapports.Add(report);
                }

            }

            #endregion
            #endregion


            return rapports;
        }
        public static List<TableVerificationModel> getVerificationNonReponsePartielleForAllSdes(string path)
        {
            List<TableVerificationModel> rapports = new List<TableVerificationModel>();
            IConfigurationService configurationService = new ConfigurationService();
            List<SdeModel> listOfSdes = configurationService.searchAllSdes();
            int nbreLogements = 0;
            int nbreTotalLogement = 0;
            int nbreTotalMenages = 0;
            int nbreTotalIndividus = 0;
            int nbreMenages = 0;
            int nbreIndividus = 0;
            int nbreLogementsOccupantsAbsents = 0;
            int total = 0;
            int notFinishObject = 0;
            int totalBatiments = 0;
            int totalBatimentsPasFini = 0;
            ISqliteReader service = null;

            foreach (SdeModel sde in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sde.SdeId));
                totalBatiments += service.GetAllBatimentModel().Count();
                totalBatimentsPasFini += service.GetAllBatimentNonTermine().Count();
                nbreTotalIndividus += service.GetAllIndividusModel().Count;
                nbreTotalLogement += service.GetAllLogementsModel().Count;
                nbreTotalMenages += service.GetAllMenagesModel().Count;
                nbreLogements = nbreLogements + service.GetAllLogementNonTermines().Count;
                nbreMenages += service.GetAllMenageNonTermine().Count;
                nbreIndividus += service.GetAllIndividuNonTermine().Count;
                nbreLogementsOccupantsAbsents += service.GetAllLogementOccupantAbsent().Count;
                notFinishObject += (nbreLogements + nbreMenages + nbreIndividus);
            }
            //Le Total des objets
            total += nbreTotalIndividus + nbreTotalLogement + nbreTotalMenages + nbreLogementsOccupantsAbsents;

            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            int parentLogement = 0;
            int parentMenage = 0;
            int parentIndividus = 0;
            int lastId = 0;
            int lastParentId = 0;

            #region STATUT DE REMPLISSAGE INITIAL
            //Ajout de la branche STATUT DE REMPLISSAGE INITIAL
            TableVerificationModel report = new TableVerificationModel();
            report.Type = "STATUT DE REMPLISSAGE INITIAL";
            report.Indicateur = "";
            report.ParentID = 0;
            report.ID = 1;
            report.Niveau = "1";
            lastId = report.ID;
            lastParentId = report.ID;
            parent_0 = lastParentId;
            report.Total = "" + totalBatiments;
            rapports.Add(report);

            #region NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS
            //Branche 1
            report = new TableVerificationModel();
            report.Type = "1- NOMBRE QUESTIONNAIRES PARTIELLEMENT REMPLIS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + totalBatimentsPasFini;
            report.Niveau = "2";
            report.Taux = "" + getPourcentage(totalBatimentsPasFini, totalBatiments) + "%";
            rapports.Add(report);

            //Liste des logements, menages individus
            List<LogementModel> logements = null;
            List<MenageModel> menages = null;
            List<IndividuModel> individus = null;
            //
            //Ajout des sdes
            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                logements = service.GetAllLogementNonTermines();
                menages = service.GetAllMenageNonTermine();
                individus = service.GetAllIndividuNonTermine();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Niveau = "3";
                report.Total = "" + (logements.Count + menages.Count + individus.Count);
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);
                //
                //Ajout de la branche Logements
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre de logements individuels";
                report.Total = "" + logements.Count;
                report.Niveau = "4";
                report.Taux = "%" + getPourcentage(logements.Count, service.GetAllLogementsModel().Count);
                lastId = report.ID;
                parentLogement = report.ID;
                rapports.Add(report);
                //Ajout des branches logements, menages individus dans la SDE


                foreach (LogementModel logment in logements)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentLogement;
                    report.Type = "Batiman-" + logment.batimentId + "/Lojman-" + logment.logeId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //
                //Ajout de la branche Menages
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre de ménages";
                report.Total = "" + menages.Count;
                report.Niveau = "4";
                report.Taux = "" + getPourcentage(menages.Count, service.GetAllMenagesModel().Count) + "%";
                lastId = report.ID;
                parentMenage = report.ID;
                rapports.Add(report);

                foreach (MenageModel men in menages)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentMenage;
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "/Menaj-" + men.menageId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //
                //Ajout de la branche Menages
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Nombre d´individus";
                report.Total = "" + individus.Count;
                report.Niveau = "4";
                report.Taux = "%" + getPourcentage(individus.Count, service.GetAllIndividusModel().Count);
                lastId = report.ID;
                parentIndividus = report.ID;
                rapports.Add(report);
                foreach (IndividuModel ind in individus)
                {
                    report = new TableVerificationModel();
                    report.ID = lastId + 1;
                    report.ParentID = parentIndividus;
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "/Menaj-" + ind.menageId + "/Endividi-" + ind.individuId;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

            }
            #endregion

            #region NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS
            report = new TableVerificationModel();
            report.Type = "2- NOMBRE DE LOGEMENTS OCCUPÉS AVEC OCCUPANTS ABSENTS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + nbreLogementsOccupantsAbsents;
            report.Taux = "" + getPourcentage(nbreLogementsOccupantsAbsents, nbreTotalLogement) + "%";
            report.Niveau = "2";
            rapports.Add(report);

            //AJout des branches Sdes
            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                logements = new List<LogementModel>();
                logements = service.GetAllLogementOccupantAbsent();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Total = "" + logements.Count;
                report.Taux = "" + getPourcentage(logements.Count, nbreTotalLogement) + "%";
                report.Niveau = "3";
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);

                //Ajout des logemnts dans chaque SDE
                foreach (LogementModel logChild in logements)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + logChild.batimentId + "/Lojman-" + logChild.logeId;
                    report.ID = lastId + 1;
                    lastId = report.ID;
                    report.Niveau = "5";
                    report.ParentID = parent_2;
                    rapports.Add(report);
                }
            }
            #endregion

            #region DE MÉNAGES PARTIELLEMENT REMPLIS
            int parentAbandon = 0;
            int parentRendezVous = 0;
            int parentAutre = 0;
            report = new TableVerificationModel();
            report.Type = "3- % DE MÉNAGES PARTIELLEMENT REMPLIS";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + nbreMenages;
            report.Taux = "" + getPourcentage(nbreMenages, nbreTotalMenages) + "%";
            report.Niveau = "2";
            rapports.Add(report);

            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                menages = service.GetAllMenageNonTermine();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Total = "" + menages.Count;
                report.Taux = "" + getPourcentage(menages.Count, nbreTotalMenages);
                report.Niveau = "3";
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);

                //Ajout des branches Abandon, Rendex-Vous, Autre et leurs filles
                int NbreIndAvecRendezVous = 0;
                List<MenageModel> menageEnIndAvecRendezVous = new List<MenageModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<MenageModel> menageAbandon = new List<MenageModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<MenageModel> menageEnAutre = new List<MenageModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (MenageModel menage in menages)
                {
                    List<RapportArModel> rarForMenage = service.GetAllRptAgentRecenseurByMenage(menage.menageId);
                    foreach (RapportArModel rar in rarForMenage)
                    {
                        if (rar.RaisonActionId == 7)
                        {
                            nbreAbandon = nbreAbandon + 1;
                            menageAbandon.Add(menage);
                            raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 8)
                        {
                            NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                            menageEnIndAvecRendezVous.Add(menage);
                            raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                        }
                        if (rar.RaisonActionId == 10)
                        {
                            nbreAutre = nbreAutre + 1;
                            menageEnAutre.Add(menage);
                            raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;
                        }

                    }
                }
                //
                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                parentAbandon = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Total = "" + menageAbandon.Count;
                report.Taux = "" + getPourcentage(menageAbandon.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (MenageModel men in menageAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Interruption avec Rendez-vous";
                report.Total = "" + menageEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(menageEnIndAvecRendezVous.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Autre";
                report.Total = "" + menageEnAutre.Count;
                parentAutre = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(menageEnAutre.Count, nbreTotalMenages);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (MenageModel men in menageEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + men.batimentId + "/Lojman-" + men.logeId + "'Menaj-" + men.menageId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

            }
            #endregion

            #region % D´INDIVIDUS PARTIELLEMENT REMPLIS
            parentAbandon = 0;
            parentRendezVous = 0;
            parentAutre = 0;

            report = new TableVerificationModel();
            report.Type = "4- % D´INDIVIDUS PARTIELLEMENT REMPLI";
            report.ID = lastId + 1;
            report.ParentID = parent_0;
            lastId = report.ID;
            parent_1 = report.ID;
            report.Total = "" + nbreIndividus;
            report.Taux = "" + getPourcentage(nbreIndividus, nbreTotalIndividus) + "%";
            report.Niveau = "2";
            rapports.Add(report);

            foreach (SdeModel sd in listOfSdes)
            {
                service = new SqliteReader(Utilities.getConnectionString(path, sd.SdeId));
                individus = new List<IndividuModel>();
                individus = service.GetAllIndividuNonTermine();
                report = new TableVerificationModel();
                report.Type = "" + sd.SdeId;
                report.ID = lastId + 1;
                lastId = report.ID;
                report.Total = "" + individus.Count;
                report.Taux = "" + getPourcentage(individus.Count, nbreTotalIndividus) + "%";
                report.Niveau = "3";
                report.ParentID = parent_1;
                parent_2 = report.ID;
                rapports.Add(report);

                //Ajout des branches Abandon, Rendex-Vous, Autre et leurs filles
                int NbreIndAvecRendezVous = 0;
                List<IndividuModel> individusEnIndAvecRendezVous = new List<IndividuModel>();
                string raisonAvecRendezVous = "";

                //Abandon
                int nbreAbandon = 0;
                List<IndividuModel> individusAbandon = new List<IndividuModel>();
                string raisonAbandon = "";

                //Autre
                int nbreAutre = 0;
                List<IndividuModel> individusEnAutre = new List<IndividuModel>();
                string raisonAutre = "";
                //
                //Constrcution des branches Abandon, Rendexvous, Autre
                foreach (IndividuModel ind in individus)
                {
                    List<RapportArModel> rarForIndividu = service.GetAllRptAgentRecenseurByIndividu(ind.individuId);
                    foreach (RapportArModel rar in rarForIndividu)
                    {
                        //if (rar.RaisonActionId == 7)
                        //{
                        //    nbreAbandon = nbreAbandon + 1;
                        //    individusAbandon.Add(ind);
                        //    raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                        //}
                        //if (rar.RaisonActionId == 8)
                        //{
                        //    NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                        //    individusEnIndAvecRendezVous.Add(ind);
                        //}
                        //if (rar.RaisonActionId == 10)
                        //{
                        //    nbreAutre = nbreAutre + 1;
                        //    ind.Raison = rar.AutreRaisonAction;
                        //    individusEnAutre.Add(ind);
                        //    raisonAutre = Constant.getRaison(rar.RaisonActionId).Value;

                        //}
                        if (rar.RaisonActionId == 7)
                        {
                            if (individusAbandon.Count > 0)
                            {
                                if (Utilities.isIndividuExist(individusAbandon, ind) == false)
                                {
                                    nbreAbandon = nbreAbandon + 1;
                                    individusAbandon.Add(ind);
                                    raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                                }
                            }
                            else
                            {
                                nbreAbandon = nbreAbandon + 1;
                                individusAbandon.Add(ind);
                                raisonAbandon = Constant.getRaison(rar.RaisonActionId).Value;
                            }

                        }

                        if (rar.RaisonActionId == 8)
                        {
                            if (individusEnIndAvecRendezVous.Count > 0)
                            {
                                if (Utilities.isIndividuExist(individusEnIndAvecRendezVous, ind) == false)
                                {
                                    NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                                    individusEnIndAvecRendezVous.Add(ind);
                                }
                            }
                            else
                            {
                                NbreIndAvecRendezVous = NbreIndAvecRendezVous + 1;
                                individusEnIndAvecRendezVous.Add(ind);
                                raisonAvecRendezVous = Constant.getRaison(rar.RaisonActionId).Value;
                            }

                        }
                        if (rar.RaisonActionId == 10)
                        {
                            if (individusEnAutre.Count > 0)
                            {
                                if (Utilities.isIndividuExist(individusEnAutre, ind) == false)
                                {
                                    nbreAutre = nbreAutre + 1;
                                    individusEnAutre.Add(ind);
                                    raisonAutre = rar.AutreRaisonAction;
                                }
                            }
                            else
                            {
                                nbreAutre = nbreAutre + 1;
                                individusEnAutre.Add(ind);
                                raisonAutre = rar.AutreRaisonAction;
                            }

                        }

                    }
                }
                //
                //Ajout de la branche Refus
                report = new TableVerificationModel();
                report.Type = "Abandon";
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                parentAbandon = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Total = "" + individusAbandon.Count;
                report.Taux = "" + getPourcentage(individusAbandon.Count, nbreTotalIndividus);
                rapports.Add(report);
                //Ajout des menages se trouvant a l'interieur 
                foreach (IndividuModel ind in individusAbandon)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "Menaj-" + ind.menageId + "/Endividi" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAbandon;
                    report.Indicateur = raisonAbandon;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }

                //Ajout de la branche Interruption avec Rendez-vous
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Interruption avec Rendez-vous";
                report.Total = "" + individusEnIndAvecRendezVous.Count;
                parentRendezVous = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(individusEnIndAvecRendezVous.Count, nbreTotalIndividus);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individusEnIndAvecRendezVous)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "Menaj-" + ind.menageId + "/Endividi" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentRendezVous;
                    report.Indicateur = raisonAvecRendezVous;
                    lastId = report.ID;
                    report.Niveau = "5";
                    rapports.Add(report);
                }
                //Ajout de la branche Autre
                report = new TableVerificationModel();
                report.ID = lastId + 1;
                report.ParentID = parent_2;
                report.Type = "Autre";
                report.Total = "" + individusEnAutre.Count;
                parentAutre = report.ID;
                report.Niveau = "4";
                lastId = report.ID;
                report.Taux = "" + getPourcentage(individusEnAutre.Count, nbreTotalIndividus);
                rapports.Add(report);
                //Ajout de menages se trouvant a l'interieur
                foreach (IndividuModel ind in individusEnAutre)
                {
                    report = new TableVerificationModel();
                    report.Type = "Batiman-" + ind.batimentId + "/Lojman-" + ind.logeId + "Menaj-" + ind.menageId + "/Endividi" + ind.individuId;
                    report.ID = lastId + 1;
                    report.ParentID = parentAutre;
                    lastId = report.ID;
                    report.Niveau = "5";
                    report.Indicateur = ind.Raison;
                    rapports.Add(report);
                }

            }

            #endregion

            #endregion

            return rapports;
        }
        public static List<VerificationFlag> getVerificationNonReponseParVariable(string path, string sdeId)
        {
            List<VerificationFlag> rapports = new List<VerificationFlag>();
            //Ajout des ID  parents 
            int parent_0 = 0;
            int parent_1 = 0;
            int parent_2 = 0;
            int parent_3 = 0;
            //
            VerificationFlag report = new VerificationFlag();
            report.ID = 1;
            report.ParentID = 0;
            parent_0 = report.ParentID;
            report.Theme = "Caractéristiques socio-démographiques";
            report.Sous_Theme = "Age";
            report.Denominateur = "Population totale";
            report.Flag = "FL1";
            report.Variable1 = "M11.5";
            report.Variable2 = "M11.6";
            report.Indice = "% de la population totale sans information sur la date de naissance ni sur l´âge";
            rapports.Add(report);

            return rapports;
        }
        #endregion

    }

}
