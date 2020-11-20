﻿using Newtonsoft.Json;
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
        private static List<University> universitys;
        private const string FILE_NAME = @"Universitys.json";
        private readonly string dbFolder;
        private FileInfo file;

        public UniversityDAO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            file = new FileInfo(Path.Combine(this.dbFolder,FILE_NAME)); 
            if (!file.Directory.Exists)
            {
                file.Directory.Create(); 
            }

            if (!file.Exists)
            {
                file.Create().Close();
                file.Refresh();
            }

            if (file.Length > 0)
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string json = sr.ReadToEnd();
                    universitys = JsonConvert.DeserializeObject<List<University>>(json);
                }
            }
            if (universitys == null)
            {
                universitys = new List<University>(); 
            }

        }

        public void Set(University oldUniversity, University newUniversity)
        {
            var oldIndex = universitys.IndexOf(oldUniversity);
            var newIndex = universitys.IndexOf(newUniversity);

            if (oldIndex < 0)
                throw new KeyNotFoundException("University reference doesn't exists !");

            if (newIndex > 0 && newIndex != oldIndex)
                throw new DuplicateNameException("this University already exists !");

            universitys[oldIndex] = newUniversity;
            Save();

        }

        public void Add(University university)
        {
            var index = universitys.IndexOf(university);
            if (index >= 0)
                throw new DuplicateNameException("University reference already exist !");

            universitys.Add(university);
            Save();
        }
        public void Remove(University university)
        {
            universitys.Remove(university); 
            Save();

        }
        public void Save()
        {
            using (StreamWriter sw = new StreamWriter(file.FullName, false))
            {
                string json = JsonConvert.SerializeObject(universitys);
                sw.WriteLine(json);
            }
        }
        public IEnumerable<University> Find()
        {
            return new List<University>(universitys);
        }
        public IEnumerable<University> Find(Func<University, bool> predicate)
        {
            return new List<University>(universitys.Where(predicate).ToArray());
        }

    }
}
