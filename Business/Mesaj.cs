using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class Mesaj
    {
        public static string CalistirSonuc(int sayi,string tur, string islem)
        {
            if (sayi > 0)
            {
                return "Başarıyla " + tur + " " + islem;
            }
            else
                return "Bir hata oluştu." + Veri.Data.Hata;  // using kullanmadan böyle yazdık.

            //Eğer sayi 0 dan büyükse 
            //Başarıyla tür islem. -> Örnek: (Başarıyla kategori silindi; Başarıyla not güncellendi.)
            //Değilse, Bir hata oluştu.
        }
    }
}
