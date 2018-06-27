using FluentCSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCSharp.After.Repositories
{
    public class BuildOutputString
    {
        public Func<IEnumerable<NationalPublicSiteInfo>, Func<IEnumerable<State>, string>> BuildOutput { get; set; } = GetBuildOutput;
        public Action<string, Action<string>> WriteOutput { get; set; } = WriteOutputToAction;


        private static Func<IEnumerable<State>, string> GetBuildOutput(IEnumerable<NationalPublicSiteInfo> nationalSites) =>
            states => 
                GetNationalSiteTypes(nationalSites)
                .Transform(types => BuildOutputStringForNation(states, nationalSites, types));

        private static string BuildOutputStringForNation(IEnumerable<State> states, IEnumerable<NationalPublicSiteInfo> nationalSites, IEnumerable<string> types) =>
            states.Select(state => BuildStringForState(state, nationalSites, types))
            .Transform(stateStrings => new StringBuilder()
                                      .AppendLine("Sites Overseen by the National Parks Service by State:")
                                      .AppendLine("------------------------------------------")
                                      .AppendSequence(stateStrings, "\n------------------------------------------\n")
                                      .ToString());

        private static string BuildStringForState(State st, IEnumerable<NationalPublicSiteInfo> nationalSites, IEnumerable<string> publicSiteTypes) =>
            nationalSites.Where(s => s.States.Contains(st.abbreviation))
            .Transform(stateSites => BuildStringForTypesByState(stateSites, publicSiteTypes, st.name));

        private static string BuildStringForTypesByState(IEnumerable<NationalPublicSiteInfo> stateSites, IEnumerable<string> publicSiteTypes, string stateName) =>
            publicSiteTypes.Select(type => GetPublicSitesByTypeForState(type, stateSites))
            .Transform(publicSites => new StringBuilder()
                                      .AppendLine("")
                                      .AppendLine($"{stateName}:")
                                      .AppendLine($"Total National Sites: {stateSites.Count()}")
                                      .AppendSequence(publicSites, "")
                                      .ToString());

        private static string GetPublicSitesByTypeForState(string type,  IEnumerable<NationalPublicSiteInfo> sites) =>
            sites.Where(s => s.Designation == type)
            .Transform(sitesByType => new StringBuilder()
                                      .When(
                                            () => sitesByType.Any() && !String.IsNullOrWhiteSpace(type),
                                            sb => sb.AppendLine("")
                                                  .AppendLine($"{type}s ({sitesByType.Count()}): ")
                                                  .AppendSequence(sitesByType.Select(site => site.Name), "\n")
                                                  .AppendLine(""))
                                      .ToString());

        private static IEnumerable<string> GetNationalSiteTypes(IEnumerable<NationalPublicSiteInfo> nationalSites) => 
            nationalSites.Select(s => s.Designation).Distinct();

        private static void WriteOutputToAction(string @this, Action<string> outputMethod)
        {
            outputMethod(@this);
        }
    }
}
