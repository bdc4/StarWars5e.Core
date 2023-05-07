using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using StarWars5e.Api.Storage;
using StarWars5e.Models;
using StarWars5e.Models.Enums;

namespace StarWars5e.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IAzureTableStorage _tableStorage;

        public LanguagesController(IAzureTableStorage tableStorage)
        {
            _tableStorage = tableStorage;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonLanguage>>> Get(Language language = Language.en)
        {
            List<CommonLanguage> languages;
            try
            {
                languages = (await _tableStorage.GetAllAsync<CommonLanguage>($"languages{language}")).ToList();
            }
            catch (StorageException e)
            {
                if (e.Message == "Not Found")
                {
                    languages = (await _tableStorage.GetAllAsync<CommonLanguage>($"languages{Language.en}")).ToList();
                    return Ok(languages);
                }
                throw;
            }
            return Ok(languages);
        }
    }
}
