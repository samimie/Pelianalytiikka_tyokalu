using MySql.Data.MySqlClient;
using System;

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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Pelin Tuotto"));
                    }
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        double temp = reader.GetDouble(reader.GetOrdinal("Pelisessioiden_pituudet"));
                        tulos = temp.ToString("F2");
                    }
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
            string tulos = "";
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Rahasiirtojen summa"));
                    }
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee pelaajan ostot 30 päivän sisällä, jotka jaetaan päivien määrällä, niin saadaan keskimääräiset päivittäiset ostot.
        public string haePaivanKeskimaaraisetOstot(string etunimi, string sukunimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("Select SUM(Summa) AS Paivan_keskimaaraiset_ostot " +
            "FROM Rahasiirto, Pelisessio, Pelaaja " +
            "WHERE Rahasiirto.Sessio_ID = Pelisessio.Sessio_ID " +
            "AND Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + etunimi +
            "' AND Pelaaja.Sukunimi = '" + sukunimi +
            "' AND Alkuaika BETWEEN NOW() - INTERVAL 30 DAY AND NOW()" + ";", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        double temp = reader.GetDouble(reader.GetOrdinal("Paivan_keskimaaraiset_ostot"));
                        temp = temp / 30;
                        tulos = temp.ToString("F2");
                    }
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee pelaajan pelisessioiden summan 30 päivän sisältä, joka myöhemmi njaetaan 30, jotta saadaan päivittäinen keskiarvo.
        public string haePaivanKeskimaaraisetPelimaarat(string etunimi, string sukunimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(TIMESTAMPDIFF(MINUTE, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) AS average " +
            "FROM Pelisessio, Pelaaja " +
            "WHERE Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "AND Pelaaja.Etunimi = '" + etunimi +
            "' AND Pelaaja.Sukunimi = '" + sukunimi +
            "' AND Alkuaika BETWEEN NOW() - INTERVAL 30 DAY AND NOW()" + ";", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        double temp = reader.GetDouble(reader.GetOrdinal("average"));
                        temp = temp / 30;
                        tulos = temp.ToString("F2");
                    }
                }
            }
            reader.Close();
            return tulos;
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Kuukausittaiset_pelaajat"));
                    }
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
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(DISTINCT Pelaaja_ID) AS Viikottaiset_pelaajat " +
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Viikottaiset_pelaajat"));
                    }
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
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(DISTINCT Pelaaja_ID) AS Paivittaiset_pelaajat " +
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Paivittaiset_pelaajat"));
                    }
                }
            }
            reader.Close();
            return tulos;
        }
        //Hakee pelaajan pelisessioiden summan 7 päivän sisällä
        public string haePelaajanViikottaisetPelimaarat(string etunimi, string sukunimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(TIMESTAMPDIFF(MINUTE, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) AS Viikottaiset_tunnit " +
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
                    //Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Viikottaiset_tunnit"));
                    }
                }
            }
            reader.Close();
            return tulos;

        }
        //Hakee pelaajan pelisessioiden summan 24 tunnin sisällä.
        public string haePelaajanPaivittaisetPelimaarat(string etunimi, string sukunimi)
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(TIMESTAMPDIFF(MINUTE, Pelisessio.Alkuaika, Pelisessio.Loppuaika)) AS Paivan_tunnit " +
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
                    // Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        tulos = "null";
                        break;
                    }
                    else
                    {
                        tulos = reader.GetString(reader.GetOrdinal("Paivan_tunnit"));
                    }
                }
            }
            reader.Close();
            return tulos;

        }
        //Hakee kymmenen pelaajaa, joilla on suurimmat rahasiirtojen summat kaikkien pelien välillä.
        public void HaeTopTenTuottavimmatPelaajat()
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT Pelaaja.Pelaaja_ID, Pelaaja.Etunimi, Pelaaja.Sukunimi, SUM(Rahasiirto.Summa) AS Summa " +
            "FROM Rahasiirto LEFT JOIN Pelisessio " +
            "ON Rahasiirto.Sessio_ID = Pelisessio.Sessio_ID " +
            "INNER JOIN Pelaaja ON Pelisessio.Pelaaja_ID = Pelaaja.Pelaaja_ID " +
            "GROUP BY Pelaaja.Pelaaja_ID " +
            "ORDER BY Summa DESC " + "LIMIT 10;", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("\n");
            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    // Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        string etunimi = reader.GetString(reader.GetOrdinal("Etunimi"));
                        string sukunimi = reader.GetString(reader.GetOrdinal("Sukunimi"));
                        int summa = reader.GetInt32(reader.GetOrdinal("Summa"));

                        Console.WriteLine(etunimi + " " + sukunimi + " " + summa);
                    }
                }
            }
            reader.Close();
        }
        //Hakee kymmenen peliä, joissa on isoimmat rahasiirtojen summat.
        public void HaeTopTenTuottavimmatPelit()
        {
            string tulos = "";
            // Tietokantakyselyn tekeminen
            MySqlCommand cmd = new MySqlCommand("SELECT Peli.Peli_ID, Peli.Nimi, SUM(Rahasiirto.Summa) AS Summa " +
            "FROM Rahasiirto LEFT JOIN Pelisessio " +
            "ON Rahasiirto.Sessio_ID = Pelisessio.Sessio_ID " +
            "INNER JOIN Peli ON Pelisessio.Peli_ID = Peli.Peli_ID " +
            "GROUP BY Peli.Peli_ID " +
            "ORDER BY Summa DESC " + "LIMIT 10;", this.tietokantaYhteys);
            MySqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("\n");
            // Check is the reader has any rows at all before starting to read.
            if (reader.HasRows)
            {
                // Read advances to the next row.
                while (reader.Read())
                {
                    // Testataan ettei arvo ole null
                    if (reader.IsDBNull(0))
                    {
                        break;
                    }
                    else
                    {
                        string nimi = reader.GetString(reader.GetOrdinal("Nimi"));
                        int summa = reader.GetInt32(reader.GetOrdinal("Summa"));

                        Console.WriteLine(nimi + " " + summa);
                    }
                }
            }
            reader.Close();
        }
    }
}
