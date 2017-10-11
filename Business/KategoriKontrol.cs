using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veri;

namespace Business
{
    public class KategoriKontrol
    {
        Data d = new Data();

        public Kategori IDdenKatGetir(int id)
        {
            //Sadece bir satır gelecek old. DateRow gelecek.
            d.komut.CommandText = "SELECT * FROM tblKategori WHERE KategoriID = @pid";
            d.komut.Parameters.AddWithValue("pid",id);
            DataRow dr = d.SatirGetir();

            Kategori k = new Kategori();
            k.KategoriID = (int) dr["KategoriID"]; //DB den çekilen ifadeyi nesneye dönüştürdük.
            k.KategoriAdi = dr["KategoriAdi"].ToString();
            return k;
        }

        public List<Kategori> TumKategoriler()
        {
            d.komut.CommandText = "SELECT * FROM tblKategori ORDER BY KategoriAdi ASC";
            DataTable dt = d.TabloGetir();

            List<Kategori> liste = new List<Kategori>();
            foreach (DataRow item in dt.Rows)
            {
                Kategori k = new Kategori(); // her gelen kategori için boş bir nesne ürettik.
                k.KategoriID = (int)item["KategoriID"];
                k.KategoriAdi = item["KategoriAdi"].ToString();
                liste.Add(k);
            }
            return liste;
        }

        public string Ekle(Kategori k)
        {
            d.komut.CommandText = "INSERT INTO tblKategori (KategoriAdi) VALUES(@pkadi)";
            d.komut.Parameters.AddWithValue("pkadi",k.KategoriAdi);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s, "kategori","eklendi.");
        }
        public string Sil(Kategori k)
        {
            d.komut.CommandText = "DELETE FROM tblKategori WHERE KategoriID=@pkid";
            d.komut.Parameters.AddWithValue("pkid",k.KategoriID);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s,"kategori","silindi");
        }
        public string Duzenle (Kategori k)
        {
            d.komut.CommandText = "UPDATE tblKategori SET KategoriAdi = @pkatadi WHERE KategoriID=@pkatid";
            d.komut.Parameters.AddWithValue("pkatadi",k.KategoriAdi);
            d.komut.Parameters.AddWithValue("pkatid",k.KategoriID);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s,"kategori","düzenlendi.");
        }

        /*********************
        //1.Veri katmanına referans ekleyelim.
        //2.Data.cs nin public old. kontrol edelim.
        //3.using bloğuna "using Veri;"
        *************************/
    }
}
