using DemoWebApi.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsingADO.Models;

namespace WebApiUsingADO.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepo _repo;
        public StudentController(IStudentRepo repo)
        {
            _repo = repo;

        }
        [HttpGet]
        public IActionResult Get()
        {
            var list = _repo.Get();
            if (list.Count == 0)
                return NotFound();
            else 
           return(Ok(list));

        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var student = _repo.Get(id);
            if (student==null)
                return NotFound();
            else
                return (Ok(student));

        }


        [HttpPost]
        public IActionResult Post(Student student)
        {
            _repo.Post(student);
            return Created("Record inserted", student);
        }


        [HttpPut]
        [Route("{id:int}")]

        public IActionResult Put(int id, Student student)

        {
            _repo.Put(id, student);
            return Ok("Edited");
        }


        [HttpDelete]
        [Route("{id:int}")]
        [ActionName("DeleteStudent")]
        public IActionResult Delete(int id)

        {
            _repo.Delete(id);
            return Ok("Deleted");
        }

    }

}

