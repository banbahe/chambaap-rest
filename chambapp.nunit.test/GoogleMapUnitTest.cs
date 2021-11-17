using chambapp.bll.Helpers;
using chambapp.dto;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Linq;

namespace chambapp.nunit.test
{
    internal class GoogleMapUnitTest
    {
        //private IInterviewBll _interviewsBll;
        //private InterviewDal _interviewDal;
        //private MainMapper _mainMapper;
        //private ResponseModel _response;

        [SetUp]
        public void Setup()
        {
            //_response = new ResponseModel();
            //_mainMapper = new MainMapper();
            //_interviewDal = new InterviewDal();
            //_interviewsBll = new InterviewBll(_interviewDal, _response, _mainMapper);
        }

        [Test]
        public async Task TestTextsearch()
        {
            string search = "morganna games";
            var resultTextSearch0 = await HttpHelper<RootGoogleMapsDto>.GetGmpAsync($"place/textsearch/json?query={search}");
            var resultLat = resultTextSearch0.Results.FirstOrDefault().geometry.location.lat;

            Assert.IsTrue(!string.IsNullOrEmpty(resultLat.ToString()),"OK lat found");
            //var resultTextSearch1 = await HttpHelper<dynamic>.GetGmpAsync($"place/textsearch/json?query={search}");

            // resultTextSearch.Results.FirstOrDefault().name;
            // Assert.IsTrue(list.Count > 0, "Sucess");
            //Assert.Pass();
        }
    }

}
