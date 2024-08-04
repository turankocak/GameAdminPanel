using Microsoft.AspNetCore.Mvc;
using AdminPanelBackend.Models;
using AdminPanelBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPanelBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly BuildingService _buildingService;

        public BuildingController(BuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // Binaları listele
        [HttpGet]
        public async Task<ActionResult<List<Building>>> Get()
        {
            return await _buildingService.GetAllAsync();
        }

        // Belirli bir bina getirme
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> Get(string id)
        {
            var building = await _buildingService.GetByIdAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // Yeni bina oluşturma
        [HttpPost]
        public async Task<ActionResult<Building>> Create([FromBody] Building building)
        {
            
            building.Id = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _buildingService.AddAsync(building);
            
            return CreatedAtAction(nameof(Get), new { id = building.Id }, building);
        }

        // Var olan bir binayı güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Building buildingIn)
        {
            if (id != buildingIn.Id)
            {
                return BadRequest(new { Error = "URL'deki ID, JSON verisindeki ID ile eşleşmiyor." });
            }

            var building = await _buildingService.GetByIdAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            await _buildingService.UpdateAsync(id, buildingIn);

            return NoContent();
        }

        // Var olan bir binayı silme
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var building = await _buildingService.GetByIdAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            await _buildingService.RemoveAsync(id);

            return NoContent();
        }

        // Bina tiplerini listele
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<BuildingType>>> GetBuildingTypes()
        {
            var buildingTypes = await _buildingService.GetBuildingTypesAsync();
            return Ok(buildingTypes);
        }
    }
}
