using ht.ihsi.rgph.epc.supervision.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.viewmodels
{
   public class TreeViewModel
    {
        readonly ReadOnlyCollection<SdeViewModel> _sdes;


        public TreeViewModel(SdeModel[] models)
        {
            List<SdeViewModel> list = new List<SdeViewModel>();
            try
            {
                foreach (SdeModel sde in models.ToList())
                {
                    SdeViewModel view = new SdeViewModel(sde);
                    view.IsLoading = false;
                    list.Add(view);
                }
                _sdes = new ReadOnlyCollection<SdeViewModel>(list);
            }
            catch (Exception)
            {

            }

        }

        public ReadOnlyCollection<SdeViewModel> Sdes
        {
            get { return _sdes; }
        }
    }
}
