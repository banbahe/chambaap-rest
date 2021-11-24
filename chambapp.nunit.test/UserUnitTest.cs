using chambapp.bll.Helpers;
using chambapp.dto;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;
using chambapp.bll.Users;
using chambapp.dal.Users;
using chambapp.bll.AutoMapper;
using System;
using System.Text;
using System.Text.Json;
using chambapp.storage.Models;
using chambapp.bll.Helpers;

namespace chambapp.nunit.test
{
    public class UserUnitTest
    {
        private IUserBll _userBll;
        private IUserDal _userDal;
        private MainMapper _mainMapper;
        //private ResponseModel<Use> _response;


        //[SetUp]
        //public void Setup()
        //{
        //    _response = new ResponseModel();
        //    _userDal = new UserDll();
        //    _mainMapper = new MainMapper();

        //    // main 
        //    _userBll = new UserBll(_userDal, _response, _mainMapper);
        //}
        //[Test]
        //public async Task TestCreateRecruiterAsync()
        //{

        //    RecruiterDto recruiterDto = new RecruiterDto();
        //    recruiterDto.IdRecruiter = 0;
        //    recruiterDto.Name = "Pathfinder";
        //    recruiterDto.Email = "eban.blanquel@gmail.com";
        //    recruiterDto.Phone = "+525534348486";
        //    recruiterDto.ReplyEmail = "X";

