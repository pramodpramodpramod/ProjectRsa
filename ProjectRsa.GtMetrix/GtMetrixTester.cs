using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectRsa.AbstractObjects;
using GTmetrix;
using GTmetrix.Http;
using GTmetrix.Model;

namespace ProjectRsa.GtMetrix
{
    public class GtMetrixTester : IWebPageTester
    {
        private Connection _connection;
        public GtMetrixTester(string username, string password)
        {
            _connection = Connection.Create(username, password);
        }
        public List<string> GetBrowsers()
        {
            return Enum.GetValues(typeof(Browsers))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToList();
        }

        public List<string> GetLocations()
        {
            return Enum.GetValues(typeof(Locations))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToList();
        }

        public TestResponse GetResult(string testId)
        {
            var testResult = GetTestResults(testId);
            return testResult.Result;
        }

        public SubmitResponse SubmitTest(TestRequestParameter testRequest)
        {
            return SubmitAndGetResponse(testRequest).Result;
        }

        private async Task<TestResponse> GetTestResults(string testId)
        {
            var client = new GTmetrix.Client(_connection);
            var response = await client.GetTest(testId);
            var testResponse = new TestResponse()
            {
                Success = response.Success,
                HttpStatusCode = response.StatusCode,
                TestId = testId,
                TestCompelted = false
            };
            if (response.Success && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                testResponse.TaskStatus = response.Body.State.ToString("D");
                if(response.Body.State == ResultStates.Completed)
                {
                    testResponse.TestCompelted = true;
                    testResponse.PageBytes = response.Body.Results.PageBytes;
                    testResponse.PageElements = response.Body.Results.PageBytes;
                    testResponse.PageLoadTime = response.Body.Results.PageBytes;
                    testResponse.TestScore = response.Body.Results.YSlowScore;
                    testResponse.ScreenShotUrl = response.Body.Resources.ScreenshotUrl;
                    testResponse.ReportUrl = response.Body.Resources.DetailedPdfReportUrl;
                }
                else if(response.Body.State == ResultStates.Error)
                {
                    testResponse.Success = false;
                    testResponse.Error = response.Body.Error;
                }
            }
            else
            {
                testResponse.Error = response.Error;
            }
            return testResponse;
        }


        private async Task<SubmitResponse> SubmitAndGetResponse(TestRequestParameter testRequest)
        {
            var client = new Client(_connection);
            var location = (Locations)Enum.Parse(typeof(Locations), testRequest.Location ?? "London");
            var browser = (Browsers)Enum.Parse(typeof(Browsers), testRequest.Browser ?? "Chrome");
            var request = new TestRequest(new Uri(testRequest.Url), location, browser);
            var response = await client.SubmitTest(request);
            var submitResponse = new SubmitResponse();
            submitResponse.Success = response.Success;
            submitResponse.HttpStatusCode = response.StatusCode;
            if (response.Success)
            {
                submitResponse.TestId = response.Body.TestId;
                submitResponse.PollStateUrl = response.Body.PollStateUrl;
            }
            else
            {
                submitResponse.Error = response.Error;
            }
            return submitResponse;
        }

    }
}
