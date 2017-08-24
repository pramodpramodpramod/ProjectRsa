using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectRsa.AbstractObjects;
using ProjectRsa.CustomLibrary;
using ProjectRsa.UI.Models;

namespace ProjectRsa.UI.Controllers
{
    public class ServerController : Controller
    {
        public ActionResult Index(int? sort)
        {
            var model = GetViewModel(sort);
            return View(model);
        }

		/// <summary>
		/// handles contents posted from page
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
        public ActionResult Index(ServerPageModel model)
        {
            model.Urls = model.UrlCsv.Trim(',').Split(',').ToList();
            var location = Helper.ScreenShotPath;
            var snapshotUrl = Helper.ScreenShotUrl;
            model.Results = new List<TestResponse>();
            var threadingOptions = new ParallelOptions() { MaxDegreeOfParallelism = 5 };
            Parallel.ForEach(model.Urls, (url) =>
            {
                IWebPageTester tester = new CustomTester(location, snapshotUrl);
                var testRequest = new TestRequestParameter(url);
                var submissionResponse = tester.SubmitTest(testRequest);
                var result = tester.GetResult(submissionResponse.TestId);
                model.Results.Add(result);
            });
            model.Results = SortResultsBy(model.Results, model.SortBy);
            Session["ViewModel"] = model;
            return View(model);
        }

		/// <summary>
		/// Check for sort parameter, sorts the session results 
		/// </summary>
		/// <param name="sort">paramter for sorting</param>
		/// <returns>View model with sorted results</returns>
        private ServerPageModel GetViewModel(int? sort)
        {
            var model = new ServerPageModel();
            if (sort.HasValue && sort.Value > 0 && Session["ViewModel"] != null)
            {
                model = (ServerPageModel)Session["ViewModel"];
                model.SortBy = sort.Value;
                model.Results = SortResultsBy(model.Results, sort.Value);
                Session["ViewModel"] = model;
            }
            return model;
        }

		/// <summary>
		/// Helper to sort the test results
		/// </summary>
		/// <param name="results">result set</param>
		/// <param name="sort">sort paramter</param>
		/// <returns>sorted list </returns>
        private List<TestResponse> SortResultsBy(IEnumerable<TestResponse> results, int sort)
        {
            return results.OrderBy(r => sort == 2 ? r.PageBytes : r.PageLoadTime).ToList();
        }

    }
}