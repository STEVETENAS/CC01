using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class UniversityBLO
    {
        UniversityDAO UniversityRepo;
        public UniversityBLO(string dbFolder)
        {
            UniversityRepo = new UniversityDAO(dbFolder);
        }
        public void CreateUniversity(University University)
        {
            UniversityRepo.Add(University);
        }
        public void DeleteUniversity(University University)
        {
            UniversityRepo.Remove(University);
        }
        public IEnumerable<University> GetAllUniversitys()
        {
            return UniversityRepo.Find();
        }
        public IEnumerable<University> GetByName(string Name)
        {
            return UniversityRepo.Find(x => x.Name == Name);
        }

        public IEnumerable<University> GetBy(Func<University, bool> predicate)
        {
            return UniversityRepo.Find(predicate);
        }

        public void EditUniversity(University oldUniversity, University newUniversity)
        {
            UniversityRepo.Set(oldUniversity, newUniversity);
        }
    }
}
