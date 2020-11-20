using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BO
{
    [Serializable]
    public class University
    {
        public string Name { get; set; }
        public int Tel { get; set; }
        public byte[] Logo { get; set; }
        public string Email { get; set; }

        public University()
        {

        }

        public University(string name, int tel, byte[] logo, string email)
        {
            Name = name;
            Tel = tel;
            Logo = logo;
            Email = email;
        }

        public University(University u)
        {
            Name = u.Name;
            Tel = u.Tel;
            Logo = u.Logo;
            Email = u.Email;

        }

        public override bool Equals(object obj)
        {
            return obj is University university &&
                   Name == university.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
