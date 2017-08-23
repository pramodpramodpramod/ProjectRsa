using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRsa.AbstractObjects
{
    public class TestRequestParameter
    {
        public TestRequestParameter(string url)
        {
            this.Url = url;
        }
        public TestRequestParameter(string url, string location) : this (url)
        {
            this.Location = location;
        }
        public TestRequestParameter(string url, string location, string browser) : this(url, location)
        {
            this.Browser = browser;
        }

        public string Url { get; set; }

        public string Location { get; set; }

        public string Browser { get; set; }

    }
}
