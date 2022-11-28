using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWars5e.Models;
using StarWars5e.Models.Enums;
using StarWars5e.Models.Utils;

namespace StarWars5e.Parser.Processors.PHB
{
    public class PlayerHandbookLanguagesProcessor : BaseProcessor<CommonLanguage>
    {
        public override Task<List<CommonLanguage>> FindBlocks(List<string> lines)
        {
            var languages = new List<CommonLanguage>();

            var languagesStart = lines.FindIndex(x => x.StartsWith("##### Common Languages"));
            var languagesEnd = lines.FindIndex(languagesStart + 1, x => x.StartsWith("#"));

            languages.AddRange(lines
                .GetRange(languagesStart, languagesEnd - languagesStart)
                .Where(l => l.StartsWith("|") && !l.StartsWith("||") && !l.StartsWith("|:"))
                .Select(l => new CommonLanguage()
                {
                    Name = l.Split("|")[1].Trim(),
                    RowKey = l.Split("|")[1].Trim(),
                    PartitionKey = "Core"
                }));

            return Task.FromResult(languages);
        }
    }
}
