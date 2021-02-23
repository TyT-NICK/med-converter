using System;
using System.Collections.Generic;

namespace medConvert
{
    class Rate
    {
        public int Year { get; private set; } = 0;
        public int StaffTotal { get; private set; } = 0;
        public int StaffAvgMonth { get; private set; } = 0;
        public float SalaryAvg { get; private set; } = 0;
        public float Finance { get; private set; } = 0;
        public float SalaryFund { get; private set; } = 0;
        public float SalarySeniorStaff { get; private set; } = 0;
        public float SalaryMiddleStaff { get; private set; } = 0;
        public float SalaryJunStaff { get; private set; } = 0;

        public Rate()
        {

        }
    }
}
