using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
using ht.ihsi.rgph.epc.supervision.exceptions;
using ht.ihsi.rgph.epc.supervision.models;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.utils
{
   public class SqliteDataWriter:IsqliteDataWriter
    {
        public MainRepository repository;
        Logger log;
        private SqliteReader sr;
        private static string className = "SqliteDataWriter";

        public SqliteDataWriter()
        {

        }
       public SqliteDataWriter(string sdeId)
        {
            repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath,sdeId));
            log = new Logger();
        }
        public SqliteDataWriter(string path, string fileName)
        {
            repository = new MainRepository(Utilities.getConnectionString(path, fileName));
            log = new Logger();
        }
   
        public bool verified<T>(T obj, string sdeId)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                }
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.batimentId);
                    tbl_batiment batiment = repository.BatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isValidated = Convert.ToInt32(bat.isVerified);
                        repository.BatimentRepository.UpdateGB(batiment);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_LOGEMENT)
                {
                    LogementModel logm = obj as LogementModel;
                    tbl_logement logement = repository.LogementRepository.FindOne(logm.logeId);
                    if (logement.logeId != 0)
                    {
                        logement.isValidated = Convert.ToInt32(logm.isVerified);
                        repository.LogementRepository.UpdateGB(logement);
                        repository.SaveGB();
                        return true;
                    }

                }
                if (obj.ToString() == Constant.OBJET_MODEL_MENAGE)
                {
                    MenageModel men = obj as MenageModel;
                    tbl_menage menage = repository.MenageRepository.FindOne(men.menageId);
                    if (menage.menageId != 0)
                    {
                        menage.isValidated = Convert.ToInt32(men.isVerified);
                        repository.MenageRepository.UpdateGB(menage);
                        repository.SaveGB();
                        return true;
                    }

                }

            }
            catch (Exception ex)
            {
                log.Info("<>:SqliteDataWriter/validate:Error:" + ex.Message);
                throw new MessageException("Erreur lors de la sauvegarde" + ex.Message);
            }
            return false;
        }

        public bool changeStatus<T>(T obj, string sdeId)
        {
            throw new NotImplementedException();
        }

        public bool validate<T>(T obj, string sdeId)
        {
            throw new NotImplementedException();
        }

        public bool changeToVerified<T>(T obj, string sdeId, string path)
        {
            try
            {
                if (repository == null)
                {
                    repository = new MainRepository(Utilities.getConnectionString(path, sdeId));
                }
                #region VERIFIED BATIMENT
                if (obj.ToString() == Constant.OBJET_MODEL_BATIMENT)
                {
                    BatimentModel bat = obj as BatimentModel;
                    int batID = Convert.ToInt32(bat.batimentId);
                    tbl_batiment batiment = repository.BatimentRepository.Find(b => b.batimentId == batID).FirstOrDefault();
                    if (batiment.batimentId != 0)
                    {
                        batiment.isVerified = (int)Constant.StatutVerifie.Verifie;
                        repository.BatimentRepository.UpdateGB(batiment);
                        List<tbl_logement> logements = repository.LogementRepository.Find(l => l.batimentId == batiment.batimentId).ToList();
                        if (logements != null)
                        {
                            foreach (tbl_logement lg in logements)
                            {
                                if (lg.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                {
                                    lg.isVerified = (int)Constant.StatutVerifie.Verifie;
                                    repository.LogementRepository.UpdateGB(lg);
                                }
                                if (lg.qlin5NbreTotalMenage.GetValueOrDefault() != 0)
                                {
                                    List<tbl_menage> menages = repository.MenageRepository.Find(m => m.logeId == lg.logeId).ToList();
                                    if (menages != null)
                                    {
                                        foreach (tbl_menage men in menages)
                                        {
                                            if (men.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                            {
                                                men.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                repository.MenageRepository.UpdateGB(men);
                                            }

                                            //Changement de statut sur les deces
                                            if (men.qm22TotalAncienMembre.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_AncienMembre> ancienMembres = repository.AncienMembreRepository.Find(an => an.menageId == men.menageId).ToList();
                                                foreach (tbl_AncienMembre dec in ancienMembres)
                                                {
                                                    if (dec.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        dec.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                        repository.AncienMembreRepository.UpdateGB(dec);
                                                    }
                                                }
                                            }
                                            //Changement de statut sur les individus
                                            if (men.qm2TotalIndividuVivant.GetValueOrDefault() != 0)
                                            {
                                                List<tbl_individu> individus = repository.IndividuRepository.Find(em => em.menageId == men.menageId).ToList();
                                                foreach (tbl_individu ind in individus)
                                                {
                                                    if (ind.statut == Constant.STATUT_MODULE_KI_FINI_1)
                                                    {
                                                        ind.isVerified = (int)Constant.StatutVerifie.Verifie;
                                                        repository.IndividuRepository.UpdateGB(ind);
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        repository.SaveGB();
                        return true;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new MessageException("<=====================>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
                //log.Info("<>:SqliteDataWriter/changeStatus:Error:" + ex.Message);
            }
            return false;
        }
    }
}
