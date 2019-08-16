using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPCore.Models
{
    public class Barang
    {
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public int Jumlah { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public string Deskripsi { get; set; }
    }
}
