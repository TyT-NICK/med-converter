using System;
using System.IO;
using OfficeOpenXml;
using MySqlX;

namespace medConvert
{
    class Program
    {
        static void ReadFile()
        {
            string filePath = $@"{Environment.CurrentDirectory}\med.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                for (int i = 5; i <= 29; i++)
                {
                    // [Row, Column]
                    string fullName = sheet.Cells[i, 2].Value.ToString();
                    string city = sheet.Cells[i, 5].Value.ToString();

                    new MedicalCentre(fullName, city);
                }
            }
        }

        static void Main(string[] args)
        {
            //ReadFile();
            MedicalCentre.InsertAllToDB();
        }
    }
}
