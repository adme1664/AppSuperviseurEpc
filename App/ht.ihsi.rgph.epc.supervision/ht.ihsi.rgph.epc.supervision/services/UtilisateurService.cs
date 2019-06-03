using ht.ihsi.rgph.epc.database.dao;
using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.services
{
    public class UtilisateurService: IUtilisateurService
    {
        private DaoUtilisateur dao;
        private Logger log;
        public UtilisateurService()
        {
            string path = Utilities.getConnectionString(Users.users.SupDatabasePath);
            dao = new DaoUtilisateur(path);
            log = new Logger(); 
        }
        public bool isSuperviseurAccountExist()
        {
            try
            {
                int profileId= (int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG;
                if (dao.getRepository().UtilisateurRepository.Find(u => u.ProfileId ==profileId).Count() > 0) return true;
            }
            catch (Exception ex)
            {
                log.Info("ERROR:" + ex.Message);
            }
            
            return false;
        }

        public bool isAsticAccountExist()
        {
            if (dao.getRepository().UtilisateurRepository.Find(u => u.ProfileId == (int)Constant.ProfileUtilisateur.PROFIL_ASTIC).Count() > 0) return true;
            return false;
        }

        public models.UtilisateurModel authenticateUserLocally(string username, string password)
        {
            log.Info("<>======authenticateUserLocally avant:");
            UtilisateurModel user = findUserByUsername(username);
            if (Utils.IsNotNull(user))
            {
                log.Info("<>=======Password : " + password + " Pwd encryted : " + user.MotDePasse);

                //if (user.MotDePasse==MD5Encoder.encode(Password))
                if (user.MotDePasse == password)
                {
                    log.Info("<>==========OK");
                    return user;
                }
            }
            return new UtilisateurModel();
        }

        public void insertUser(models.UtilisateurModel utilisateur)
        {
            dao.insertUser(ModelMapper.MapTo(utilisateur));
        }

        public void updateUser(models.UtilisateurModel utilisateur)
        {
            dao.updateUser(ModelMapper.MapTo(utilisateur));
        }

        public models.UtilisateurModel findUserById(int idUser)
        {
            return ModelMapper.MapTo(dao.findUserById(idUser));
        }

        public models.UtilisateurModel findUserByUsername(string username)
        {
            return ModelMapper.MapTo(dao.findUserByUsername(username));
        }

        public models.UtilisateurModel getSuperviseur(int profilId)
        {
            try
            {
                return ModelMapper.MapTo(dao.getRepository().UtilisateurRepository.Find(u => u.ProfileId == (int)Constant.ProfileUtilisateur.PROFIL_SUPERVISEUR_SUPERVISION_SG).FirstOrDefault());
            }
            catch (Exception)
            {

            }
            return new UtilisateurModel();
        }   

        public List<database.entities.Tbl_Agent> getAllAgentBySuperviseur(string supId)
        {
            return dao.getAllAgentBySup(supId);
        }

        public void deleteUser(long idUser)
        {
            throw new NotImplementedException();
        }
    }
}
