using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class UniversityBLO
    {
        UniversityDAO UniversityRepo;
        private string dbFolder;
        public UniversityBLO(string dbFolder)
        {
            this.dbFolder = dbFolder;
            UniversityRepo = new UniversityDAO(dbFolder);
        }
        public void CreateUniversity(University oldUniversity, University newUniversity)
        {
            string filename = null;
            if (!string.IsNullOrEmpty(newUniversity.Logo))
            {
                string ext = Path.GetExtension(newUniversity.Logo);
                filename = Guid.NewGuid().ToString() + ext;
                FileInfo fileSource = new FileInfo(newUniversity.Logo);
                string filePath = Path.Combine(dbFolder, "logo", filename);
                FileInfo fileDest = new FileInfo(filePath);
                if (!fileDest.Directory.Exists)
                    fileDest.Directory.Create();
                fileSource.CopyTo(fileDest.FullName);
            }
            newUniversity.Logo = filename;
            UniversityRepo.Add(newUniversity);

            if (!string.IsNullOrEmpty(oldUniversity.Logo))
                File.Delete(oldUniversity.Logo);
        }
        //public void DeleteUniversity(University University)
        //{
        //    UniversityRepo.Remove(University);
        //}
        //public IEnumerable<University> GetAllUniversitys()
        //{
        //    return UniversityRepo.Find();
        //}
        //public IEnumerable<University> GetByName(string Name)
        //{
        //    return UniversityRepo.Find(x => x.Name == Name);
        //}

        //public IEnumerable<University> GetBy(Func<University, bool> predicate)
        //{
        //    return UniversityRepo.Find(predicate);
        //}

        //public void EditUniversity(University oldUniversity, University newUniversity)
        //{
        //    UniversityRepo.Set(oldUniversity, newUniversity);
        //}

        public University GetUniversity()
        {
            University university = UniversityRepo.Get();
            if (university != null)
                if (!string.IsNullOrEmpty(university.Logo))
                    university.Logo = Path.Combine(dbFolder, "logo", university.Logo);
            return university;
        }

    }
}
