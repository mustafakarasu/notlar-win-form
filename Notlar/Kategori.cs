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
    public partial class Kategori : Form
    {
        public Kategori()
        {
            InitializeComponent();
        }

        KategoriKontrol k = new KategoriKontrol();

        private void Kategori_Load(object sender, EventArgs e)
        {
            /** // Burası sayfayı yenilemek için kullanacağı için method şeklinde yazacağız.
            listBox1.DataSource = k.TumKategoriler();
            listBox1.ValueMember = "KategoriId"; // DB den bağımsız Model sınıfına göre
            listBox1.DisplayMember = "KategoriAdi";
            **/
            Yenile();

            btn_Sil.Enabled = false;
            btn_Duzenle.Enabled = false;
        }

        void Yenile()
        {
            listBox1.DataSource = k.TumKategoriler();
            listBox1.ValueMember = "KategoriId";   // DB den bağımsız Model sınıfına göre
            listBox1.DisplayMember = "KategoriAdi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*** 1.Seçenek --> Uzun hali
            Model.Kategori eklenecek = new Model.Kategori();
            eklenecek.KategoriAdi = textBox1.Text;
            k.Ekle(eklenecek);
            string mesaj = k.Ekle(eklenecek);
            Message.Show(mesaj);
            ****/
  
            if (button1.Text == "Ekle")
            {
                //2.Seçenek --> Daha kısa 
                MessageBox.Show(k.Ekle(new Model.Kategori() { KategoriAdi = textBox1.Text }));
            }

            if(button1.Text == "Kaydet")
            {
                Model.Kategori duzenlenecek = new Model.Kategori();
                duzenlenecek.KategoriID = (int)listBox1.SelectedValue;
                duzenlenecek.KategoriAdi = textBox1.Text;
                MessageBox.Show(k.Duzenle(duzenlenecek));
                GroupYenile();
            }
            Yenile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Giris g = (Giris)Application.OpenForms["Giris"];
            g.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1) // indisler 0 dan başlar. Seçilme anlamında
            {
                btn_Sil.Enabled = true;
                btn_Duzenle.Enabled = true;
            }
        }

        private void btn_Sil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Emin misiniz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show(k.Sil(new Model.Kategori() { KategoriID = (int)listBox1.SelectedValue }));
                Yenile();

                /*** Uzun yol
                Model.Kategori silinecek = new Model.Kategori();
                silinecek.KategoriID = (int)listBox1.SelectedValue;
                MessageBox.Show(k.Sil(silinecek));
                Yenile();
                ***/
            }
        }

        void GroupYenile()
        {
            groupBox1.Text = "Yeni Kategori";
            button1.Text = "Ekle";
            textBox1.Text = "";
        }

        //Eğer ESC ye basıldıysa çalışacak Event yazıyoruz.
        public void ESCbasildi(object sender,KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape) // ESC value su 27
            {
                GroupYenile();
            }
        }

        private void btn_Duzenle_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Kategori Düzenle";
            button1.Text = "Kaydet";
            textBox1.Text = ((Model.Kategori)listBox1.SelectedItem).KategoriAdi;
        }
    }
}
