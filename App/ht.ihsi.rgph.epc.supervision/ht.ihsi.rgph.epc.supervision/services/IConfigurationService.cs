using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.beans;
using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.services
{
    public interface IConfigurationService
    {
        List<SdeModel> searchAllSdes();
        SdeModel getSdeByAgent(long agentId);
        bool saveSdeDetails(Tbl_Sde sde);
        SdeModel getSdeDetails(string sdeId);

        #region GESTIONS DES AGENTS
        AgentModel insertAgentSde(AgentModel agent);
        List<AgentModel> searchAllAgents();
        List<AgentModel> searchAllAgentsToDisplay();
        void updateAgentSde(AgentModel agent);
        AgentModel findAgentSderById(long agentId);
        AgentModel findAgentByUsername(string username);
        void deleteAgentSde(long agentId);
        bool isAgentExist(int agentId);
        #endregion

        #region SUPERVISUERS ET AGENTS
        bool savePersonne(tbl_personnel person);
        bool ifPersonExist(tbl_personnel person);
        bool ifSuperviseurExist(int profilId);
        bool deleteSuperviseur(string username);
        Utilisateur getSuperviseur(int profilId);
        #endregion

        #region GESTION DES MATERIELS
        MaterielModel getMateriel(string serialNumber);
        MaterielModel getMaterielByAgent(long agentId);
        bool isMaterielExist(string serial);
        bool isMaterielConfigure(string serial);
        List<MaterielModel> SearchMateriels();
        bool saveMateriel(Tbl_Materiel materiel);
        bool updateMateriel(Tbl_Materiel materiel);
        bool deleteMateriel(int id);
        #endregion
    }
}
