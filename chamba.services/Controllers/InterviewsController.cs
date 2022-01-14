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
using Microsoft.AspNetCore.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chambapp.services.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ApiController]
    public class InterviewsController : ControllerBase
    {

        private readonly IInterviewBll _interviewBll;

        public InterviewsController(IInterviewBll interviewBll) => _interviewBll = interviewBll;


        [HttpDelete]
        public async Task<ResponseModel> Delete([FromQuery] int interviewId)
        {
            return await _interviewBll.DeleteAsync(interviewId);
        }
        /// <summary>
        /// Read inbox and match reply mail
        /// </summary>
        /// <param name="user">id user</param>
        /// <returns>Response Model</returns>
        [HttpGet]

        [Route("readinbox")]
        public async Task<ResponseModel> ReadInbox([FromQuery] int user = -1)
        {
            return await _interviewBll.ReadMail(user);
        }
        [HttpGet]
        public ResponseModel GetPerFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0)
        {
            return _interviewBll.GetPerFilter(all: all, id: id, idstatus: idstatus, iduser: iduser, idcompany: idcompany);
        }
        [HttpGet]
        [Route("init")]
        public async Task<ResponseModel> Init([FromQuery]int idCandidate = -1)
        {
            return await _interviewBll.InitProcess(idCandidate);
        }

        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] InterviewDto value)
        {
            return await _interviewBll.CreateAsync(value);

        }
        [HttpPost]
        [Route("proposal")]
        public async Task<ResponseModel> PostProposal([FromBody] InterviewProposalDto value)
        {
            return await _interviewBll.CreateProposalAsync(value);
        }
    }
}
