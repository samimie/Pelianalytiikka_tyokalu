using Pelianalytiikka_työkalu;
using System;

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
            while (input != 6)
            {
                //Käyttöliittymän määrittely
                Console.WriteLine("Valitse toiminnallisuus syöttämällä numero:\n" +
                                    "1: Katsele pelien tietoja\n" +
                                    "2: Katsele Pelaajien tietoja\n" +
                                    "3: Katsele Pelistudioiden tietoja\n" +
                                    "4: Kymmenen tuottavinta pelaajaa\n" +
                                    "5: Kymmenen tuottavinta peliä\n" +
                                    "6: Lopeta tietojen katselu\n");

                // Valinnan talletus
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    //Pelien tiedot
                    case 1:
                        Console.WriteLine("Minkä pelin tietoja haluat katsella?\n");
                        choice = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelinTiedot(choice);
                        break;
                    case 2:
                        Console.WriteLine("Kenen pelaajan tietoja haluat katsella? Anna etunimi:\n");
                        string etunimi = Console.ReadLine().ToLower();
                        Console.WriteLine("Anna sukunimi:\n");
                        string sukunimi = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelaajanTiedot(etunimi, sukunimi);
                        break;
                    case 3:
                        Console.WriteLine("Minkä pelistudion tietoja haluat katsella?\n");
                        choice = Console.ReadLine().ToLower();
                        tietojenKasittely.HaePelistudionTiedot(choice);
                        break;
                    case 4:
                        tietojenKasittely.HaeTopTenTuottavimmatPelaajat();
                        break;
                    case 5:
                        tietojenKasittely.HaeTopTenTuottavimmatPelit();
                        break;
                }


            }
            tietojenKasittely.GetTietokanta().SuljeYhteys();
        }
    }

}

