using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC01.BO;


namespace CC01.DAL
{
    public class UniversityDAO
    {
        private static List<University> Universitys;
        private const string FILE_NAME = @"Universitys.json";
        private readonly string dbFolder;
        private FileInfo file;

        public UniversityDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder, FILE_NAME)); 
            if (!file.Directory.Exists)
            {
                file.Directory.Create(); 
            }
            if (!file.Exists)
            {
                file.Create().Close(); 
            }
            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName, false))
                {
                    string json = sr.ReadToEnd();
                    Universitys = JsonConvert.DeserializeObject<List<University>>(json);
                }
            }
            if (Universitys == null)
            {
                Universitys = new List<University>(); 
            }

        }

        public void Set(University oldUniversity, University newUniversity)
        {
            var oldIndex = Universitys.IndexOf(oldUniversity);
            var newIndex = Universitys.IndexOf(newUniversity);

            if (oldIndex < 0)
                throw new KeyNotFoundException("University reference doesn't exists !");

            if (newIndex > 0 && newIndex != oldIndex)
                throw new DuplicateNameException("thos University already exists !");

            Universitys[oldIndex] = newUniversity;
            Save();

        }

        public void Add(University University)
        {
            var index = Universitys.IndexOf(University);
            if (index >= 0)
                throw new DuplicateNameException("University reference already exist !");

            Universitys.Add(University);
            Save();
        }
        public void Remove(University University)
        {
            Universitys.Remove(University); 
            Save();

        }
        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(Universitys);
                sw.WriteLine(json);
            }
        }
        public IEnumerable<University> Find()
        {
            return new List<University>(Universitys);
        }
        public IEnumerable<University> Find(Func<University, bool> predicate)
        {
            return new List<University>(Universitys.Where(predicate).ToArray());
        }

    }
}
