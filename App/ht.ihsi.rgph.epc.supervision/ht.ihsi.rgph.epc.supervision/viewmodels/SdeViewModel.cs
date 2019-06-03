using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.MVVM;
using ht.ihsi.rgph.epc.supervision.services;
using ht.ihsi.rgph.epc.supervision.utils;
using Ht.Ihsi.Rgph.Logging.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ht.ihsi.rgph.epc.supervision.viewmodels
{
    public class SdeViewModel : TreeViewItemViewModel
    {
        private SdeModel _sde;
        private bool isWaitVisible;
        bool _isLoading;
        Thread t;
        ThreadStart ths;
        string pathDefaultConfigurationFile = AppDomain.CurrentDomain.BaseDirectory + @"App_data\";
        string file = "";
        Logger log;
        bool _allreadyLoad;

        public bool AllreadyLoad
        {
            get { return _allreadyLoad; }
            set
            {
                if (_allreadyLoad != value)
                {
                    _allreadyLoad = value;
                    this.OnPropertyChanged("AllreadyLoad");
                }
            }
        }
        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                if (value != _isLoading)
                {
                    _isLoading = value;
                    this.OnPropertyChanged("IsLoading");
                }
            }
        }
        public SdeModel Sde
        {
            get { return _sde; }
            set { _sde = value; }
        }
        

        public SdeViewModel(SdeModel sde)
            : base(null, true)
        {
            _sde = sde;
            log = new Logger();
            service = new SqliteDataReaderService(Utilities.getConnectionString(Users.users.DatabasePath, _sde.SdeId));
        }

        public bool IsWaitVisible
        {
            get
            {
                return this.isWaitVisible;
            }
            set { this.isWaitVisible = value; }
        }
        public string SdeName
        {
            get { return _sde.SdeId; }

        }
        public override bool IsExpand()
        {
            if (base.IsExpanded == true)
            {
                IsWaitVisible = true;
                log.Info("IsExpanded: true");
                return true;
            }
            else
                IsWaitVisible = false;
            log.Info("IsExpanded: false");
            return false;
        }
        protected override void LoadChildren()
        {
            try
            {
                BatimentModel[] batiments = base.service.getAllBatiments();
                if (batiments == null)
                {

                }
                else
                {

                    foreach (BatimentModel batiment in batiments)
                    {
                        batiment.sdeId = _sde.SdeId;
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => base.Children.Add(new BatimentViewModel(batiment, this))));
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => IsLoading = false));
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() => AllreadyLoad = true));

            }

        }

        public void getAllBatiments()
        {
            BatimentModel[] batiments = base.service.getAllBatiments();
            foreach (BatimentModel batiment in batiments)
            {
                if (batiment.statut == (int)Constant.StatutModule.MalRempli)
                {
                    batiment.IsMalRempli = true;
                }
                if (batiment.statut == (int)Constant.StatutModule.PasFini)
                {
                    batiment.IsNotFinished = true;
                }
                if (batiment.statut == (int)Constant.StatutModule.Fini)
                {
                    batiment.IsFinished = true;
                }
                if (batiment.IsValidated == Convert.ToBoolean(Constant.StatutValide.Valide))
                {
                    batiment.IsValidated = true;
                }
                batiment.sdeId = _sde.SdeId;
                base.Children.Add(new BatimentViewModel(batiment, this));
            }
        }
        protected override void Selected()
        {
            if (base.IsSelected == true)
            {
                log.Info("<>===========Selected");
                log.Info("<>===========SDE:" + _sde.SdeId);
            }
        }
       
    }
}
