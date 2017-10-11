using Business;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class EkleDuzenle : Form
    {  
        public EkleDuzenle()
        {
            InitializeComponent();
        }

        KategoriKontrol k = new KategoriKontrol();
        NotControl nk = new NotControl();
        public Button Btn_Ekle
        {
            get { return btn_Ekle; }
        }
        public Not GelenNot { get; set; }

        private void EkleDuzenle_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = k.TumKategoriler();
            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";

            if(GelenNot !=null)
            {
                //Düzenleme işlemiyse
                comboBox1.SelectedValue = GelenNot.KategoriID;
                richTextBox1.Text = GelenNot.Yazi;
            }
        }
        private void btn_Ekle_Click(object sender, EventArgs e)
        {
            Not nt = new Not();
            nt.Yazi = richTextBox1.Text;
            nt.KategoriID = (int)comboBox1.SelectedValue;

            if(btn_Ekle.Text == "Ekle")
            {
                MessageBox.Show(nk.Ekle(nt)); 
            }
            else
            {
                //Düzenleme kısmı
                nt.NotID = GelenNot.NotID;
                MessageBox.Show(nk.Duzenle(nt));
            }

            NotEkran ne = (NotEkran)Application.OpenForms["NotEkran"];
            ne.GridDoldur();
        }
    }
}
