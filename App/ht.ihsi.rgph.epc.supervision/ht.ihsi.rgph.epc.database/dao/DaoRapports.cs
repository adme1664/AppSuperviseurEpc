using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.dao
{
    /// <summary>
    /// Gestion des rapports personnels sur les agens EPC
    /// </summary>
   public class DaoRapports: IDaoRapports
   {
       public static string CLASS_NAME = "DaoRapports";
       #region DECLARATIONS
       private MainRepository repository;
       private Logger log;
       private string connectionString;
       #endregion

       #region CONTRUCTORS ...
       /// <summary>
       /// Constructors
       /// </summary>
       public DaoRapports(string connectionString)
       {
           this.connectionString = connectionString;
           repository = new MainRepository(connectionString,true);
           log = new Logger();
       }
        #endregion

       #region RAPPORTS PERSONNELS
       /// <summary>
       /// Retourne un rapport pour un agent
       /// </summary>
       /// <param name="codeAgent"></param>
       /// <returns></returns>
       public List<Tbl_RapportPersonnel> searchRapportPersonnelByAgent(int codeAgent)
       {
           try
           {
               return repository.RapportPersonnelRepository.Find(r => r.persId == codeAgent).ToList();
           }
           catch (Exception e)
           {
               log.Info("Error:DAO=>" + e.Message);
           }
           return null;
       }
       /// <summary>
       /// Sauvegarder un rapport personnel pour un agent
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
        public bool saveRapportPersonnel(entities.Tbl_RapportPersonnel rpt)
        {
            try
            {
                repository.RapportPersonnelRepository.Insert(rpt);
                repository.Save();
                return true;
            }
            catch (Exception e)
            {
                log.Info("Error:DAO=>" + e.Message);
            }
            return false;

        }

       /// <summary>
       /// Modifier un rapport personnel
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
        public bool updateRapportPersonnel(entities.Tbl_RapportPersonnel rpt)
        {
            try
            {
                repository.RapportPersonnelRepository.UpdateGB(rpt);
                repository.Save();
                return true;
            }
            catch (Exception e)
            {
                log.Info("Error:DAO=>" + e.Message);
            }
            return false;
        }

       /// <summary>
       /// Supprimer un rapport
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
        public bool supprimerRapportPersonnel(int rptId)
        {
            try
            {
                Tbl_RapportPersonnel rpt = repository.RapportPersonnelRepository.FindOne(rptId);
                if (rpt != null)
                    repository.RapportPersonnelRepository.DeleteGB(rpt);
                return true;
            }
            catch (Exception e)
            {
                log.Info("Error:DAO=>" + e.Message);
            }
            return false;
            
        }

       /// <summary>
       /// Retourne un rapport
       /// </summary>
       /// <param name="rptId"></param>
       /// <returns></returns>
        public entities.Tbl_RapportPersonnel getRapportPersonnel(int rptId)
        {
            return repository.RapportPersonnelRepository.FindOne(rptId);
        }

       /// <summary>
       /// Retourne tous les rapports personnels
       /// </summary>
       /// <returns></returns>
        public List<Tbl_RapportPersonnel> listOfRapports()
        {
            return repository.RapportPersonnelRepository.Find().ToList();
        }
       #endregion

       #region RAPPORTS DE DEROULEMENT SUR LA COLLECTE ..
       /// <summary>
       /// Sauvegarder un rapport de deroulement
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
        public bool saveRapportDeroulement(Tbl_RprtDeroulement rpt)
        {
            string methodName = "saveRapportDeroulement";
            try
            {
                repository.RapportDeroulementRepository.Insert(rpt);
                repository.Save();
                return true;
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
        public bool updateRapportDeroulement(Tbl_RprtDeroulement rpt)
        {
            string methodName = "updateRapportDeroulement";
            try
            {
                repository.RapportDeroulementRepository.UpdateGB(rpt);
                repository.Save();
                return true;
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
            string methodName = "";
            try
            {
                Tbl_RprtDeroulement rpt = repository.RapportDeroulementRepository.FindOne(rapportId);
                if (rpt != null)
                {
                    repository.RapportDeroulementRepository.Delete(rpt);
                    repository.Save();
                }
                return true;
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
        public Tbl_RprtDeroulement getRapportDeroulement(long rapportId)
        {
            string methodName = "";
            try
            {
                return repository.RapportDeroulementRepository.FindOne(rapportId);
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }

        public List<Tbl_RprtDeroulement> searchRapportDeroulement()
        {
            string methodName = "";
            try
            {
                return repository.RapportDeroulementRepository.Find().ToList();
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        #endregion

        #region DETAILS RAPPORTS DE DEROULEMENT ...
        /// <summary>
        /// Sauvegarder les details sur un rapport de deroulement
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        public bool saveDetailsRapportDeroulement(Tbl_DetailsRapport rpt)
        {
            string methodName = "saveDetailsRapportDeroulement";
            try
            {
                repository.DetailsRapportDeroulementRepository.Insert(rpt);
                repository.Save();
                return true;
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
        public bool updateDetailsRapportDeroulement(Tbl_DetailsRapport rpt)
        {
            string methodName = "updateDetailsRapportDeroulement";
            try
            {
                repository.DetailsRapportDeroulementRepository.UpdateGB(rpt);
                repository.Save();
                return true;
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
       /// <summary>
       /// Supprimer les details sur rapport de deroulement
       /// </summary>
       /// <param name="rapportId"></param>
       /// <returns></returns>
        public bool deleteDetailsRapportDeroulement(long rapportId)
        {
            string methodName = "deleteDetailsRapportDeroulement";
            try
            {
                List<Tbl_DetailsRapport> details = getDetailsRapportDeroulement(rapportId);
                if (details != null)
                {
                    foreach (Tbl_DetailsRapport detail in details)
                    {
                        repository.DetailsRapportDeroulementRepository.DeleteGB(detail);
                        repository.Save();
                    }
                }
                return true;
                    
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return false;
        }
       /// <summary>
       /// Retourne les details sur un rapport de deroulement
       /// </summary>
       /// <param name="rapportId"></param>
       /// <param name="detailsRapportId"></param>
       /// <returns></returns>
        public List<Tbl_DetailsRapport> getDetailsRapportDeroulement(long rapportId)
        {
            string methodName = "getDetailsRapportDeroulement";
            try
            {
                return repository.DetailsRapportDeroulementRepository.Find(d => d.RapportId == rapportId ).ToList();
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }

       /// <summary>
       /// Retourne tous les details des rapports
       /// </summary>
       /// <returns></returns>
        public List<Tbl_DetailsRapport> searchDetailsRapportDeroulement()
        {
            string methodName = "searchDetailsRapportDeroulement";
            try
            {
                return repository.DetailsRapportDeroulementRepository.Find().ToList();
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
        #endregion

       /// <summary>
       /// Retourne un detail sur un rapport de deroulement
       /// </summary>
       /// <param name="rapportId"></param>
       /// <param name="detailsRapportId"></param>
       /// <returns></returns>
        public Tbl_DetailsRapport getDetailRapportDeroulement(long rapportId, long detailsRapportId)
        {
            string methodName = "getDetailRapportDeroulement";
            try
            {
                return repository.DetailsRapportDeroulementRepository.Find(d=>d.RapportId==rapportId && d.DetailsRapportId==detailsRapportId).FirstOrDefault();
            }
            catch (Exception e)
            {
                log.Info("ERROR=>/" + CLASS_NAME + "/" + methodName + ":" + e.Message);
            }
            return null;
        }
   }
}
