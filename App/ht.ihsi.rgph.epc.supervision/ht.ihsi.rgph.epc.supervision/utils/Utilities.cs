using ht.ihsi.rgph.epc.supervision.models;
using Ht.Ihsi.Rgph.Utility.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public class Utilities
    {
        #region ConnectionString SQLITE Database
        /// <summary>
        /// get ConnectionString SQLITE Database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sdeID"></param>
        /// <returns></returns>
        public static string getConnectionString(string path, string sdeID)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/entities.EpcEntities.csdl|res://*/entities.EpcEntities.ssdl|res://*/entities.EpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + sdeID + ".SQLITE"
                }.ConnectionString


            }.ConnectionString;
            return connectionString;

        }
        public static string getConnectionString2(string path, string fileName)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = "res://*/entities.EpcEntities.csdl|res://*/entities.EpcEntities.ssdl|res://*/entities.EpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + fileName
                }.ConnectionString

            }.ConnectionString;
            return connectionString;
        }

        public static string getConnectionString(string path)
        {
            string connectionString = new EntityConnectionStringBuilder
            {
                Metadata = @"res://*/entities.SupEpcEntities.csdl|res://*/entities.SupEpcEntities.ssdl|res://*/entities.SupEpcEntities.msl",
                Provider = "System.Data.SQLite.EF6",
                ProviderConnectionString = new SqlConnectionStringBuilder
                {
                    DataSource = path + Constant.SUPDATABASE_FILE_NAME,
                }.ConnectionString
            }.ConnectionString;
            return connectionString;
        }

        public static void killProcess(Process[] procs)
        {
            if (procs.Length != 0)
            {
                foreach (var proc in procs)
                {
                    if (!proc.HasExited)
                    { 
                        proc.Kill();
                    }
                }
            }
        }

   #endregion

        #region UTILITIES
        public static SdeModel getSdeInformation(string sdeId)
        {
            SdeModel sde = new SdeModel();
            try
            {
                if (Utils.IsNotNull(sdeId))
                {
                    sde.DeptId = sdeId.Substring(0, 2);
                    sde.ComId = sdeId.Substring(0, 4);
                    sde.VqseId = sdeId.Substring(0, 7);
                }
             }
            catch (Exception e)
            {
                
            }
            return sde;
        }

        public static SdeModel getSdeInformationForTabletConf(string sdeId)
        {
            SdeModel sde = new SdeModel();
            try
            {
                if (Utils.IsNotNull(sdeId))
                {
                    sde.DeptId = "0" + sdeId.Substring(0, 1);
                    sde.ComId = "0" + sdeId.Substring(0, 3);
                    sde.VqseId = "0" + sdeId.Substring(0, 6);
                    if (sdeId.Substring(4, 2) == "90")
                    {
                        sde.Zone = ""+(int)Constant.Zone.SDE_ZONE_URBAINE;
                    }
                    else
                        sde.Zone = "" + (int)Constant.Zone.SDE_ZONE_RURAL;
                    return sde;
                }
             }
            catch (Exception)
            {

            }
            return null;
        }

        public static string getSdeFormatWithDistrict(string sdeId)
        {
            if (sdeId.Length <= 14)
            {
                string[] splits = sdeId.Split('-');
                string convertSde = "0" + splits[0] + "-" + splits[1] + "-" + splits[2] + "-" + splits[3];
                return convertSde;
            }
            else
            {
                return sdeId;
            }
         }

        public static void showControl(Control control, Grid grid)
        {
            control.Dispatcher.BeginInvoke((Action)(() => control.VerticalAlignment = System.Windows.VerticalAlignment.Stretch));
            control.Dispatcher.BeginInvoke((Action)(() => control.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Clear()));
            grid.Dispatcher.BeginInvoke((Action)(() => grid.Children.Add(control)));
        }
        public static bool isCategorieExist(List<string> list, string categorie)
        {
            foreach (string cat in list)
            {
                if (cat == categorie)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
