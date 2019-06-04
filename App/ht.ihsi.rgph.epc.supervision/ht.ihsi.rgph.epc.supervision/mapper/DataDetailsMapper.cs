using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.mapper
{
   public class DataDetailsMapper
    {
      static Logger log;
       public DataDetailsMapper()
       {
           log = new Logger();
       }

       public static List<DataDetails> MapToMobile<T>(T obj, string sdeId)
       {
           SqliteDataReaderService service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
           List<tbl_question_module> listOf = new List<tbl_question_module>();
           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Batiment))
           {
               listOf = service.listOfQuestionModule(Constant.GetStringValue(Constant.ModuleQuestionnaire.Batiment), sdeId);
           }
           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.AncienMembre))
           {
               listOf = service.listOfQuestionModule(Constant.GetStringValue(Constant.ModuleQuestionnaire.AncienMembre), sdeId);
           }
           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
           {
               listOf = service.listOfQuestionModule(Constant.GetStringValue(Constant.ModuleQuestionnaire.Individu), sdeId);
           }
           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Logement))
           {
               listOf = service.listOfQuestionModule(Constant.GetStringValue(Constant.ModuleQuestionnaire.Logement), sdeId);
           }
           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Menage))
           {
               listOf = service.listOfQuestionModule(Constant.GetStringValue(Constant.ModuleQuestionnaire.Menage), sdeId);
           }

           List<DataDetails> reponses = new List<DataDetails>();
           try
           {
               if (listOf.Count != 0)
               {
                   foreach (tbl_question_module qm in listOf)
                   {
                       tbl_question question = service.sr.getQuestion(qm.codeQuestion);
                       if (question != null)
                       {
                           tbl_categorie_question tcq = service.sr.getCategorie(question.codeCategorie);
                           string nomChamps = question.nomChamps.Remove(1).ToLower() + question.nomChamps.Substring(1);
                           PropertyInfo property = obj.GetType().GetProperty(nomChamps);
                           if (question.typeQuestion == (int)Constant.TypeQuestionMobile.Choix || question.typeQuestion == 4 || question.typeQuestion==1)
                           {
                               if (question.nomChamps.Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                               {
                                   string reponse = "";
                                   object valeur = obj.GetType().GetProperty(property.Name).GetValue(obj);
                                   if (valeur == null)
                                   {
                                   }
                                   else
                                   {
                                       reponse = service.sr.getReponse(question.codeQuestion, obj.GetType().GetProperty(property.Name).GetValue(obj).ToString());
                                   }
                                   if (question.detailsQuestion != "")
                                   {
                                       if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                       {
                                           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
                                           {
                                               IndividuModel ind = obj as IndividuModel;
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), reponse, tcq.detailsCategorie));
                                           }
                                           else
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), reponse, tcq.detailsCategorie));
                                       }
                                       else
                                       {
                                           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
                                           {
                                               IndividuModel ind = obj as IndividuModel;
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), reponse, tcq.categorieQuestion));
                                           }
                                           else
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), reponse, tcq.categorieQuestion));
                                       }
                                   }
                                   else
                                   {
                                       if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                       {
                                           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
                                           {
                                               IndividuModel ind = obj as IndividuModel;
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom)), reponse, tcq.detailsCategorie));
                                           }
                                           else
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.detailsCategorie));
                                       }
                                       else
                                       {
                                           if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
                                           {
                                               IndividuModel ind = obj as IndividuModel;
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom)), reponse, tcq.categorieQuestion));
                                           }
                                           else
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.categorieQuestion));
                                       }
                                   }
                               }
                           }
                           //
                           //Question Pays
                           if (question.typeQuestion.GetValueOrDefault() == 5)
                           {
                               if (question.nomChamps == property.Name)
                               {
                                   if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                   {
                                       if (question.detailsQuestion != "")
                                       {
                                           if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                           {
                                               string pays = service.sr.getpays(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomPays;
                                               if (obj.ToString() == Constant.GetStringValue(Constant.ModuleQuestionnaire.Individu))
                                               {
                                                   IndividuModel ind = obj as IndividuModel;
                                                   DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), pays, tcq.detailsCategorie);
                                                   reponses.Add(detail);
                                               }
                                               else
                                                   reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), pays, tcq.detailsCategorie));
                                           }
                                           else
                                           {
                                               string pays = service.sr.getpays(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomPays;
                                               if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Individu))
                                               {
                                                   IndividuModel ind = obj as IndividuModel;
                                                   DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), pays, tcq.categorieQuestion);
                                                   reponses.Add(detail);
                                               }
                                           }
                                       }

                                   }
                               }

                           }
                           //
                           //

                           //
                           //Question Communes
                           if (question.typeQuestion.GetValueOrDefault() == 7)
                           {
                               IndividuModel ind = obj as IndividuModel;
                               if (question.nomChamps == property.Name)
                               {
                                   if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                   {
                                       if (question.detailsQuestion != "")
                                       {
                                           if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                           {
                                               string commune = service.sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), commune, tcq.detailsCategorie);
                                               reponses.Add(detail);
                                           }
                                           else
                                           {
                                               string commune = service.sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom) + "__________________:" + question.detailsQuestion), commune, tcq.categorieQuestion);
                                               reponses.Add(detail);
                                           }
                                       }
                                       else
                                       {
                                           if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                           {
                                               string commune = service.sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom)), commune, tcq.detailsCategorie);
                                               reponses.Add(detail);
                                           }
                                           else
                                           {
                                               string commune = service.sr.getCommune(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).ComNom;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower().Replace("{0}", ind.qp2APrenom)), commune, tcq.categorieQuestion);
                                               reponses.Add(detail);
                                           }
                                       }

                                   }
                               }

                           }
                           //
                           //

                           //
                           //Question Communes
                           if (question.typeQuestion.GetValueOrDefault() == 8)
                           {
                               if (nomChamps == property.Name)
                               {
                                   if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                   {
                                       if (question.detailsQuestion != "")
                                       {
                                           if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                           {
                                               string domaine = service.sr.getDomaine(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomDomaine;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), domaine, tcq.detailsCategorie);
                                               reponses.Add(detail);
                                           }
                                           else
                                           {
                                               string domaine = service.sr.getDomaine(obj.GetType().GetProperty(property.Name).GetValue(obj).ToString()).NomDomaine;
                                               DataDetails detail = new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), domaine, tcq.categorieQuestion);
                                               reponses.Add(detail);
                                           }
                                       }

                                   }
                               }

                           }
                           //
                           //

                           if (question.typeQuestion == (int)Constant.TypeQuestionMobile.Saisie 
                               || question.typeQuestion == 22 
                               || question.typeQuestion == 13 
                               || question.typeQuestion == 19
                               || question.typeQuestion == 23)
                           {
                               if (property != null)
                               {
                                   if (nomChamps == property.Name)
                                   {
                                       if (question.detailsQuestion != "")
                                       {
                                           if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null)
                                           {
                                               if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                               {
                                                   reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), tcq.detailsCategorie));
                                               }
                                               else
                                               {
                                                   reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower() + "__________________:" + question.detailsQuestion), obj.GetType().GetProperty(property.Name).GetValue(obj).ToString(), tcq.categorieQuestion));
                                               }
                                           }
                                       }
                                       else
                                       {
                                           if (tcq.categorieQuestion != "" && tcq.detailsCategorie != "")
                                           {
                                               string reponse = "";
                                               if (obj.GetType().GetProperty(property.Name).GetValue(obj) == null)
                                                   reponse = "";
                                               else
                                                   reponse = obj.GetType().GetProperty(property.Name).GetValue(obj).ToString();

                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.detailsCategorie));
                                           }
                                           else
                                           {
                                               string reponse = "";
                                               if (obj.GetType().GetProperty(property.Name).GetValue(obj).ToString() == null)
                                                   reponse = "";
                                               else
                                                   reponse = obj.GetType().GetProperty(property.Name).GetValue(obj).ToString();
                                               reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion + "-" + question.libelle.ToLower()), reponse, tcq.categorieQuestion));
                                           }
                                       }
                                   }
                               }
                               
                               else
                               {
                                   if (nomChamps == "qp5JourMoisAnneeDateNaissance")
                                   {
                                       IndividuModel ind = obj as IndividuModel;
                                       reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion+"-"+question.libelle.ToLower()),ind.q7DateNaissanceJour+"-"+ind.q7DateNaissanceMois+"-"+ind.q7DateNaissanceAnnee,tcq.categorieQuestion));
                                   }
                                   if (nomChamps == "q6bJourMoisAnneeDateMembreMenage")
                                   {
                                       IndividuModel ind = obj as IndividuModel;
                                       reponses.Add(new DataDetails(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(question.codeQuestion+"-"+question.libelle.ToLower()),ind.q6bDateMembreMenageJour+"-"+ind.q6bDateMembreMenageJour+"-"+ind.q6bDateMembreMenageAnnee,tcq.categorieQuestion));
                                   }

                               }
                           }
                       }
                   }
               }
               if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Batiment))
               {
                   tbl_categorie_question tcq = service.sr.getCategorie("LOC");
                   BatimentModel bat = obj as BatimentModel;
                   service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, sdeId));
                   SdeModel sdeInformation = Utilities.getSdeInformation(Utilities.getSdeFormatWithDistrict(sdeId));
                   string comId = service.sr.getCommune(sdeInformation.ComId).ComNom;
                   string deptId = service.sr.getDepartement(sdeInformation.DeptId).DeptNom;
                   string vqse = service.sr.getVqse(sdeInformation.VqseId).VqseNom;
                   if (sdeInformation != null)
                   {
                       reponses.Add(new DataDetails("Depatman:", deptId, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Komin:", comId, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Seksyon Kominal:", vqse, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Distri:", bat.disctrictId, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Bitasyon:", bat.qhabitation, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Lokalite:", bat.qlocalite, tcq.detailsCategorie));
                       reponses.Add(new DataDetails("Adrès:", bat.qadresse, tcq.detailsCategorie));

                   }


               }
           }
           catch (Exception ex)
           {
               log.Info("Erreur:" + ex.Message);
           }
           return reponses;
       }

    }
}
