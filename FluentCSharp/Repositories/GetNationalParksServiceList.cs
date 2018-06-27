using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FluentCSharp
{
    public interface IGetNationalParksServiceList
    {
        Task<List<NationalPublicSiteInfo>> GetNationalSitesListFromNPS(string apikey, int limit);
    }

    public class GetNationalParksServiceList : IGetNationalParksServiceList
    {
        public async Task<List<NationalPublicSiteInfo>> GetNationalSitesListFromNPS(string apikey, int limit)
        {
            HttpClient client = GetHttpClient();

            HttpResponseMessage response = await client.GetAsync($"api/v1/parks?api_key={apikey}&limit={limit}");

            var nationalSites = new List<NationalPublicSiteInfo>();

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var responses = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ApiResponse>(responseString));

                foreach(NationalPublicSiteInfoFromAPI park in responses.data)
                {
                    nationalSites.Add(new NationalPublicSiteInfo(park));
                }
            }

            return nationalSites;
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://developer.nps.gov/");

            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
