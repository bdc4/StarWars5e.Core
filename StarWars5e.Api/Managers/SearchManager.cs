using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Search.Documents;
using StarWars5e.Api.Interfaces;
using StarWars5e.Models.Enums;
using StarWars5e.Models.Search;

namespace StarWars5e.Api.Managers
{
    public class SearchManager : ISearchManager
    {
        private readonly SearchClient _searchClient;

        public SearchManager(SearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<IEnumerable<GlobalSearchTerm>> RunGlobalSearch(string searchText, Language language)
        {
            try {
                var search = await _searchClient.SearchAsync<GlobalSearchTerm>(searchText, new SearchOptions {Size=1000});
                var results = search.Value.GetResults()
                    .Select(r => r.Document).Where(d => d.LanguageEnum == language);
                return results;
            } catch(System.Exception e) {
                return new List<GlobalSearchTerm>();
            } 
        }
    }
}