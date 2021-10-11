using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pelianalytiikka_työkalu
{
    class Tietokanta
    {
        private string tietokantaYhteysString;
        private MySqlConnection tietokantaYhteys;

        //Konstruktori, jossa alustetaan tietokantayhteys käyttämällä tietoKantaYteysString-muuttujan tietoja
        public Tietokanta(string tietokantaYhteysString)
        {
            this.tietokantaYhteysString = tietokantaYhteysString;
            // Tietokantayhteyden luominen
            string connectionString = null;
            MySqlConnection cnn;
            connectionString = "Server=localhost;Database=Pelianalytiikka;Uid=root;pwd=muhmah93;SSL Mode=None;";
            cnn = new MySqlConnection(connectionString);
            this.tietokantaYhteys = cnn;
        }

        public void AvaaYhteys()
        {
            try
            {
                this.tietokantaYhteys.Open();
                Console.WriteLine("Yhteys muodostettu tietokantaan!");

            }
            catch (Exception ex)
            {

                Console.WriteLine("Yhteyden muodostus epäonnistui");
            }
        }

        public void SuljeYhteys()
        {
            this.tietokantaYhteys.Close();
            Console.WriteLine("Yhteys tietokantaan suljettiin");
        }
        //Hakee yksittäisen pelin kaikkien rahasirtojen summan
        public string haePelinTuotto(string peliNimi)
        {
            string summa = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select SUM(Summa) AS 'Pelin Tuotto' " +
                "From Peli, Rahasiirto, Pelisessio " +
                "Where Peli.Peli_ID = Pelisessio.Peli_ID " +
                "AND Pelisessio.Sessio_ID = Rahasiirto.Sessio_ID " +
                "AND Peli.Nimi = '" + peliNimi + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    summa = reader.GetString(reader.GetOrdinal("Pelin Tuotto"));
                }
            }
            reader.Close();
            return summa;
        }
        //Hakee tietyn pelin pelaajien kokonaiskulutuksen keskiarvon
        public string haePelaajienKeskikulutus(string peliNimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select SUM(Summa) AS 'Pelin Tuotto' " +
                "From Peli, Rahasiirto, Pelisessio " +
                "Where Peli.Peli_ID = Pelisessio.Peli_ID " +
                "AND Pelisessio.Sessio_ID = Rahasiirto.Sessio_ID " +
                "AND Peli.Nimi = '" + peliNimi + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tulos = reader.GetString(reader.GetOrdinal("Pelin Tuotto"));
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee tietyn pelin pelaajien keskimääräisen pelisession pituuden
        public string haePelisessionKeskipituus(string peliNimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT AVG(TIMESTAMPDIFF(MINUTE, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) AS Pelisessioiden_pituudet " +
                "FROM Pelisessio, Peli " +
                "WHERE Pelisessio.Peli_ID = Peli.Peli_ID " +
                "AND Peli.Nimi = '" + peliNimi + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tulos = reader.GetString(reader.GetOrdinal("Pelisessioiden_pituudet"));
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee tietyn pelistudion kaikki pelit
        public void haePelistudionPelit(string studioNimi)
        {
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select Peli.Nimi " +
                "FROM Pelistudio, Peli " +
                "WHERE Pelistudio.Nimi = '" + studioNimi + "' AND Peli.Studio_ID = Pelistudio.Studio_ID; ", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    string nimi = reader.GetString(reader.GetOrdinal("Nimi"));
                    Console.WriteLine(nimi);
                }
            }
            reader.Close();
        }
        //Hakee tietyn pelaajan kaikkien rahasiirtojen summan
        public string haeRahasiirtojenSumma(string pelaajaNimi)
        {
            string summa = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select SUM(Summa) AS 'Rahasiirtojen Summa'" +
            " from Rahasiirto, Pelisessio, Pelaaja " +
            "where Rahasiirto.Sessio_ID = Pelisessio.Sessio_ID " +
            "AND Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + pelaajaNimi + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    summa = reader.GetString(reader.GetOrdinal("Rahasiirtojen Summa"));
                }
            }
            reader.Close();
            return summa;
        }
        //Hakee tietyn pelaajan keskimääräiset päivän ostot
        public string haePaivanKeskimaaraisetOstot(string etunimi, string sukunimi)
        {
            string summa = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select AVG(Summa) AS Rahasiirtojen_summa " +
            "FROM Rahasiirto, Pelisessio, Pelaaja " +
            "WHERE Rahasiirto.Sessio_ID = Pelisessio.Sessio_ID " +
            "AND Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + etunimi +
            "' AND Pelaaja.Sukunimi = '" + sukunimi +
            "' AND Alkuaika BETWEEN NOW() - INTERVAL 1 DAY AND NOW()" + ";", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    summa = reader.GetString(reader.GetOrdinal("Rahasiirtojen_summa"));
                }
            }
            reader.Close();
            return summa;
        }
        //Hakee pelin pelisessioista uniikkien Pelaaja_ID-kenttien summan viimeisen 30 päivän sisällä.
        public string haeKuukaudenPelaajat(string peli)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(DISTINCT Pelaaja_ID) AS Kuukausittaiset_pelaajat " +
            "FROM Pelisessio, Peli " +
            "WHERE Alkuaika BETWEEN NOW() - INTERVAL 30 DAY AND NOW() " +
            "AND Pelisessio.Peli_ID = Peli.Peli_ID AND Peli.Nimi ='" + peli + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    tulos = reader.GetString(reader.GetOrdinal("Kuukausittaiset_pelaajat"));
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee pelin pelisessioista uniikkien Pelaaja_ID-kenttien summan viimeisen 7 päivän sisällä.
        public string haeViikonPelaajat(string peli)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(DISTINCT Pelaaja_ID) AS Kuukausittaiset_pelaajat " +
            "FROM Pelisessio, Peli " +
            "WHERE Alkuaika BETWEEN NOW() - INTERVAL 7 DAY AND NOW() " +
            "AND Pelisessio.Peli_ID = Peli.Peli_ID AND Peli.Nimi ='" + peli + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    tulos = reader.GetString(reader.GetOrdinal("Kuukausittaiset_pelaajat"));
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee pelin pelisessioista uniikkien Pelaaja_ID-kenttien summan viimeisen 24 tunnin sisällä.
        public string haePaivanPelaajat(string peli)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(DISTINCT Pelaaja_ID) AS Kuukausittaiset_pelaajat " +
            "FROM Pelisessio, Peli " +
            "WHERE Alkuaika BETWEEN NOW() - INTERVAL 1 DAY AND NOW() " +
            "AND Pelisessio.Peli_ID = Peli.Peli_ID AND Peli.Nimi ='" + peli + "';", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    tulos = reader.GetString(reader.GetOrdinal("Kuukausittaiset_pelaajat"));
                }
            }
            reader.Close();
            return tulos;
        }

        public string haePelaajanViikottaisetPelitunnit(string etunimi, string sukunimi)
        {
            string summa = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(TIMESTAMPDIFF(HOUR, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) " +
            "FROM Pelisessio, Pelaaja " +
            "WHERE Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + etunimi +
            "' AND Pelaaja.Sukunimi = '" + sukunimi +
            "' AND Alkuaika BETWEEN NOW() - INTERVAL 7 DAY AND NOW()" + ";", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    summa = reader.GetString(reader.GetOrdinal("Rahasiirtojen_summa"));
                }
            }
            reader.Close();
            return summa;

        }

        public string haePelaajanPaivittaisetPelitunnit(string etunimi, string sukunimi)
        {
            string summa = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(TIMESTAMPDIFF(HOUR, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) " +
            "FROM Pelisessio, Pelaaja " +
            "WHERE Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + etunimi +
            "' AND Pelaaja.Sukunimi = '" + sukunimi +
            "' AND Alkuaika BETWEEN NOW() - INTERVAL 1 DAY AND NOW()" + ";", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    summa = reader.GetString(reader.GetOrdinal("Rahasiirtojen_summa"));
                }
            }
            reader.Close();
            return summa;

        }

    }
}
