using System;
using System.IO;
using OfficeOpenXml;
using MySqlX;

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
                        string city = sheet.Cells[row, 4].Value.ToString();
                        //sheet.
                        new MedicalCentre(fullName, city, 1);
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
            OpenFile();
            while(true)
            {
                Console.Write("Путь к файлу: ");
                try
                {
                    OpenFile(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
