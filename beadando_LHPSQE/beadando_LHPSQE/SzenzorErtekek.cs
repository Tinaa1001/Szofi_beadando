using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando_LHPSQE
{
    public class SzenzorErtekek  //Tóth Krisztina
    {
        public int Id { get; set; }  // Automatikus azonosító
        public string SzenzorId { get; set; }  // Szenzor azonosítója
        public double MertErtek { get; set; }  // Mért érték
        public DateTime Ido { get; set; }  // Időbélyeg
    }
}
