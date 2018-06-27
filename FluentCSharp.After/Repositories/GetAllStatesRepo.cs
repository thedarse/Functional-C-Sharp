using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FluentCSharp.After.Repositories
{
    public class GetAllStatesRepo
    {

        public Func<Task<IEnumerable<State>>> GetAllStatesFromFile { get; set; } = GetStatesFromJsonFile;

        private static async Task<IEnumerable<State>> GetStatesFromJsonFile() =>
            (await File.ReadAllTextAsync("USStates.json"))
            .Transform(str => JsonConvert.DeserializeObject<IEnumerable<State>>(str));
    }
}
