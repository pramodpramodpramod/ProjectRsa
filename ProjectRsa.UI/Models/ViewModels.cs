using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRsa.AbstractObjects;

namespace ProjectRsa.UI.Models
{
    public class ServerPageModel
    {
        public string UrlCsv { get; set; }
        public List<string> Urls { get; set; }
        public List<TestResponse> Results{ get; set; }

        public int SortBy { get; set; }
        public IEnumerable<SelectListItem> AllSortOptions
        {
            get
            {
                var items = new List<SelectListItem>();
                items.Add(new SelectListItem() { Value = "1", Text = "Load Time" });
                items.Add(new SelectListItem() { Value = "2", Text = "Size" });
                return new SelectList(items,"Value","Text");
            }
        }


    }
}