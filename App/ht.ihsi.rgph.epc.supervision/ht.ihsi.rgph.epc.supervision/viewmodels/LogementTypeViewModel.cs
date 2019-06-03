using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.MVVM;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.viewmodels
{
    public class LogementTypeViewModel : TreeViewItemViewModel
    {
        readonly LogementTypeModel _logementType;

        public LogementTypeModel LogementType
        {
            get { return _logementType; }
        }

        private BatimentViewModel _parent;
        private SqliteDataReaderService service = null;
        public LogementTypeViewModel(LogementTypeModel _logementType, BatimentViewModel _parentView)
            : base(_parentView, true)
        {
            this._logementType = _logementType;
            _parent = _parentView;
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _parentView.Batiment.sdeId));

        }


        public string LogementName
        {
            get
            {
                return LogementType.LogementName;
            }
        }

        public long BatimentId
        {
            get
            {
                return LogementType.BatimentId;
            }
        }
        protected override void LoadChildren()
        {
            foreach (LogementModel logement in service.getAllLogement(_parent.Batiment, LogementType))
            {
                logement.sdeId = _parent.SdeName;
                base.Children.Add(new LogementViewModel(logement, this));
            }
        }
    }
}
