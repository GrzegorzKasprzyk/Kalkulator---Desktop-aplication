﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGKonsoleGame
{
    class Program
    {
        private static Bohater _bohater;
        private static List<IBron> _bronie;
        private static List<Zbroja> _zbroje;


        static void Main(string[] args)
        {
            {
                StworzBronie();
                ObsługaMenu();
            }

            //static 
            void StworzBronie()
            {
                Dane dane = new Dane();
                _bronie = dane.WczytajBronie();
                _zbroje = new List<Zbroja>();

                //List<IBron> bronie = new List<IBron>();
               /* Broń broń = new Broń("Wrzeszczący kijaszek", 3, 4);
                _bronie.Add(broń);
                _bronie.Add(new Broń("Magiczny Róg", 10, 6));
                _bronie.Add(new Broń("Badyl", 1, 10));
                _bronie.Add(new BronDworeczna("Śmiercionośna Dwuręczna Łodyga", 15, 4));
               */

                _zbroje.Add(new Tarcza
                {
                    Nazwa = "Tarcza Niebios",
                    Obrona = 10,
                    PoziomZuzycia = 0,

                });
                Napiersnik napiersnik = new Napiersnik();
                napiersnik.Nazwa = "Klata Chwały";
                napiersnik.Obrona = 15;
                napiersnik.PoziomZuzycia = 0;
                _zbroje.Add(napiersnik);
                

            }

            //static 
            void ObsługaMenu()
            {
                Console.WriteLine("Dokonaj wyboru...");
                Console.WriteLine("\n1. Nowa Gra");
                Console.WriteLine("2. Wczytaj Grę");
                Console.WriteLine("3. Koniec");

                string opcja = Console.ReadLine();
                //Console.WriteLine("Wybrano opcję: " + opcja);

                if (opcja == "1")
                {
                    StworzPostac();


                }

                else if (opcja == "2")
                {

                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Dzięki za grę");
                    return;
                }

                while (opcja != "5")
                {
                    MenuGry();

                    opcja = Console.ReadLine();
                    if (opcja == "0")
                    {
                        _bohater.PokazPostac();

                    }

                    else if (opcja == "1")
                    {
                        IdzNaWyprawe();
                    }
                    else if (opcja == "2")
                    {
                        _bohater.Odpocznij();
                    }
                    else if (opcja == "3")
                    {
                        Console.WriteLine("Opcja chwilowo niedostępna");
                    }
                    else if (opcja == "4")
                    {
                        Sklep();
                    }
                    _bohater.Przegrana();
                    Console.WriteLine("Naciśnij enter, aby kontynuować");
                    Console.ReadLine();

                }

            }


            //static
            void MenuGry()
            {
                Console.Clear();
                Console.WriteLine("0. Zobacz postać");
                Console.WriteLine("1. Idź na wyprawę");
                Console.WriteLine("2. Odpocznij");
                Console.WriteLine("3. Ekwipunek");
                Console.WriteLine("4. Sklep");
                Console.WriteLine("5. Koniec");


            }

            //static 
            int DajWieksza(int liczba1, int liczba2)
            {
                if (liczba1 < liczba2)
                    return liczba2;
                return liczba1;
            }

            //static 
            void StworzPostac()
            {
                Console.Clear();
                Console.Write("Podaj imię postaci: ");
                string imie = Console.ReadLine();
                _bohater = new Bohater(imie);

                _bohater.Imie = Console.ReadLine();
                // _bohater.Level = 1;
                // _bohater.MaksymalneZycie = _bohater.PosiadaneZycie = 10;
            }

            //static 
            void IdzNaWyprawe()

            {
                Console.Clear();
                Console.WriteLine("Wyruszyłeś na wyprawę");
                bool WynikWalki = Walka();

                if (WynikWalki)
                {
                    BonusyZaZwyciestwo();

                }



            }
            //static
            void BonusyZaZwyciestwo()
            {

            }


            //static 
            bool Walka()
            {

                Random losuj = new Random();
                int ZyciePrzeciwnika = losuj.Next(8, 12);



                while (_bohater.PosiadaneZycie > 0)

                {
                    int obrazenia = _bohater.NoszonaBron.ObliczObrazenia();
                    int obrazeniaZadane = losuj.Next(obrazenia - 2, obrazenia + 2);
                    ZyciePrzeciwnika -= obrazeniaZadane;

                    if (ZyciePrzeciwnika <= 0)
                        return true;

                    int obrazeniaOtrzymane = losuj.Next(0, 4);
                    _bohater.PosiadaneZycie -= obrazeniaOtrzymane;
                }
                return true;

            }
            //static 
            void Sklep()
            {
                Console.Clear();
                int licznik = 1;

                foreach (Broń broń in bronie)
                {
                    Console.WriteLine(licznik + ". " + broń.Nazwa);
                    licznik++;
                }



                /*
                for (int nrBroni = 0 ; nrBroni < bronie.Count;nrBroni++)
                {
                    Console.WriteLine((nrBroni + 1) + ". " + bronie[nrBroni].Nazwa);
                }
                */

                foreach(Zbroja zbroja in _zbroje)
                {
                    Console.WriteLine(licznik + ". " + zbroja.Nazwa);
                    licznik++;
                }
                Console.WriteLine("Wybierz broń: ");
                string odczyt = Console.ReadLine();
                int opcja = int.Parse(odczyt);

                if(opcja <= _bronie.Count)
                {
                    Broń wybranaBron = bronie[opcja - 1];
                    _bohater.KupBron(wybranaBron);
                }

                else
                {
                    opcja -= _bronie.Count;
                    Zbroja wybranaZbroja = _zbroje[opcja - 1];
                    if(wybranaZbroja is Tarcza)
                    {
                        _bohater.NoszonaTarcza = wybranaZbroja as Tarcza;
                    }
                    else
                    {
                        _bohater.NoszonyNapiersnik = (Napiersnik)wybranaZbroja;
                    }
                }
                

            }

        }
    }
}
