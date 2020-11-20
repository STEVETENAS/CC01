using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Student //: University
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailS { get; set; }
        public int TelS { get; set; }
        public string Sexe { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Matricule { get; set; }

        public static int count = 0;

        public Student() : base()
        {

        }

        public Student(string firstName, string lastName, string emailS, int telS, string sexe, DateTime bornOn, string bornAt, byte[] photo) // : base()
        {
            FirstName = firstName;
            LastName = lastName;
            EmailS = emailS;
            TelS = telS;
            Sexe = sexe;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0,1)}";
        }

        public Student(Student s)
        {
            FirstName = s.FirstName;
            LastName = s.LastName;
            EmailS = s.EmailS;
            TelS = s.TelS;
            Sexe = s.Sexe;
            BornOn = s.BornOn;
            BornAt = s.BornAt;
            Photo = s.Photo;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }

    }
}
