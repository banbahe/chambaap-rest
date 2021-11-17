using System;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

using System.Threading.Tasks;
using chambapp.bll.Interviews;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Unicode;
using chambapp.bll.Helpers;
using chambapp.dto;
using System.Diagnostics;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chambapp.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        
        private readonly IInterviewBll _interviewBll;

        public InterviewsController(IInterviewBll interviewBll) => _interviewBll = interviewBll;


        //public async Task<IEnumerable<Employee>> GetEmployeesAsync(
        //    string firstName = null, string lastName = null)

        // GET: api/<InterviewsController>
        [HttpGet]
        public async Task<ResponseModel> Get()
        {
            return _interviewBll.GetAll();
        }

        // GET api/<InterviewsController>/5
        [HttpGet]
        [Route("init")]
        public async Task<ResponseModel> Init()
        {
            // Read File
            string fileRoot = "./assets/chambas.txt";
            //var y = System.IO.File.ReadAllText(fileRoot) + "ReadAllText";
            var linesInFile = System.IO.File.ReadLines(fileRoot);
            return await _interviewBll.InitProcess(linesInFile);
        }

        // POST api/<InterviewsController>

        // public async Task<IActionResult> PutEmployeeAsync([FromBody] Employee emp)

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
        }

        // PUT api/<InterviewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InterviewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
