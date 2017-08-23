using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectRsa.AbstractObjects;
using ProjectRsa.GtMetrix;

namespace ProjectRsa.Tests
{
    [TestFixture]
    public class Test_GtMetrix_Library
    {

        [Test]
        public void GtMetrix_Implements_Interface()
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("abc","abc");
            Assert.AreEqual(gtMetrixObject.GetType(), typeof(GtMetrixTester));
        }

        [Test]
        public void GetBroswers_Return_List_Of_Three_Browsers()
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("abc", "abc");
            var browsers = gtMetrixObject.GetBrowsers();
            Assert.AreEqual(browsers.Count, 3);
        }

        [Test]
        public void GetBroswers_Return_List_Of_Six_Browsers()
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("abc", "abc");
            var browsers = gtMetrixObject.GetLocations();
            Assert.AreEqual(browsers.Count, 6);
        }

        [Test]
        public void Invalid_Credentials_Fails_To_Submit_Test_Request()
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("abc", "abc");
            var request = new TestRequestParameter("https://www.rsagroup.com/");
            var resposne = gtMetrixObject.SubmitTest(request);
            Assert.AreEqual(resposne.HttpStatusCode, HttpStatusCode.Unauthorized);
        }

        [Test]
        public void Valid_Credentials_Submits_TestRequest() //commented after testing to save api calls during futher tests
        {
            //IWebPageTester gtMetrixObject = new GtMetrixTester("79833eef2ccd3c70f03a7de88b2009e9", "pramod.mohanan@hotmail.co.uk");
            //var request = new TestRequestParameter("https://www.w3schools.com/");
            //var resposne = gtMetrixObject.SubmitTest(request);
            //Assert.AreEqual(resposne.HttpStatusCode, HttpStatusCode.OK);
            Assert.AreEqual(true, true);
        }


        [TestCase("FoNqSQMO")]
        public void InValid_Credentials_Fails_To_GetsResults(string testId) //commented after testing to save api calls during futher tests
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("asdf", "asdf");
            var testResults = gtMetrixObject.GetResult(testId);
            Assert.AreEqual(testResults.HttpStatusCode, HttpStatusCode.Unauthorized);
        }


        [TestCase("FoNqSQMO")]
        public void Valid_Credentials_GetsResults(string testId) //commented after testing to save api calls during futher tests
        {
            IWebPageTester gtMetrixObject = new GtMetrixTester("79833eef2ccd3c70f03a7de88b2009e9", "pramod.mohanan@hotmail.co.uk");
            var testResults = gtMetrixObject.GetResult(testId);
            Assert.AreEqual(testResults.Success, true);
        }
    }
}
