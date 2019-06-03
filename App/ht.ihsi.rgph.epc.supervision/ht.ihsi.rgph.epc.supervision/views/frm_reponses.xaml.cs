using ht.ihsi.rgph.epc.supervision.mapper;
using ht.ihsi.rgph.epc.supervision.models;
using ht.ihsi.rgph.epc.supervision.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ht.ihsi.rgph.epc.supervision.views
{
    /// <summary>
    /// Logique d'interaction pour frm_reponses.xaml
    /// </summary>
    public partial class frm_reponses : UserControl
    {
        #region CONSTRUCTORS
        public frm_reponses()
        {
            InitializeComponent();
        }
        #endregion

        public frm_reponses(Object obj, string sdeId)
        {
            InitializeComponent();
            List<DataDetails> reponses = null;
            IndividuModel _mIndividu = null;
            BatimentModel _mBatiment = null;
            MenageModel _mMenage = null;
            LogementModel _mLogement = null;
            RapportFinalModel _rapo = null;
            MenageDetailsModel detailsModel = null;
            AncienMembreModel ancienModel = null;
            string libele = null;
            ISqliteReader reader = new SqliteReader(Utilities.getConnectionString(Users.users.DatabasePath,sdeId));
            if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Batiment))
            {
                _mBatiment = obj as BatimentModel;
                libele ="#SDE:"+_mBatiment.sdeId+"/Batiman:"+_mBatiment.batimentId;
                reponses = DataDetailsMapper.MapToMobile<BatimentModel>(_mBatiment, sdeId);
            }
            if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Logement))
            {
                _mLogement= obj as LogementModel;
                libele = "#SDE:" + _mLogement.sdeId + "/Batiman:" + _mLogement.batimentId.ToString()+"/Lojman:"+_mLogement.qlin1NumeroOrdre.ToString();
                reponses = DataDetailsMapper.MapToMobile<LogementModel>(_mLogement, sdeId);
            }
            if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.Menage))
            {
                _mMenage = obj as MenageModel;
                libele = "#SDE:" + _mMenage.sdeId + "/Batiman:" + _mMenage.batimentId.ToString() + "/Lojman:" + _mMenage.logeId.ToString()+"/Menaj:"+_mMenage.qm1NoOrdre.ToString();
                reponses = DataDetailsMapper.MapToMobile<MenageModel>(_mMenage, sdeId);
            }
            if (obj.ToString() == Constant.GetStringValue(Constant.ObjetModel.MenageDetails))
            {
                detailsModel = obj as MenageDetailsModel;
                if (detailsModel.Type == (int)Constant.CodeMenageDetails.AncienMembre)
                {
                    ancienModel = reader.getAncienMembre(Convert.ToInt32(detailsModel.Id));
                    libele = "#SDE:" + ancienModel.sdeId + "/Batiman:" + ancienModel.batimentId.ToString() + "/Lojman:" + ancienModel.logeId.ToString() + "/Menaj:" + ancienModel.menageId.ToString() + "/Emigre:" + ancienModel.q1NoOrdre.ToString();
                    reponses = DataDetailsMapper.MapToMobile<AncienMembreModel>(ancienModel, sdeId);
                }
                if (detailsModel.Type == (int)Constant.CodeMenageDetails.Individu)
                {
                    _mIndividu = reader.GetIndividuModel(Convert.ToInt32(detailsModel.Id));
                    libele = "#SDE:" + _mIndividu.sdeId + "/Batiman:" + _mIndividu.batimentId.ToString() + "/Lojman:" + _mIndividu.logeId.ToString() + "/Menaj:" + _mIndividu.menageId.ToString() + "/Endividi:" + _mIndividu.q1NoOrdre.ToString();
                    reponses = DataDetailsMapper.MapToMobile<IndividuModel>(_mIndividu, sdeId);
                }
                //if (detailsModel.Type == (int)Constant.CodeMenageDetails.RapportFinal)
                //{
                //    _rapo = reader.getRapportFinalById(Convert.ToInt32(detailsModel.Id));
                //    _mMenage = reader.GetMenageById(detailsModel.MenageId);
                //    libele = "#SDE:" + _mMenage.sdeId + "/Batiman:" + _mMenage.batimentId.ToString() + "/Lojman:" + _mMenage.logeId.ToString() + "/Menaj:" + _mMenage.qm1NoOrdre.ToString() + "/Rapo:" + _rapo.rapportFinalId;
                //    reponses = DataDetailsMapper.RapportFinal(_rapo,sdeId);
                //}
            }
            if (obj.ToString() == Constant.OBJET_MODEL_INDIVIDU)
            {
                _mIndividu=obj as IndividuModel;
                libele = "#SDE:" + _mIndividu.sdeId + "/Batiman:" + _mIndividu.batimentId.ToString() + "/Lojman:" + _mIndividu.logeId.ToString() + "/Menaj:" + _mIndividu.menageId.ToString() + "/Endividi:" + _mIndividu.q1NoOrdre.ToString();
                reponses = DataDetailsMapper.MapToMobile<IndividuModel>(_mIndividu, sdeId);
            }
            
            string kategori = reponses.ElementAt(0).Kategori;
            List<String> listOfkategori = new List<string>();
            listOfkategori.Add(kategori);
            foreach (DataDetails rep in reponses)
            {
                if (rep.Kategori != kategori)
                {
                    if (Utilities.isCategorieExist(listOfkategori, rep.Kategori) == false)
                    {
                        listOfkategori.Add(rep.Kategori);
                        kategori = rep.Kategori;
                    }
                }
            }
            if (listOfkategori.Count == 0)
            {
                listOfkategori.Add(kategori);
            }
            if (listOfkategori.Count > 0)
            {
                foreach (String kat in listOfkategori)
                {
                    List<DataDetails> listPerkategori = reponses.FindAll(k => k.Kategori == kat);
                    TabItem item = new TabItem();
                    item.HorizontalAlignment = HorizontalAlignment.Stretch;
                    item.VerticalAlignment = VerticalAlignment.Stretch;
                    item.VerticalContentAlignment = VerticalAlignment.Stretch;
                    StackPanel stc = new StackPanel();
                    stc.Height = 35;
                    stc.Orientation = Orientation.Horizontal;
                    stc.Margin = new Thickness(4, 0, 39, 0);

                    Image img = new Image();
                    BitmapImage bImg = new BitmapImage();
                    bImg.BeginInit();
                    bImg.UriSource = new Uri(@"/images/tb.png", UriKind.Relative);
                    bImg.EndInit();
                    img.Source = bImg;
                    stc.Children.Add(img);

                    TextBlock txt = new TextBlock();
                    txt.FontSize = 11;
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.FontWeight = FontWeights.Bold;
                    txt.Text = kat;
                    stc.Children.Add(txt);

                    item.Header = stc;

                    Grid mgrd = new Grid();
                    mgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    mgrd.VerticalAlignment = VerticalAlignment.Stretch;

                    Grid cgrd = new Grid();
                    cgrd.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cgrd.VerticalAlignment = VerticalAlignment.Stretch;
                    cgrd.Margin = new Thickness(10, 10, 0, 0);

                    Label lbl_details = new Label();
                    lbl_details.Content =libele;
                    lbl_details.FontWeight = FontWeights.Bold;
                    lbl_details.FontStyle = FontStyles.Italic;
                    lbl_details.Foreground = Brushes.Blue;
                    lbl_details.HorizontalAlignment = HorizontalAlignment.Left;
                    lbl_details.Margin = new Thickness(3, 0, 0, 0);
                    lbl_details.VerticalAlignment = VerticalAlignment.Top;

                    cgrd.Children.Add(lbl_details);
                    DataGrid dtg1 = new DataGrid();
                    dtg1.ColumnWidth = 605;
                    dtg1.FontSize = 13;
                    Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC9E7F5"));
                    dtg1.AlternatingRowBackground = brush;
                    dtg1.AlternationCount = 2;
                    dtg1.Margin = new Thickness(10, 24, 10, 10);
                    dtg1.HorizontalAlignment = HorizontalAlignment.Stretch;
                    dtg1.VerticalAlignment = VerticalAlignment.Stretch;
                    dtg1.IsReadOnly = true;
                    dtg1.CanUserAddRows = false;
                    dtg1.ItemsSource = listPerkategori;
                    dtg1.AutoGeneratingColumn += dtg1_AutoGeneratingColumn;
                    cgrd.Children.Add(dtg1);
                    mgrd.Children.Add(cgrd);
                    item.Content = mgrd;
                    main_tab.Items.Add(item);
                }
            }
        }

        //Generating the colunm automaticlly
        void dtg1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Kategori")
            {
                e.Column.Visibility = Visibility.Hidden;
            }
            if (e.Column.Header.ToString() == "Kesyon" || e.Column.Header.ToString() == "Repons")
            {
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
                col.ElementStyle = style;
            }
        }

    }
}
