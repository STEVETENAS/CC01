using CC01.BO;
using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace CC01.WinForms
{
    class StudentListPrint
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Matricule { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Sexe { get; set; }
        public string EmailS { get; set; }
        public string Email { get; set; }
        public int Tel { get; set; }
        public int TelS { get; set; }
        public byte[] Logo { get; set; }
        public byte[] QrCode { get; set; }

        public static int count = 0;
        public StudentListPrint()
        {

        }
        public StudentListPrint(string firstName, string lastName, DateTime bornOn, string bornAt,
            byte[] photo, string sexe, string emailS, int telS, 
            University u)
        {
            FirstName = firstName;
            LastName = lastName;
            BornOn = bornOn;
            BornAt = bornAt;
            Photo = photo;
            Sexe = sexe;
            EmailS = emailS;
            TelS = telS;
            Name = u.Name;
            Tel = u.Tel;
            Logo = u.Logo;
            Email = u.Email;
            Matricule = $"{FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }
        //QRCodeGenerator qr = new QRCodeGenerator();
        //QRCodeData data = qr.CreateQrCode(Matricule, QRCodeGenerator.ECCLevel.Q);
        //QRCode code = new QRCode(data);

    }
}
