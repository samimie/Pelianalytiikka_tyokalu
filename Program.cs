using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Pelianalytiikka_työkalu;

namespace TietokantaTesti
{
    class Program
    {
        static void Main(string[] args)
        {
            string tietokantaYhteysString = "Server=localhost;Database=Pelianalytiikka;Uid=root;pwd=muhmah93;SSL Mode=None;";
            Tietokanta peliAnalytiikka = new Tietokanta(tietokantaYhteysString);
            Sovelluslogiikka tietojenKasittely = new Sovelluslogiikka(peliAnalytiikka);

            tietojenKasittely.GetTietokanta().AvaaYhteys();

            //Käyttöliittymälooppi ja sen muuttujat
            string choice = "";
            int input = 0;
            while (input != 4)
            {
                //Käyttöliittymän määrittely
                Console.WriteLine("Valitse toiminnallisuus syöttämällä numero:\n" +
                                    "1: Katsele pelien tietoja\n" +
                                    "2: Katsele Pelaajien tietoja\n" +
                                    "3: Katsele Pelistudioiden tietoja\n" +
                                    "4: Lopeta tietojen katselu\n");

                // Valinnan talletus
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    //Pelien tiedot
                    case 1:
                        Console.WriteLine("Minkä pelin tietoja haluat katsella?\n");
                        Console.WriteLine("WoW\nCS: GO\nDead By Daylight\nHearthstone\n");
                        choice = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelinTiedot(choice);
                        break;
                    case 2:
                        Console.WriteLine("Kenen pelaajan tietoja haluat katsella? Anna etunimi:\n");
                        Console.WriteLine("Matti\nTeppo\nSeppo\n");
                        string etunimi = Console.ReadLine().ToLower();
                        Console.WriteLine("Anna sukunimi:\n");
                        Console.WriteLine("Mursu\nVirtanen\nTaalasmaa\n");
                        string sukunimi = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelaajanTiedot(etunimi, sukunimi);
                        break;
                    case 3:
                        Console.WriteLine("Minkä pelistudion tietoja haluat katsella?\n");
                        Console.WriteLine("Blizzard\nValve\nBehaviour Inc\n");
                        choice = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelistudionTiedot(choice);
                        break;
                }
                
                
            }
            tietojenKasittely.GetTietokanta().SuljeYhteys();
        }
    }

}

