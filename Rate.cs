using System;
using System.Collections.Generic;

namespace medConvert
{
    class Rate
    {
        public int Year { get; private set; } = 0;
        public int StaffTotal { get; private set; } = 0;
        public int StaffAvgMonth { get; private set; } = 0;
        public double SalaryAvg { get; private set; } = 0;
        public double Finance { get; private set; } = 0;
        public double SalaryFund { get; private set; } = 0;
        public double SalarySeniorStaff { get; private set; } = 0;
        public double SalaryMiddleStaff { get; private set; } = 0;
        public double SalaryJunStaff { get; private set; } = 0;

        public Rate(int year, int staffTotal, int staffAvg, double salaryAvg, double finance, double salaryFund)
        {
            Year = year;
            StaffTotal = staffTotal;
            StaffAvgMonth = staffAvg;
            SalaryAvg = salaryAvg;
            Finance = finance;
            SalaryFund = salaryFund;
        }

        public Rate(int year, int staffTotal, int staffAvg, double salaryAvg, double finance, double salaryFund, double salarySen, double salaryMid, double salaryJun)
        {
            Year = year;
            StaffTotal = staffTotal;
            StaffAvgMonth = staffAvg;
            SalaryAvg = salaryAvg;
            Finance = finance;
            SalaryFund = salaryFund;
            SalarySeniorStaff = salarySen;
            SalaryMiddleStaff = salaryMid;
            SalaryJunStaff = salaryJun;
        }
    }
}
