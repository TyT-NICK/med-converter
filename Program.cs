using System;
using System.IO;
using OfficeOpenXml;
using MySqlX;
using System.Diagnostics;

namespace medConvert
{
    class Program
    {
        static void ReadFile(FileInfo file)
        {
            const int AMOUNT_OF_TYPES = 8;

            int[] centresAtSheet = new int[AMOUNT_OF_TYPES] { 25, 26, 16, 25, 8, 3, 10, 5 };

            using (ExcelPackage package = new ExcelPackage(file))
            {
                for (int sheetId = 0; sheetId < AMOUNT_OF_TYPES; sheetId++)
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[sheetId];
                    int amountOfCentres = centresAtSheet[sheetId];

                    for (int i = 1; i <= amountOfCentres; i++)
                    {
                        int row = i + 4;

                        // [Row, Column]
                        string fullName = sheet.Cells[row, 2].Value.ToString();
                        string type = sheet.Cells[row, 3].Value.ToString();
                        string city = sheet.Cells[row, 4].Value.ToString();

                        var centre = new MedicalCentre(fullName, city, type, sheetId.ToString());

                        for (int yearColumn = 0; yearColumn < 4; yearColumn++)
                        {
                            int year = 2015 + yearColumn;

                            int staff = Convert.ToInt32((double)sheet.Cells[row, 5 + yearColumn].Value);
                            int staffAvg = Convert.ToInt32((double)sheet.Cells[row, 9 + yearColumn].Value);
                            double salAvg = (double)sheet.Cells[row, 13 + yearColumn].Value;
                            double fin = (double)sheet.Cells[row, 17 + yearColumn].Value;
                            double fund = (double)sheet.Cells[row, 21 + yearColumn].Value;

                            if (year == 2018)
                            {
                                double salSenior = (double)sheet.Cells[row, 28].Value;
                                double salMiddle = (double)sheet.Cells[row, 29].Value;
                                double salJun = (double)sheet.Cells[row, 30].Value;

                                Rate rate = new Rate(year, staff, staffAvg, salAvg, fin, fund, salSenior, salMiddle, salJun);
                                centre.Rates.Add(rate);
                            } else
                            {
                                Rate rate = new Rate(year, staff, staffAvg, salAvg, fin, fund);
                                centre.Rates.Add(rate);
                            }
                            
                        }
                    }
                }
            }
        }

        static void OpenFile()
        {
            string filePath = $@"{Environment.CurrentDirectory}\..\..\..\med.xlsx";
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                throw new Exception("Файл не найден");
            }

            ReadFile(file);
        }

        static void OpenFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                throw new Exception("Неправильно указан путь к файлу");
            }

            ReadFile(file);
        }

        static void Main(string[] args)
        {
            Console.Write("Путь к файлу: ");
            string path = Console.ReadLine();
            if (path != String.Empty)
            {
                OpenFile(path);
            }
            else
            {
                OpenFile();
            }

            MedicalCentre.SaveAll();

            Console.WriteLine("Файл считан (esc - выход)");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
                return;
        }
    }
}
