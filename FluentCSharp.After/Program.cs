using FluentCSharp.After.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentCSharp.After
{
    class Program
    {
        static void Main(string[] args)
        {
            new BuildOutputString().Tee(outputRepo =>
                GetNationalSiteFromNPS()
                .Transform(CreateOutputString(ReadAllStatesFromJsonFile, outputRepo))
                .Tee(outputString => outputRepo.WriteOutput(outputString, Console.WriteLine)))
            .Tee(t => { Console.ReadLine(); });
        }

        private static Func<IEnumerable<NationalPublicSiteInfo>, string> CreateOutputString(Func<IEnumerable<State>> statesFunction, BuildOutputString outputRepo) =>
            parks => statesFunction().Transform(outputRepo.BuildOutput(parks));

        private static IEnumerable<NationalPublicSiteInfo> GetNationalSiteFromNPS() =>
            new GetNationalParksServiceList()
                //.Tee(s => s.GetNationalSitesListFromNPS = async (key, num) => await Task.Factory.StartNew(() => new List<NationalPublicSiteInfo>()))
                .GetNationalSitesListFromNPS("vqi9TPwyfJioXQrmDg0ogccN1sQSheE6hgP7f0ZM", 501)
                .GetAwaiter().GetResult();

        private static IEnumerable<State> ReadAllStatesFromJsonFile() =>
            new GetAllStatesRepo().GetAllStatesFromFile().GetAwaiter().GetResult();
    }
}
