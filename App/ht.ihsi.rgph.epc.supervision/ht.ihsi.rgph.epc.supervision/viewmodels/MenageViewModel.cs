using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.MVVM;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ht.ihsi.rgph.epc.supervision.viewmodels
{
    public class MenageViewModel : TreeViewItemViewModel
    {
        private MenageModel _menage;
        private LogementViewModel _parent;
        public SqliteDataReaderService service = null;
        public MenageModel Model
        {
            get { return _menage; }
            set { _menage = value; }
        }


        public MenageViewModel(MenageModel _menage, LogementViewModel _parentView)
            : base(_parentView, true)
        {
            this._menage = _menage;
            this._parent = _parentView;
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _parent.Logement.sdeId));
            if (_menage.statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (_menage.statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            if (_menage.statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
        }

        public string MenageName
        {
            get { return _menage.MenageName; }
        }

        public string LogementName
        {
            get { return _parent.LogementName; }
        }
        public long MenageId
        {
            get { return _menage.menageId; }
        }
        public long LogementId
        {
            get { return _menage.logeId.GetValueOrDefault(); }
        }
        public string NumSde
        {
            get { return _menage.sdeId; }
        }
        public long BatimentId
        {
            get { return _menage.batimentId.GetValueOrDefault(); }
        }
        public long BatimanName
        {
            get { return _parent.BatimentId; }
        }
        protected override void LoadChildren()
        {
            foreach (MenageTypeModel model in service.getAllInMenage())
            {
                model.SdeId = _menage.sdeId;
                base.Children.Add(new MenageTypeViewModel(model, this));
            }
        }

    }
}
