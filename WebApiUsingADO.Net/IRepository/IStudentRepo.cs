 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsingADO.Models;

namespace DemoWebApi.IRepository
{
    public interface IStudentRepo
    {
        List<Student> Get();
        Student Get(int id);
        void Post(Student student);
        void Put(int id, Student student);
        void Delete(int id);
    }
}
