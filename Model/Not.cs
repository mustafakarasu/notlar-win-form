using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Not
    {
        public int NotID { get; set; }
        public string Yazi { get; set; }
        public DateTime Tarih { get; set; }
        public int KategoriID { get; set; }
        public bool OkunduMu { get; set; }

        // DB deki GETDATE() gibi. Constroctur method.
        public Not()
        {
            Tarih = DateTime.Now;
        }
    }
}
