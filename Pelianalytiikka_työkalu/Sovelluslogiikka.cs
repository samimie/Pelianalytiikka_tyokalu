using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Pelaajia tänään: " + this.tietokanta.haePaivanPelaajat(peli) +
            "\nPelaajia viikossa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelaajia kuukaudessa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelin tuotto: " + this.tietokanta.haePelinTuotto(peli) + " euroa." +
            "\nPelin keskimääräisen pelisession pituus: " + this.tietokanta.haePelisessionKeskipituus(peli) + " minuuttia." +
            "\n \n");
        }

        public void HaePelaajanTiedot(string etunimi, string sukunimi)
        {
            Console.WriteLine("Pelaajan " + etunimi + " " + sukunimi + " tiedot: "+
            "\nPelaajan rahasiirtojen summa: " + this.tietokanta.haeRahasiirtojenSumma(etunimi) +
            "\nKeskimääräiset päivittäiset ostot: " + this.tietokanta.haePaivanKeskimaaraisetOstot(etunimi, sukunimi) +
            "\nPelaajan viikon aikana pelatut tunnit: " + this.tietokanta.haePelaajanViikottaisetPelitunnit(etunimi, sukunimi) +
            "\nPelaajan tänään pelatut tunnit: " + this.tietokanta.haePelaajanPaivittaisetPelitunnit(etunimi, sukunimi) +
            "\n \n");
        }

        public void HaePelistudionTiedot(string pelistudio)
        {
            this.tietokanta.haePelistudionPelit(pelistudio);
        }
    }
}
