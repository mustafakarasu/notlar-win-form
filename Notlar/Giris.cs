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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kategori k = new Kategori(); // Diğer projeyi görmediğinden Arayuz.Kategori demeye gerek yok.
            k.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotEkran n = new NotEkran();
            n.Ryeninotlar.Checked = true; //Form açılmadan önce rbuton seçili olsun.
            n.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NotEkran n = new NotEkran();
            n.Rtumnotlar.Checked = true;
            n.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NotEkran n = new NotEkran();
            n.Rokunmamislar.Checked = true;
            n.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NotAra nara = new NotAra();
            nara.Show();
            this.Hide();
        }
    }
}
