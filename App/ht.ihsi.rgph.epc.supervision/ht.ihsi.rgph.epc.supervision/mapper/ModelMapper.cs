using ht.ihsi.rgph.epc.database.entities;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.mapper
{
    public class ModelMapper
    {
        #region TABLES MOBILES
        public static BatimentModel MapTo(tbl_batiment batiment)
        {
            BatimentModel model = new BatimentModel();
            if (batiment.batimentId != 0)
            {
                model.batimentId = batiment.batimentId;
                model.deptId = batiment.deptId;
                model.comId = batiment.comId;
                model.vqseId = batiment.vqseId;
                model.sdeId = batiment.sdeId;
                model.zone = batiment.zone;
                model.disctrictId = batiment.disctrictId;
                model.qhabitation = batiment.qhabitation;
                model.qlocalite = batiment.qlocalite;
                model.qadresse = batiment.qadresse;
                model.qrec = batiment.qrec;
                model.qepc = batiment.qepc;
                model.qb1Etat = batiment.qb1Etat;
                model.qb2Type = batiment.qb2Type;
                model.qb3StatutOccupation = batiment.qb3StatutOccupation;
                model.qb4NbreLogeIndividuel = batiment.qb4NbreLogeIndividuel;
                model.statut = batiment.statut;
                model.dateEnvoi = batiment.dateEnvoi;
                model.isValidated = batiment.isValidated;
                model.isSynchroToAppSup = batiment.isSynchroToAppSup;
                model.isSynchroToCentrale = batiment.isSynchroToCentrale;
                model.dateDebutCollecte = batiment.dateDebutCollecte;
                model.dateFinCollecte = batiment.dateFinCollecte;
                model.dureeSaisie = batiment.dureeSaisie;
                model.isFieldAllFilled = batiment.isFieldAllFilled;
                model.isContreEnqueteMade = batiment.isContreEnqueteMade;
                model.latitude = batiment.latitude;
                model.longitude = batiment.longitude;
                model.codeAgentRecenceur = batiment.codeAgentRecenceur;
                model.isVerified = batiment.isVerified;
                model.BatimentName = "batiman-" + batiment.batimentId;
            }
            return model;
        }

        public static List<BatimentModel> MapTo(List<tbl_batiment> batiments)
        {
            List<BatimentModel> listTo = new List<BatimentModel>();
            if (batiments != null)
            {
                foreach (tbl_batiment b in batiments)
                {
                    BatimentModel bat = MapTo(b);
                    listTo.Add(bat);
                }
            }
            return listTo;
        }
        public static  LogementModel MapTo(tbl_logement logement) 
        {
            LogementModel model = new LogementModel();
            if (logement.logeId != 0)
            {
                model.logeId = logement.logeId;
                model.batimentId = logement.batimentId;
                model.sdeId = logement.sdeId;
                model.qlCategLogement = logement.qlCategLogement;
                model.qlin1NumeroOrdre = logement.qlin1NumeroOrdre;
                model.qlin2StatutOccupation = logement.qlin2StatutOccupation;
                model.qlin3TypeLogement = logement.qlin3TypeLogement;
                model.qlin4IsHaveIndividuDepense = logement.qlin4IsHaveIndividuDepense;
                model.qlin5NbreTotalMenage = logement.qlin5NbreTotalMenage;
                model.statut = logement.statut;
                model.isValidated = logement.isValidated;
                model.isFieldAllFilled = logement.isFieldAllFilled;
                model.dateDebutCollecte = logement.dateDebutCollecte;
                model.dateFinCollecte = logement.dateFinCollecte;
                model.dureeSaisie = logement.dureeSaisie;
                model.isContreEnqueteMade = logement.isContreEnqueteMade;
                model.nbrTentative = logement.nbrTentative;
                model.codeAgentRecenceur = logement.codeAgentRecenceur;
                model.isVerified = logement.isVerified;
                model.LogementName = "lojman-" + logement.qlin1NumeroOrdre;
            }
            return model;
        }

        public static List<LogementModel> MapTo(List<tbl_logement> logements)
        {
            List<LogementModel> listTo = new List<LogementModel>();
            if (logements != null)
            {
                foreach (tbl_logement l in logements)
                {
                    LogementModel lg = MapTo(l);
                    listTo.Add(lg);
                }
            }
            return listTo;
        }
        public static  MenageModel MapTo(tbl_menage menage)
        {
            MenageModel model = new MenageModel();
            if (menage.menageId != 0)
            {
                model.menageId = menage.menageId;
                model.logeId = menage.logeId;
                model.batimentId = menage.batimentId;
                model.sdeId = menage.sdeId;
                model.qm1NoOrdre = menage.qm1NoOrdre;
                model.qm2TotalIndividuVivant = menage.qm2TotalIndividuVivant;
                model.qm22IsHaveAncienMembre = menage.qm22IsHaveAncienMembre;
                model.qm22TotalAncienMembre = menage.qm22TotalAncienMembre;
                model.statut = menage.statut;
                model.isValidated = menage.isValidated;
                model.isFieldAllFilled = menage.isFieldAllFilled;
                model.dateDebutCollecte = menage.dateDebutCollecte;
                model.dateFinCollecte = menage.dateFinCollecte;
                model.dureeSaisie = menage.dureeSaisie;
                model.isContreEnqueteMade = menage.isContreEnqueteMade;
                model.codeAgentRecenceur = menage.codeAgentRecenceur;
                model.isVerified = menage.isVerified;
                model.MenageName = "menaj-" + menage.qm1NoOrdre;
            }
            return model;
        }

        public static List<MenageModel> MapTo(List<tbl_menage> menages)
        {
            List<MenageModel> listTo = new List<MenageModel>();
            if (menages != null)
            {
                foreach (tbl_menage m in menages)
                {
                    MenageModel men = MapTo(m);
                    listTo.Add(men);
                }
            }
            return listTo;
        }
        public static AncienMembreModel MapTo(tbl_AncienMembre ancienmembre)
        {
            AncienMembreModel model = new AncienMembreModel();
            if (ancienmembre.ancienMembreId != 0)
            {
                model.ancienMembreId = ancienmembre.ancienMembreId;
                model.menageId = ancienmembre.menageId;
                model.logeId = ancienmembre.logeId;
                model.batimentId = ancienmembre.batimentId;
                model.sdeId = ancienmembre.sdeId;
                model.q1NoOrdre = ancienmembre.q1NoOrdre;
                model.qp2APrenom = ancienmembre.qp2APrenom;
                model.qp2BNom = ancienmembre.qp2BNom;
                model.qp4Sexe = ancienmembre.qp4Sexe;
                model.q5EstMortOuQuitter = ancienmembre.q5EstMortOuQuitter;
                model.q6HabiteDansMenage = ancienmembre.q6HabiteDansMenage;
                model.q7DateQuitterMenageJour = ancienmembre.q7DateQuitterMenageJour;
                model.q7DateQuitterMenageMois = ancienmembre.q7DateQuitterMenageMois;
                model.q7DateQuitterMenageAnnee = ancienmembre.q7DateQuitterMenageAnnee;
                model.q7bDateMouriJour = ancienmembre.q7bDateMouriJour;
                model.q7bDateMouriMois = ancienmembre.q7bDateMouriMois;
                model.q7bDateMouriAnnee = ancienmembre.q7bDateMouriAnnee;
                model.q8DateNaissanceJour = ancienmembre.q8DateNaissanceJour;
                model.q8DateNaissanceMois = ancienmembre.q8DateNaissanceMois;
                model.q8DateNaissanceAnnee = ancienmembre.q8DateNaissanceAnnee;
                model.q9Age = ancienmembre.q9Age;
                model.q10LienDeParente = ancienmembre.q10LienDeParente;
                model.q11Nationalite = ancienmembre.q11Nationalite;
                model.q11PaysNationalite = ancienmembre.q11PaysNationalite;
                model.q12NiveauEtude = ancienmembre.q12NiveauEtude;
                model.q12StatutMatrimonial = ancienmembre.q12StatutMatrimonial;
                model.statut = ancienmembre.statut;
                model.isValidated = ancienmembre.isValidated;
                model.isFieldAllFilled = ancienmembre.isFieldAllFilled;
                model.dateDebutCollecte = ancienmembre.dateDebutCollecte;
                model.dateFinCollecte = ancienmembre.dateFinCollecte;
                model.dureeSaisie = ancienmembre.dureeSaisie;
                model.isContreEnqueteMade = ancienmembre.isContreEnqueteMade;
                model.codeAgentRecenceur = ancienmembre.codeAgentRecenceur;
                model.isVerified = ancienmembre.isVerified;
            }
            return model;
        }

        public static List<AncienMembreModel> MapTo(List<tbl_AncienMembre> ancienMembres)
        {
            List<AncienMembreModel> listTo = new List<AncienMembreModel>();
            if (ancienMembres != null)
            {
                foreach (tbl_AncienMembre am in ancienMembres)
                {
                    AncienMembreModel ac = MapTo(am);
                    listTo.Add(ac);
                }
            }
            return listTo;
        }

        public static IndividuModel MapTo(tbl_individu individu) 
        {
            IndividuModel model = new IndividuModel();
            if (individu.individuId != 0)
            {
                model.individuId = individu.individuId;
                model.menageId = individu.menageId;
                model.logeId = individu.logeId;
                model.batimentId = individu.batimentId;
                model.sdeId = individu.sdeId;
                model.q1NoOrdre = individu.q1NoOrdre;
                model.qp2APrenom = individu.qp2APrenom;
                model.qp2BNom = individu.qp2BNom;
                model.qp4Sexe = individu.qp4Sexe;
                model.q5HabiteDansMenage = individu.q5HabiteDansMenage;
                model.q6aMembreMenageDepuisQuand = individu.q6aMembreMenageDepuisQuand;
                model.q6bDateMembreMenageJour = individu.q6bDateMembreMenageJour;
                model.q6bDateMembreMenageMois = individu.q6bDateMembreMenageMois;
                model.q6bDateMembreMenageAnnee = individu.q6bDateMembreMenageAnnee;
                model.q7DateNaissanceJour = individu.q7DateNaissanceJour;
                model.q7DateNaissanceMois = individu.q7DateNaissanceMois;
                model.q7DateNaissanceAnnee = individu.q7DateNaissanceAnnee;
                model.q8Age = individu.q8Age;
                model.q9LienDeParente = individu.q9LienDeParente;
                model.q10Nationalite = individu.q10Nationalite;
                model.q10PaysNationalite = individu.q10PaysNationalite;
                model.q11NiveauEtude = individu.q11NiveauEtude;
                model.q12StatutMatrimonial = individu.q12StatutMatrimonial;
                model.statut = individu.statut;
                model.isValidated = individu.isValidated;
                model.isFieldAllFilled = individu.isFieldAllFilled;
                model.dateDebutCollecte = individu.dateDebutCollecte;
                model.dateFinCollecte = individu.dateFinCollecte;
                model.dureeSaisie = individu.dureeSaisie;
                model.isContreEnqueteMade = individu.isContreEnqueteMade;
                model.codeAgentRecenceur = individu.codeAgentRecenceur;
                model.isVerified = individu.isVerified;
            }
            return model;
        }

        public static List<IndividuModel> MapTo(List<tbl_individu> individus)
        {
            List<IndividuModel> listTo = new List<IndividuModel>();
            if (individus != null)
            {
                foreach (tbl_individu ind in individus)
                {
                    IndividuModel ac = MapTo(ind);
                    listTo.Add(ac);
                }
            }
            return listTo;
        }

        public static RapportFinalModel MapTo(tbl_rapportfinal rapport)
        {
            RapportFinalModel model = new RapportFinalModel();
            if (Utils.IsNotNull(rapport))
            {
                model.aE_EsKeGenMounKiEde = rapport.aE_EsKeGenMounKiEde;
                model.aE_IsVivreDansMenage = rapport.aE_IsVivreDansMenage;
                model.aE_RepondantQuiAideId = rapport.aE_RepondantQuiAideId;
                model.batimentId = rapport.batimentId;
                model.codeAgentRecenceur = rapport.codeAgentRecenceur;
                model.dateDebutCollecte = rapport.dateDebutCollecte;
                model.dateFinCollecte = rapport.dateFinCollecte;
                model.dureeSaisie = rapport.dureeSaisie;
            }
            return model;
        }
        public static List<RapportFinalModel> MapTo(List<tbl_rapportfinal> entities)
        {
            List<RapportFinalModel> models = new List<RapportFinalModel>();
            if (entities != null)
            {
                foreach (tbl_rapportfinal rpt in entities)
                {
                    RapportFinalModel m = new RapportFinalModel();
                    m = MapTo(rpt);
                    models.Add(m);
                }
            }
            return models;
        }

        public static MenageDetailsModel MapToListModel<T>(T type)
        {
            MenageDetailsModel model = new MenageDetailsModel();
            if (type.ToString() == Constant.OBJET_ANSYEN_MODEL)
            {
                AncienMembreModel am = type as AncienMembreModel;
                model.Name = Constant.STR_TYPE_ANSYEN_MANM + "-" + am.q1NoOrdre.ToString();
                model.Id = am.ancienMembreId.ToString();
                model.Type = 1;
                model.LogementId = am.logeId.GetValueOrDefault();
                model.BatimentId = am.batimentId.GetValueOrDefault();
                model.MenageId = am.menageId.GetValueOrDefault();
                model.Statut = Convert.ToInt32(am.statut.GetValueOrDefault());
            }
            if (type.ToString() == Constant.OBJET_MODEL_INDIVIDU)
            {
                IndividuModel ind = type as IndividuModel;
                model.Name = Constant.STR_TYPE_ENVDIVIDI + "-" + ind.q1NoOrdre.ToString();
                model.Id = ind.individuId.ToString();
                model.Type = 3;
                model.LogementId = ind.logeId.GetValueOrDefault();
                model.BatimentId = ind.batimentId.GetValueOrDefault();
                model.MenageId = ind.menageId.GetValueOrDefault();
                model.Statut = Convert.ToInt32(ind.statut.GetValueOrDefault());
            }
            
            if (type.ToString() == Constant.OBJET_MODEL_RAPO_FINAL)
            {
                RapportFinalModel rpf = type as RapportFinalModel;
                model.Name = "Rapo final";
                model.Id = "" + rpf.rapportFinalId;

            }
            return model;
        }
        #endregion

        #region TABLES SUPERVISEURS
        public static UtilisateurModel MapTo(Tbl_Utilisateur entity)
        {
            UtilisateurModel model = new UtilisateurModel();
            if (entity != null)
            {
                model.CodeUtilisateur = entity.CodeUtilisateur;
                model.MotDePasse = entity.MotDePasse;
                model.Nom = entity.Nom;
                model.Prenom = entity.Prenom;
                model.ProfileId = Convert.ToInt32(entity.ProfileId.GetValueOrDefault());
                model.Statut = Convert.ToByte(entity.Statut.GetValueOrDefault());
            }
            return model;
        }
        public static SdeModel MapTo(Tbl_Sde entity)
        {
            SdeModel model = new SdeModel();
            if (entity != null)
            {
                model.AgentId = entity.AgentId;
                model.CodeDistrict = entity.CodeDistrict;
                model.ComId = entity.ComId;
                model.DeptId = entity.DeptId;
                model.NoOrdre = entity.NoOrdre;
                model.SdeId = entity.SdeId;
                model.SdeName = entity.SdeId;
            }
            return model;
        }
        public static AgentModel MapTo(Tbl_Agent entity)
        {
            AgentModel model = new AgentModel();
            if (entity != null)
            {
                model.AgentId = entity.AgentId;
                model.Cin = entity.Cin;
                model.CodeUtilisateur = entity.CodeUtilisateur;
                model.Email = entity.Email;
                model.MotDePasse = entity.MotDePasse;
                model.Nif = entity.Nif;
                model.Nom = entity.Nom;
                model.Prenom = entity.Prenom;
                model.Sexe = entity.Sexe;
                model.Telephone = entity.Telephone;
            }
            return model;
        }

        public static List<UtilisateurModel> MapTo(List<Tbl_Utilisateur> entities)
        {
            List<UtilisateurModel> models = new List<UtilisateurModel>();
            if (entities != null)
            {
                foreach (Tbl_Utilisateur u in entities)
                {
                    UtilisateurModel model = new UtilisateurModel();
                    model = MapTo(u);
                    models.Add(model);
                }
            }
            return models;
        }

        public static List<SdeModel> MapTo(List<Tbl_Sde> entities)
        {
            List<SdeModel> models = new List<SdeModel>();
            if (entities != null)
            {
                foreach (Tbl_Sde s in entities)
                {
                    SdeModel model = new SdeModel();
                    model = MapTo(s);
                    models.Add(model);
                }
            }
            return models;
        }

        public static List<AgentModel> MapTo(List<Tbl_Agent> entities)
        {
            List<AgentModel> models = new List<AgentModel>();
            if (entities != null)
            {
                foreach (Tbl_Agent a in entities)
                {
                    AgentModel model = new AgentModel();
                    model = MapTo(a);
                    models.Add(model);
                }
            }
            return models;
        }

        public static Tbl_Utilisateur MapTo(UtilisateurModel model)
        {
            Tbl_Utilisateur entity = new Tbl_Utilisateur();
            if (model != null)
            {
                entity.CodeUtilisateur = model.CodeUtilisateur;
                entity.MotDePasse = model.MotDePasse;
                entity.Nom = model.Nom;
                entity.Prenom = model.Prenom;
                entity.ProfileId = Convert.ToInt32(model.ProfileId);
                entity.Statut = Convert.ToByte(model.Statut.GetValueOrDefault());
            }
            return entity;
        }
        public static Tbl_Sde MapTo(SdeModel model)
        {
            Tbl_Sde entity = new Tbl_Sde();
            if (model != null)
            {
                entity.AgentId = model.AgentId;
                entity.CodeDistrict = model.CodeDistrict;
                entity.ComId = model.ComId;
                entity.DeptId = model.DeptId;
                entity.NoOrdre = model.NoOrdre;
                entity.SdeId = model.SdeId;
            }
            return entity;
        }

        public static Tbl_Agent MapTo(AgentModel model)
        {
            Tbl_Agent entity = new Tbl_Agent();
            if (model != null)
            {
                entity.AgentId = model.AgentId;
                entity.Cin = model.Cin;
                entity.CodeUtilisateur = model.CodeUtilisateur;
                entity.Email = model.Email;
                entity.MotDePasse = model.MotDePasse;
                entity.Nif = model.Nif;
                entity.Nom = model.Nom;
                entity.Prenom = model.Prenom;
                entity.Sexe = model.Sexe;
                entity.Telephone = model.Telephone;
            }
            return entity;
        }

        public static MaterielModel MapTo(Tbl_Materiel entity)
        {
            MaterielModel model = new MaterielModel();
            if (entity != null)
            {
                model.AgentId = entity.AgentId;
                model.DateAssignation = entity.DateAssignation;
                model.Imei = entity.Imei;
                model.IsConfigured = entity.IsConfigured;
                model.LastSynchronisation = entity.LastSynchronisation;
                model.MaterielId = entity.MaterielId;
                model.Model = entity.Model;
                model.Serial = entity.Serial;
                model.Version = entity.Version;
            }
            return model;
        }
        public static Tbl_Materiel MapToEntity(MaterielModel model)
        {
            Tbl_Materiel entity = new Tbl_Materiel();
            if (Utils.IsNotNull(model))
            {
                entity.AgentId = model.AgentId;
                entity.DateAssignation = model.DateAssignation;
                entity.Imei = model.Imei;
                entity.IsConfigured = model.IsConfigured;
                entity.LastSynchronisation = model.LastSynchronisation;
                entity.MaterielId = model.MaterielId;
                entity.Model = model.Model;
                entity.Serial = model.Serial;
                entity.Version = model.Version;
            }
            return entity;
        }

        public static MaterielModel MapModelToModel(MaterielModel mTMap)
        {
            MaterielModel model = new MaterielModel();
            if (mTMap != null)
            {
                model.AgentId = mTMap.AgentId;
                model.DateAssignation = mTMap.DateAssignation;
                model.Imei = mTMap.Imei;
                model.IsConfigured = mTMap.IsConfigured;
                model.LastSynchronisation = mTMap.LastSynchronisation;
                model.MaterielId = mTMap.MaterielId;
                model.Model = mTMap.Model;
                model.Serial = mTMap.Serial;
                model.Version = mTMap.Version;
            }
            return model;
        }

        public static List<MaterielModel> MapToList(List<Tbl_Materiel> entities)
        {
            List<MaterielModel> list = new List<MaterielModel>();
            if (entities!=null)
            {
                foreach (Tbl_Materiel m in entities)
                {
                    MaterielModel mat = MapTo(m);
                    list.Add(mat);
                }
            }
            return list;
        }
        #endregion



    }
}
