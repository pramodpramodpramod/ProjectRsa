using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRsa.AbstractObjects
{
    public class SubmitResponse
    {
        public string TestId { get; set; }
        public string Status { get; set; }
        public string PollStateUrl { get; set; }
        public bool Success { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
        public string Error { get; set; }
    }
}
