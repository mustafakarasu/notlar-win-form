using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Model;

namespace Arayuz
{
    public partial class NotEkran : Form
    {
        //private int myVar;  // Asıl olarak kulanılacak private değişken. Başka yerden erişilemez.
        //public int MyProperty // Dışarıya açık public değişken. Private alanı kontrol ediyor.
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}

        /**** NotAra FORMUNDAN BURAYA GELECEK DEĞİŞKENLER ***/
        public string ArananKelime { get; set; }
        public DateTime? ArananTarih { get; set; }

        public RadioButton Ryeninotlar
        {
            get { return radioButton1; }
        }
        public RadioButton Rokunmamislar
        {
            get { return radioButton2; }
        }
        public RadioButton Rtumnotlar
        {
            get { return radioButton3; }
        }

        public NotEkran()
        {
            InitializeComponent();
        }

        NotControl n = new NotControl();

        public void GridDoldur()
        {
            List<Not> notliste = new List<Not>();

            //Yeni notlar
            if (radioButton1.Checked) // Yeni notlar seçildiğinde
            {
                //dataGridView1.DataSource = n.YeniNotlar();
                notliste = n.YeniNotlar();
            }

            // Okunmamış Notlar
            else if (radioButton2.Checked)
            {
                //dataGridView1.DataSource = n.OkunmamislariNotlar(); --> Bunları üç kere yazmamak için listeye ekledik.
                notliste = n.OkunmamislariNotlar();
            }

            //Tüm Notlar
            else if (radioButton3.Checked)
            {
                //dataGridView1.DataSource = n.TumNotlar();
                notliste = n.TumNotlar();
            }

            if (ArananKelime != null)
                notliste = notliste.Where(x => x.Yazi.Contains(ArananKelime)).ToList();
            //if(ArananTarih !=DateTime.MinValue) --> DateTime? değilse
            if (ArananTarih != null)
                notliste = notliste.Where(n => n.Tarih == ArananTarih).ToList();

            dataGridView1.DataSource = notliste;

            //Datagridview da okunmamış satırlar sarı renginde
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if ((bool)item.Cells["OkunduMu"].Value == false)
                    item.DefaultCellStyle.BackColor = Color.Yellow;
                else
                    item.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Yeni notlar
            GridDoldur();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Okunmamış Notlar
            GridDoldur();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //Tüm Notlar
            GridDoldur();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //EkleDuzenel formu.Ekle butonu
            EkleDuzenle ed = new EkleDuzenle();
            ed.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Duzenle butonu.
            if (dataGridView1.SelectedRows.Count > 0)
            {
                EkleDuzenle ed = new EkleDuzenle();
                ed.Btn_Ekle.Text = "Kaydet";
                ed.GelenNot = (Not)dataGridView1.SelectedRows[0].DataBoundItem;
                ed.Show();
            }
            else
                MessageBox.Show("Düzenlemek istediğiniz notu seçin.");           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Sil butonu
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Seçilen kayıt silinsin mi?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    object id = dataGridView1.SelectedRows[0].Cells["NotID"].Value; //SelectedRows[0]: Seçilen satır demek anlamında. Çoğul old.
                    MessageBox.Show(n.Sil(new Not() { NotID = (int) id }));
                    GridDoldur();
                }
            }
            else
                MessageBox.Show("Silinecek notu seçiniz.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotAra nara = new NotAra();
            nara.Show();
        }

        private void NotEkran_Load(object sender, EventArgs e)
        {
            GridDoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Okundu olarak işaretle butonu
            List<int> idliste = new List<int>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if ((bool)item.Cells["OkunduMu"].Value == true)
                {
                    int id =(int) item.Cells["NotID"].Value;
                    idliste.Add(id);
                }
            }
            n.Oku(idliste);
            GridDoldur();
        }
    }
}
