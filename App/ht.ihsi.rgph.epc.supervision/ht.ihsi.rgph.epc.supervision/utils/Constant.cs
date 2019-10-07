using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ht.ihsi.rgph.epc.supervision.utils
{
   public  class Constant
    {
        #region ANDROID ADB COMMAND
        public static string CMD_IMEI = "/c adb shell dumpsys iphonesubinfo";
        public static string CMD_SERIAL = "/c adb shell getprop ro.boot.serialno";
        public static string CMD_MODEL = "/c adb shell getprop ro.product.model";
        public static string CMD_VERSION = "/c adb shell getprop ro.build.version.release";
        public static string DB_NAME = "epc_data";

        public static string DEVICE_DIRECTORY_DATA = "sdcard/Data/rgph_epc_db/";
        public static string CMD_PULL_DB = "/c adb pull sdcard/Data/rgph_epc_db/";
        public static string CMD_PUSH_DB = "/c adb push";
        public static string CMD_ADB_VERSION = "/c adb version";
        public static int AGE_PA_KONNEN = 999;
        public static short STATUT_MODULE_KI_FINI_1 = 1;
        public static short STATUT_MODULE_KI_MAL_RANPLI_2 = 2;
        public static short STATUT_MODULE_KI_PA_FINI_3 = 3;
        #endregion

        #region MODELS
        public static string OBJET_MODEL_LOGEMENT = "ht.ihsi.rgph.epc.supervision.models.LogementModel";
        public static string OBJET_MODEL_BATIMENT = "ht.ihsi.rgph.epc.supervision.models.BatimentModel";
        public static string OBJET_MODEL_MENAGE = "ht.ihsi.rgph.epc.supervision.models.MenageModel";
        public static string OBJET_MODEL_INDIVIDU = "ht.ihsi.rgph.epc.supervision.models.IndividuModel";
        public static string OBJET_MODEL_ANCIEN_MEMBRE = "ht.ihsi.rgph.epc.supervision.models.AncienMembreModel";
        #endregion

        #region ENUMERATIONS

        public enum EtatBatiment : int
        {
            Inobservable = 5
        }
        public enum RapportTypeQuestion : int
        {
            question_4 = 1,
            question_8 = 2
        }

        public enum Sexe : int
        {
            Gason = 1,
            Fi = 2
        }

        public enum StatutVerifie : short
        {
            Verifie = 1,
            PasVerifie = 0
        }
        public enum TypeQuestionMobile : int
        {
            Choix = 17,
            Saisie = 2,
            Automatique = 3,
            Departement = 4,
            Commune = 7,
            Vqse = 6,
            Pays = 18,
            Grid = 8,
            Utilisation = 9
        }

        public enum Zone : int
        {
            SDE_ZONE_URBAINE=1,
            SDE_ZONE_RURAL=2
        }
        public enum ProfileUtilisateur : int
        {
            PROFIL_AGENT_RECENSEUR_MOBILE = 8,
            PROFIL_SUPERVISEUR_SUPERVISION_SG = 8,
            PROFIL_SUPERVISEUR_SUPERVISION_MOBILE = 7,
            PROFIL_ASTIC = 6
        }
        public enum StatutModule : short
        {
            Fini = 1,
            MalRempli = 2,
            PasFini = 3
        }
        public enum ImagePath : int
        {
            [StringValue("/images/malrempli.png")]
            MalRempli = 1,
            [StringValue("/images/notfinish.png")]
            PasFini = 2,
            [StringValue("/images/check.png")]
            Fini = 3,
            [StringValue("/images/validate_check.png")]
            Valide = 4,
            [StringValue("/images/ce.png")]
            ContreEnquete = 5
        }

        public enum CodeMenageDetails : int
        {
            Individu=3,
            AncienMembre=1,
            RapportFinal=2
        }

        public enum ToolTipMessage : int
        {
            [StringValue("Pa byen ranpli")]
            MalRempli = 1,
            [StringValue("Poko fini")]
            PasFini = 2,
            [StringValue("Fini")]
            Fini = 3,
            [StringValue("Kont ankèt sa a fèt deja")]
            Kont_anket_fet = 4,
            [StringValue("Valide deja")]
            Valide_deja = 5,
            [StringValue("Kont ankèt sa a poko fèt")]
            Kont_Anket_Not_Made = 6,
            [StringValue("Nan Kont ankèt")]
            Kont_Anket_IN = 7
        }

        public enum TypeTreeView : int
        {
            Contre_enquete = 1,
            Batiment = 2,
            Logement = 3,
            Menage = 4,
            Deces = 5,
            Individu = 6
        }
        public enum StatutValide : short
        {
            Valide = 1,
            PasValide = 0
        }
        public enum TypeLogement : int
        {
            Kolektif = 0,
            Endividyel = 1
        }
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();
            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
            return output;
        }
        #endregion

        #region CONSTANT
        public static string SUPDATABASE_FILE_NAME = "rgph_epc-db.sqlite";
        public static string XML_ELEMENT_ADR_SERVER = "adrServer";
        public static string XML_ELEMENT_VARIABLE = "variable";
        public static string MSG_TRANSFERT_TERMINE = "Transfè a fèt avèk siksè.";
        public static string MSG_TABLET_PAS_CONNECTE = "Pa gen tablèt ki konekte.";
        public static string MSG_SAVE_ERROR = "Erreur au moment de l'enregistrement.";
        public static string MSG_FICHIER_PAS_COPIE = "Fichye yo pa kopye. Eseye ankò";
        public static string WINDOW_TITLE = "IHSI-RGPH-EPC[2019]";
        public static int TRANSFERT_MOBILE = 1;
        public static int TRANSFERT_PC = 2;
        public static string MSG_TABLET_PAS_CONFIGURE = "Tablèt sa a poko konfigire.";
        public static string STR_TYPE_ANSYEN_MANM = "Ansyen_manm";
        public static string STR_TYPE_ENVDIVIDI = "Endividi";
        public static string STR_TYPE_RAPO_FINAL = "Rapò final";

        public static List<KeyValue> ListOfRaisons()
        {
            List<KeyValue> listOf = new List<KeyValue>();
            listOf.Add(new KeyValue(1, "Ranpli nèt / depi nan batiman rive nan manb menaj la"));
            listOf.Add(new KeyValue(2, "Ranpli nèt / Batiman vid"));
            listOf.Add(new KeyValue(3, "Ranpli nèt / Lojman vid"));
            listOf.Add(new KeyValue(4, "Ranpli nèt depi nan premye entèvyou a"));
            listOf.Add(new KeyValue(5, "Refus converti"));
            listOf.Add(new KeyValue(6, "Randevou ou retou pwograme/fèt/fini"));
            //Menage
            listOf.Add(new KeyValue(7, "Moun ki tap reponn nan pa vle kontinye"));
            listOf.Add(new KeyValue(8, "Moun ki tap reponn nan kanpe, men gen randevou"));
            listOf.Add(new KeyValue(9, "Lojman an okipe, men moun yo pa la"));
            listOf.Add(new KeyValue(10, "Lòt. Presize poukisa..."));
            //Menage
            listOf.Add(new KeyValue(11, "\"Refus non converti\" / Kesyonè a pa fini (NRP)"));
            listOf.Add(new KeyValue(12, "Moun ki pou reponn nan pa janm la/disponib pou AR la fin ranpli kesyonè a"));
            listOf.Add(new KeyValue(13, "Lojman an okipe, men moun yo pa la"));
            listOf.Add(new KeyValue(14, "Lòt. Presize poukisa..."));
            listOf.Add(new KeyValue(15, "Pa gen mwayen obsève batiman an / pa gen enfòmasyon sou lojman an"));
            listOf.Add(new KeyValue(16, "Moun yo refize reponn nètalkole"));
            listOf.Add(new KeyValue(17, "Moun ki pou reponn nan pa la/ pa disponib men gen yon randevou"));
            listOf.Add(new KeyValue(18, "Moun ki pou reponn nan pa la oubyen li pa disponib"));
            listOf.Add(new KeyValue(19, "Lòt. Presize poukisa..."));
            listOf.Add(new KeyValue(20, "Pa janm gen mwayen obsève batiman an"));
            listOf.Add(new KeyValue(21, "\"Refus non converti\" / Non-réponse totale (NRT)"));
            listOf.Add(new KeyValue(22, "Moun ki pou reponn nan pa janm la/disponib pou AR la ranpli kesyonè a"));
            listOf.Add(new KeyValue(23, "Lòt. Presize poukisa..."));
            return listOf;
        }
        public static KeyValue getRaison(int raisonId)
        {
            return ListOfRaisons().Find(r => r.Key == raisonId);
        }
        #endregion     
 
        #region MODELS
        public static string OBJET_BATIMENT = "ht.ihsi.rgph.epc.supervision.models.BatimentModel";
        public static string OBJET_LOGEMENT = "ht.ihsi.rgph.epc.supervision.models.LogementModel";
        public static string OBJET_ANSYEN_MODEL = "ht.ihsi.rgph.epc.supervision.models.AncienMembreModel";
        public static string OBJET_MENAGE = "ht.ihsi.rgph.epc.supervision.models.MenageModel";
        public static string OBJET_INDIVIDU = "ht.ihsi.rgph.epc.supervision.models.IndividuModel";
        public static string OBJET_MODEL_RAPO_FINAL = "ht.ihsi.rgph.epc.supervision.models.RapportFinalModel";
        #endregion

        #region VIEWMODELS
       public enum DataContext:int
       {
           [StringValue("ht.ihsi.rgph.epc.supervision.viewmodels.SdeViewModel")]
           SdeViewModel=1,
           [StringValue("ht.ihsi.rgph.epc.supervision.viewmodels.BatimentViewModel")]
           BatimentViewModel=2,
           [StringValue("ht.ihsi.rgph.epc.supervision.viewmodels.LogementViewModel")]
           LogementViewModel=3,
           [StringValue("ht.ihsi.rgph.epc.supervision.viewmodels.MenageViewModel")]
           MenageViewModel=4,
           [StringValue("ht.ihsi.rgph.epc.supervision.viewmodels.MenageDetailsViewModel")]
           MenageDetailsViewModel=5
       }

       public enum ObjetModel : int
       {
           [StringValue("ht.ihsi.rgph.epc.supervision.models.BatimentModel")]
           Batiment=1,
           [StringValue("ht.ihsi.rgph.epc.supervision.models.LogementModel")]
           Logement=2,
           [StringValue("ht.ihsi.rgph.epc.supervision.models.MenageModel")]
           Menage=3,
           [StringValue("ht.ihsi.rgph.epc.supervision.models.AncienMembreModel")]
           AncienMembre=4,
           [StringValue("ht.ihsi.rgph.epc.supervision.models.IndividuModel")]
           Individu=5,
           [StringValue("ht.ihsi.rgph.epc.supervision.models.MenageDetailsModel")]
           MenageDetails=6
       }
       public enum ModuleQuestionnaire : int
       {
           [StringValue("FRM-BAT")]
           Batiment = 1,
           [StringValue("FRM-LIN")]
           Logement = 2,
           [StringValue("FRM-MEN")]
           Menage = 3,
           [StringValue("FRM-ANM")]
           AncienMembre = 4,
           [StringValue("FRM-IND")]
           Individu = 5
       }
        #endregion    

        #region RAPPORT PERSONNEL
       public static string q1 = "1-Est-ce que l´agent recenseur s´est orienté correctement à l´aide de la carte de la SDE ?";
       public static string q2 = "2-Est-ce que l´AR s´est présenté correctement au ménage ?";
       public static string q3 = "3-Est-ce que l´AR a correctement identifié le répondant selon les instructions ?";
       public static string q4 = "4-Est-ce que l´AR a formulé les questions conformément aux instructions ?";
       public static string q5 = "5-Est-ce que toutes les questions clés ont été couvertes pour les caractéristiques de la personne ?";
       public static string q6 = "6-Est-ce que l´AR a bien géré l´administration du module caractéristiques du bâtiment?";
       public static string q7 = "7-Est-ce que l´AE a identifié correctement le nombre de logement dans le bâtiment selon les instructions qui lui ont été transmises ?";
       public static string q8 = "8-Est-ce que l´AE a bien géré l´administration du module caractéristique des actuels membres du ménage?";
       public static string q9 = "9-Est-ce que l´AE a bien géré l´administration du module caractéristique des anciens membres ménage ? ";
       public static string q10 = "10-Est-ce que l´AE a influencé la réponse du répondant ?";
       public static string q11 = "11-Est-ce que l´AE interprète correctement les informations qui lui sont données ? ";
       public static string q12 = "12-Est-ce que l´AE est en mesure de déceler les incohérences ou d´apprécier le degré de précision des réponses obtenues ?";
       public static string q13 = "13-Est-ce que l´AE a précisé le résultat de la visite à la fin du questionnaire ?";
       public static string q14 = "14-Faire la liste des problèmes à résoudre avec l´AE. Sélectionner le type de problème, puis en faire la description librement.";
       public static string q15 = "15-Commentaires généraux sur le comportement de l´agent enquêteur (à rédiger) ";

       #endregion

        #region CHOIX RAPPORT
       public static List<ReponseModel> listOf4Choix()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1.Oui"));
           listOf.Add(new ReponseModel("2", "2.Non"));
           listOf.Add(new ReponseModel("3", "3.Moyennement "));
           listOf.Add(new ReponseModel("4", "4.Hors observation"));
           return listOf;
       }
       public static List<ReponseModel> listOf3Choix()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1.Oui"));
           listOf.Add(new ReponseModel("2", "2.Non"));
           listOf.Add(new ReponseModel("3", "3.Moyennement "));
           return listOf;
       }
       public static List<ReponseModel> listOf10Choix()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1.Une fois"));
           listOf.Add(new ReponseModel("2", "2.Au moins 2 fois"));
           listOf.Add(new ReponseModel("3", "3.Non "));
           return listOf;
       }
       public static List<ReponseModel> listOfChoixQ13()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1.Oui"));
           listOf.Add(new ReponseModel("2", "2.Non"));
           listOf.Add(new ReponseModel("3", "3.Hors observation "));
           return listOf;
       }
       public static List<ReponseModel> listOfChoixQ14()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1.Non maîtrise de certains concepts clé"));
           listOf.Add(new ReponseModel("2", "2.Du mal à gérer l´interview"));
           listOf.Add(new ReponseModel("3", "3.Attitude peu respectueuse vis-à-vis du répondant"));
           listOf.Add(new ReponseModel("4", "4.Ne prend pas le temps de bien gérer l´entrevue"));
           return listOf;
       }
       public static List<ReponseModel> listOfChoixQ15()
       {
           List<ReponseModel> listOf = new List<ReponseModel>();
           listOf.Add(new ReponseModel("1", "1. Oui, Agent problématique "));
           listOf.Add(new ReponseModel("2", "2. Agent à suivre pour renforcement"));
           listOf.Add(new ReponseModel("3", "3. Non, aucune alerte"));
           return listOf;
       }
       #endregion

     

    }
}
