using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.services
{
    public interface IUtilisateurService
    {
        bool isSuperviseurAccountExist();
        bool isAsticAccountExist();
        UtilisateurModel authenticateUserLocally(string username, string password);
        void insertUser(UtilisateurModel utilisateur);
        void updateUser(UtilisateurModel utilisateur);
        UtilisateurModel findUserById(int idUser);
        UtilisateurModel findUserByUsername(string username);
        UtilisateurModel getSuperviseur(int profilId);
        List<Tbl_Agent> getAllAgentBySuperviseur(string supId);
        void deleteUser(long idUser);
    }
}
