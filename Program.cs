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
                        
                        new MedicalCentre(fullName, city, type, sheetId.ToString());
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
            try
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

                MedicalCentre.Centres[0].Save();

                Console.WriteLine("Файл считан (esc - выход)");
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
