using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beadando_LHPSQE
{
    public class EsemenyKezeles
    {
        // Esemény definíciója
        public event EventHandler<SzenzorErtekek> SzenzorErtekValtozasEvent;

        // Esemény kiváltása
        public void OnSzenzorErtekValtozas(SzenzorErtekek ertekek)
        {
            SzenzorErtekValtozasEvent?.Invoke(this, ertekek);
        }

        // Feliratkozás az eseményre
        public void Feliratkozas()
        {
            SzenzorErtekValtozasEvent += (sender, e) =>
            {
                // Az esemény kezelése
                Console.WriteLine($"Esemény: Szenzor {e.SzenzorId} érték: {e.MertErtek} °C, Idő: {e.Ido}");
            };
        }
    }
}