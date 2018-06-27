using System;
using System.Collections.Generic;
using System.Text;

namespace FluentCSharp
{
    public class ApiResponse
    {
        public int total { get; set; }
        public IEnumerable<NationalPublicSiteInfoFromAPI> data { get; set; }
    }

    public class NationalPublicSiteInfoFromAPI
    {
        public string states { get; set; }
        public string latlon { get; set; }
        public string description { get; set; }
        public string designation { get; set; }
        public string parkCode { get; set; }
        public Guid Id { get; set; }
        public string directionsInfo { get; set; }
        public string directionsUrl { get; set; }
        public string fullName { get; set; }
        public string url { get; set; }
        public string weatherInfo { get; set; }
        public string name { get; set; }
    }

    public class NationalPublicSiteInfo
    {
        public NationalPublicSiteInfo(NationalPublicSiteInfoFromAPI apiInfo)
        {
            States = apiInfo.states.Split(',');
            Designation = apiInfo.designation;
            Name = apiInfo.fullName;
            Url = apiInfo.url;
        }

        public IEnumerable<string> States { get; }
        public string Designation { get; }
        public string Name { get; }
        public string Url { get; }
    }
}
