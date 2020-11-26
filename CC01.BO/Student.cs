using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CC01.BO
{
    [Serializable]
    public class Student 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailS { get; set; }
        public long TelS { get; set; }
        public string Sexe { get; set; }
        public DateTime BornOn { get; set; }
        public string BornAt { get; set; }
        public byte[] Photo { get; set; }
        public string Matricule { get; set; }
        public byte[] QrCode { get; set; }

        public static int count = 0;

        public Student()
        {

        }

        public Student(string firstName, string lastName, string emailS, long telS, string sexe, DateTime bornOn, string bornAt, byte[] photo,byte[] qrCode)
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
                        $"{count++.ToString().PadLeft(4, '0')}{Sexe.Substring(0, 1)}";
            QrCode = qrCode;

            //QRCodeGenerator qr = new QRCodeGenerator();
            //QRCodeData data = qr.CreateQrCode(Matricule, QRCodeGenerator.ECCLevel.Q);
            //QRCode code = new QRCode(data);


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

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   Matricule == student.Matricule;
        }

        public override int GetHashCode()
        {
            int hashCode = -1381900569;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Matricule);
            return hashCode;
        }
    }
}
