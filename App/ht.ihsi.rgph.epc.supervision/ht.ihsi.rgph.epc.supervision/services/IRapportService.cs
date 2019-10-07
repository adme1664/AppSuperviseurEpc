using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.services
{
    public interface IRapportService
    {
        #region RAPPORTS PERSONNELS ...
        /// <summary>
        /// Sauvegarder un rapport
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        bool saveRapportPersonnel(RapportPersonnelModel rpt);
        /// <summary>
        /// Modififer un rapport personnel
        /// </summary>
        /// <param name="rpt"></param>
        /// <returns></returns>
        bool updateRapportPersonnel(RapportPersonnelModel rpt);
        /// <summary>
        /// Supprimer un rapport personnel
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        bool supprimerRapportPersonnel(int rptId);
        /// <summary>
        /// Retrouver un rapport
        /// </summary>
        /// <param name="rptId"></param>
        /// <returns></returns>
        RapportPersonnelModel getRapportPersonnel(int rptId);
        /// <summary>
        /// Retourne tous les rapports
        /// </summary>
        /// <returns></returns>
        List<RapportPersonnelModel> listOfRapportsPersonnels();
        /// <summary>
        /// Retourne un rapport personnel pour un agent
        /// </summary>
        /// <param name="codeAgent"></param>
        /// <returns></returns>
       List<RapportPersonnelModel> searchRapportPersonnelByAgent(int codeAgent);
        #endregion

        #region RAPPORTS DEROULEMENTS
       /// <summary>
       /// Sauvegarder un nouveau rapport de deroulement de la collecte
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
       bool saveRapportDeroulement(RapportDeroulementModel rpt);
       /// <summary>
       /// Modifier un nouveau rapport de deroulement
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
       bool updateRapportDeroulement(RapportDeroulementModel rpt);
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
       RapportDeroulementModel getRapportDeroulement(long rapportId);
       /// <summary>
       /// Retourne tous les rapports de deroulement
       /// </summary>
       /// <returns></returns>
       List<RapportDeroulementModel> searchRapportDeroulement();
       #endregion

        #region DETAILS RAPPORT DE DEROULEMENT ...
       /// <summary>
       /// Sauvegarder les details sur un rapport de deroulement
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
       bool saveDetailsRapportDeroulement(DetailsRapportModel rpt);
       /// <summary>
       /// Modifier les details sur un rapport de deroulement
       /// </summary>
       /// <param name="rpt"></param>
       /// <returns></returns>
       bool updateDetailsRapportDeroulement(DetailsRapportModel rpt);
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
       List<DetailsRapportModel> getDetailsRapportDeroulement(long rapportId);
       /// <summary>
       /// Retourne tous les details sur un rapport de deroulement
       /// </summary>
       /// <returns></returns>
       List<DetailsRapportModel> searchDetailsRapportDeroulement();
        /// <summary>
        /// Retourne un detail sur un rapport de deroulement
        /// </summary>
        /// <param name="rapportId"></param>
        /// <param name="detailRapportId"></param>
        /// <returns></returns>
       DetailsRapportModel getDetailRapportModel(long rapportId, long detailRapportId);

       #endregion
    }
}
