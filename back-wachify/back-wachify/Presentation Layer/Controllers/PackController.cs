using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_wachify.Presentation_Layer.Controllers

{

    [Route("api/[controller]")]
    [ApiController]
    public class PackController : ControllerBase
    {
        private readonly IPackService _packService;

        public PackController(IPackService packService)
        {
            _packService = packService;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetAllPacks()
        {
            var packs = await _packService.GetAllPacks();
            return Ok(packs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackById(int id)
        {
            var pack = await _packService.GetPackById(id);
            if (pack == null)
                return NotFound();
            return Ok(pack);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePack([FromBody] Pack pack)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newPackId = await _packService.CreatePack(pack);
            return CreatedAtAction(nameof(GetPackById), new { id = newPackId }, pack);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePack(int id, [FromBody] Pack pack)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _packService.UpdatePack(id, pack);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePack(int id)
        {
            var success = await _packService.DeletePack(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }

}
