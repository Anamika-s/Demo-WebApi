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
    public class StudentsActionResultController : ControllerBase
    {
        private readonly IStudentRepo _repo;

        public StudentsActionResultController(IStudentRepo repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            if (_repo.Get().Count == 0)
                return NotFound();
            else
                //return Ok(_repo.Get());

                return _repo.Get();
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Student> Get(int id)
        {
           var  student =  _repo.Get(id);
            if (student == null)
                return NotFound();
            else
                return student;
        }

        [HttpPost]
        public ActionResult<Student> Post(Student student)
        {
            _repo.Post(student);

            return Created("Record added", student);

        }
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult<string> Put(int id, Student student)
        {
            _repo.Put(id, student);
            //return Ok("edited");
            return "edited";
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult<int> Delete(int id)
        {
            _repo.Delete(id);
            //return Ok("deleted");
            return 1;
        }

    }
}
