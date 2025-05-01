using Microsoft.AspNetCore.Mvc;
using Forum.Models;   // modèle Etablissement

namespace Forum.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EtablissementsApiController : ControllerBase
    {
        private readonly EtablissementService _service;

        public EtablissementsApiController(EtablissementService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string region, string niveau)
        {
            var etablissements = await _service.GetEtablissementsAsync(region, niveau);
            return Ok(etablissements);

        }
    }
}
