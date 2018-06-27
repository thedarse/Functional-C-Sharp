using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FluentCSharp
{
    public class GetNationalParksServiceList 
    {
        public Func<string, int, Task<IEnumerable<NationalPublicSiteInfo>>> GetNationalSitesListFromNPS { get; set; } = GetNationalSitesList;

        private static async Task<IEnumerable<NationalPublicSiteInfo>> GetNationalSitesList(string apikey, int limit) =>
            await (
                   await GetHttpClient().GetAsync($"api/v1/parks?api_key={apikey}&limit={limit}"))
                        .Transform(response => Enumerable.Empty<NationalPublicSiteInfo>()
                                               .WhenAsync(
                                                          () => response.IsSuccessStatusCode,
                                                          async r => await ReadResponse(response)));


        private static async Task<IEnumerable<NationalPublicSiteInfo>> ReadResponse(HttpResponseMessage response) =>
            (await(
                   await response.Content.ReadAsStringAsync()
                  ).TransformAsync(async responseString => 
                                                         await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<ApiResponse>(responseString)))
            ).data.Select(park => new NationalPublicSiteInfo(park));

        private static HttpClient GetHttpClient() =>
            new HttpClient
            {
                BaseAddress = new Uri("https://developer.nps.gov/")
            }
            .Tee(c => c.DefaultRequestHeaders.Accept.Clear())
            .Tee(c => c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")));
    }
}
