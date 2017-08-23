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
    public class CustomTester : IWebPageTester
    {
        private string _downloadLocation;
        private string _screenshotUrlPrefix;
        private TestResponse _testResponse;
        public CustomTester(string downloadLocation, string screenShotUrlPrefix)
        {
            _downloadLocation = downloadLocation;
            _screenshotUrlPrefix = screenShotUrlPrefix;
        }
        //screenshot API key bf00597a1e21c182ca629879d0b953d4
        //https://screenshotlayer.com/dashboard

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

        private bool SubmitAllTests(string testId, TestRequestParameter testRequest)
        {
            var url = testRequest.Url;
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
            catch (Exception ex)
            {
                _testResponse.Success = false;
                _testResponse.TestCompelted = false;
                _testResponse.TestStatus = "Error while processing.";
                _testResponse.HttpStatusCode = HttpStatusCode.OK;
            }
            return true;
        }

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

    }
}
