using ht.ihsi.rgph.epc.database.dao;
using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.services
{
    /// <summary>
    /// Gestion des rapports personnels (Operation CRUD)
    /// </summary>
    public class RapportService:IRapportService
    {
        #region DECLARTIONS...
        private DaoRapports daoRapports;
        private Logger log;
        public static string CLASS_NAME = "RapportService";
        #endregion

        #region CONTRUCTORS
        /// <summary>
        /// Constructors qui initialise la connection avec la base de donnees
        /// </summary>
        public RapportService()
        {
            string connectionString = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoRapports = new DaoRapports(connectionString);
        }
        #endregion

        #region RAPPORT PERSONNEL...
        /// <summary>
        /// Sauvegarder un rapport personnel
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool saveRapportPersonnel(models.RapportPersonnelModel rpt)
        {
            try
            {
               return daoRapports.saveRapportPersonnel(ModelMapper.MapTo(rpt));
            }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Modifier un rapport personnel
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool updateRapportPersonnel(models.RapportPersonnelModel rpt)
        {
            try
            {
                if (rpt != null)
                {
                    RapportPersonnelModel modelToUpdate = getRapportPersonnel(Convert.ToInt32(rpt.rapportId));
                    modelToUpdate = ModelMapper.MapToModel(rpt);
                    return daoRapports.updateRapportPersonnel(ModelMapper.MapTo(rpt));
                }
                return false;
             }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Supprimer un rapport persnnel
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        public bool supprimerRapportPersonnel(int rptId)
        {
            try
            {
                return daoRapports.supprimerRapportPersonnel(rptId);
            }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Retourne un rapport personnel
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        public models.RapportPersonnelModel getRapportPersonnel(int rptId)
        {
            try
            {
                return ModelMapper.MapTo(daoRapports.getRapportPersonnel(rptId));
            }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return null;
            
        }
        /// <summary>
        /// Retourne la liste des rapports personnels
        /// </summary>
        /// <returns></returns>
        public List<models.RapportPersonnelModel> listOfRapportsPersonnels()
        {
            try
            {
                return ModelMapper.MapTo(daoRapports.listOfRapports());
            }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return null; 
        }
        /// <summary>
        /// Retourne un rapport pour un agent
        /// </summary>
        /// <param name="codeAgent"></param>
        /// <returns></returns>
        public List<RapportPersonnelModel> searchRapportPersonnelByAgent(int codeAgent)
        {
            try
            {
                return ModelMapper.MapTo(daoRapports.searchRapportPersonnelByAgent(codeAgent));
            }
            catch (Exception e)
            {
                log.Info("ERROR:RapportService=> :" + e.Message);
            }
            return null; 
        }
        #endregion

        #region RAPPORT DEROULEMENT ...
        /// <summary>
        /// Sauvegarder un rapport de deroulement
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool saveRapportDeroulement(RapportDeroulementModel rpt)
        {
            string methodName = "saveRapportDeroulement";
            try
            {
                return daoRapports.saveRapportDeroulement(ModelMapper.MapTo(rpt));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Modifier un rapport de deroulement
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool updateRapportDeroulement(RapportDeroulementModel rpt)
        {
            string methodName = "updateRapportDeroulement";
            try
            {
                RapportDeroulementModel modelToUpdate =getRapportDeroulement(rpt.RapportId);
                if (modelToUpdate != null)
                    modelToUpdate = ModelMapper.MapToModel(rpt);
                return daoRapports.updateRapportDeroulement(ModelMapper.MapTo(modelToUpdate));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Supprimer un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <returns></returns>
        public bool deleteRapportDeroulement(long rapportId)
        {
            string methodName = "deleteRapportDeroulement";
            try
            {
                return daoRapports.deleteRapportDeroulement(rapportId);
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Retourne un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <returns></returns>
        public RapportDeroulementModel getRapportDeroulement(long rapportId)
        {
            string methodName = "getRapportDeroulement";
            try
            {
                return ModelMapper.MapTo(daoRapports.getRapportDeroulement(rapportId));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les rapports de deroulement
        /// </summary>
        /// <returns></returns>
        public List<RapportDeroulementModel> searchRapportDeroulement()
        {
            string methodName = "searchRapportDeroulement";
            try
            {
                return ModelMapper.MapTo(daoRapports.searchRapportDeroulement());
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        #endregion

        #region DETAILS RAPPORTS DEROULEMENTS...
        /// <summary>
        /// Sauvegarder les details sur un rapport de deroulement
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool saveDetailsRapportDeroulement(DetailsRapportModel rpt)
        {
            string methodName = "saveDetailsRapportDeroulement";
            try
            {
                return daoRapports.saveDetailsRapportDeroulement(ModelMapper.MapTo(rpt));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Modifier les details sur un rapport de deroulement
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool updateDetailsRapportDeroulement(DetailsRapportModel rpt)
        {
            string methodName = "updateDetailsRapportDeroulement";
            try
            {
                DetailsRapportModel modelToUpdate = getDetailRapportModel(rpt.RapportId, rpt.DetailsRapportId);
                if (modelToUpdate != null)
                    modelToUpdate = ModelMapper.MapToModel(rpt);
                return daoRapports.updateDetailsRapportDeroulement(ModelMapper.MapTo(modelToUpdate));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Supprimer les details sur un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <returns></returns>
        public bool deleteDetailsRapportDeroulement(long rapportId)
        {
            string methodName = "deleteDetailsRapportDeroulement";
            try
            {
                return daoRapports.deleteDetailsRapportDeroulement(rapportId);
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
        /// <summary>
        /// Retourne les details pour un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <returns></returns>
        public List<DetailsRapportModel> getDetailsRapportDeroulement(long rapportId)
        {
            string methodName = "getDetailsRapportDeroulement";
            try
            {
                return ModelMapper.MapTo(daoRapports.getDetailsRapportDeroulement(rapportId));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne tous les details 
        /// </summary>
        /// <returns></returns>
        public List<DetailsRapportModel> searchDetailsRapportDeroulement()
        {
            string methodName = "searchDetailsRapportDeroulement";
            try
            {
                return ModelMapper.MapTo(daoRapports.searchDetailsRapportDeroulement());
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// Retourne sur un detail sur un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <param name="detailRapportId"></param>
        /// <returns></returns>
        public DetailsRapportModel getDetailRapportModel(long rapportId, long detailRapportId)
        {
            string methodName = "getDetailRapportModel";
            try
            {
                return ModelMapper.MapTo(daoRapports.getDetailRapportDeroulement(rapportId, detailRapportId));
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        #endregion


       
    }
}
