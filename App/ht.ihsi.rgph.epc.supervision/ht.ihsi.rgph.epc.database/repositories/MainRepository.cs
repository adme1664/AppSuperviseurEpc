using ht.ihsi.rgph.epc.database.entities;
using Ht.Ihsi.Rgph.DataAccess.Repositories;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.repositories
{
    public class MainRepository:IDisposable
    {

        private GenericDatabaseContext context;
        private bool disposed = false;
        private GenericSupDatabaseContext supContext;

        #region Declarations of Mobiles tables
        private GenericRepository<tbl_batiment> batimentRepository;
        private GenericRepository<tbl_logement> logementRepository;
        private GenericRepository<tbl_menage> menageRepository;
        private GenericRepository<tbl_individu> individuRepository;
        private GenericRepository<tbl_AncienMembre> ancienMembreRepository;
        private GenericRepository<tbl_rapportfinal> rapportFinalRepository;
        private GenericRepository<tbl_pays> paysRepository;
        private GenericRepository<tbl_departement> departementRepository;
        private GenericRepository<tbl_commune> communeRepository;
        private GenericRepository<tbl_vqse> vqsesRepository;
        private GenericRepository<tbl_question> questionRepository;
        private GenericRepository<tbl_question_module> questionModuleRepository;
        private GenericRepository<tbl_question_reponse> questionReponseRepository;
        private GenericRepository<tbl_categorie_question> categorieQuestionRepository;
        private GenericRepository<tbl_domaine_etude> domaineEtudeRepository;
        #endregion

        #region Declarations of sup tables
        private GenericRepository<Tbl_Utilisateur> utilisateurRepository;
        private GenericRepository<Tbl_Agent> agentRepository;
        private GenericRepository<Tbl_Sde> sdeRepository;
        private GenericRepository<Tbl_Materiel> materielRepository;
        #endregion

        public MainRepository(string connectionString)
        {
            context = new GenericDatabaseContext(connectionString);
        }

        public MainRepository(string connectionString, bool typeDb)
        {
            supContext = new GenericSupDatabaseContext(connectionString);
        }

        #region Implementations 
        public GenericRepository<tbl_domaine_etude> DomaineEtudeRepository
        {
            get
            {
                if (this.domaineEtudeRepository == null)
                {
                    this.domaineEtudeRepository = new GenericRepository<tbl_domaine_etude>(context);
                }
                return domaineEtudeRepository;
            }
        }
        public GenericRepository<tbl_batiment> BatimentRepository
        {
            get
            {
                if (this.batimentRepository == null)
                {
                    this.batimentRepository = new GenericRepository<tbl_batiment>(context);
                }
                return batimentRepository;
            }
        }
        public GenericRepository<tbl_rapportfinal> RapportFinalRepository
        {
            get
            {
                if (this.rapportFinalRepository == null)
                {
                    this.rapportFinalRepository = new GenericRepository<tbl_rapportfinal>(context);
                }
                return rapportFinalRepository;
            }
        }
        public GenericRepository<Tbl_Materiel> MaterielRepository
        {
            get
            {
                if (this.materielRepository == null)
                {
                    this.materielRepository = new GenericRepository<Tbl_Materiel>(this.supContext);
                }
                return materielRepository;
            }
        }
        public GenericRepository<tbl_logement> LogementRepository
        {
            get
            {
                if (this.logementRepository == null)
                {
                    this.logementRepository = new GenericRepository<tbl_logement>(context);
                }
                return logementRepository;
            }
        }
        public GenericRepository<tbl_menage> MenageRepository
        {
            get
            {
                if (this.menageRepository == null)
                {
                    this.menageRepository = new GenericRepository<tbl_menage>(context);
                }
                return menageRepository;
            }
        }
        public GenericRepository<tbl_individu> IndividuRepository
        {
            get
            {
                if (this.individuRepository == null)
                {
                    this.individuRepository = new GenericRepository<tbl_individu>(context);
                }
                return individuRepository;
            }
        }
        public GenericRepository<tbl_AncienMembre> AncienMembreRepository
        {
            get
            {
                if (this.ancienMembreRepository == null)
                {
                    this.ancienMembreRepository = new GenericRepository<tbl_AncienMembre>(context);
                }
                return ancienMembreRepository;
            }
        }
        public GenericRepository<tbl_question> QuestionRepository
        {
            get
            {
                if (this.questionRepository == null)
                {
                    this.questionRepository = new GenericRepository<tbl_question>(context);
                }
                return questionRepository;
            }
        }
        public GenericRepository<tbl_question_module> QuestionModuleRepository
        {
            get
            {
                if (this.questionModuleRepository == null)
                {
                    this.questionModuleRepository = new GenericRepository<tbl_question_module>(context);
                }
                return questionModuleRepository;
            }
        }
        public GenericRepository<tbl_question_reponse> QuestionReponseRepository
        {
            get
            {
                if (this.questionReponseRepository == null)
                {
                    this.questionReponseRepository = new GenericRepository<tbl_question_reponse>(context);
                }
                return questionReponseRepository;
            }
        }
        public GenericRepository<tbl_categorie_question> CategorieQuestionRepository
        {
            get
            {
                if (this.categorieQuestionRepository == null)
                {
                    this.categorieQuestionRepository = new GenericRepository<tbl_categorie_question>(context);
                }
                return categorieQuestionRepository;
            }
        }

        public GenericRepository<tbl_pays> PaysRepository
        {
            get
            {
                if (this.paysRepository == null)
                {
                    this.paysRepository = new GenericRepository<tbl_pays>(context);
                }
                return paysRepository;
            }
        }
       public GenericRepository<tbl_departement> DepartementRepository
        {
            get
            {
                if (this.departementRepository == null)
                {
                    this.departementRepository = new GenericRepository<tbl_departement>(context);
                }
                return departementRepository;
            }
        }
        public GenericRepository<tbl_commune> CommuneRepository
        {
            get
            {
                if (this.communeRepository == null)
                {
                    this.communeRepository = new GenericRepository<tbl_commune>(context);
                }
                return communeRepository;
            }
        }

        public GenericRepository<tbl_vqse> VqseRepository
        {
            get
            {
                if (this.vqsesRepository == null)
                {
                    this.vqsesRepository = new GenericRepository<tbl_vqse>(context);
                }
                return vqsesRepository;
            }
        }

        public GenericRepository<Tbl_Sde> SdeRepository
        {
            get
            {
                if (this.sdeRepository == null)
                {
                    this.sdeRepository = new GenericRepository<Tbl_Sde>(this.supContext);
                }
                return this.sdeRepository;
            }
        }

        public GenericRepository<Tbl_Agent> AgentRepository
        {
            get
            {
                if (this.agentRepository == null)
                {
                    this.agentRepository = new GenericRepository<Tbl_Agent>(this.supContext);
                }
                return this.agentRepository;
            }

        }

        public GenericRepository<Tbl_Utilisateur> UtilisateurRepository
        {
            get
            {
                if (utilisateurRepository == null)
                {
                    this.utilisateurRepository = new GenericRepository<Tbl_Utilisateur>(this.supContext);
                }
                return this.utilisateurRepository;

            }

        }

        #endregion
        public GenericDatabaseContext Context
        {
            get
            {
                return this.context;
            }
        }

        public GenericSupDatabaseContext SupContext
        {
            get
            {
                return this.supContext;
            }
        }
        /// <summary>
        /// Enregistrer les modifications dans la base de données.
        /// </summary>
        public void SaveGB()
        {
            context.SaveChanges();
        }
        public void Save()
        {
            Logger log = new Logger();
            try
            {
                supContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        log.Info("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
