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
            this.tietokanta.haePelinViikottaisetKayttajat(peli);
        }

        public void HaePelaajanTiedot(string etunimi, string sukunimi)
        {

        }

        public void HaePelistudionTiedot(string pelistudio)
        {

        }
    }
}
