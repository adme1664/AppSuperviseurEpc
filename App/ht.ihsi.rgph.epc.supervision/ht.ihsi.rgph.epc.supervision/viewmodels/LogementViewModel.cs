using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.MVVM;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ht.ihsi.rgph.epc.supervision.viewmodels
{
    public class LogementViewModel : TreeViewItemViewModel
    {
        readonly LogementModel _logement;
        Logger log;
        public SqliteDataReaderService service;
        public LogementModel Logement
        {
            get { return _logement; }
        }

        private LogementTypeViewModel parentView;
        public LogementViewModel(LogementModel _logement, LogementTypeViewModel _parent)
            : base(_parent, true)
        {
            this._logement = _logement;
            parentView = _parent;
            log = new Logger();
            service=new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _logement.sdeId));
            if (_logement.statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (_logement.statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (_logement.statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
            if (_logement.IsValidated == Convert.ToBoolean(Constant.StatutValide.Valide))
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
            }

        }

        public string LogementName
        {
            get { return "Lojman-" + _logement.qlin1NumeroOrdre.ToString(); }
        }
        public long LogementId
        {
            get { return _logement.logeId; }
        }
        public string NumSde
        {
            get { return _logement.sdeId; }
        }
        public long BatimentId
        {
            get { return parentView.BatimentId; }
        }
        protected override void LoadChildren()
        {
            foreach (MenageModel _menage in service.getAllMenage(_logement))
                {
                    _menage.sdeId = _logement.sdeId;
                    base.Children.Add(new MenageViewModel(_menage, this));
                }
        }
    }
}
