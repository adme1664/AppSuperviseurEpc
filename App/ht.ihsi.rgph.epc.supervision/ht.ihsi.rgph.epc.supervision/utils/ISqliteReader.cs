﻿using ht.ihsi.rgph.epc.database.entities;
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
       #endregion

       #region Logements
       List<LogementModel> GetAllLogementsModel();
       List<LogementModel> GetAllLogementsByBatiment(long batimentId);
       LogementModel GetLogementById(long logementId);
       List<LogementModel> GetLogementIByBatiment(long batimentId);
       #endregion

       #region Menages
       List<MenageModel> GetAllMenagesModel();
       List<MenageModel> GetMenagesByLogement(long logementId);
       List<MenageModel> GetMenagesByBatiment(long batimentId);
       MenageModel GetMenageById(long menageId);
       #endregion

       #region Individus
       List<IndividuModel> GetAllIndividusModel();
       List<IndividuModel> GetAllIndividusByMenage(long menageId);
       List<IndividuModel> GetAllIndividusByLogement(long logementId);
       IndividuModel GetIndividuModel(long individuId);
       List<MenageDetailsModel> GetIndividuByMenageDetails(long menageId);
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
   }
}