using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectRsa.AbstractObjects;
using ProjectRsa.CustomLibrary;

namespace ProjectRsa.Tests
{
    [TestFixture]
    class Test_Custom_Library
    {
        [Test]
        public void CustomTester_Implements_Interface()
        {
            IWebPageTester gtMetrixObject = new CustomTester("abc", "abc");
            Assert.AreEqual(gtMetrixObject.GetType(), typeof(CustomTester));
        }

        [Test]
        public void GetBroswers_Return_List_Of_One_Browsers()
        {
            IWebPageTester gtMetrixObject = new CustomTester("abc", "abc");
            var browsers = gtMetrixObject.GetBrowsers();
            Assert.AreEqual(browsers.Count, 1);
        }

        [Test]
        public void GetLocations_Return_List_Of_One_Location()
        {
            IWebPageTester gtMetrixObject = new CustomTester("abc", "abc");
            var browsers = gtMetrixObject.GetLocations();
            Assert.AreEqual(browsers.Count, 1);
        }

        [Test]
        public void GetLocations_SubmitSite_Returns_new_TestId()
        {
            var url = "www.w3schools.com";
            var location = @"C:\Users\Pramod\Documents\visual studio 2017\Projects\ProjectRsa\ProjectRsa.UI\Content\screenshots";
            var screenshotPrefix = @"http://api.screenshotlayer.com/api/capture?access_key=bf00597a1e21c182ca629879d0b953d4&amp;width=540&amp;viewport=1440x900&amp;url=";
            IWebPageTester gtMetrixObject = new CustomTester(location, screenshotPrefix);
            var response = gtMetrixObject.SubmitTest(new TestRequestParameter(url));
            Assert.IsNotNull(response.TestId);
        }


    }
}
