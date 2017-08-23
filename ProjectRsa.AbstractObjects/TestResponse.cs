using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRsa.AbstractObjects
{
    public class TestResponse
    {
        public string Url { get; set; }
        public string TestId { get; set; }

        public string TestStatus { get; set; }

        public double PageLoadTime { get; set; }
        public int PageElements { get; set; }
        public int TestScore { get; set; }
        public int PageBytes { get; set; }
        public string TaskStatus { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool TestCompelted { get; set; }
        public string Error { get; set; }

        public string ScreenShotUrl { get; set; }
        public string ReportUrl { get; set; }
    }
}
