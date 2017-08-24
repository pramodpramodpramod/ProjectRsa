using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ProjectRsa.AbstractObjects;

namespace ProjectRsa.CustomLibrary
{
	/// <summary>
	/// Class that implements the IWebPageTester Interface
	/// </summary>
    public class CustomTester : IWebPageTester
    {

		#region private fields
		private string _downloadLocation;
        private string _screenshotUrlPrefix;
        private TestResponse _testResponse;
		#endregion

		#region constructor
		/// <summary>
		/// Initiates Custom Tester calss 
		/// </summary>
		/// <param name="downloadLocation">physical path where the screen shot image would be stored</param>
		/// <param name="screenShotUrlPrefix">the api URL to which the url could be applied</param>
		public CustomTester(string downloadLocation, string screenShotUrlPrefix)
        {
            _downloadLocation = downloadLocation;
            _screenshotUrlPrefix = screenShotUrlPrefix;
        }
		#endregion

		#region implementation of IWebPageTester methods

		public List<string> GetBrowsers()
        {
            return new List<string> { "Custom" };
        }

        public List<string> GetLocations()
        {
            return new List<string> { "Custom" };
        }

        public TestResponse GetResult(string testId)
        {
            return _testResponse;
        }


		/// <summary>
		/// sumbmits the urls passed for basic testing
		/// </summary>
		/// <param name="testRequest">parameters for testing</param>
		/// <returns>A response with a test Id. This can be used to query the results using the GetResult method</returns>
        public SubmitResponse SubmitTest(TestRequestParameter testRequest)
        {
            var testId = Guid.NewGuid().ToString();
            var result = SubmitAllTests(testId, testRequest);
            var submitResponse = new SubmitResponse()
            {
                Status = "OK",
                Success = true,
                TestId = testId
            };
            return submitResponse;
        }


		#endregion

		#region Private methods

		private bool SubmitAllTests(string testId, TestRequestParameter testRequest)
        {
            var url = testRequest.Url;
			//add http protocol if protocol is missing
            if (!testRequest.Url.StartsWith("http"))
                url = $"http://{testRequest.Url}";
            _testResponse = new TestResponse();
            _testResponse.TestId = testId;
            _testResponse.Url = testRequest.Url;
            try
            {
                GetPageStats(testId, url);
                GetPageScreenshot(testId, url);
                _testResponse.Success= true;
                _testResponse.TestCompelted = true;
                _testResponse.TestStatus = "Completed";
                _testResponse.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex) // if an error occurs return false
            {
                _testResponse.Success = false;
                _testResponse.TestCompelted = false;
                _testResponse.TestStatus = "Error while processing.";
                _testResponse.HttpStatusCode = HttpStatusCode.OK;
            }
            return true;
        }

		//uses the screenshot API to get a screen shot of the URL
        private bool GetPageScreenshot(string testId, string url)  
        {
            using (WebClient client = new WebClient())
            {
                var imageUrl = $"{_screenshotUrlPrefix}{url}";
                var imageFileName = $"{testId}.jpg";
                client.DownloadFile(new Uri(imageUrl), Path.Combine(_downloadLocation, imageFileName));
                _testResponse.ScreenShotUrl = imageFileName;
            }
            return true;
        }

		//uses the WebClient class to download contents of a URL and calculates size and download time
		private bool GetPageStats(string testId, string url)
        {
            using (var webClient = new WebClient())
            {
                var timer = new Stopwatch();
                timer.Start();
                var data = webClient.DownloadData(url);
                timer.Stop();
                var loadTime = timer.Elapsed.TotalMilliseconds;
                _testResponse.PageLoadTime = loadTime;
                _testResponse.PageBytes = data.Length;
            }
            return true;
        }
		#endregion

	}
}
