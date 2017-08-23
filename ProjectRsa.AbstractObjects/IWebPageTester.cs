using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRsa.AbstractObjects
{
    public interface IWebPageTester
    {

        SubmitResponse SubmitTest(TestRequestParameter testRequest);
        TestResponse GetResult(string testId);

        List<string> GetLocations();
        List<string> GetBrowsers();

    }
}
