using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteDB; //A LiteDatabase nuget package névtere az adatbáziskezeléshez
using Szenzorkonyvtar; //A DLL névtere
using Newtonsoft.Json; //A JSON nuget package névtere


namespace beadando_LHPSQE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Tóth Krisztina
            //Adatok létrehozása
            var szenzorok = new List<Szenzor> //List típusú variáns létrehozása a Szenzor osztály metódusai szerint
            {
                new Szenzor("ABC1"), //Szenzor típusú objektumot hoz létre, mindegyik egyedi azonosítóval
                new Szenzor("DEF2"),
                new Szenzor("GHI3")
            };

            //Tóth Krisztina
            //Adatok adatbázisban való elhelyezése
            using (var db = new LiteDatabase("szenzorhalozat.db"))  //szenzorhalozat.db használata (létrehozza ha még nincs)
            {
                var collection = db.GetCollection<SzenzorErtekek>("ertekek"); // Kollekció létrehozása az adatok tárolásához

                Console.WriteLine("Szenzorhálózat szimulációjának indítása...");  //Visszajelzés, hogy az adatot sikeresen befogadtuk és
                                                                                  //el tud indulni a szimuláció
                /*
                foreach (var elem in szenzorok)   //Végigmegyünk a szenzorok lista adatain egy elem nevű változóval
                {
                    //Értékek generálása
                    double ertek = elem.AdatGeneralas();   //meghívjuk az értékgeneráló fv-t amely random számokat generál értékként
                    var adat = new SzenzorErtekek       //átadjuk egy új változóban az azonosítót, 
                                                        //az értéket és az (akkori) pontos időt
                    {
                        SzenzorId = elem.Id,
                        MertErtek = ertek,
                        Ido = DateTime.Now
                    };

                    // Adat mentése a kollekcióba az új változó segítségével egyben
                    collection.Insert(adat);

                    // Adat kiírása a konzolra futtatáskor
                    Console.WriteLine($"[{adat.Ido}] Szenzor {adat.SzenzorId}: {adat.MertErtek} °C");
                }
                Console.WriteLine("\nA szenzor adatainak lefuttatása és mentése sikeresen véget ért.");  //Tudatjuk a felhasználóval
                                                                                                         //mikor ért véget az adatok lefuttatása
                */

                //Tóth Krisztina
                //Adtok kiiratása az adatbázisból (visszajelzés)
                Console.WriteLine("\nAz adatbázis adatai:");  //Kiiratjuk az adatbázisba beolvasott adatokat
                
                var mindenadat = collection.FindAll();     //vesszük a kollekció minden adatát egy változóban
                foreach (var item in mindenadat)           //Végigmegyünk az új változó segítségével az adatokon egy item változóval
                {
                    Console.WriteLine($"{item.Id} - {item.SzenzorId}: {item.MertErtek} °C - [{item.Ido}]");   //kiiratjuk a részadatokat egyben
                }

                Console.WriteLine("\nA folytatáshoz nyomjon le egy billentyűt.");  //Futtatási rész vége
                Console.ReadKey();

                //Tóth Krisztina
                //Adatok JSON fáljba való kiiratása
                string jsonString = JsonConvert.SerializeObject(mindenadat, Formatting.Indented);  //beolvassuk a már beolvasott adatokat egy
                                                                                                   //json string változóba
                File.WriteAllText("jsonadatok.json", jsonString);      //Kiiratjuk a json string változó tartalmát egy json fileba

                Console.WriteLine("\nA JSON fájl sikeresen elkészült.");    //Visszajelzés a sikeres fileiratásról

                Console.WriteLine("\nA kilépéshez nyomjon le egy billentyűt.");   //Kilépés a futtatásból
            }
            Console.ReadKey();
        }
    }
}
