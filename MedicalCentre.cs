using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace medConvert
{
    class MedicalCentre
    {
        private static List<MedicalCentre> centres = new List<MedicalCentre>();
        public static List<MedicalCentre> Centres { get; private set; }

        private List<Rate> rates = new List<Rate>();

        public int          Id { get; private set; }
        public string       FullName { get; private set; }
        public string       City { get; private set; }
        public int          Type { get; private set; }
        public List<Rate>   Rates { get; private set; }

        public static void InsertAllToDB()
        {
            const string server = "127.0.0.1",
                         userId = "root",
                         password = "root",
                         database = "medicine",
                         port = "3306";

            MySqlConnection sqlClient = new MySqlConnection($"Server={server};UserId={userId};Password={password};Database={database}");
            Debug.WriteLineIf(sqlClient.Ping(), "Соединение с БД установлено");

            sqlClient.Open();

            MySqlCommand command = new MySqlCommand("SELECT * FROM med_uchr", sqlClient);
            MySqlDataReader reader = command.ExecuteReader();

            sqlClient.Close();
        }

        public MedicalCentre(string fullName, string city, int type)
        {
            this.FullName = fullName;
            this.City = city;
            this.Type = type;

            Debug.WriteLine(this);
            centres.Add(this);
        }

        override public string ToString()
        {
            return $"{this.FullName} - {this.City}";
        }
    }
}
