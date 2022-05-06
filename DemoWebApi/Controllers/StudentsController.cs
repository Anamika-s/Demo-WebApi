using DemoWebApi.IRepository;
using DemoWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _repo;

        public StudentsController(IStudentRepo repo)
        {
            _repo = repo;

        }


        public List<Student> Get()
        {
            return (_repo.Get());
        }

        [HttpGet]
        [Route("{id:int}")]
        public Student Get(int id)
        {
            return _repo.Get(id);
        }

        [HttpPost]
        public void Post(Student student)
        {
            _repo.Post(student);
        }


        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, Student student)
        {
            _repo.Put(id, student);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }

    }
}
