using CC01.BO;
using CC01.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC01.BLL
{
    public class StudentBLO
    {
        StudentDAO StudentRepo;
        public StudentBLO(string dbFolder)
        {
            StudentRepo = new StudentDAO(dbFolder);
        }
        public void CreateStudent(Student Student)
        {
            StudentRepo.Add(Student);
        }
        public void DeleteStudent(Student Student)
        {
            StudentRepo.Remove(Student);
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return StudentRepo.Find();
        }
        public IEnumerable<Student> GetByReference(string reference)
        {
            return StudentRepo.Find(x => x.Matricule == reference);
        }

        public IEnumerable<Student> GetBy(Func<Student, bool> predicate)
        {
            return StudentRepo.Find(predicate);
        }

        public void EditStudent(Student oldStudent, Student newStudent)
        {
            StudentRepo.Set(oldStudent, newStudent);
        }

    }
}
