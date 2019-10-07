using ht.ihsi.rgph.epc.database.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.dao
{
 public interface IDaoRapports
 {
     #region RAPPORTS PERSONNELS ...
     bool saveRapportPersonnel(Tbl_RapportPersonnel rpt);
     bool updateRapportPersonnel(Tbl_RapportPersonnel rpt);
     bool supprimerRapportPersonnel(int rptId);
     Tbl_RapportPersonnel getRapportPersonnel(int rptId);
     List<Tbl_RapportPersonnel> listOfRapports();
     List<Tbl_RapportPersonnel> searchRapportPersonnelByAgent(int codeAgent);
     #endregion

     #region RAPPORTS DEROULEMENTS
     /// <summary>
     /// Sauvegarder un nouveau rapport de deroulement de la collecte
     /// </summary>
     /// <param name="rpt"></param>
     /// <returns></returns>
     bool saveRapportDeroulement(Tbl_RprtDeroulement rpt);
     /// <summary>
     /// Modifier un nouveau rapport de deroulement
     /// </summary>
     /// <param name="rpt"></param>
     /// <returns></returns>
     bool updateRapportDeroulement(Tbl_RprtDeroulement rpt);
     /// <summary>
     /// Supprimer un rapport de deroulement
     /// </summary>
     /// <param name="rapportId"></param>
     /// <returns></returns>
     bool deleteRapportDeroulement(long rapportId);
     /// <summary>
     /// Retourne un rapport de deroulement
     /// </summary>
     /// <param name="rapportId"></param>
     /// <returns></returns>
     Tbl_RprtDeroulement getRapportDeroulement(long rapportId);
     /// <summary>
     /// Retourne tous les rapports de deroulement
     /// </summary>
     /// <returns></returns>
     List<Tbl_RprtDeroulement> searchRapportDeroulement();
     #endregion

     #region DETAILS RAPPORT DE DEROULEMENT ...
     /// <summary>
     /// Sauvegarder les details sur un rapport de deroulement
     /// </summary>
     /// <param name="rpt"></param>
     /// <returns></returns>
     bool saveDetailsRapportDeroulement(Tbl_DetailsRapport rpt);
     /// <summary>
     /// Modifier les details sur un rapport de deroulement
     /// </summary>
     /// <param name="rpt"></param>
     /// <returns></returns>
     bool updateDetailsRapportDeroulement(Tbl_DetailsRapport rpt);
     /// <summary>
     /// Supprimer les details sur un rapport de deroulement
     /// </summary>
     /// <param name="rapportId"></param>
     /// <returns></returns>
     bool deleteDetailsRapportDeroulement(long rapportId);
     /// <summary>
     /// Retourne les details sur un rapport de deroulement
     /// </summary>
     /// <param name="rapportId"></param>
     /// <param name="detailsRapportId"></param>
     /// <returns></returns>
     List<Tbl_DetailsRapport> getDetailsRapportDeroulement(long rapportId);
     /// <summary>
     /// Retourne un detail sur un rapport de deroulement
     /// </summary>
     /// <param name="rapportId"></param>
     /// <param name="detailsRapportId"></param>
     /// <returns></returns>
     Tbl_DetailsRapport getDetailRapportDeroulement(long rapportId, long detailsRapportId);
     /// <summary>
     /// Retourne tous les details
     /// </summary>
     /// <returns></returns>
     List<Tbl_DetailsRapport> searchDetailsRapportDeroulement();
     #endregion
 }
}
