using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.database.repositories;
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
   public class SqliteDataReaderService
    {
        private MainRepository sqliteRepository;
        private ILogger log;
        public ISqliteReader sr;


        public SqliteDataReaderService(string connectionString)
        {
            Users.users.SupDatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            sqliteRepository = new MainRepository(Users.users.SupDatabasePath, true);
            log = new Logger();
            sr = new SqliteReader(connectionString);
        }
       public SqliteDataReaderService()
       {
       }

       #region LOGEMENTS
       //retourne les types de logements
       public LogementTypeModel[] getLogementType()
       {
           return new LogementTypeModel[]
            {
                new LogementTypeModel("Lojman Endividyel")
            };
       }

       //retourne tous les batiments sous forme de tableau
       public BatimentModel[] getAllBatiments()
       {
           try
           {
               if (sr.GetAllBatimentModel() == null)
                   return null;
               else
               {
                   return sr.GetAllBatimentModel().ToArray();
               }

           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getAllBatiments" + ex.Message);
           }
           return null;
       }
       //
       //Retourne tous les types logements
       public LogementModel[] getAllLogement(BatimentModel _batiment, LogementTypeModel _logementType)
       {
           switch (_logementType.LogementName)
           {
               case "Lojman Endividyel":
                   return getAllLogementIndividuel(_batiment);
            }
           return null;
       }
       //
       //
       //Retourne tous les logements individuels
       public LogementModel[] getAllLogementIndividuel(BatimentModel _batiment)
       {
           try
           {
               if (sr == null)
               {
                   sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.sdeId));
               }
               return sr.GetLogementIByBatiment(Convert.ToInt32(_batiment.batimentId)).ToArray();
           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getAllLogementIndividuel" + ex.Message);
           }
           return null;
       }

       #endregion

       #region MENAGES...
       public MenageModel[] getAllMenage(LogementModel _logement)
       {
           try
           {
               if (sr == null)
               {
                   sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _logement.sdeId));
               }
               return sr.GetMenagesByLogement(Convert.ToInt32(_logement.logeId)).ToArray();
           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getAllMenage" + ex.Message);
           }
           return null;
       }
       //
       //Retourne les types objets dans le menage
       public MenageTypeModel[] getAllInMenage()
       {
           return new MenageTypeModel[]
            {
                new MenageTypeModel("Ansyen manm"),
                new MenageTypeModel("Endividi"),
                new MenageTypeModel("Rapo"),
            };
       }
       //
       //Retourne tous les elements a l'interieur d'un menage
       public MenageDetailsModel[] getDetailsMenage(MenageModel _model, MenageTypeModel _menageType)
       {
           switch (_menageType.Name)
           {
               case "Rapo":
                   return getRapportFinal(_model);
               case "Ansyen manm":
                   return getAnsyenManmForMenage(_model);

                case "Endividi":
                   return getIndividuForMenage(_model);

           }
           return null;
       }

       public MenageDetailsModel[] getRapportFinal(MenageModel _model)
       {
           try
           {
               if (sr == null)
               {
                   sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.sdeId));
               }
               return sr.GetRapportFinal(Convert.ToInt32(_model.menageId)).ToArray();

           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getRapportFinal" + ex.Message);
           }
           return null;
       }
       public MenageDetailsModel[] getAnsyenManmForMenage(MenageModel _model)
       {
           try
           {
               if (sr == null)
               {
                   sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.sdeId));
               }
               return sr.GetAncienMembreByMenageDetails(Convert.ToInt32(_model.menageId)).ToArray();

           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getRapportFinal" + ex.Message);
           }
           return null;
       }
       public MenageDetailsModel[] getIndividuForMenage(MenageModel _model)
       {
           try
           {
               if (sr == null)
               {
                   sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, _model.sdeId));
               }
               return sr.GetIndividuByMenageDetails(Convert.ToInt32(_model.menageId)).ToArray();

           }
           catch (Exception ex)
           {
               log.Info("SqliteDataReaderService/getRapportFinal" + ex.Message);
           }
           return null;
       }
       #endregion

        #region QUESTIONS
       public List<tbl_question_module> listOfQuestionModule(string codeModule, string sdeId)
       {
           if (sr == null)
           {
               sr = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
           }
           return sr.listOfQuestionModule(codeModule);
       }
        #endregion
    }
}
