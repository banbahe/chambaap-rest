using chamba.bll.Interviews;
using chamba.dal.Interviews;
using NUnit.Framework;

namespace chambapp.nunit.test
{
    public class Tests
    {
        public class InterviewsUnitTest
        {
            private IInterviewBll _interviewsBll;
            private InterviewDal _interviewDal;

            [SetUp]
            public void Setup()
            {
                _interviewDal = new InterviewDal();
                _interviewsBll = new InterviewBll(_interviewDal);
            }

            [Test]
            public void TestGetAllInterviews()
            {
                var list = _interviewsBll.GetAll();
                Assert.IsTrue(list.Count > 0, "Sucess");
                //Assert.Pass();
            }
        }
    }
}