        //    var result = await _userBll.CreateRecruiterAsync(recruiterDto);
        //    if (result.Flag == (int)EnumStatusCatalog.Ok)
        //    {
        //        var resultcandidate = JsonSerializer.Deserialize<User>(result.Datums);
        //        Assert.IsTrue(resultcandidate.Id > 0 && result.Flag == (int)EnumStatusCatalog.Ok, "Operation Success");
        //    }
        //    else
        //    {
        //        Console.Write(result.Message);
        //        Assert.IsFalse(result.Flag <= 0, "Operation Failure");
        //    }
        //}
//        [Test]
//        public async Task TestCreateCandidateAsync()
//        {

//            var MailProvider = new
//            {
//                email = "eban.blanquel@gmail.com",
//                pwd = "@VueJS2018",
//                smt = "smtp.gmail.com",
//                port = 587
//            };
//            var root = new
//            {
//                MailProvider
//            };

//            var jsonstring = JsonSerializer.Serialize(root);
//            //var jsonobject = JsonSerializer.Deserialize<dynamic>(jsonobject);


//            CandidateDto candidateDto = new CandidateDto
//            {
//                IdCandidate = 0,
//                Name = "Esteban Blanquel",
//                Email = "eban.blanquel@gmail.com",
//                Phone = "+525542846626",
//                Pwd = "@VueJS2018",
//                EmailSubject = "CV Esteban Blanquel, Dev Full Stack",
//                EmailConfiguration = jsonstring,
//                EmailKeyword = @"
//			                    {

//			                        ""binding_imghead"": ""https://dl.dropbox.com/s/ufh269rf590xlaq/email_head.png"",
//			                    	""binding_title"": ""Developer Full Stack"",
//			                    	""binding_recruitername"": ""Mr Recruiter"",	
//			                    	""binding_candidatename"": ""Esteban Blanquel"",
//			                    	""binding_companyname"": ""Company X"",	
//			                    	""binding_imgbody"": ""https://dl.dropbox.com/s/182r6mm0syymuly/email_body.png"",	
//			                    	""binding_imgbodycaption"": ""Necesitas consultar más acerca de mis <strong>skills</strong> accede aqui <a href=&quot;http://github.com/stbndev&quot;>GitHub! &raquo;</a>"",	
//			                    	""binding_cvuri"": [
//			                    				""Curriculum Vitae|https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf"",
//			                    	            ""CV|https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf""
//			                    				 ],
//			                    	""binding_social_media"": [
//			                    				 ""Linkedin|https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf"",
//			                    	             ""GitHub Dev|https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf"",
//			                    				 ""GitHub|https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf""
//			                    				 ],
//			                    	""binding_phone"": ""5542846626"",
//			                    	""binding_email"":""eban.blanquel@gmail.com"",
//			                    	""binding_phrase"": ""Muchas veces la gente no sabe lo que quiere hasta que se lo enseñas""
//			                    }",
//                EmailTemplate =
//@"<!DOCTYPE><html xmlns=""http://www.w3.org/1999/xhtml""><head><meta name=""viewport"" content=""width=device-width"" /><meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" /><title>Dev</title><style>* { margin:0;padding:0;}* { font-family: ""Helvetica Neue"", ""Helvetica"", Helvetica, Arial, sans-serif; }img { max-width: 100%; }.collapse {margin:0;padding:0;}body {-webkit-font-smoothing:antialiased; -webkit-text-size-adjust:none; width: 100%!important; height: 100%;}a { color: #2BA6CB;}.btn {text-decoration:none;color: #FFF;background-color: #e700ae;padding:10px 16px;font-weight:bold;margin-right:10px;text-align:center;cursor:pointer;display: inline-block;}p.callout {padding:15px;background-color:#9AE66E; margin-bottom: 15px;}.callout a {font-weight:bold;color: #142F43;}table.social {background-color: #f1f1f1;}.social .soc-btn {padding: 3px 7px;font-size:12px;margin-bottom:10px;text-decoration:none;color: #FFF;font-weight:bold;display:block;text-align:center;}a.ln { background-color: #0a66c2!important; }a.gh { background-color: #9841a5!important; }a.gh2 { background-color: #5a2681!important; }.sidebar .soc-btn { display:block;width:100%;}table.head-wrap { width: 100%;}.header.container table td.logo { padding: 15px; }.header.container table td.label { padding: 15px; padding-left:0px;}table.body-wrap { width: 100%;}table.footer-wrap { width: 100%;clear:both!important;}.footer-wrap .container td.content p { border-top: 1px solid rgb(215,215,215); padding-top:15px;}.footer-wrap .container td.content p {font-size:10px;font-weight: bold;}h1,h2,h3,h4,h5,h6 {font-family: ""HelveticaNeue-Light"", ""Helvetica Neue Light"", ""Helvetica Neue"", Helvetica, Arial, ""Lucida Grande"", sans-serif; line-height: 1.1; margin-bottom:15px; color:#000;}h1 small, h2 small, h3 small, h4 small, h5 small, h6 small { font-size: 60%; color: #6f6f6f; line-height: 0; text-transform: none; }h1 { font-weight:200; font-size: 44px;}h2 { font-weight:200; font-size: 37px;}h3 { font-weight:500; font-size: 27px;}h4 { font-weight:500; font-size: 23px;}h5 { font-weight:900; font-size: 17px;}h6 { font-weight:900; font-size: 14px; text-transform: uppercase; color:#444;}.collapse { margin:0!important;}p, ul { margin-bottom: 10px; font-weight: normal; font-size:14px; line-height:1.6;}p.lead { font-size:17px; }p.last { margin-bottom:0px;}ul li {margin-left:5px;list-style-position: inside;}ul.sidebar {background:#ebebeb;display:block;list-style-type: none;}ul.sidebar li { display: block; margin:0;}ul.sidebar li a {text-decoration:none;color: #666;padding:10px 16px;margin-right:10px;cursor:pointer;border-bottom: 1px solid #777777;border-top: 1px solid #FFFFFF;display:block;margin:0;}ul.sidebar li a.last { border-bottom-width:0px;}ul.sidebar li a h1,ul.sidebar li a h2,ul.sidebar li a h3,ul.sidebar li a h4,ul.sidebar li a h5,ul.sidebar li a h6,ul.sidebar li a p { margin-bottom:0!important;}.container {display:block!important;max-width:600px!important;margin:0 auto!important; clear:both!important;}.content {padding:15px;max-width:600px;margin:0 auto;display:block; }.content table { width: 100%; }.column {width: 300px;float:left;}.column tr td { padding: 15px; }.column-wrap { padding:0!important; margin:0 auto; max-width:600px!important;}.column table { width:100%;}.social .column {width: 280px;min-width: 279px;float:left;}.clear { display: block; clear: both; }@media only screen and (max-width: 600px) {a[class=""btn""] { display:block!important; margin-bottom:10px!important; background-image:none!important; margin-right:0!important;}div[class=""column""] { width: auto!important; float:none!important;}table.social div[class=""column""] {width:auto!important;}}</style></head> <body bgcolor=""#FFFFFF""><table class=""head-wrap"" bgcolor=""#5327E0""><tr><td></td><td class=""header container""><div class=""content""><table bgcolor=""#5327E0""><tr><td><img src=""binding_imghead"" /></td><td align=""right""><h4 style=""color:#FFE1EA;"" class=""collapse"">binding_title</h4></td></tr></table></div></td><td></td></tr></table><table class=""body-wrap""><tr><td></td><td class=""container"" bgcolor=""#FFFFFF""><div class=""content""><table><tr><td><h3>Estimado/a binding_recruitername </h3><p class=""lead"">Mi nombre es binding_candidatename y le escribo porque he visto que en binding_companyname están realizando un proceso de selección para una vacante de Programador.Tengo 8 años de experiencia en el área de Tecnologías de la información y la comunicación. Es un placer para mí hacerle llegar mi Currículum; me gustaría decirle que estoy realmente interesado en este puesto y considero que puedo desempeñarlo a la perfección.</p><p><img src=""binding_imgbody"" /></p> <p class=""callout"">binding_imgbodycaption</p><h4> binding_companyname <small></small></h4><p class=""lead"">Por otro lado, e indagado sobre binding_companyname y me es una magnífica opción para continuar mi carrera profesional, por lo que me gustaría aprovechar la oportunidad de participar en el proceso de selección de este puesto.</p><p class=""lead"">Quedo a su entera disposición si considera oportuno mantener una entrevista. Será para mí un placer ampliarle información sobre cualquier duda que tenga.</p><p class=""lead"">Reciba un cordial saludo</p><p class=""lead"">Atentamente: binding_candidatename</p><a href=""https://dl.dropbox.com/s/600f2kpi1f4grok/cv.esteban.blanquel.pdf"" target=""_blank"" class=""btn"">Currículum Vitae!</a>binding_cvurl<br/><br/><table class=""social"" width=""100%""><tr><td><table align=""left"" class=""column""><tr><td><h5 class="""">Redes Sociales:</h5><p class=""""><a href=""https://mx.linkedin.com/"" class=""soc-btn ln"">Linkedin</a> binding_socialmedia</p></td></tr></table><table align=""left"" class=""column""><tr><td><h5 class="""">Contacto Personal:</h5><p>Movil: <strong>binding_phone</strong><br/> Email: <strong><a href=""emailto:binding_candidateemail"">binding_candidateemail</a></strong></p> </td></tr></table><span class=""clear""></span></td></tr></table></td></tr></table></div></td><td></td></tr></table><table class=""footer-wrap""><tr><td></td><td class=""container""><div class=""content""><table><tr><td align=""center""><p>binding_phrase</p></td></tr></table></div></td><td></td></tr></table></body></html>"""
//            };


//            var result = await _userBll.CreateCandidateAsync(candidateDto);
//            if (result.Flag == (int)EnumStatusCatalog.Ok)
//            {
//                var resultcandidate = JsonSerializer.Deserialize<User>(result.Datums);
//                Assert.IsTrue(resultcandidate.Id > 0 && result.Flag == (int)EnumStatusCatalog.Ok, "Operation Success");
//            }
//            else
//            {
//                Console.Write(result.Message);
//                Assert.IsFalse(result.Flag <= 0, "Operation Failure");
//            }




//        }

    }

    
}
