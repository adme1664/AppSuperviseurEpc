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
    public class DaoUtilisateur:  IDaoUtilisateur
    {
        private MainRepository repository;
        private Logger log;
        public DaoUtilisateur(string connectionString)
        {
            repository = new MainRepository(connectionString,true);
            log = new Logger();
        }


        public MainRepository getRepository()
        {
            return repository;
        }
        public void insertUser(entities.Tbl_Utilisateur utilisateur)
        {
            try
            {
                if (Utils.IsNotNull(utilisateur))
                {
                    log.Info("<>======Info user:" + utilisateur.Nom);
                    repository.UtilisateurRepository.Insert(utilisateur);
                    repository.SupContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Info("<>=================Insert user Error:" + ex.Message);
                log.Info("<>=================Insert user Error:" + ex.InnerException);
            }
        }

        public void updateUser(entities.Tbl_Utilisateur utilisateur)
        {
            try
            {
                if (Utils.IsNotNull(utilisateur))
                {
                    Tbl_Utilisateur u = findUserById(utilisateur.UtilisateurId);
                    u.MotDePasse = utilisateur.MotDePasse;
                    u.Nom = utilisateur.Nom;
                    u.Prenom = utilisateur.Prenom;
                    u.ProfileId = utilisateur.ProfileId;
                    u.Statut = utilisateur.Statut;
                    u.UtilisateurId = utilisateur.UtilisateurId;
                    repository.UtilisateurRepository.Update(u);
                    repository.Save();
                }
            }
            catch (Exception)
            {

            }
        }

        public entities.Tbl_Utilisateur findUserById(long idUser)
        {
            return repository.UtilisateurRepository.FindOne(idUser);
        }

        public entities.Tbl_Utilisateur findUserByUsername(string username)
        {
            try
            {
                IQueryable<Tbl_Utilisateur> query = repository.UtilisateurRepository.supDatabaseContext.Tbl_Utilisateur;
                query = query.Where(u => u.CodeUtilisateur == username);
                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public void deleteUser(long idUser)
        {
            throw new NotImplementedException();
        }

        public List<entities.Tbl_Utilisateur> findAllUser()
        {
            throw new NotImplementedException();
        }

        public List<entities.Tbl_Agent> getAllAgentBySup(string supId)
        {
            IQueryable<Tbl_Agent> query = repository.AgentRepository.supDatabaseContext.Tbl_Agent;
            return query.ToList();
        }

       
    }
}
