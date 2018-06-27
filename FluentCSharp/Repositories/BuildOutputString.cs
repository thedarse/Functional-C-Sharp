using FluentCSharpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentCSharp
{
    public interface IBuildOutputString
    {
        string BuildOutput(List<NationalPublicSiteInfo> nationalSites, List<State> states);
        void WriteOutput(string @this, Action<string> outputMethod);
    }

    public class BuildOutputString: IBuildOutputString
    {
        public string BuildOutput(List<NationalPublicSiteInfo> nationalSites, List<State> states)
        {
            List<string> publicSiteTypes = GetNationalSiteTypes(nationalSites);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Sites Overseen by the National Parks Service by State:");

            //var stateStrings = states.Select(st => BuildStringForState(st, nationalSites, publicSiteTypes));
            //sb.AppendLine("------------------------------------------");
            //sb.AppendSequence(stateStrings, "------------------------------------------");
            foreach (State st in states)
            {
                string stateString = BuildStringForState(st, nationalSites, publicSiteTypes);

                sb.AppendLine("------------------------------------------")
                    .Append(stateString);
            }

            return sb.ToString();
        }

        //private static Func<State,string> BuildStringForState(List<NationalPublicSiteInfo> nationalSites, List<string> publicSiteTypes) => st =>
        private static string BuildStringForState(State st, List<NationalPublicSiteInfo> nationalSites, List<string> publicSiteTypes)
        {
            List<NationalPublicSiteInfo> sites = nationalSites.Where(s => s.States.Contains(st.abbreviation)).ToList();


            var stateSb = new StringBuilder()
                .AppendLine("")
                .AppendLine($"{st.name}:")
                .AppendLine($"Total National Sites: {sites.Count()}");

            foreach(string type in publicSiteTypes)
            {
                List<NationalPublicSiteInfo> stateSitesByType = sites.Where(s => s.Designation == type).ToList();

                if (stateSitesByType.Any() && !String.IsNullOrWhiteSpace(type))
                {

                    stateSb
                        .AppendLine("")
                        .AppendLine($"{type}s ({stateSitesByType.Count()}): ");

                    foreach (NationalPublicSiteInfo site in stateSitesByType)
                    {
                        stateSb.AppendLine(site.Name);
                    }

                }
            }

            return stateSb.AppendLine("").ToString();
        }

        private static List<string> GetNationalSiteTypes(List<NationalPublicSiteInfo> nationalSites)
        {
            return nationalSites.Select(s => s.Designation).Distinct().ToList();
        }

        public void WriteOutput(string @this, Action<string> outputMethod)
        {
            outputMethod(@this);
        }
    }
}
