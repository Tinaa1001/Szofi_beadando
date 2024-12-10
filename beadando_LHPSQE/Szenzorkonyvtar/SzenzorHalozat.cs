using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szenzorkonyvtar
{
    public class Szenzor   //Tóth Krisztina
    {
        public static Random randomszam = new Random(); //random szám generálása a randomszam változóba
        public string Id { get; set; }   //A Szenzor osztály azonosító tulajdonsága

        public Szenzor(string id)  //A Szenzor osztály konstruktora
        {
            Id = id;               //Beállítja az azonosító tulajdonságát a kapott id paraméter értékére
        }
        public double AdatGeneralas()      //egy függvény amelynek a visszatéri értéke mindig
        {
            return Math.Round(randomszam.Next(15, 29) + randomszam.NextDouble(), 2); //egy 15 és 29 közötti egész szám és egy 0.0 és 1.0 közötti
                                                                                     //lebegőpontos szám összege két tizedesjegyre kerekítve
        }
    }
}
