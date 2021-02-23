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

        public static long InsertCenter(MedicalCentre centre)
        {
            long insert_id = 0;

            sqlClient.Open();

            MySqlCommand command = 
                new MySqlCommand($"INSERT INTO med_uchr (fullName, okato, type, id_mun_obr, vid) VALUES (?name, ?location, ?type, 1, ?vid)", sqlClient);

            command.Parameters.AddWithValue("?name", centre.FullName);
            command.Parameters.AddWithValue("?location", centre.Location);
            command.Parameters.AddWithValue("?type", centre.Type);
            command.Parameters.AddWithValue("?vid", centre.Vid);

            command.ExecuteNonQuery();
            insert_id = command.LastInsertedId;

            sqlClient.Close();

            return insert_id;
        }

        public static void InsertRates(MedicalCentre centre)
        {
            foreach (var rate in centre.Rates)
            {
                sqlClient.Open();

                MySqlCommand command;
                
                if (rate.Year == 2018)
                {
                    command = new MySqlCommand("INSERT INTO pokazateli " +
                        "(id_med_uchr, year, staffTotal, staffAvgMonth, salaryAvg, finance, salaryFund, salarySeniorStaff, salaryMiddleStaff, salaryJunStaff) " +
                        "VALUES (?medId, ?year, ?staffTotal, ?staffAvg, ?salaryAvg, ?finance, ?salaryFund, ?salSenior, ?salMiddle, ?salJun)", sqlClient);

                    command.Parameters.AddWithValue("?salSenior", rate.SalarySeniorStaff);
                    command.Parameters.AddWithValue("?salMiddle", rate.SalaryMiddleStaff);
                    command.Parameters.AddWithValue("?salJun", rate.SalaryJunStaff);
                } else
                {
                    command = new MySqlCommand("INSERT INTO pokazateli (id_med_uchr, year, staffTotal, staffAvgMonth, salaryAvg, finance, salaryFund) " +
                    "VALUES (?medId, ?year, ?staffTotal, ?staffAvg, ?salaryAvg, ?finance, ?salaryFund)", sqlClient);
                }

                command.Parameters.AddWithValue("?medId", centre.Id);
                command.Parameters.AddWithValue("?year", rate.Year);
                command.Parameters.AddWithValue("?staffTotal", rate.StaffTotal);
                command.Parameters.AddWithValue("?staffAvg", rate.StaffAvgMonth);
                command.Parameters.AddWithValue("?salaryAvg", rate.SalaryAvg);
                command.Parameters.AddWithValue("?finance", rate.Finance);
                command.Parameters.AddWithValue("?salaryFund", rate.SalaryFund);

                command.ExecuteNonQuery();

                sqlClient.Close();
            }
        }
    }
}
