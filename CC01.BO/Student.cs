using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }
        public string Sexe { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Matricule { get; set; }

        public static int count = 0;

        public Student()
        {

        }

        public Student(string firstName, string lastName, string email, int tel, string sexe, DateTime bornOn, string bornAt, byte[] photo)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Tel = tel;
            Sexe = sexe;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            University u = new University();
            Matricule = $"{FirstName.Substring(0, 2)}" +
                        $"{DateTime.Now.Year.ToString().Substring(2, 3)}" +
                        $"{count++.ToString().PadLeft(4, '0')}" +
                        $"{BornOn.Year.ToString().Substring(2, 3)}";
        }

        public Student(Student s)
        {
            FirstName = s.FirstName;
            LastName = s.LastName;
            Email = s.Email;
            Tel = s.Tel;
            Sexe = s.Sexe;
            BornOn = s.BornOn;
            BornAt = s.BornAt;
            Photo = s.Photo;
            University u = new University();
            Matricule = $"{FirstName.Substring(0, 2)}" +
                        $"{DateTime.Now.Year.ToString().Substring(2, 3)}" +
                        $"{count++.ToString().PadLeft(4, '0')}" +
                        $"{BornOn.Year.ToString().Substring(2, 3)}";

        }
    }
}
