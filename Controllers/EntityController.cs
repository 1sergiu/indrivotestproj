using IndrivoTestProj.Data.Services;
using IndrivoTestProj.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IndrivoTestProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly EntityService _entityService;

        public EntityController(EntityService entityService)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        [HttpGet("get-all-entities")]
        public async Task<IActionResult> GetAllEntities()
        {
            var entities = await _entityService.GetAllEntitiesAsync();
            return Ok(entities);
        }

        [HttpGet("get-entity-by-id/{guid}")]
        public async Task<IActionResult> GetEntityById(Guid guid)
        {
            var entity = await _entityService.GetEntityByIdAsync(guid);
            return entity != null ? Ok(entity) : NotFound();
        }

        [HttpPost("add-entity")]
        public async Task<IActionResult> AddEntity([FromBody] EntityVM entityVM)
        {
            await _entityService.AddEntityAsync(entityVM);
            return CreatedAtAction(nameof(GetEntityById), new { guid = Guid.NewGuid() }, entityVM);
        }

        [HttpPut("update-entity-by-id/{guid}")]
        public async Task<IActionResult> UpdateEntityById(Guid guid, [FromBody] EntityVM entityVM)
        {
            var updatedEntity = await _entityService.UpdateEntityByIdAsync(guid, entityVM);
            return updatedEntity != null ? Ok(updatedEntity) : NotFound();
        }

        [HttpDelete("delete-entity-by-id/{guid}")]
        public async Task<IActionResult> DeleteEntityById(Guid guid)
        {
            await _entityService.DeleteEntityByIdAsync(guid);
            return NoContent();
        }
    }

}