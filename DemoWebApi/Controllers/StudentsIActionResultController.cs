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
    public class StudentsIActionResultController : ControllerBase
    {
        private readonly IStudentRepo _repo;

        public StudentsIActionResultController(IStudentRepo repo)
        {
            _repo = repo;

        }


        public IActionResult Get()
        {
            if (_repo.Get().Count == 0)
                return NotFound();
            else 
            return Ok(_repo.Get());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
           var  student =  _repo.Get(id);
            if (student == null)
                return NotFound();
            else
                return Ok(student);
        }

        [HttpPost]
        public IActionResult Post(Student student)
        {
            _repo.Post(student);

            return Created("Record added", student);

        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, Student student)
        {
            _repo.Put(id, student);
            return Ok("edited");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok("deleted");
        }

    }
}
