using CC01.BO;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
        public string Tel { get; set; }
        public long TelS { get; set; }
        public byte[] Logo { get; set; }
        public byte[] QrCode { get; set; }

        public static int count = 0;
        public StudentListPrint()
        {

        }

        public StudentListPrint(string name, string firstName, string lastName, DateTime bornOn, 
            string bornAt, byte[] photo, string sexe, string emailS, string email, string tel, 
            long telS, byte[] logo, byte[] qrCode)
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
            QrCode = qrCode;
        }

        //QRCodeGenerator qr = new QRCodeGenerator();
        //QRCodeData data = qr.CreateQrCode(Matricule, QRCodeGenerator.ECCLevel.Q);
        //QRCode code = new QRCode(data);
        //QrCode = code.GetGraphic(3);

        public StudentListPrint(StudentListPrint s)
        {
            Name = s.Name;
            FirstName = s.FirstName;
            LastName = s.LastName;
            BornOn = s.BornOn;
            BornAt = s.BornAt;
            Photo = s.Photo;
            Sexe = s.Sexe;
            EmailS = s.EmailS;
            Email = s.Email;
            Tel = s.Tel;
            TelS = s.TelS;
            Logo = s.Logo;

            Matricule = $"{ FirstName.Substring(0, 2)}{BornOn.Year.ToString().Substring(2)}" +
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";

        }

    }
}
