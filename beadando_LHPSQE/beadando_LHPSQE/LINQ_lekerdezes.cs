using beadando_LHPSQE;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace beadando_LHPSQE
{
    public class LINQ_lekerdezes //Nagy Máté Iván
    {
        private string _adatbazisNev;

        // Konstruktor, amely beállítja az adatbázis nevét
        public LINQ_lekerdezes(string adatbazisNev)
        {
            _adatbazisNev = adatbazisNev;  // A kapott adatbázis nevét elmentjük az osztály változójába
        }

        // LINQ-lekérdezés: Minden szenzor legfrissebb mérése
        public IEnumerable<SzenzorErtekek> LegfrissebbAdatok()
        {
            using (var db = new LiteDatabase(_adatbazisNev))  // Csatlakozunk a LiteDB adatbázishoz
            {
                var collection = db.GetCollection<SzenzorErtekek>("ertekek");  // Lekérdezzük az "ertekek" nevű gyűjteményt

                // LINQ: Csoportosítjuk a mérési adatokat a szenzor azonosítók (SzenzorId) alapján,
                // és minden csoportból kiválasztjuk a legfrissebb (legújabb időbélyegű) mérést.
                return collection
                    .FindAll()  // Az összes adat lekérése
                    .GroupBy(x => x.SzenzorId)  // Csoportosítás a szenzorok azonosítója alapján
                    .Select(group => group.OrderByDescending(x => x.Ido).First())  // A legfrissebb adat kiválasztása minden csoportból
                    .ToList();  // Eredmény visszaadása listaként
            }
        }

        // LINQ-lekérdezés: Azok a szenzorok, amelyek mért értékei meghaladják a megadott küszöböt
        public IEnumerable<string> SzenzorokMagasErtekkel(double kuszob)
        {
            using (var db = new LiteDatabase(_adatbazisNev))  // Csatlakozunk az adatbázishoz
            {
                var collection = db.GetCollection<SzenzorErtekek>("ertekek");  // Lekérdezzük az "ertekek" gyűjteményt

                // LINQ: Azokat a mérési adatokat vesszük figyelembe, ahol a mért érték nagyobb, mint a megadott küszöb.
                // A szenzorok azonosítóit egyedileg (Distinct) visszaadjuk.
                return collection
                    .FindAll()  // Az összes adat lekérése
                    .Where(x => x.MertErtek > kuszob)  // Csak azokat a szenzorokat választjuk, amelyek mért értéke meghaladja a küszöböt
                    .Select(x => x.SzenzorId)  // A szenzorok azonosítóját választjuk
                    .Distinct()  // Az egyedi szenzorok visszaadása
                    .ToList();  // Az eredmény visszaadása listaként
            }
        }

        // LINQ-lekérdezés: Az összes mért adat alapján az átlagos hőmérséklet kiszámítása
        public double AtlagosHomerseklet()
        {
            using (var db = new LiteDatabase(_adatbazisNev))  // Csatlakozunk az adatbázishoz
            {
                var collection = db.GetCollection<SzenzorErtekek>("ertekek");  // Lekérdezzük az "ertekek" gyűjteményt

                // LINQ: Az összes mérési érték átlaga. Ha nincs adat, alapértelmezett értékként 0-t használunk.
                return collection
                    .FindAll()  // Az összes adat lekérése
                    .Select(x => x.MertErtek)  // A mért értékeket választjuk ki
                    .DefaultIfEmpty(0)  // Ha nincs mérési adat, 0-t használunk alapértelmezett értékként
                    .Average();  // Az összes mért érték átlagának számítása
            }
        }
    }
}