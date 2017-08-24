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
	/// <summary>
	/// implementation of IWebPageTester using GtMetrix.com API
	/// </summary>
    public class GtMetrixTester : IWebPageTester
    {
		#region private variables

		private Connection _connection;

		#endregion


		#region constructor
		public GtMetrixTester(string username, string password)
        {
            _connection = Connection.Create(username, password);
        }

		#endregion


		#region implementation of IWebPageTester methods
		/// <summary>
		/// gets all browsers that GtMetrix can test with
		/// </summary>
		/// <returns>string list of browsers</returns>
		public List<string> GetBrowsers()
        {
            return Enum.GetValues(typeof(Browsers))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToList();
        }
		/// <summary>
		/// get all locations from where GtMetrix can test from
		/// </summary>
		/// <returns>string list of locations</returns>
        public List<string> GetLocations()
        {
            return Enum.GetValues(typeof(Locations))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToList();
        }
		/// <summary>
		/// gets the results of a test using the test id
		/// </summary>
		/// <param name="testId">if of the test</param>
		/// <returns>results of the test</returns>
        public TestResponse GetResult(string testId)
        {
            var testResult = GetTestResults(testId);
            return testResult.Result;
        }

		/// <summary>
		/// Submits a test to GtMetrix.com via the API
		/// </summary>
		/// <param name="testRequest">required test paramters</param>
		/// <returns>A response with a test Id. This can be used to query the results using the GetResult method</returns>
		public SubmitResponse SubmitTest(TestRequestParameter testRequest)
        {
            return SubmitAndGetResponse(testRequest).Result;
        }

		#endregion


		#region private methods
		/// <summary>
		/// gets the results of GtMetrix.com test
		/// </summary>
		/// <param name="testId">the Id of the test </param>
		/// <returns>results of the test</returns>

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
                testResponse.TaskStatus = response.Body.State.ToString();
                if(response.Body.State == ResultStates.Completed)
                {
                    testResponse.TestCompelted = true;
                    testResponse.PageBytes = response.Body.Results.PageBytes;
                    testResponse.PageElements = response.Body.Results.PageBytes;
                    testResponse.PageLoadTime = response.Body.Results.PageElements;
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

		/// <summary>
		/// submits a request to GtMetrix.com via API
		/// </summary>
		/// <param name="testRequest">required paramters for testing</param>
		/// <returns>a submit response with the testId which can then be used to get the results of the test</returns>
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
		#endregion
	}
}
