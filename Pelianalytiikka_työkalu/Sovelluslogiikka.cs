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
            this.tietokanta.haePelinTuotto(peli);
            Console.WriteLine("Pelaajia tänään: " + this.tietokanta.haePaivanPelaajat(peli) +
            "\nPelaajia viikossa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelaajia kuukaudessa: " + this.tietokanta.haeKuukaudenPelaajat(peli) +
            "\nPelin tuotto: " + this.tietokanta.haePelinTuotto(peli) + "\n \n");
        }

        public void HaePelaajanTiedot(string etunimi, string sukunimi)
        {
            this.tietokanta.haeRahasiirtojenSumma(etunimi);
        }

        public void HaePelistudionTiedot(string pelistudio)
        {
            this.tietokanta.haePelistudionPelit(pelistudio);
        }
    }
}
