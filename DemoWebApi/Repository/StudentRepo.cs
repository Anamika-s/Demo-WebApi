using DemoWebApi.DBContext;
using DemoWebApi.IRepository;
using DemoWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AppDbContext _context;
        public StudentRepo(AppDbContext context)
        {
            _context = context;

        }

        public void Delete(int id)
        {

            Student temp = _context.Students.FirstOrDefault(x => x.StudentId == id);
            if (temp != null)
            {
                _context.Students.Remove(temp);
                _context.SaveChanges();
            }
        }

        public List<Student> Get()
        {
            return _context.Students.ToList();
        }

        public Student Get(int id)
        {
            return _context.Students.FirstOrDefault(x => x.StudentId == id);
        }

        public void Post(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

        }

        public void Put(int id, Student student)
        {
            Student temp = _context.Students.FirstOrDefault(x => x.StudentId == id);
            if (temp != null)
            {
                foreach (Student obj in _context.Students)
                {
                    if (obj.StudentId == id)
                    {
                        obj.Batch = student.Batch;
                        obj.Marks = student.Marks;
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}
