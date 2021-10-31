using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chamba.bll.Interviews;
using chamba.dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chamba.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly  IInterviewBll _interviewBll;

        public InterviewsController(IInterviewBll interviewBll) => _interviewBll = interviewBll;


        //public async Task<IEnumerable<Employee>> GetEmployeesAsync(
        //    string firstName = null, string lastName = null)

        // GET: api/<InterviewsController>
        [HttpGet]
        public async Task<IEnumerable<InterviewDto>> Get()
        {
            return _interviewBll.GetAll();
        }

        // GET api/<InterviewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InterviewsController>
        [HttpPost]
        public void Post([FromBody] string value)
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
