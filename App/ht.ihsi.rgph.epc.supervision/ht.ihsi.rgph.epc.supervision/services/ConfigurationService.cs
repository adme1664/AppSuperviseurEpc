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
    public class ConfigurationService:IConfigurationService
    {

        #region DECLARATIONS VARIABLES
        private IDaoConfiguration daoConfiguration;
        Logger log;
        #endregion

        #region CONSTRUCTORS
        public ConfigurationService()
        {
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            daoConfiguration = new DaoConfiguration(path);
            log = new Logger();
        }
        #endregion
       
        #region AGENTS
        public models.SdeModel getSdeByAgent(long agentId)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.getSdeByAgent(agentId));
            }
            catch (Exception)
            {

            }
            return new models.SdeModel();
        }

        public models.AgentModel insertAgentSde(models.AgentModel agent)
        {
            throw new NotImplementedException();
        }

        public List<models.AgentModel> searchAllAgents()
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.searchAllAgents());
            }
            catch (Exception)
            {

            }
            return new List<AgentModel>();
        }

        public List<models.AgentModel> searchAllAgentsToDisplay()
        {
            throw new NotImplementedException();
        }

        public void updateAgentSde(models.AgentModel agent)
        {
            try
            {
                daoConfiguration.updateAgent(ModelMapper.MapTo(agent));
            }
            catch (Exception)
            {

            }
        }

        public models.AgentModel findAgentSderById(long agentId)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.findAgentById(agentId));
            }
            catch (Exception e)
            {

            }
            return new AgentModel();
        }

        public models.AgentModel findAgentByUsername(string username)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.findAgentByUsername(username));
            }
            catch (Exception e)
            {

            }
            return new AgentModel();
        }

        public void deleteAgentSde(long agentId)
        {
            throw new NotImplementedException();
        }

        public bool isAgentExist(int agentId)
        {
            try
            {
                return daoConfiguration.isAgentGotDevice(agentId);
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion

        #region PERSONNES   
        public bool savePersonne(database.entities.tbl_personnel person)
        {
            throw new NotImplementedException();
        }

        public bool ifPersonExist(database.entities.tbl_personnel person)
        {
            throw new NotImplementedException();
        }

        public bool ifSuperviseurExist(int profilId)
        {
            throw new NotImplementedException();
        }

        public bool deleteSuperviseur(string username)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SDE
        public List<models.SdeModel> searchAllSdes()
        {
            List<SdeModel> sdes = new List<SdeModel>();
            try
            {
                return ModelMapper.MapTo(daoConfiguration.searchAllSdes());
            }
            catch (Exception)
            {

            }
            return new List<models.SdeModel>();
        }
        public bool saveSdeDetails(database.entities.Tbl_Sde sde)
        {
            try
            {
                return daoConfiguration.saveSdeDetails(sde);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public SdeModel getSdeDetails(string sdeId)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.getSdeDetails(sdeId));

            }
            catch (Exception)
            {

            }
            return null;
        }
        #endregion

        #region MATERIELS
        public MaterielModel getMateriel(string serialNumber)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.getMateriels(serialNumber));
            }
            catch (Exception e)
            {
                log.Info("Unable this materiel===>"+e.Message);
            }
            return new MaterielModel();
        }


        public MaterielModel getMaterielByAgent(long agentId)
        {
            try
            {
                return ModelMapper.MapTo(daoConfiguration.getMaterielByAgent(agentId));
            }
            catch (Exception e)
            {
                log.Info("Unable this materiel===>" + e.Message);
            }
            return new MaterielModel();
        }
        

        public bool isMaterielConfigure(string serial)
        {
            try
            {
                MaterielModel mat = ModelMapper.MapTo(daoConfiguration.getMateriels(serial));
                if (mat.IsConfigured.GetValueOrDefault() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("isMaterielConfigure/Error:" + ex.Message);
            }
            return false;
        }

        public List<MaterielModel> SearchMateriels()
        {
            throw new NotImplementedException();
        }

        public bool saveMateriel(database.entities.Tbl_Materiel materiel)
        {
            try
            {
                return daoConfiguration.saveMateriels(materiel);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool updateMateriel(database.entities.Tbl_Materiel materiel)
        {
            try
            {
                return daoConfiguration.updateMateriels(materiel);
            }
            catch (Exception)
            {

            }
            return false;
        }

        public beans.Utilisateur getSuperviseur(int profilId)
        {
            throw new NotImplementedException();
        }

        public bool isMaterielExist(string serial)
        {
            try
            {
                MaterielModel mat = ModelMapper.MapTo(daoConfiguration.getMateriels(serial));
                if (mat != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Info("isMaterielExist/Error:" + ex.Message);
            }
            return false;
        }

        public bool deleteMateriel(int id)
        {
            try
            {
                return daoConfiguration.deleteMateriel(id);
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion


    }
}
