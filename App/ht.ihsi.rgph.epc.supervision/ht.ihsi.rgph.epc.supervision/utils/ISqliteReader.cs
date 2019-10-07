using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.utils
{
   public interface ISqliteReader
   {
       #region BATIMENTS
       List<BatimentModel> GetAllBatimentModel();
       BatimentModel GetBatimentbyId(long batimentId);
       List<BatimentModel> GetAllBatimentsInobservables();
       List<BatimentModel> GetAllBatimentNonTermine();
       List<BatimentModel> GetAllBatimentsWithAtLeastOneBlankObject();
       #endregion

       #region Logements
       List<LogementModel> GetAllLogementsModel();
       List<LogementModel> GetAllLogementsByBatiment(long batimentId);
       LogementModel GetLogementById(long logementId);
       List<LogementModel> GetLogementIByBatiment(long batimentId);
       List<LogementModel> GetAllLogementNonTermines();
       List<LogementModel> GetAllLogementOccupantAbsent();
       #endregion

       #region Menages
       List<MenageModel> GetAllMenageNonTermine();
       List<MenageModel> GetAllMenagesModel();
       List<MenageModel> GetMenagesByLogement(long logementId);
       List<MenageModel> GetMenagesByBatiment(long batimentId);
       MenageModel GetMenageById(long menageId);
       Flag compteurFlagParMenages(List<MenageModel> menages);
       List<MenageModel> GetAllMenage_1_Personne();
       List<MenageModel> GetAllMenage_2_3_Personnes();
       List<MenageModel> GetAllMenage_4_5_Personnes();
       List<MenageModel> GetAllMenage_6_Personnes();
       #endregion

       #region Individus
       List<IndividuModel> GetAllIndividuNonTermine();
       List<IndividuModel> GetAllIndividusModel();
       List<IndividuModel> GetAllIndividusByMenage(long menageId);
       List<IndividuModel> GetAllIndividusByLogement(long logementId);
       IndividuModel GetIndividuModel(long individuId);
       List<MenageDetailsModel> GetIndividuByMenageDetails(long menageId);

       //Flag getIndividuWithP10();
       //Flag getIndividuWithP12();
       //Flag getIndividuWithA5();
       //Flag getIndividuWithA7();
       Flag CountTotalFlag(List<IndividuModel> individus);
       Flag Count2FlagAgeDateNaissance();
       Flag CountFlagFecondite();
       string locateIndividu(IndividuModel individu);
       //Flag CountFlagEmploi();
       #endregion

       #region AncienMembre
       List<AncienMembreModel> GetAllAncienMembreModel();
       List<AncienMembreModel> GetAllAncienMembreByMenage(long menageId);
       List<MenageDetailsModel> GetAncienMembreByMenageDetails(long menageId);
       AncienMembreModel getAncienMembre(int ancienMembreId);
       #endregion

       #region RAPPORTS
       List<MenageDetailsModel> GetRapportFinal(int menageId);
       RapportFinalModel getRapportFinalById(int rapportFinalId);
       #endregion

       #region QUESTIONS
       tbl_question getQuestion(string codeQuestion);
       tbl_question getQuestionByNomChamps(string nomChamps);
       string getReponse(string codeQuestion, string codeReponse);
       string getLibelleCategorie(string codeCategorie);
       List<tbl_question> searchQuestionByCategorie(string codeCategorie);
       tbl_categorie_question getCategorie(string codeCategorie);
       List<tbl_question_module> listOfQuestionModule(string codeModule);
       #endregion

       #region GEOGRAPHIQUES
       tbl_pays getpays(string codePays);
       tbl_departement getDepartement(string deptId);
       tbl_commune getCommune(string comId);
       tbl_vqse getVqse(string vqse);

       #endregion

       #region DOMAINE ETUDE
       tbl_domaine_etude getDomaine(string domaineId);
       #endregion

       #region DEMOGRAPHIQUE
       double tailleMoyenneMenage();
       float getIndiceMasculinite();
       int getTotalMenageUnipersonnel();
       List<MenageModel> searchMenageUnipersonnel();
       List<MenageModel> searchMenage6PlusPersonne();
       int getTotalMenageDe6IndsEtPlus();
       int getTotalFemmeChefMenage();
       int getTotalFemme();
       int getTotalHommeChefMenage();
       int getTotalHomme();
       int getTotalPersonnes15_64Ans();
       int getTotalPersonnes18EtPlusAns();
       int getTotalDecesOuLaisse();
       int getTotalEnfantDeMoinsDe1Ans();
       #endregion

       #region PERFORMANCE
       double getTotalBatRecenseParJourV();     
       double getTotalLogeRecenseParJourV();  
       double getTotalMenageRecenseParJourV();
       double getTotalIndRecenseParJourV();
       #endregion

       #region GESTION
       int getTotalBatiment();
       int getTotalLogement();
       int getTotalMenages();
       int getTotalAncienMembre();
       int getTotalIndividus();
       int getTotalBatRecenseV();
       int getTotalBatRecenseNV();
       int getTotalLogeIRecenseV();
       int getTotalLogeIRecenseNV();
       int getTotalMenageRecenseV();
       int getTotalMenageRecenseNV();
       int getTotalIndRecenseV();
       int getTotalIndRecenseNV();
       int getTotalAncienMembreV();
       int getTotalAncienMembreNV();

       int getTotalBatRecenseTermine();
       int getTotalBatRecenseNTermine();
       int getTotalLogeIRecenseTermine();
       int getTotalLogeIRecenseNTermine();
       int getTotalMenageRecenseTermine();
       int getTotalMenageRecenseNTermine();
       int getTotalIndRecenseTermine();
       int getTotalIndRecenseNTermine();
       int getTotalAncienMembreTermine();
       int getTotalAncienMembreNTermine();
       #endregion

       #region RAPPORT AGENT RECENSEUR
       List<RapportArModel> GetAllRptAgentRecenseur();
       List<RapportArModel> GetAllRptAgentRecenseurByBatiment(long batimentId);
       List<RapportArModel> GetAllRptAgentRecenseurByLogement(long logeId);
       List<RapportArModel> GetAllRptAgentRecenseurByIndividu(long individuId);
       List<RapportArModel> GetAllRptAgentRecenseurForNotFinishedObject();
       List<RapportArModel> GetAllRptAgentRecenseurByMenage(long menageId);
       #endregion
   }
}
