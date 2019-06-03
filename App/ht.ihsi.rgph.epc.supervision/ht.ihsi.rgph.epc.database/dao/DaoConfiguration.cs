using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.dao
{
    public class DaoConfiguration:IDaoConfiguration
    {

        #region DECLARATIONS
        MainRepository repository;
        Logger log;
        #endregion

        #region CONSTRUCTORS
        public DaoConfiguration(string connectionString)
        {
            repository = new MainRepository(connectionString, true);
            log = new Logger();
        }
        #endregion

        #region SDES
        public entities.Tbl_Sde getSdeDetails(string sdeId)
        {
            return repository.SdeRepository.FindOne(sdeId);
        }
        public bool saveSdeDetails(Tbl_Sde sde)
        {
            try
            {
                repository.SdeRepository.Insert(sde);
                repository.Save();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public entities.Tbl_Sde getSdeByAgent(long agentId)
        {
            return repository.SdeRepository.Find(s => s.AgentId == agentId).FirstOrDefault();
        }

        public List<entities.Tbl_Sde> searchAllSdes()
        {
            return repository.SdeRepository.Find().ToList();
        }
#endregion

        #region AGENTS

        public entities.Tbl_Agent insertAgent(entities.Tbl_Agent agent)
        {
            try
            {
                if (Utils.IsNotNull(agent))
                {
                    Tbl_Agent a = repository.AgentRepository.Insert(agent);
                    repository.Save();
                    return a;
                }
            }
            catch (Exception)
            {

            }
            return new Tbl_Agent();
        }

        public void updateAgent(entities.Tbl_Agent agent)
        {
            try
            {
                if (Utils.IsNotNull(agent))
                {
                    Tbl_Agent a = findAgentById(agent.AgentId);
                    a.Cin = a.Cin;
                    a.CodeUtilisateur = agent.CodeUtilisateur;
                    a.Email = agent.Email;
                    a.MotDePasse = agent.MotDePasse;
                    a.Nif = agent.Nif;
                    a.Nom = agent.Nom;
                    a.Prenom = agent.Prenom;
                    a.Sexe = agent.Sexe;
                    a.Telephone = agent.Telephone;
                    repository.AgentRepository.Update(a);
                    repository.Save();
                }
            }
            catch (Exception)
            {

            }
        }

        public entities.Tbl_Agent findAgentById(long agentId)
        {
            return repository.AgentRepository.FindOne(agentId);
        }

        public entities.Tbl_Agent findAgentByUsername(string username)
        {
            return repository.AgentRepository.Find(a => a.CodeUtilisateur == username).FirstOrDefault();
        }

        public void deleteAgent(long agentId)
        {
            try
            {
                Tbl_Agent agent = findAgentById(agentId);
                if (Utils.IsNotNull(agent))
                {
                    repository.AgentRepository.Delete(agent);
                }
            }
            catch (Exception)
            {

            }
        }
        public List<entities.Tbl_Agent> searchAllAgents()
        {
            try
            {
                return repository.AgentRepository.Find().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public bool isAgentGotDevice(int agentId)
        {
            try
            {
                Tbl_Materiel matForAgent = repository.MaterielRepository.Find(m => m.AgentId == agentId).FirstOrDefault();
                if (matForAgent != null)
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        #endregion

        #region MATERIELS
        public bool saveMateriels(Tbl_Materiel materiels)
        {
            try
            {
                repository.MaterielRepository.Insert(materiels);
                repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                log.Info("<>===========Error:" + ex.Message);
            }
            return false;
        }

        public bool updateMateriels(Tbl_Materiel materiels)
        {
            try
            {
                Tbl_Materiel mat = repository.MaterielRepository.Find(m => m.Serial == materiels.Serial).FirstOrDefault();
                mat.LastSynchronisation = materiels.LastSynchronisation;
                mat.AgentId = materiels.AgentId;
                mat.Version = materiels.Version;
                //mat.IsConfigured = materiels.IsConfigured;
                repository.MaterielRepository.Update(mat);
                repository.Save();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        public Tbl_Materiel getMateriels(string serial)
        {
            return repository.MaterielRepository.Find(m => m.Serial == serial).FirstOrDefault();
        }

        public List<Tbl_Materiel> searchMateriels()
        {
            return repository.MaterielRepository.Find().ToList();
        }
        public Tbl_Materiel getMaterielByAgent(long agentId)
        {
            try
            {
                return repository.MaterielRepository.Find(m => m.AgentId == agentId).FirstOrDefault();
            }
            catch (Exception e)
            {
                log.Info("Unable this materiel==>" + e.Message);
            }
            return null;
        }
        public bool deleteMateriel(int materielId)
        {
            try
            {
                repository.MaterielRepository.Delete(materielId);
                repository.Save();
                return true;
            }
            catch (Exception e)
            {
                log.Info("Unable to delete this materiel==>" + e.Message);
            }
            return false;
        }
        #endregion





        
    }
}
