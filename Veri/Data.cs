using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veri
{
    public class Data
    {
        public static string ServerName { get; set; }
        public static string Database { get; set; }
        public static string UserID { get; set; }
        public static string Password { get; set; }
        public static bool WindowsAuthentication { get; set; }
        public static string Hata { get; set; }

        public SqlCommand komut { get; set; }
                
        public SqlConnection con = new SqlConnection();

        //Data d = new Data(); // Ornek olarak

        public Data()
        {
            ServerName = "DESKTOP-93G7NTJ";
            UserID = "DESKTOP-93G7NTJ\\section-1";
            WindowsAuthentication = true;
            Database = "NotlarDB";

            komut = new SqlCommand();
            string son = WindowsAuthentication ? "Integrated Security = true;" : "Password=" + Password;
            con.ConnectionString = string.Format("Server={0};Database={1};User ID = {2}; {3};", ServerName, Database, UserID, son);
            komut.Connection = con; //tek satır yerine. "",con
        }
        public DataTable TabloGetir()
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Temizlik();
                return dt;
            }
            catch(Exception ex)
            {
                Hata = ex.Message;
                Temizlik();
                return null;
            }            
        }

        //Komut çalıştıktan sonra komut sorgusunu ve parametreleri temizlemek için.
        public void Temizlik()  //Datayı ortak kullandığımızdan sadece önlem için.
        {
            komut = new SqlCommand(); // komut değişkenine boş bir sqlcommand nesnesi oluşturduk.
            komut.Connection = con;
        }

        public DataRow SatirGetir()
        {
            try
            {
                return TabloGetir().Rows[0];
            }
            catch
            {
                return null;
            }
        }
        public string AlanGetir()
        {
            try
            {
                return SatirGetir()[0].ToString();
            }
            catch
            {
                return null;
            }
        }

        public int KomutCalistir() //Ekleme,silme,günceleme gibi
        {
            try
            {
                con.Open();
                int sayi = komut.ExecuteNonQuery();
                Temizlik();
                con.Close();
                return sayi; //jj.return gördüğü anda bloktan çıkar.
            }
            catch(SqlException ex)
            {
                Hata = ex.Message;
                Temizlik();
                return 0;
            }
        }
    }
}
