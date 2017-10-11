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
    public partial class NotAra : Form
    {
        public NotAra()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Filtrele butonu
            NotEkran nekran = (NotEkran) Application.OpenForms["NotEkran"];
            if (nekran != null)
            {
                nekran.Activate(); //Gizlenmiş, yada arkada olabilir. Focuslansın. Öne çıksın.   
            }  
            else
            {
                nekran = new NotEkran(); // Yeni form açıldığında rbutonarından hiçbiri seçili değil.
                nekran.Rtumnotlar.Checked = true; // Tüm notlar rbutonu.
                nekran.Show();
            }

            if (checkBox1.Checked)
                nekran.ArananTarih = dateTimePicker1.Value;
            else
                nekran.ArananTarih = null;

            if (!string.IsNullOrEmpty(textBox1.Text))
                nekran.ArananKelime = textBox1.Text;
            else
                nekran.ArananKelime = null;

            nekran.GridDoldur();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                dateTimePicker1.Enabled = true;
            else
                dateTimePicker1.Enabled = false;
        }

        private void NotAra_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
        }
    }
}
