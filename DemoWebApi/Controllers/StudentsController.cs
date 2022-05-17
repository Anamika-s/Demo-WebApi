using DemoWebApi.IRepository;
using DemoWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepo _repo;
        private readonly ILogger<StudentsController> _logger;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(StudentsController));
        public StudentsController(IStudentRepo repo
            , ILogger <StudentsController> logger)
        {
            _repo = repo;
            _logger = logger;

        }

        [HttpGet]

        public List<Student> Get()
        {
            
            //_logger.LogTrace("This is a trcae");
            //_logger.LogWarning("This is a warning");
            //_logger.LogError("This a error");
            //_logger.LogCritical("Critical ");
            //_logger.LogInformation("Its an info");
            //_logger.LogDebug("Its a debug Message");
            //if (_repo.Get().Count < 10)
            //    _log4net.Error("Pl do something imm");
            //_log4net.Debug("Its a debufg message");
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
