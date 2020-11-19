using CC01.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.WinForms
{
    class StudentListPrint
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Matricule  { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Sexe { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }

        public static int count = 0;

        public StudentListPrint(string firstName, string lastName, 
            DateTime bornOn, string bornAt, byte[] photo, string sexe, string email, int tel)
        {
            FirstName = firstName;
            LastName = lastName;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            Sexe = sexe;
            Email = email;
            Tel = tel;
            University u = new University();
            Matricule = $"{u.Name.Substring(0, 2)}" +
                        $"{DateTime.Now.Year.ToString().Substring(2, 3)}" +
                        $"{count++.ToString().PadLeft(4, '0')}" +
                        $"{BornOn.Year.ToString().Substring(2, 3)}";

        }
    }
}
