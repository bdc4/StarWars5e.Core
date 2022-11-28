using StarWars5e.Models;
using StarWars5e.Models.Enums;
using StarWars5e.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarWars5e.Parser.Processors
{
    internal static class FeatureMetadataProcessor
    {

        public static string ProcessMetadata(Feature feature)
        {
            var manifestStreams = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var key = $"StarWars5e.Parser.Sources.metadata.features.{feature.Source}.{feature.SourceName.Replace(",", "").Replace(" ", "")}.{feature.Name.Replace(",", "").Replace(" ", "")}.json";
            key = manifestStreams.FirstOrDefault(s => s.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (key == null) return String.Empty;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(key))
            {
                if (stream == null)
                {
                    return String.Empty;
                }
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, 128))
                {
                    return reader.ReadToEnd().MinifyJson();
                }
            }
        }

        public static string ProcessMetadata(FightingStrategy fightingStrategy)
        {
            var manifestStreams = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var key = $"StarWars5e.Parser.Sources.metadata.FightingStrategy.{fightingStrategy.Name.Replace(",", "").Replace(" ", "")}.json";
            key = manifestStreams.FirstOrDefault(s => s.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (key == null) return String.Empty;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(key))
            {
                if (stream == null)
                {
                    return String.Empty;
                }
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, 128))
                {
                    return reader.ReadToEnd().MinifyJson();
                }
            }
        }
    }
}
