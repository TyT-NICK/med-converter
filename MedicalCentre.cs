using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace medConvert
{
    class MedicalCentre
    {
        public static List<MedicalCentre> Centres = new List<MedicalCentre>();

        public long         Id { get; set; }
        public string       FullName { get; private set; }
        public string       Location { get; private set; }
        public string       Vid { get; private set; }
        public string       Type { get; private set; }
        public List<Rate> Rates { get; private set; } = new List<Rate>();

        public static void SaveAll()
        {
            foreach (var centre in Centres)
            {
                centre.Save();
            }
        }

        void Save()
        {
            this.Id = DBWriter.InsertCenter(this);
            DBWriter.InsertRates(this);
        }

        public MedicalCentre(string fullName, string city, string type, string vid)
        {
            this.FullName = fullName;
            this.Location = city;
            this.Type = type;
            this.Vid = vid;

            Debug.WriteLine(this);
            Centres.Add(this);
        }

        override public string ToString()
        {
            return $"{this.FullName} - {this.Location}";
        }
    }
}
