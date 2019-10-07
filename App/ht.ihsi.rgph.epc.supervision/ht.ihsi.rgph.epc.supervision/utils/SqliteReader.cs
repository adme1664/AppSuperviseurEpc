using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.utils
{
   public  class SqliteReader:ISqliteReader
   {
       private string className = "SqliteReader";
       private Logger log;
       private MainRepository repository;


       public    SqliteReader(string connectionString)
       {
           repository = new MainRepository(connectionString);
           log = new Logger();
       }

        #region Batiments
       public List<models.BatimentModel> GetAllBatimentModel()
        {
            string methodName = "GetAllBatimentModel";
            try
            {
                return ModelMapper.MapTo(repository.BatimentRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.BatimentModel>();
        }

        public models.BatimentModel GetBatimentbyId(long batimentId)
        {
            string methodName = "GetBatimentbyId";
            try
            {
                return ModelMapper.MapTo(repository.BatimentRepository.Find(b => b.batimentId == batimentId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new models.BatimentModel();
        }
        /// <summary>
        /// Retourne tous les batiments ayant au moins un objet vide
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentsWithAtLeastOneBlankObject()
        {
            try
            {
                List<BatimentModel> finalBatimentsList = new List<BatimentModel>();
                List<BatimentModel> batiments = GetAllBatimentNonTermine();
                //Recherche les logements dans ces batiments
                foreach (BatimentModel bat in batiments)
                {
                    //Recherche des logements par batiment
                    List<LogementModel> getLogements = GetLogementIByBatiment(bat.batimentId);
                    if (getLogements != null)
                    {
                        foreach (LogementModel logement in getLogements)
                        {
                            if (logement.qlin2StatutOccupation == 2 || logement.qlin2StatutOccupation == 3)
                            {
                                finalBatimentsList.Add(bat);
                            }
                        }
                    }
                }
                return finalBatimentsList;
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentsWithAtLeastOneBlankObject" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les batiments qui sont inobservables (modalite=5)
        /// </summary>
        /// <returns></returns>
        public List<BatimentModel> GetAllBatimentsInobservables()
        {
            try
            {
                return ModelMapper.MapTo(repository.BatimentRepository.Find(b => b.qb1Etat == (int)Constant.EtatBatiment.Inobservable).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllBatimentsInobservables" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne Tous les batiments pas finis
        /// </summary>
        /// <returns></returns>
        /// 
        public List<BatimentModel> GetAllBatimentNonTermine()
        {
            List<BatimentModel> result = new List<BatimentModel>();
            try
            {
                result = ModelMapper.MapTo(repository.BatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.PasFini).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReqder/GetAllBatimentNotFinished:" + ex.Message);
            }
            return result;
        }

       #endregion

        #region Logements
        public List<models.LogementModel> GetAllLogementsModel()
        {
            string methodName = "GetAllLogementsModel";
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.LogementModel>();
        }

        public List<models.LogementModel> GetAllLogementsByBatiment(long batimentId)
        {
            string methodName = "GetAllLogementsByBatiment";
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find(l => l.batimentId == batimentId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.LogementModel>();
        }
        public models.LogementModel GetLogementById(long logementId)
        {
            string methodName = "GetLogementById";
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find(l => l.logeId == logementId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new models.LogementModel();
        }
        public List<LogementModel> GetLogementIByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find(l => l.batimentId == batimentId && l.qlCategLogement ==(int) Constant.TypeLogement.Endividyel).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetLogementIByBatiment" + ex.Message);
            }
            return null;
        }

        public List<LogementModel> GetAllLogementNonTermines()
        {
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find(l => l.statut != (int)Constant.StatutModule.Fini).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllLogementIndNotFinish" + ex.Message);
            }
            return new List<LogementModel>();
        }
        /// <summary>
        /// Retourne les logements dont les occupants sont absents
        /// </summary>
        /// <returns>List GetAllLogementOccupantAbsent</returns>
        public List<LogementModel> GetAllLogementOccupantAbsent()
        {
            try
            {
                return ModelMapper.MapTo(repository.LogementRepository.Find(l => l.qlin2StatutOccupation == 2).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllLogementOccupantAbsent" + ex.Message);
            }
            return new List<LogementModel>();
        }
        #endregion

        #region Menage
        public List<MenageModel> GetAllMenageNonTermine()
        {
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.statut != (int)Constant.StatutModule.Fini).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllMenageNotFinish" + ex.Message);
            }
            return new List<MenageModel>();
        }
        public List<models.MenageModel> GetAllMenagesModel()
        {
            string methodName = "GetAllMenagesModel";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.MenageModel>();
        }

        public List<models.MenageModel> GetMenagesByLogement(long logementId)
        {
            string methodName = "GetMenagesByLogement";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.logeId == logementId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.MenageModel>();
        }

        public List<models.MenageModel> GetMenagesByBatiment(long batimentId)
        {
            string methodName = "GetMenagesByBatiment";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.batimentId == batimentId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.MenageModel>();
        }

        public models.MenageModel GetMenageById(long menageId)
        {
            string methodName = "GetMenageById";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.FindOne(menageId));
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new models.MenageModel();
        }
        public Flag compteurFlagParMenages(List<MenageModel> menages)
        {
            string methodName = "compteurFlagParMenages";
            Flag flagMenage = new Flag();
            try
            {
                if (menages != null)
                {
                    foreach (MenageModel menage in menages)
                    {
                        List<IndividuModel> individus = GetAllIndividusByMenage(menage.menageId);
                        if (individus != null)
                        {
                            Flag flg = new Flag();
                            flg = CountTotalFlag(individus);
                            flagMenage.Flag_Aucun += flg.Flag_Aucun;
                            flagMenage.Flag_1_4 += flg.Flag_1_4;
                            flagMenage.Flag_5_14 += flg.Flag_5_14;
                            flagMenage.Flag_15_26 += flg.Flag_15_26;
                            flagMenage.Flag_27_47 += flg.Flag_27_47;
                            flagMenage.Flag_48_70 += flg.Flag_48_70;
                            flagMenage.Flag_71_130 += flg.Flag_71_130;
                        }
                    }
                    return flagMenage;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return flagMenage;
        }
        public List<MenageModel> GetAllMenage_1_Personne()
        {
            string methodName = "GetAllMenage_1_Personne";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant == 1).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new List<MenageModel>();
        }

        public List<MenageModel> GetAllMenage_2_3_Personnes()
        {
            string methodName = "GetAllMenage_2_3_Personnes";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant == 2 || m.qm2TotalIndividuVivant == 3).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new List<MenageModel>();
        }

        public List<MenageModel> GetAllMenage_4_5_Personnes()
        {
            string methodName = "GetAllMenage_4_5_Personnes";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant == 4 || m.qm2TotalIndividuVivant == 5).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new List<MenageModel>();
        }

        public List<MenageModel> GetAllMenage_6_Personnes()
        {
            string methodName = "GetAllMenage_6_Personnes";
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant >= 6).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new List<MenageModel>();
        }
        #endregion

        #region Individus
        public List<models.IndividuModel> GetAllIndividusModel()
        {
            string methodName = "GetAllIndividusModel";
            try
            {
                return ModelMapper.MapTo(repository.IndividuRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.IndividuModel>();
        }

        public List<models.IndividuModel> GetAllIndividusByMenage(long menageId)
        {
            string methodName = "GetAllIndividusByMenage";
            try
            {
                return ModelMapper.MapTo(repository.IndividuRepository.Find(i => i.menageId==menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.IndividuModel>();
        }

        public List<models.IndividuModel> GetAllIndividusByLogement(long logementId)
        {
            string methodName = "GetAllIndividusByLogement";
            try
            {
                return ModelMapper.MapTo(repository.IndividuRepository.Find(i => i.logeId == logementId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.IndividuModel>();
        }

        public models.IndividuModel GetIndividuModel(long individuId)
        {
            string methodName = "GetIndividuModel";
            try
            {
                return ModelMapper.MapTo(repository.IndividuRepository.FindOne(individuId));
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new models.IndividuModel();
        }
        public List<MenageDetailsModel> GetIndividuByMenageDetails(long menageId)
        {
            try
            {
                List<IndividuModel> individus = ModelMapper.MapTo(repository.IndividuRepository.Find(d => d.menageId == menageId).ToList());
                if (individus != null)
                {
                    List<MenageDetailsModel> models = new List<MenageDetailsModel>();
                    foreach (IndividuModel ind in individus)
                    {
                        MenageDetailsModel menageDetails = ModelMapper.MapToListModel<IndividuModel>(ind);
                        models.Add(menageDetails);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuByMenageDetails" + ex.Message);
            }
            return null;
        }
        public string locateIndividu(IndividuModel individu)
        {
            string methodName = "locateIndividu";
            try
            {
                BatimentModel bat = GetBatimentbyId(individu.batimentId.GetValueOrDefault());
                LogementModel logement = GetLogementById(individu.logeId.GetValueOrDefault());
                MenageModel menage = GetMenageById(individu.menageId.GetValueOrDefault());
                if (menage != null)
                {
                    return "Batiman-" + bat.batimentId + "/Lojman-" + logement.qlin1NumeroOrdre + "/Menaj-" + menage.qm1NoOrdre + "/Endividi-" + individu.q1NoOrdre;
                }
                return "Batiman-" + bat.batimentId + "/Lojman-" + logement.qlin1NumeroOrdre + "/Endividi-" + individu.q1NoOrdre;
            }
            catch (Exception ex)
            {
                log.Info("Erreur/" + methodName + ":" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retrouve les individus qui ne sont pas encore termines
        /// </summary>
        /// <returns></returns>
        public List<IndividuModel> GetAllIndividuNonTermine()
        {
            try
            {
                return ModelMapper.MapTo(repository.IndividuRepository.Find(i => i.isFieldAllFilled == 0).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllIndividuNotFinish" + ex.Message);
            }
            return new List<IndividuModel>();
        }
        #endregion

        #region AncienMenbre
        public List<models.AncienMembreModel> GetAllAncienMembreModel()
        {
            string methodName = "GetAllAncienMembreModel";
            try
            {
                return ModelMapper.MapTo(repository.AncienMembreRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.AncienMembreModel>();
        }

        public List<models.AncienMembreModel> GetAllAncienMembreByMenage(long menageId)
        {
            string methodName = "GetAllAncienMembreByMenage";
            try
            {
                return ModelMapper.MapTo(repository.AncienMembreRepository.Find(a => a.menageId == menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("" + className + "/" + methodName + ": MESSAGE=>" + ex.Message);
            }
            return new List<models.AncienMembreModel>();
        }
        public List<MenageDetailsModel> GetAncienMembreByMenageDetails(long menageId)
        {
            try
            {
                List<AncienMembreModel> ancienMembres = ModelMapper.MapTo(repository.AncienMembreRepository.Find(d => d.menageId == menageId).ToList());
                if (ancienMembres != null)
                {
                    List<MenageDetailsModel> models = new List<MenageDetailsModel>();
                    foreach (AncienMembreModel ind in ancienMembres)
                    {
                        MenageDetailsModel menageDetails = ModelMapper.MapToListModel<AncienMembreModel>(ind);
                        models.Add(menageDetails);
                    }
                    return models;
                }
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetIndividuByMenageDetails" + ex.Message);
            }
            return null;
        }
        public AncienMembreModel getAncienMembre(int ancienMembreId)
        {
            try
            {
                return ModelMapper.MapTo(repository.AncienMembreRepository.Find(a => a.ancienMembreId == ancienMembreId).FirstOrDefault());
            }
            catch (Exception e)
            {
                log.Info("SqliteReader:/getAncienMembre" + e.Message);
            }
            return new AncienMembreModel();
        }
        #endregion

        #region    RAPPORTS...   
        public List<MenageDetailsModel> GetRapportFinal(int menageId)
        {
            if (menageId != 0)
            {
                List<MenageDetailsModel> menages = new List<MenageDetailsModel>();
                menages.Add(ModelMapper.MapToListModel(repository.RapportFinalRepository.Find(r => r.rapportFinalId == menageId).FirstOrDefault()));
                return menages;
            }
            return new List<MenageDetailsModel>();
        }
        public RapportFinalModel getRapportFinalById(int rapportFinalId)
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportFinalRepository.Find(r => r.rapportFinalId == rapportFinalId).FirstOrDefault());
            }
            catch (Exception e)
            {
                log.Info("SqliteReader:/getRapportFinalById" + e.Message);
            }
            return new RapportFinalModel();
        }
        #endregion

        #region Questions
        public List<tbl_question> searchQuestionByCategorie(string codeCategorie)
        {
            try
            {
                return repository.QuestionRepository.Find(q => q.codeCategorie == codeCategorie).ToList();
            }
            catch (Exception ex)
            {
                log.Info("Error:searchQuestionByCategorie" + ex.Message);
            }
            return new List<tbl_question>();
        }
        public tbl_question getQuestion(string codeQuestion)
        {
            try
            {
                return repository.QuestionRepository.Find(q => q.codeQuestion == codeQuestion).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getQuestion" + ex.Message);
            }
            return null;
        }

        public string getReponse(string codeQuestion, string codeReponse)
        {
            try
            {
                List<tbl_question_reponse> ListOfreponse = repository.QuestionReponseRepository.Find(rep => rep.codeQuestion == codeQuestion).ToList();
                foreach (tbl_question_reponse qr in ListOfreponse)
                {
                    if (qr.codeReponse == codeReponse)
                    {
                        return qr.libelleReponse;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error:getReponse" + ex.Message);
            }
            return null;
        }


        public List<tbl_question_module> listOfQuestionModule(string codeModule)
        {
            try
            {
                return repository.QuestionModuleRepository.Find(qm => qm.codeModule == codeModule).ToList();
            }
            catch (Exception ex)
            {
                log.Info("Error:listOfQuestionModule" + ex.Message);
            }
            return null;
        }


        public string getLibelleCategorie(string codeCategorie)
        {
            try
            {
                return repository.CategorieQuestionRepository.Find(qc => qc.codeCategorie == codeCategorie).FirstOrDefault().categorieQuestion;
            }
            catch (Exception ex)
            {
                log.Info("Error:libelleCategorie" + ex.Message);
            }
            return null;
        }
        public tbl_categorie_question getCategorie(string codeCategorie)
        {
            try
            {
                return repository.CategorieQuestionRepository.Find(qc => qc.codeCategorie == codeCategorie).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getCategorie" + ex.Message);
            }
            return null;
        }


        public tbl_question getQuestionByNomChamps(string nomChamps)
        {
            try
            {
                return repository.QuestionRepository.Find(q => q.nomChamps == nomChamps).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Info("Error:getQuestionByNomChamps" + ex.Message);
            }
            return null;
        }
        #endregion

        #region GEOGRAPHIQUES
        public tbl_pays getpays(string codePays)
        {
            if (codePays != "" && codePays != null)
            {
                return repository.PaysRepository.Find(p => p.CodePays == codePays).FirstOrDefault();
            }
            return new tbl_pays();
        }

        public tbl_departement getDepartement(string deptId)
        {
            if (deptId != "" && deptId != null)
            {
                return repository.DepartementRepository.Find(p => p.DeptId == deptId).FirstOrDefault();
            }
            return new tbl_departement();
        }

        public tbl_commune getCommune(string comId)
        {
            if (comId != "" && comId != null)
            {
                string[] com = comId.Split('-');
                string code = com[0];
                return repository.CommuneRepository.Find(c => c.ComId == code).FirstOrDefault();
            }
            return new tbl_commune();
        }

        public tbl_vqse getVqse(string vqse)
        {
            if (vqse != "" && vqse != null)
            {
                return repository.VqseRepository.Find(c => c.VqseId == vqse).FirstOrDefault();
            }
            return new tbl_vqse();
        }
        #endregion

        #region DOMAINES
        public tbl_domaine_etude getDomaine(string domaineId)
        {
            if (domaineId != "" && domaineId != null)
            {
                return repository.DomaineEtudeRepository.Find(d => d.Code == domaineId).FirstOrDefault();
            }
            return new tbl_domaine_etude();
        }
        #endregion

        #region DEMOGRAPHIQUE...
        public double tailleMoyenneMenage()
        {
            try
            {
                double ind = GetAllIndividusModel().Count;
                double men = GetAllMenagesModel().Count;
                double res = ind / men;
                return res;
            }
            catch (Exception)
            {

            }
            return 0;
        }
        public int getTotalFemme()
        {
            return repository.IndividuRepository.Find(ind => ind.qp4Sexe == (int)Constant.Sexe.Fi).Count();
        }

        public int getTotalHomme()
        {
            return repository.IndividuRepository.Find(ind => ind.qp4Sexe == (int)Constant.Sexe.Gason).Count();
        }
        public float getIndiceMasculinite()
        {
            try
            {
                float indice = 0;
                float nbreG = (float)getTotalHomme();
                float nbreF = (float)getTotalFemme();
                indice = nbreG / nbreF;
                indice = indice * 100;
                return indice;
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenageUnipersonnel()
        {
            try
            {
                return repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant == 1).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalMenageUnipersonnel" + ex.Message);
            }
            return 0;
        }

        public List<MenageModel> searchMenageUnipersonnel()
        {
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant.GetValueOrDefault() == 1).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/searchMenageUnipersonnel" + ex.Message);
            }
            return new List<MenageModel>();
        }

       //Retourne tous les menages ayant plus de 6 personnes
        public List<MenageModel> searchMenage6PlusPersonne()
        {
            try
            {
                return ModelMapper.MapTo(repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant >= 6 ).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/searchMenage6PlusPersonne" + ex.Message);
            }
            return new List<MenageModel>();
        }

        public int getTotalMenageDe6IndsEtPlus()
        {
            try
            {
                return repository.MenageRepository.Find(m => m.qm2TotalIndividuVivant >= 6).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalMenageDe6IndsEtPlus" + ex.Message);
            }
            return 0;
        }

        public int getTotalFemmeChefMenage()
        {
            try
            {
                return repository.IndividuRepository.Find(i => i.q9LienDeParente == 1 && i.qp4Sexe == (int)Constant.Sexe.Fi).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalFemmeChefMenage:" + ex.Message);
            }
            return 0;
        }

        public int getTotalHommeChefMenage()
        {
            try
            {
                return repository.IndividuRepository.Find(i => i.q9LienDeParente == 1 && i.qp4Sexe == (int)Constant.Sexe.Gason).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/getTotalFemmeChefMenage:" + ex.Message);
            }
            return 0;
        }
        public int getTotalPersonnes15_64Ans()
        {
            try
            {
                //|| i.q8Age == Constant.AGE_PA_KONNEN
                return repository.IndividuRepository.Find(i => i.q8Age >= 15 && i.q8Age<=64  ).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalPersonnes15_64Ans:Error=> " + ex.Message);
            }
            return 0;
        }

        public int getTotalPersonnes18EtPlusAns()
        {
            try
            {
                return repository.IndividuRepository.Find(i => i.q8Age >= 18 ).Count();
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalPersonnes18EtPlusAns:Error=> " + ex.Message);
            }
            return 0;
        }
        public int getTotalEnfantDeMoinsDe1Ans()
        {
            try
            {
                return repository.IndividuRepository.Find(i => i.q8Age < 1).Count();
                //< 1 && i.isVerified == (int)Constant.StatutVerifie.PasVerifie
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/getTotalEnfantDeMoinsDe1Ans:Error=> " + ex.Message);
            }
            return 0;
        }
        public int getTotalDecesOuLaisse()
        {
            throw new NotImplementedException();
        }
        #endregion;

        #region GESTION
        public int getTotalBatiment()
        {
            return repository.BatimentRepository.Find().Count();
        }
        public int getTotalIndividus()
        {
            return repository.IndividuRepository.Find().Count();
        }

        public int getTotalLogement()
        {
            return repository.LogementRepository.Find() .Count();
        }

        public int getTotalMenages()
        {
            return repository.MenageRepository.Find().Count();
        }

        public int getTotalAncienMembre()
        {
            return repository.AncienMembreRepository.Find().Count();
        }
        public double getTotalBatRecenseParJourV()
        {
            string methodName = "getTotalBatRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<BatimentModel> listOfBatiments = ModelMapper.MapTo(repository.BatimentRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                BatimentModel firstBatiment = listOfBatiments.First();
                BatimentModel lastBatiment = listOfBatiments.Last();

                DateTime dateSaisieFirst = new DateTime();
                DateTime dateSaisieLast = new DateTime();

                dateSaisieFirst = Convert.ToDateTime(firstBatiment.dateDebutCollecte);
                dateSaisieLast = Convert.ToDateTime(lastBatiment.dateFinCollecte);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfBatiments.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfBatiments.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfBatiments.Count();
                    }
                }
                log.Info("Nombre de batiments/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public double getTotalLogeRecenseParJourV()
        {
            string methodName = "getTotalLogeRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<LogementModel> listOfLogements = ModelMapper.MapTo(repository.LogementRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                LogementModel firstBatiment = listOfLogements.First();
                LogementModel lastBatiment = listOfLogements.Last();

                DateTime dateSaisieFirst = new DateTime();
                DateTime dateSaisieLast = new DateTime();

                dateSaisieFirst = Convert.ToDateTime(firstBatiment.dateDebutCollecte);
                dateSaisieLast = Convert.ToDateTime(lastBatiment.dateFinCollecte);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfLogements.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfLogements.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfLogements.Count();
                    }
                }
                log.Info("Nombre de logements/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public double getTotalMenageRecenseParJourV()
        {
            string methodName = "getTotalMenageRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<MenageModel> listOfMenages = ModelMapper.MapTo(repository.MenageRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                MenageModel firstMenage = listOfMenages.First();
                MenageModel lastMenage = listOfMenages.Last();

                DateTime dateSaisieFirst = new DateTime();
                DateTime dateSaisieLast = new DateTime();

                dateSaisieFirst = Convert.ToDateTime(firstMenage.dateDebutCollecte);
                dateSaisieLast = Convert.ToDateTime(lastMenage.dateFinCollecte);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfMenages.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfMenages.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfMenages.Count();
                    }
                }
                log.Info("Nombre de menages/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public double getTotalIndRecenseParJourV()
        {
            string methodName = "getTotalIndRecenseParJourV";
            double nbreParJour = 0;
            try
            {
                List<IndividuModel> listOfIndividus = ModelMapper.MapTo(repository.IndividuRepository.Find(b => b.statut == (int)Constant.StatutModule.Fini).ToList());
                IndividuModel firstIndividu = listOfIndividus.First();
                IndividuModel lastIndividu = listOfIndividus.Last();

                DateTime dateSaisieFirst = new DateTime();
                DateTime dateSaisieLast = new DateTime();

                dateSaisieFirst = Convert.ToDateTime(firstIndividu.dateDebutCollecte);
                dateSaisieLast = Convert.ToDateTime(lastIndividu.dateFinCollecte);
                double totalOfDays = (dateSaisieLast - dateSaisieFirst).TotalDays;
                totalOfDays = Math.Truncate(totalOfDays);
                if (totalOfDays == 0)
                    nbreParJour = listOfIndividus.Count();
                else
                {
                    if (totalOfDays >= 2)
                        nbreParJour = listOfIndividus.Count() / (totalOfDays - 1);
                    else
                    {
                        nbreParJour = listOfIndividus.Count();
                    }
                }
                log.Info("Nombre de Individus/jar:" + nbreParJour);
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + " : " + ex.Message);
            }
            return nbreParJour;
        }

        public int getTotalBatRecenseV()
        {
            return repository.LogementRepository.Find(lo => lo.statut == (int)Constant.StatutValide.Valide).Count();
        }

        public int getTotalBatRecenseNV()
        {
            return repository.BatimentRepository.Find(ba => ba.statut == (int)Constant.StatutValide.PasValide).Count();
        }

        public int getTotalLogeIRecenseV()
        {
            return repository.LogementRepository.Find(lo => lo.statut == (int)Constant.StatutValide.Valide).Count();
        }

        public int getTotalLogeIRecenseNV()
        {
            try
            {
                return repository.LogementRepository.Find(lo => lo.statut == (int)Constant.StatutValide.PasValide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenageRecenseV()
        {
            try
            {
                return repository.MenageRepository.Find(anc => anc.statut == (int)Constant.StatutValide.Valide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalMenageRecenseNV()
        {
            try
            {
                return repository.MenageRepository.Find(anc => anc.statut == (int)Constant.StatutValide.PasValide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalIndRecenseV()
        {
            try
            {
                return repository.IndividuRepository.Find(ind=>ind.statut==(int)Constant.StatutValide.Valide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalIndRecenseNV()
        {
            try
            {
                return repository.IndividuRepository.Find(ind => ind.statut == (int)Constant.StatutValide.PasValide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalAncienMembreV()
        {
            try
            {
                return repository.AncienMembreRepository.Find(anc => anc.statut == (int)Constant.StatutValide.Valide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalAncienMembreNV()
        {
            try
            {
                return repository.AncienMembreRepository.Find(anc => anc.statut == (int)Constant.StatutValide.Valide).Count();

            }
            catch (Exception)
            {

            }
            return 0;
        }

        public int getTotalBatRecenseTermine()
        {
            return repository.BatimentRepository.Find(obj => obj.statut == (int)Constant.StatutModule.Fini).Count();
        }

        public int getTotalBatRecenseNTermine()
        {
            return repository.BatimentRepository.Find(obj => obj.statut == (int)Constant.StatutModule.PasFini || obj.statut == (int)Constant.StatutModule.MalRempli).Count();
        }

        public int getTotalLogeIRecenseTermine()
        {
            return repository.LogementRepository.Find(obj => obj.statut == (int)Constant.StatutModule.Fini).Count();
        }

        public int getTotalLogeIRecenseNTermine()
        {
            return repository.LogementRepository.Find(obj => obj.statut == (int)Constant.StatutModule.PasFini || obj.statut == (int)Constant.StatutModule.MalRempli).Count();
        }

        public int getTotalMenageRecenseTermine()
        {
            return repository.MenageRepository.Find(obj => obj.statut == (int)Constant.StatutModule.Fini).Count();
        }

        public int getTotalMenageRecenseNTermine()
        {
            return repository.MenageRepository.Find(obj => obj.statut == (int)Constant.StatutModule.PasFini || obj.statut == (int)Constant.StatutModule.MalRempli).Count();
        }

        public int getTotalIndRecenseTermine()
        {
            return repository.IndividuRepository.Find(obj => obj.statut == (int)Constant.StatutModule.Fini).Count();
        }

        public int getTotalIndRecenseNTermine()
        {
            return repository.IndividuRepository.Find(obj => obj.statut == (int)Constant.StatutModule.PasFini || obj.statut == (int)Constant.StatutModule.MalRempli).Count();
        }

        public int getTotalAncienMembreTermine()
        {
            return repository.AncienMembreRepository.Find(obj => obj.statut == (int)Constant.StatutModule.Fini).Count();
        }

        public int getTotalAncienMembreNTermine()
        {
            return repository.AncienMembreRepository.Find(obj => obj.statut == (int)Constant.StatutModule.PasFini || obj.statut == (int)Constant.StatutModule.MalRempli).Count();
        }
        #endregion

        #region CODIFICATION ET COMPTEUR DE FLAG
        //public Codification getInformationForCodification()
        //{
        //    string methodName = "getInformationForCodification";
        //    Codification _aCodififer = new Codification();
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = ModelMapper.MapToListModel(repository.MIndividuRepository.Find().ToList());
        //        if (listOfIndividus.Count != 0)
        //        {
        //            foreach (IndividuModel ind in listOfIndividus)
        //            {

        //                #region Recherche des individus avec la P10
        //                //P10=1
        //                if (ind.Qp10LieuNaissance == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        _aCodififer.P10_1 += 1;
        //                    }
        //                }
        //                //p10=2
        //                if (ind.Qp10LieuNaissance == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (ind.Qp10CommuneNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp10VqseNaissance == Constant.PA_KONNEN_SEKSYON_KOMINAL)
        //                    {
        //                        if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                        {
        //                            _aCodififer.P10_2 += 1;
        //                        }
        //                    }

        //                }
        //                //p10=3
        //                if (ind.Qp10LieuNaissance == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (ind.Qp10PaysNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI)
        //                    {
        //                        if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                        {
        //                            _aCodififer.P10_3 += 1;
        //                        }
        //                    }
        //                }
        //                //p10=4
        //                if (ind.Qp10LieuNaissance == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        _aCodififer.P10_4 += 1;
        //                    }
        //                }
        //                #endregion

        //                #region Recherche des individus avec la variable P12
        //                //p12=1
        //                if (ind.Qp12DomicileAvantRecensement == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        _aCodififer.P12_1 += 1;
        //                    }
        //                }
        //                //p12=2
        //                if (ind.Qp12DomicileAvantRecensement == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (ind.Qp12CommuneDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp12VqseDomicileAvantRecensement == Constant.PA_KONNEN_SEKSYON_KOMINAL)
        //                    {
        //                        if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                        {
        //                            _aCodififer.P12_2 += 1;
        //                        }
        //                    }
        //                }
        //                //p12=3
        //                if (ind.Qp12DomicileAvantRecensement == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (ind.Qp12PaysDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI)
        //                    {
        //                        if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                        {
        //                            _aCodififer.P12_3 += 1;
        //                        }
        //                    }
        //                }
        //                //p12=4
        //                if (ind.Qp12DomicileAvantRecensement == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        _aCodififer.P12_4 += 1;
        //                    }
        //                }
        //                #endregion

        //                #region Recherche des individus avec la variavle A5
        //                int typeBien = 0;
        //                if (ind.Qa5TypeBienProduitParEntreprise != "")
        //                    typeBien = Convert.ToInt32(ind.Qa5TypeBienProduitParEntreprise);
        //                //A5 est codifie
        //                if (typeBien < 40)
        //                {
        //                    _aCodififer.A5Codifie += 1;
        //                }
        //                //A5 =autre
        //                if (typeBien == 40)
        //                {
        //                    _aCodififer.A5Autre += 1;
        //                }
        //                //A5 ne sait pas
        //                if (typeBien == 41)
        //                {
        //                    _aCodififer.A5NeSaitPas += 1;
        //                }
        //                #endregion

        //                #region Recherche des individus avec la variavle A7
        //                int typeActivite = Convert.ToInt32(ind.Qa7FoncTravail);
        //                //A7 est bien codifiee
        //                if (typeActivite < 132)
        //                {
        //                    _aCodififer.A7Codifie += 1;
        //                }
        //                //A7 est codifiee a autre
        //                if (typeActivite == 132)
        //                {
        //                    _aCodififer.A7Autre += 1;
        //                }
        //                //A7 est codifiee a ne sait pas
        //                if (typeActivite == 133)
        //                {
        //                    _aCodififer.A7NeSaitPas += 1;
        //                }
        //                #endregion
        //            }
        //            _aCodififer.SommeP10PartiellementCodifie = _aCodififer.P10_2 + _aCodififer.P10_3;
        //            _aCodififer.SommeP12PartiellementCodifie = _aCodififer.P12_2 + _aCodififer.P12_3;
        //            return _aCodififer;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Codification();
        //}
        public Flag CountTotalFlag(List<IndividuModel> individus)
        {
            string methodName = "CountFlag";
            try
            {
                Flag compteur = new Flag();
                compteur.Individus = new List<IndividuModel>();
                int numberOfProblemsTotal = 0;
                foreach (IndividuModel ind in individus)
                {
                    int numberOfProblems = 0;

                    #region Test date de naissance/age
                    if (ind.q7DateNaissanceAnnee == 9999 && ind.q7DateNaissanceJour == 99 && ind.q7DateNaissanceMois == 99)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.q7DateNaissanceAnnee == 9999 && ind.q7DateNaissanceJour == 99 && ind.q7DateNaissanceMois == 99 && ind.q8Age != 999)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.q7DateNaissanceAnnee != 9999 && ind.q7DateNaissanceJour != 99 && ind.q7DateNaissanceMois != 99 && ind.q8Age == 999)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Niveau d'etude
                    if (ind.q11NiveauEtude >= 3 && ind.q11NiveauEtude < 9 )
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region Fecondite
                    //if (ind.qp4Sexe == (int)Constant.Sexe.Fi && ind.q8Age >= 13)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    //if (ind.qp4Sexe == (int)Constant.Sexe.Fi && ind.q8Age >= 13)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    //if (ind.qp4Sexe == (int)Constant.Sexe.Fi && ind.q8Age >= 13)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    #endregion

                    #region Information sur la date de naissance du premier enfant
                    //if (ind.qp4Sexe == (int)Constant.Sexe.Fi && ind.q8Age >= 13 )
                    //{
                    //    numberOfProblems += 1;
                    //}
                    #endregion

                    #region Activite Economique
                    //if (ind.q8Age >= 10 && ind.)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    //if (ind.Qp5bAge >= 10 && ind.Qa7FoncTravail == 99)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    //if (ind.Qp5bAge >= 10 && ind.Qa8EntreprendreDemarcheTravail == 10)
                    //{
                    //    numberOfProblems += 1;
                    //}
                    #endregion

                    #region Test de coherence Age/Niveau Etude
                    if (ind.q8Age >= 3 && ind.q8Age < 6 && ind.q11NiveauEtude > 3)
                    {
                        numberOfProblems += 1;
                    }
                    if (ind.q8Age >= 6 && ind.q8Age < 9 && ind.q11NiveauEtude > 5)
                    {
                        numberOfProblems += 1;
                    }
                    #endregion

                    #region DECOMPTE
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    if (numberOfProblems == 3)
                        compteur.Flag3 += 1;
                    if (numberOfProblems == 4)
                        compteur.Flag4 += 1;
                    if (numberOfProblems == 5)
                        compteur.Flag5 += 1;
                    if (numberOfProblems == 6)
                        compteur.Flag6 += 1;
                    if (numberOfProblems == 7)
                        compteur.Flag7 += 1;
                    if (numberOfProblems == 8)
                        compteur.Flag8 += 1;
                    if (numberOfProblems == 9)
                        compteur.Flag9 += 1;
                    if (numberOfProblems == 10)
                        compteur.Flag10 += 1;
                    if (numberOfProblems == 11)
                        compteur.Flag11 += 1;
                    if (numberOfProblems == 12)
                        compteur.Flag12 += 1;
                    if (numberOfProblems == 13)
                    {
                        compteur.Flag13 += 1;
                        compteur.Individus.Add(ind);
                    }
                    numberOfProblemsTotal += numberOfProblems;
                    #endregion
                }
                #region compteur pour les categories de menages
                compteur.Total = numberOfProblemsTotal;
                if (compteur.Total == 0)
                    compteur.Flag_Aucun = 1;
                if (compteur.Total >= 1 && compteur.Total <= 4)
                    compteur.Flag_1_4 = 1;
                if (compteur.Total > 4 && compteur.Total <= 14)
                    compteur.Flag_5_14 = 1;
                if (compteur.Total >= 15 && compteur.Total <= 26)
                    compteur.Flag_15_26 = 1;
                if (compteur.Total >= 27 && compteur.Total <= 47)
                    compteur.Flag_27_47 = 1;
                if (compteur.Total >= 48 && compteur.Total <= 70)
                    compteur.Flag_48_70 = 1;
                if (compteur.Total >= 71 && compteur.Total <= 130)
                    compteur.Flag_71_130 = 1;
                #endregion
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }
        public Flag Count2FlagAgeDateNaissance()
        {
            string methodName = "Count2FlagAgeDateNaissance";
            List<IndividuModel> individusWithFlags = new List<IndividuModel>();
            int numberOfProblemsTotal = 0;
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividusModel();
                Flag compteur = new Flag();
                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Test date de naissance/age
                    //Si on ne connait ni l'annee, le jour et le mois de naissance de la personne 
                    if (ind.q7DateNaissanceAnnee == 9999 && ind.q7DateNaissanceJour == 99 && ind.q7DateNaissanceMois == 99)
                    {
                        numberOfProblems += 1;
                        //Verifie si l'individu existe deja dans la liste
                        if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                        {
                            individusWithFlags.Add(ind);
                        }
                    }
                    //Si on ne connait ni l'annee, le jour et le mois de naissance de la personne mais on connait son age
                    if (ind.q7DateNaissanceAnnee == 9999 && ind.q7DateNaissanceJour == 99 && ind.q7DateNaissanceMois == 99 && ind.q8Age != 999)
                    {
                        numberOfProblems += 1;
                        //Verifie si l'individu existe deja dans la liste
                        if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                        {
                            individusWithFlags.Add(ind);
                        }
                    }
                    //Si on connait l'annee, le jour et le mois de naissance de la personne mais on ne connait pas son age
                    if (ind.q7DateNaissanceAnnee != 9999 && ind.q7DateNaissanceJour != 99 && ind.q7DateNaissanceMois != 99 && ind.q8Age == 999)
                    {
                        numberOfProblems += 1;
                        //Verifie si l'individu existe deja dans la liste
                        if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                        {
                            individusWithFlags.Add(ind);
                        }
                    }
                    #endregion
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    compteur.Individus = new List<IndividuModel>();
                    compteur.Individus = individusWithFlags;
                    numberOfProblemsTotal += numberOfProblems;
                }
                compteur.Total = numberOfProblemsTotal;
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }
        public Flag CountFlagFecondite()
        {
            string methodName = "CountFlagFecondite";
            List<IndividuModel> individusWithFlags = new List<IndividuModel>();
            int numberOfProblemsTotal = 0;
            try
            {
                List<IndividuModel> listOfIndividus = GetAllIndividusModel();
                Flag compteur = new Flag();
                foreach (IndividuModel ind in listOfIndividus)
                {
                    int numberOfProblems = 0;
                    #region Fecondite
                    ////Si on ne connait pas le nombre d'enfants nes vivants dans le menage
                    //if (ind.qp4Sexe == (int)Constant.Sexe.Fi && ind.q8Age>= 13 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    //{
                    //    numberOfProblems += 1;
                    //    //Verifie si l'individu existe deja dans la liste
                    //    if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                    //    {
                    //        individusWithFlags.Add(ind);
                    //    }
                    //}
                    ////Si on ne connait pas le nombre d'enfants de sexe feminin ne dans le menage
                    //if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99)
                    //{
                    //    numberOfProblems += 1;
                    //    //Verifie si l'individu existe deja dans la liste
                    //    if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                    //    {
                    //        individusWithFlags.Add(ind);
                    //    }
                    //}
                    ////Si on connait pas le nombre d'enfants de sexe masculin ne dans le menage
                    //if (ind.Qp4Sexe == (int)Constant.Sexe.Fi && ind.Qp5bAge >= 13 && ind.Qf1bNbreEnfantNeVivantF == 99 && ind.Qf1aNbreEnfantNeVivantM == 99)
                    //{
                    //    numberOfProblems += 1;
                    //    //Verifie si l'individu existe deja dans la liste
                    //    if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
                    //    {
                    //        individusWithFlags.Add(ind);
                    //    }
                    //}
                    #endregion
                    if (numberOfProblems == 0)
                        compteur.Flag0 += 1;
                    if (numberOfProblems == 1)
                        compteur.Flag1 += 1;
                    if (numberOfProblems == 2)
                        compteur.Flag2 += 1;
                    if (numberOfProblems == 3)
                        compteur.Flag3 += 1;
                    compteur.Individus = new List<IndividuModel>();
                    compteur.Individus = individusWithFlags;
                    numberOfProblemsTotal += numberOfProblems;
                }
                compteur.Total = numberOfProblemsTotal;
                return compteur;

            }
            catch (Exception ex)
            {
                log.Info("SqliteReader/" + methodName + ":" + ex.Message);
            }
            return new Flag();
        }
        //public Flag CountFlagEmploi()
        //{
        //    string methodName = "CountFlagEmploi";
        //    int numberOfProblemsTotal = 0;
        //    List<IndividuModel> individusWithFlags = new List<IndividuModel>();
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = GetAllIndividus();
        //        Flag compteur = new Flag();
        //        foreach (IndividuModel ind in listOfIndividus)
        //        {
        //            int numberOfProblems = 0;
        //            #region Activite Economique
        //            //Si on ne connait le type de bien produit dans l'entreprise ou l'individu travail
        //            if (ind.Qp5bAge >= 10 && ind.Qa5TypeBienProduitParEntreprise == "99")
        //            {
        //                numberOfProblems += 1;
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
        //                {
        //                    individusWithFlags.Add(ind);
        //                }
        //            }
        //            //Si on connait pas le poste qu'occupe l'individu dans l'entreprise
        //            if (ind.Qp5bAge >= 10 && ind.Qa7FoncTravail == 99)
        //            {
        //                numberOfProblems += 1;
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
        //                {
        //                    individusWithFlags.Add(ind);
        //                }
        //            }
        //            //S'il n'a pas entrepris de demarche pour pouvoir travailler
        //            if (ind.Qp5bAge >= 10 && ind.Qa8EntreprendreDemarcheTravail == 10)
        //            {
        //                numberOfProblems += 1;
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusWithFlags, ind) == false)
        //                {
        //                    individusWithFlags.Add(ind);
        //                }
        //            }
        //            #endregion
        //            if (numberOfProblems == 0)
        //                compteur.Flag0 += 1;
        //            if (numberOfProblems == 1)
        //                compteur.Flag1 += 1;
        //            if (numberOfProblems == 2)
        //                compteur.Flag2 += 1;
        //            if (numberOfProblems == 3)
        //                compteur.Flag3 += 1;
        //            compteur.Individus = new List<IndividuModel>();
        //            compteur.Individus = individusWithFlags;
        //            numberOfProblemsTotal += numberOfProblems;
        //        }
        //        compteur.Total = numberOfProblemsTotal;
        //        return compteur;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Flag();
        //}
        //public Flag getIndividuWithP10()
        //{
        //    string methodName = "getIndividuWithP10";
        //    List<IndividuModel> individusCodification = new List<IndividuModel>();
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = GetAllIndividusModel();
        //        Flag compteur = new Flag();
        //        foreach (IndividuModel ind in listOfIndividus)
        //        {
        //            #region Recherche des individus avec la P10
        //            //P10=1
        //            //if (ind.Qp10LieuNaissance == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            //{
        //            //    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //            //    {
        //            //        //Verifie si l'individu existe deja dans la liste
        //            //        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //            //        {
        //            //            individusCodification.Add(ind);
        //            //        }
        //            //    }
        //            //}
        //            //p10=2
        //            if (ind.Qp10LieuNaissance == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (ind.Qp10CommuneNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp10VqseNaissance == Constant.PA_KONNEN_SEKSYON_KOMINAL)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        //Verifie si l'individu existe deja dans la liste
        //                        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                        {
        //                            individusCodification.Add(ind);
        //                        }
        //                    }
        //                }

        //            }
        //            //p10=3
        //            if (ind.Qp10LieuNaissance == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (ind.Qp10PaysNaissance == Constant.PA_KONNEN_KOMIN_OU_PEYI)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        //Verifie si l'individu existe deja dans la liste
        //                        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                        {
        //                            individusCodification.Add(ind);
        //                        }
        //                    }
        //                }
        //            }
        //            //p10=4
        //            if (ind.Qp10LieuNaissance == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                {
        //                    //Verifie si l'individu existe deja dans la liste
        //                    if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                    {
        //                        individusCodification.Add(ind);
        //                    }
        //                }
        //            }
        //            #endregion
        //        }
        //        compteur.Individus = new List<IndividuModel>();
        //        compteur.Individus = individusCodification;
        //        return compteur;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Flag();
        //}
        //public Flag getIndividuWithP12()
        //{
        //    string methodName = "getIndividuWithP12";
        //    List<IndividuModel> individusCodification = new List<IndividuModel>();
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = GetAllIndividus();
        //        Flag compteur = new Flag();
        //        foreach (IndividuModel ind in listOfIndividus)
        //        {
        //            #region Recherche des individus avec la variable P12
        //            //p12=1
        //            //if (ind.Qp12DomicileAvantRecensement == 1 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            //{
        //            //    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //            //    {
        //            //        //Verifie si l'individu existe deja dans la liste
        //            //        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //            //        {
        //            //            individusCodification.Add(ind);
        //            //        }
        //            //    }
        //            //}
        //            //p12=2
        //            if (ind.Qp12DomicileAvantRecensement == 2 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (ind.Qp12CommuneDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI || ind.Qp12VqseDomicileAvantRecensement == Constant.PA_KONNEN_SEKSYON_KOMINAL)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        //Verifie si l'individu existe deja dans la liste
        //                        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                        {
        //                            individusCodification.Add(ind);
        //                        }
        //                    }
        //                }
        //            }
        //            //p12=3
        //            if (ind.Qp12DomicileAvantRecensement == 3 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (ind.Qp12PaysDomicileAvantRecensement == Constant.PA_KONNEN_KOMIN_OU_PEYI)
        //                {
        //                    if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                    {
        //                        //Verifie si l'individu existe deja dans la liste
        //                        if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                        {
        //                            individusCodification.Add(ind);
        //                        }
        //                    }
        //                }
        //            }
        //            //p12=4
        //            if (ind.Qp12DomicileAvantRecensement == 4 && ind.Statut == (int)Constant.StatutModule.Fini)
        //            {
        //                if (GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.Fini || GetBatimentbyId(ind.BatimentId).Statut == (int)Constant.StatutModule.PasFini)
        //                {
        //                    //Verifie si l'individu existe deja dans la liste
        //                    if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                    {
        //                        individusCodification.Add(ind);
        //                    }
        //                }
        //            }
        //            #endregion
        //        }
        //        compteur.Individus = new List<IndividuModel>();
        //        compteur.Individus = individusCodification;
        //        return compteur;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Flag();
        //}
        //public Flag getIndividuWithA5()
        //{
        //    List<IndividuModel> individusCodification = new List<IndividuModel>();
        //    string methodName = "getIndividuWithA5";
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = GetAllIndividus();
        //        Flag compteur = new Flag();

        //        foreach (IndividuModel ind in listOfIndividus)
        //        {
        //            #region Recherche des individus avec la variavle A5
        //            int typeBien = 0;
        //            if (ind.Qa5TypeBienProduitParEntreprise != "")
        //            {
        //                typeBien = Convert.ToInt32(ind.Qa5TypeBienProduitParEntreprise);
        //            }

        //            ////A5 est codifie
        //            //if (typeBien < 40)
        //            //{
        //            //    //Verifie si l'individu existe deja dans la liste
        //            //    if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //            //    {
        //            //        individusCodification.Add(ind);
        //            //    }
        //            //}
        //            //A5 =autre
        //            if (typeBien == 40)
        //            {
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                {
        //                    individusCodification.Add(ind);
        //                }
        //            }
        //            //A5 ne sait pas
        //            if (typeBien == 41)
        //            {
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                {
        //                    individusCodification.Add(ind);
        //                }
        //            }
        //            #endregion
        //        }
        //        compteur.Individus = new List<IndividuModel>();
        //        compteur.Individus = individusCodification;
        //        return compteur;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Flag();
        //}
        //public Flag getIndividuWithA7()
        //{
        //    List<IndividuModel> individusCodification = new List<IndividuModel>();
        //    string methodName = "getIndividuWithA7";
        //    try
        //    {
        //        List<IndividuModel> listOfIndividus = GetAllIndividus();
        //        Flag compteur = new Flag();
        //        foreach (IndividuModel ind in listOfIndividus)
        //        {
        //            #region Recherche des individus avec la variavle A7
        //            //int typeActivite = 0;
        //            int typeActivite = Convert.ToInt32(ind.Qa7FoncTravail);
        //            ////A7 est bien codifiee
        //            //if (typeActivite < 132)
        //            //{
        //            //    _aCodififer.A7Codifie += 1;
        //            //}
        //            //A7 est codifiee a autre
        //            if (typeActivite == 132)
        //            {
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                {
        //                    individusCodification.Add(ind);
        //                }
        //            }
        //            //A7 est codifiee a ne sait pas
        //            if (typeActivite == 133)
        //            {
        //                //Verifie si l'individu existe deja dans la liste
        //                if (Utilities.isIndividuExist(individusCodification, ind) == false)
        //                {
        //                    individusCodification.Add(ind);
        //                }
        //            }
        //            #endregion
        //        }
        //        compteur.Individus = new List<IndividuModel>();
        //        compteur.Individus = individusCodification;
        //        return compteur;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SqliteReader/" + methodName + ":" + ex.Message);
        //    }
        //    return new Flag();
        //}
        #endregion

        #region RAPPORT AGENT RECENSEUR
        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur
        /// </summary>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseur()
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find().ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseur:" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un batiment
        /// </summary>
        /// <param name="batimentId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByBatiment(long batimentId)
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find(b => b.batimentId == batimentId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByBatiment:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un logement
        /// </summary>
        /// <param name="logeId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByLogement(long logeId)
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find(l => l.logeId == logeId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByLogement:" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Retourne tous les rapports dresses par l'agent recenseur sur un menage
        /// </summary>
        /// <param name="menageId"></param>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurByMenage(long menageId)
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find(m => m.menageId == menageId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByMenage:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les rapports pour lesquels au moins un objet n'est pas termine
        /// </summary>
        /// <returns></returns>
        public List<RapportArModel> GetAllRptAgentRecenseurForNotFinishedObject()
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find(r => r.raisonActionId > 15).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurForNotFinishedObject:" + ex.Message);
            }
            return null;
        }

        public List<RapportArModel> GetAllRptAgentRecenseurByIndividu(long individuId)
        {
            try
            {
                return ModelMapper.MapTo(repository.RapportAgentRecenseurRepository.Find(r => r.individuId == individuId).ToList());
            }
            catch (Exception ex)
            {
                log.Info("SqliteReader:/GetAllRptAgentRecenseurByIndividu:" + ex.Message);
            }
            return null;
        }
        #endregion



   }
}
