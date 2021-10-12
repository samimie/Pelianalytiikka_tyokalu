using System;

namespace Pelianalytiikka_työkalu
{
    class Sovelluslogiikka
    {
        private Tietokanta tietokanta;

        public Sovelluslogiikka(Tietokanta tietokanta)
        {
            this.tietokanta = tietokanta;
        }
        public Tietokanta GetTietokanta()
        {
            return this.tietokanta;
        }
        public void HaePelinTiedot(string peli)
        {
            Console.WriteLine("\nPelaajia tänään: " + this.tietokanta.haePaivanPelaajat(peli) +
            "\nPelaajia viikossa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelaajia kuukaudessa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelin tuotto: " + this.tietokanta.haePelinTuotto(peli) + " euroa." +
            "\nPelin keskimääräisen pelisession pituus: " + this.tietokanta.haePelisessionKeskipituus(peli) + " minuuttia." +
            "\n \n");
        }

        public void HaePelaajanTiedot(string etunimi, string sukunimi)
        {
            Console.WriteLine("\nPelaajan " + etunimi + " " + sukunimi + " tiedot: " +
            "\nPelaajan rahasiirtojen summa: " + this.tietokanta.haeRahasiirtojenSumma(etunimi) + " euroa " +
            "\nKeskimääräiset päivittäiset ostot: " + this.tietokanta.haePaivanKeskimaaraisetOstot(etunimi, sukunimi) + " euroa " +
            "\nKeskimääräiset päivittäiset pelimäärät: " + this.tietokanta.haePaivanKeskimaaraisetPelimaarat(etunimi, sukunimi) + " minuuttia " +
            "\nPelaajan viikon aikana pelatut minuutit: " + this.tietokanta.haePelaajanViikottaisetPelimaarat(etunimi, sukunimi) + " minuuttia " +
            "\nPelaajan tänään pelatut minuutit: " + this.tietokanta.haePelaajanPaivittaisetPelimaarat(etunimi, sukunimi) + " minuuttia " +
            "\n \n");
        }

        public void HaePelistudionTiedot(string pelistudio)
        {
            Console.WriteLine("\n");
            this.tietokanta.haePelistudionPelit(pelistudio);
        }

        public void HaeTopTenTuottavimmatPelaajat()
        {
            this.tietokanta.HaeTopTenTuottavimmatPelaajat();
            Console.WriteLine("\n");
        }
        public void HaeTopTenTuottavimmatPelit()
        {
            this.tietokanta.HaeTopTenTuottavimmatPelit();
            Console.WriteLine("\n");
        }
    }
}
