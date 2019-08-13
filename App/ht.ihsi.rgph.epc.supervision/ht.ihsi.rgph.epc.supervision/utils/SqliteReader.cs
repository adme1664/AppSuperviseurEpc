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
        #endregion

        #region Menage
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




        
   }
}
