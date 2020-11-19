using CC01.BO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.DAL
{
    public class StudentDAO
    {
        private static List<Student> Students;
        private const string FILE_NAME = @"data/Products.json";
        //private const string FILE_NAME = @"Students.json";
        private readonly string dbFolder;
        private FileInfo file;

        public StudentDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(FILE_NAME); 
            if (!file.Directory.Exists)
            {
                file.Directory.Create(); 
            }
            if (!file.Exists)
            {
                file.Create().Close();
            }
            if (file.Length < 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName, true))
                {
                    string json = sr.ReadToEnd();
                    Students = JsonConvert.DeserializeObject<List<Student>>(json);
                }
            }
            if (Students == null)
            {
                Students = new List<Student>(); 
            }

        }

        public void Set(Student oldStudent, Student newStudent)
        {
            var oldIndex = Students.IndexOf(oldStudent);
            var newIndex = Students.IndexOf(newStudent);

            if (oldIndex < 0)
                throw new KeyNotFoundException("Student reference doesn't exists !");

            if (newIndex > 0 && newIndex != oldIndex)
                throw new DuplicateNameException("this Student already exists !");

            Students[oldIndex] = newStudent;
            Save();

        }

        public void Add(Student Student)
        {
            var index = Students.IndexOf(Student);
            if (index >= 0)
                throw new DuplicateNameException("Student reference already exist !");

            Students.Add(Student); 
            Save();
        }
        public void Remove(Student Student)
        {
            Students.Remove(Student);
            Save();

        }
        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(Students);
                sw.WriteLine(json);
            }
        }
        public IEnumerable<Student> Find()
        {
            return new List<Student>(Students);
        }
        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return new List<Student>(Students.Where(predicate).ToArray());
        }

    }
}
