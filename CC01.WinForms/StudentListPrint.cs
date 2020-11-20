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
        public string EmailS { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }
        public int TelS { get; set; }
        public byte[] Logo { get; set; }

        public static int count = 0;

        public StudentListPrint(string name, string firstName, string lastName, DateTime bornOn, string bornAt, 
            byte[] photo, string sexe, string emailS, string email, int tel, int telS, byte[] logo)
        {
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            Sexe = sexe;
            EmailS = emailS;
            Email = email;
            Tel = tel;
            TelS = telS;
            Logo = logo;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }
    }
}
