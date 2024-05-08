using Microsoft.AspNetCore.Mvc;
using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using System.Threading.Tasks;

namespace back_wachify.Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbonnementController : ControllerBase
    {
        private readonly IAbonnementService _abonnementService;

        public AbonnementController(IAbonnementService abonnementService)
        {
            _abonnementService = abonnementService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAbonnement([FromBody] AbonnementDto abnDto)
        {
            var (abonnement, message) = await _abonnementService.CreateAbonnement(abnDto);

            if (abonnement != null)
            {
                return Ok(new { Message = message, Abonnement = abonnement });
            }
            else
            {
                return BadRequest(new { Message = message });
            }
        }
    }
}
