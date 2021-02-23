using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace medConvert
{
    class DBWriter
    {
        static string server = "127.0.0.1",
                      userId = "root",
                      password = "root",
                      database = "medicine",
                      port = "3306";

        static MySqlConnection sqlClient = new MySqlConnection($"Server={server};UserId={userId};Password={password};Database={database}");

        public static void InsertCenter(MedicalCentre centre)
        {
            sqlClient.Open();

            MySqlCommand command = 
                new MySqlCommand($"INSERT INTO med_uchr (fullName, okato, type, id_mun_obr, vid) VALUES (?name, ?location, ?type, 1, ?vid)", sqlClient);

            command.Parameters.AddWithValue("?name", centre.FullName);
            command.Parameters.AddWithValue("?location", centre.Location);
            command.Parameters.AddWithValue("?type", centre.Type);
            command.Parameters.AddWithValue("?vid", centre.Vid);

            command.ExecuteNonQuery();
            centre.Id = command.LastInsertedId;

            sqlClient.Close();
        }

        public static void InsertRates(MedicalCentre centre)
        {
            foreach (var e in centre.Rates)
            {
                sqlClient.Open();

                MySqlCommand command = new MySqlCommand("INSERT INTO pokazateli () VALUES ()", sqlClient);

                command.ExecuteNonQuery();

                sqlClient.Close();
            }
        }
    }
}
