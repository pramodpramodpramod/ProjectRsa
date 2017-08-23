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

        [HttpPost]
        // GET: Custom
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
        private List<TestResponse> SortResultsBy(IEnumerable<TestResponse> results, int sort)
        {
            return results.OrderBy(r => sort == 2 ? r.PageBytes : r.PageLoadTime).ToList();
        }

    }
}