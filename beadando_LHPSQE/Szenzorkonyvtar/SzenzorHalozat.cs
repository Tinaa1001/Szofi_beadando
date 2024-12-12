using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szenzorkonyvtar
{
    // Nagy Máté Iván
    // Delegált definiálása
    public delegate void ErtekGeneralvaEventHandler(object sender, ErtekEventArgs e);

    // Nagy Máté Iván
    // Eseményargumentumok osztálya
    public class ErtekEventArgs : EventArgs
    {
        public string SzenzorId { get; set; }
        public double MertErtek { get; set; }
        public DateTime Ido { get; set; }
    }
    public class Szenzor   //Tóth Krisztina
    {
        public static Random randomszam = new Random(); //random szám generálása a randomszam változóba
        public string Id { get; set; }   //A Szenzor osztály azonosító tulajdonsága

        public event ErtekGeneralvaEventHandler ErtekGeneralva;  // Nagy Máté Iván
        public Szenzor(string id)  //A Szenzor osztály konstruktora
        {
            Id = id;               //Beállítja az azonosító tulajdonságát a kapott id paraméter értékére
        }
        public double AdatGeneralas()
        {
            Random random = new Random();
            double ertek = Math.Round(random.NextDouble() * 30, 2); // Hőmérséklet generálása 0-50 °C között

            // Nagy Máté Iván
            // Esemény kiváltása
            ErtekGeneralva?.Invoke(this, new ErtekEventArgs
            {
                SzenzorId = Id,
                MertErtek = ertek,
                Ido = DateTime.Now
            });

            return ertek;
        }
    }
}
