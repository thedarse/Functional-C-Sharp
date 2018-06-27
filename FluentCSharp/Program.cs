using FluentCSharp.ProceduralRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ///  Get National parks from national parks api
            IGetNationalParksServiceList serviceList = new GetNationalParksServiceList();
            IGetAllStatesRepo statesRepo = new GetAllStatesRepo();
            IBuildOutputString buildoutputRepo = new BuildOutputString();

            List<NationalPublicSiteInfo> nationalParks = serviceList.GetNationalSitesListFromNPS("vqi9TPwyfJioXQrmDg0ogccN1sQSheE6hgP7f0ZM", 501)
                                                            .GetAwaiter()
                                                            .GetResult();

            List<State> states = statesRepo.GetAllStatesFromFile()
                                    .GetAwaiter()
                                    .GetResult();

            string outputString = buildoutputRepo.BuildOutput(nationalParks, states);

            buildoutputRepo.WriteOutput(outputString, str => { Console.WriteLine(str); });

            Console.ReadLine();
        }
    }
}
