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

        //GET: api/<InterviewsController>
        [HttpGet]
        public ResponseModel GetPerFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0)
        {
            return _interviewBll.GetPerFilter(all: all, id: id, idstatus: idstatus, iduser: iduser, idcompany: idcompany);
        }

        // GET api/<InterviewsController>/init
        // init proccess 
        // read all interviews when is proposal
        // send email  and cv
        // change status to 10 - Sent first time

        [HttpGet]
        [Route("init")]
        public async Task<ResponseModel> Init()
        {
            return await _interviewBll.InitProcess();
        }

        // POST api/<InterviewsController>

        // public async Task<IActionResult> PutEmployeeAsync([FromBody] Employee emp)

        [HttpPost]
<<<<<<< HEAD
        // [Route("row")]
        public async Task<ResponseModel> Post([FromBody] InterviewDto value)
        {
            return await _interviewBll.CreateAsync(value);
=======
        [Route("row")]
        public async Task<string> PostRow([FromBody] InterviewRowDto value)
        {
            try
            {
                Helpers.DbContext dbContext = new Helpers.DbContext();
                string fileRoot = "./assets/chambas.json";
                if (!System.IO.File.Exists(fileRoot))
                    System.IO.File.Create(fileRoot);
                
                await System.IO.File.AppendAllTextAsync(fileRoot, _interviewBll.AddRowFormat(value) + Environment.NewLine);

                return "OK";
            }
            catch (Exception ex)
            {
                return $"{ex.Message} / {ex.InnerException.Message} ";
            }
>>>>>>> bb87007c9d35a0e01c209e330c7cb5dbb2983320
        }

        // PUT api/<InterviewsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<InterviewsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
