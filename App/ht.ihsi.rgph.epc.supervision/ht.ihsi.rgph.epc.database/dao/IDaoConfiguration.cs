using ht.ihsi.rgph.epc.database.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.dao
{
    public interface IDaoConfiguration
    {
        #region GESTION DES SDES
        Tbl_Sde getSdeDetails(string sdeId);
        Tbl_Sde getSdeByAgent(long agentId);
        List<Tbl_Sde> searchAllSdes();
        bool saveSdeDetails(Tbl_Sde sde);
        #endregion

        #region GESTION DES AGENTS
        Tbl_Agent insertAgent(Tbl_Agent agent);
        void updateAgent(Tbl_Agent agent);
        Tbl_Agent findAgentById(long agentId);
        Tbl_Agent findAgentByUsername(string username);
        void deleteAgent(long agentId);
        List<Tbl_Agent> searchAllAgents();
        bool isAgentGotDevice(int agentId);
        #endregion

        #region GESTION DES TABLETTES
        bool saveMateriels(Tbl_Materiel materiels);
        bool updateMateriels(Tbl_Materiel materiels);
        Tbl_Materiel getMateriels(string serial);
        Tbl_Materiel getMaterielByAgent(long agentId);
        List<Tbl_Materiel> searchMateriels();
        bool deleteMateriel(int materielId);
        #endregion
    }
}
