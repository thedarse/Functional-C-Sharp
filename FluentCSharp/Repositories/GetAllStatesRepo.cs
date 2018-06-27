using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FluentCSharp.ProceduralRepositories
{
    public interface IGetAllStatesRepo
    {
        Task<List<State>> GetAllStatesFromFile();
    }

    public class GetAllStatesRepo : IGetAllStatesRepo
    {
        public async Task<List<State>> GetAllStatesFromFile()
        {
            string fileString = await File.ReadAllTextAsync("USStates.json");

            var states = JsonConvert.DeserializeObject<List<State>>(fileString);

            return states;
        }
    }
}
