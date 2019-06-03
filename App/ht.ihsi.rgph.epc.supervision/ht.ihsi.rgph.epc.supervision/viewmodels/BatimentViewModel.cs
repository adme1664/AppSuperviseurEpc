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
    public class BatimentViewModel : TreeViewItemViewModel
    {
        private BatimentModel _batiment;
        SqliteDataReaderService service = null;
        public BatimentModel Batiment
        {
            get { return _batiment; }
            set { _batiment = value; }
        }


        public BatimentViewModel(BatimentModel batiment, SdeViewModel parentRegion)
            : base(parentRegion, true)
        {
            this._batiment = batiment;
            if (batiment.statut == (int)Constant.StatutModule.MalRempli)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.MalRempli);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.MalRempli);
            }
            if (batiment.statut == (int)Constant.StatutModule.PasFini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.PasFini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.PasFini);
            }
            if (batiment.statut == (int)Constant.StatutModule.Fini)
            {
                Status = true;
                Tip = Constant.GetStringValue(Constant.ToolTipMessage.Fini);
                ImageSource = Constant.GetStringValue(Constant.ImagePath.Fini);
            }
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.sdeId));
            //if (batiment.isValidated == Convert.ToBoolean(Constant.StatutValide.Valide))
            //{
            //    Status = true;
            //    Tip = Constant.GetStringValue(Constant.ToolTipMessage.Valide_deja);
            //    ImageSource = Constant.GetStringValue(Constant.ImagePath.Valide);
            //}
         }
        public void openConnection()
        {
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _batiment.sdeId));
        }


        public String BatimentName
        {
            get
            {
                return _batiment.BatimentName;
            }
        }
        public string SdeName
        {
            get
            {
                return _batiment.sdeId;
            }
        }
        public bool IsCheched
        {
            get
            {
                return _batiment.isChecked;
            }
        }
        public long BatimentId
        {
            get { return _batiment.batimentId; }
        }
        protected override void LoadChildren()
        {
            
                openConnection();
                foreach (LogementTypeModel logement in service.getLogementType())
                {
                    logement.BatimentId = BatimentId;
                    base.Children.Add(new LogementTypeViewModel(logement, this));
                }
        }
    }
}
