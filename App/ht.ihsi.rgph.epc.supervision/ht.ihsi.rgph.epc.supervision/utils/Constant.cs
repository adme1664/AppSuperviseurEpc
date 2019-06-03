using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region MODELS
        public static string OBJET_MODEL_LOGEMENT = "ht.ihsi.rgph.epc.supervision.models.LogementModel";
        public static string OBJET_MODEL_BATIMENT = "ht.ihsi.rgph.epc.supervision.models.BatimentModel";
        public static string OBJET_MODEL_MENAGE = "ht.ihsi.rgph.epc.supervision.models.MenageModel";
        public static string OBJET_MODEL_INDIVIDU = "ht.ihsi.rgph.epc.supervision.models.IndividuModel";
        public static string OBJET_MODEL_ANCIEN_MEMBRE = "ht.ihsi.rgph.epc.supervision.models.AncienMembreModel";
        #endregion

        #region ENUMERATIONS
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
        public static string MSG_FICHIER_PAS_COPIE = "Fichye yo pa kopye. Eseye ankò";
        public static string WINDOW_TITLE = "IHSI-RGPH-EPC[2018]";
        public static int TRANSFERT_MOBILE = 1;
        public static int TRANSFERT_PC = 2;
        public static string MSG_TABLET_PAS_CONFIGURE = "Tablèt sa a poko konfigire.";
        public static string STR_TYPE_ANSYEN_MANM = "Ansyen_manm";
        public static string STR_TYPE_ENVDIVIDI = "Endividi";
        public static string STR_TYPE_RAPO_FINAL = "Rapò final";
        #endregion     
 
        #region MODLES
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

    }
}
