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

        public void haePelinPaivittaisetKayttajat(string peli)
        {
                      
                // Tietokantakyselyn tekeminen
                MySqlCommand cmd = new MySqlCommand("select * from Peli", this.tietokantaYhteys);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    // Read advances to the next row.
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("Peli_ID"));
                        string nimi = reader.GetString(reader.GetOrdinal("Nimi"));

                        Console.WriteLine(id + " " + nimi);
                    }
                }
        }
        public void haePelinViikottaisetKayttajat(string peli)
        {

        }

    }
}
