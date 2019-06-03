using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.dao
{
    public interface IDaoUtilisateur
    {
        void insertUser(Tbl_Utilisateur utilisateur);
        void updateUser(Tbl_Utilisateur utilisateur);
        Tbl_Utilisateur findUserById(long idUser);
        Tbl_Utilisateur findUserByUsername(string username);
        void deleteUser(long idUser);
        List<Tbl_Utilisateur> findAllUser();
        List<Tbl_Agent> getAllAgentBySup(string supId);
        MainRepository getRepository();
    }
}
