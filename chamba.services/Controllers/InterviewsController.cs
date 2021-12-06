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

        [HttpGet]
        public ResponseModel GetPerFilter(int all = 0, int id = 0, int idstatus = 0, int iduser = 0, int idcompany = 0)
        {
            return _interviewBll.GetPerFilter(all: all, id: id, idstatus: idstatus, iduser: iduser, idcompany: idcompany);
        }
        [HttpGet]
        [Route("init")]
        public async Task<ResponseModel> Init()
        {
            return await _interviewBll.InitProcess();
        }

        [HttpPost]
        public async Task<ResponseModel> Post([FromBody] InterviewDto value)
        {
            return await _interviewBll.CreateAsync(value);

        }
    }
}
