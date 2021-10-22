//skandináv lottó felhasználótól bekért számokkal, hányadik sorsolásra húzzák ki a szelvényt?

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lottoSorsolas
{
    class Program
    {
        
        static bool tartalmaz;

        static void Main(string[] args)
        {
            int[] szelveny = new int[7];
            int[] huzas = new int[7];
            bool telitalalat = false;            
            int sorSzam = 1;
            double huzasSzamlalo = 0;
            bool joASzam;
            //Számok kiválasztása a szelvényen
            Console.WriteLine("Add meg a számokat egyesével 1 és 35 között!");
            for (int i = 0; i < 7; i++)
            {
                Console.Write("{0}. szám: ", sorSzam);
                do
                {
                    int szam = 0;
                    joASzam = int.TryParse(Console.ReadLine(), out szam);
                    if (szam < 1 || szam > 35 || !joASzam)
                    {
                        joASzam = false;
                        Console.WriteLine("Kérlek, hogy 1 és 35 közötti számot adjál meg!");
                    }                        
                    //ezzel az eljárással vizsgálom, hogy a tömb már tartalmazza-e a számot
                    Tartalmaz(szelveny, szam);
                    if (joASzam && tartalmaz) 
                        Console.WriteLine("Már adtál meg ilyen számot, adj meg egy másikat!");
                    if (!tartalmaz && joASzam) szelveny[i] = szam;
                } while (tartalmaz || !joASzam);                                
                sorSzam++;
            }
            //érték szerinti növekvő sorrendbe rakom a tömb elemeit
            Array.Sort(szelveny);

            //Sorsolás
            Random r = new Random();
            do
            {
                for (int i = 0; i < 7; i++)
                {
                    do
                    {
                        int szam = r.Next(1, 36);
                        //itt is megvizsgálom, hogy az aktuálisan húzott szám már ki van-e húzva
                        Tartalmaz(huzas, szam);
                        if(!tartalmaz)
                            huzas[i] = szam;
                    } while (tartalmaz);                     
                    if (i == 6)
                    {
                        //érték szerinti növekvő sorrendbe rakom a tömb elemeit
                        Array.Sort(huzas);
                        telitalalat = true;
                        for (int j = 0; j < 7; j++)
                        {
                            if (szelveny[j] != huzas[j])
                                telitalalat = false;
                        }
                    }
                }
                huzasSzamlalo++;
            } while (!telitalalat);

            //Eredmény
            Console.WriteLine("\n{0}. húzásra lett telitalálatod!", huzasSzamlalo);
            Console.WriteLine("\nEllenőrzés");
            Console.WriteLine("A tippjeid:");
            foreach (int item in szelveny)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine("\nA kihúzott számok:");
            foreach (int item in huzas)
            {
                Console.Write(item + ", ");
            }
            Console.ReadKey();
        }

        static void Tartalmaz(int[] tomb, int szam)
        {
            int index = Array.IndexOf(tomb, szam);
            if (index > -1)
                tartalmaz = true;
            else tartalmaz = false;
        }
    }
}
