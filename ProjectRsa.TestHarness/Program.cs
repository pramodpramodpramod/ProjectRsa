using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProjectRsa.AbstractObjects;
using ProjectRsa.CustomLibrary;
using ProjectRsa.GtMetrix;

namespace ProjectRsa.TestHarness
{
    class Program
    {

		#region private variables - in realtime this should be kept outside code
		private static string _gtMetrixApiKey = "79833eef2ccd3c70f03a7de88b2009e9";
		private static string _gtMetrixUserName = "pramod.mohanan@hotmail.co.uk";
		#endregion


		static void Main(string[] args)
        {
            var url = "http://www.w3schools.com";
            TestGetMetrix(url);
        }

        static void TestGetMetrix(string url)
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester(_gtMetrixApiKey, _gtMetrixUserName);
            var request = new TestRequestParameter(url);
            var resposne = gtMetrixObject.SubmitTest(request);

            Console.WriteLine($"Success:{resposne.Success}");
            Console.WriteLine($"HttpStatusCode: {resposne.HttpStatusCode}");
            Console.WriteLine($"PollStateUrl: {resposne.PollStateUrl ?? string.Empty}");
            Console.WriteLine($"Error: {resposne.Error ?? string.Empty}");
            Console.WriteLine($"Id: {resposne.TestId ?? string.Empty}");
            Console.WriteLine($"Status: {resposne.Status ?? string.Empty}");
            Console.WriteLine($"------------------------------------------------------------");
            var testId = resposne.TestId;
            TestResponse result = null;
            do
            {
                result = gtMetrixObject.GetResult(testId);
                Console.WriteLine($"Success:{result.Success}. Code: {result.HttpStatusCode}");
                Console.WriteLine($"Task Status:{result.TaskStatus}.");
				if (result.Success && result.TestCompelted)
					break;
                Console.WriteLine($".....Sleeping for two seconds.....");
                Thread.Sleep(2000);
            }
            while (!result.TestCompelted && result.Success);
            Console.WriteLine($"Task Status:{result.TaskStatus}");
            Console.WriteLine($"resposne.Success:{result.Success}");
            Console.WriteLine($"PageBytes:{result.PageBytes}");
            Console.WriteLine($"PageLoadTime:{result.PageLoadTime}");
            Console.WriteLine($"PageElements:{result.PageElements}");
            Console.WriteLine($"ScreenshotUrl:{result.ScreenShotUrl}");
            Console.WriteLine($"ReportUrl:{result.ReportUrl}");
        }
    }
}
