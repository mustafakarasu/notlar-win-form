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
    public class NotControl
    {
        Data d = new Data();
        public Not IDdenNotGetir(int id)
        {
            d.komut.CommandText = "SELECT * FROM tblNot WHERE NotID = @pnid";
            d.komut.Parameters.AddWithValue("pnid", id);
            DataRow dr = d.SatirGetir();

            Not n = new Not(); // Not gelmiyorsa classına git public yap.
            n.NotID = (int)dr["NotID"];
            n.Yazi = dr["Yazi"].ToString();
            n.Tarih = (DateTime)dr["Tarih"];
            n.KategoriID = (int)dr["KategoriID"];
            return n;
        }

        public List<Not> TumNotlar()
        {
            return FillDoldur("SELECT * FROM tblNot ORDER BY KategoriID");
        }

        public List<Not> KatIDdenNotlar(int katid)
        {
            //Belli bir kategoriye ait olan notların listesi
            return FillDoldur("SELECT * FROM tblNot WHERE KategoriID=" + katid);
        }
        public List<Not> YeniNotlar()
        {
            //En son eklenen 10 not listesi
            return FillDoldur("SELECT TOP 10 * FROM tblNot ORDER BY NotID DESC");
        }

        public List<Not> OkunmamislariNotlar()
        {
            //Okunmayan notları getir.
            return FillDoldur("SELECT * FROM tblNot WHERE OkunduMu=0 ORDER BY NotID DESC");
        }
        public List<Not> FillDoldur(string sorgu)
        {
            d.komut.CommandText = sorgu;
            DataTable dt = d.TabloGetir();
            List<Not> listeNot = new List<Not>();
            foreach (DataRow item in dt.Rows)
            {
                Not n = new Not();
                n.NotID = (int)item["NotID"];
                n.Yazi = item["Yazi"].ToString();
                n.Tarih = (DateTime)item["Tarih"];
                n.KategoriID = (int)item["KategoriID"];
                listeNot.Add(n);
            }
            return listeNot;
        }
        public string Ekle(Not n)
        {
            d.komut.CommandText = "INSERT INTO tblNot (Yazi,KategoriID) VALUES (@pyazi,@pkatid)";
            d.komut.Parameters.AddWithValue("pyazi", n.Yazi);
            d.komut.Parameters.AddWithValue("pkatid", n.KategoriID);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s, "not", "eklendi.");
        }
        public string Sil(Not n)
        {
            d.komut.CommandText = "DELETE FROM tblNot WHERE NotID = @pnid";
            d.komut.Parameters.AddWithValue("pnid", n.NotID);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s, "not", "silindi.");
        }
        public string Duzenle(Not n)
        {
            d.komut.CommandText = "UPDATE tblNot SET Yazi=@pyazi, KategoriID=@pkatid WHERE NotID=@pnotid";
            d.komut.Parameters.AddWithValue("pyazi", n.Yazi);
            d.komut.Parameters.AddWithValue("pkatid", n.KategoriID);
            d.komut.Parameters.AddWithValue("pnotid", n.NotID);
            int s = d.KomutCalistir();
            return Mesaj.CalistirSonuc(s, "not", "düzenlendi.");
        }

        public void Oku(List<int> idler)
        {
            string sql = "";
            foreach (int item in idler)
            {
                // d.komut.CommandText =.. --> Her sorgu için DB ye gidecek.Bunun için biriktirelim.
                sql += "UPDATE tblNot SET OkunduMu=1 WHERE NotID="+item+"; ";
            }
            d.komut.CommandText = sql;
            d.KomutCalistir();
        }
    }
}
