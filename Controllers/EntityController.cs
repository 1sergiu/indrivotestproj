using IndrivoTestProj.Data.Services;
using IndrivoTestProj.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IndrivoTestProj.Exceptions;

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

        [HttpGet]
        public async Task<IActionResult> GetAllEntities()
        {
            var entities = await _entityService.GetAllEntitiesAsync();
            return Ok(entities);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetEntityById(Guid guid)
        {
            try
            {
                var entity = await _entityService.GetEntityByIdAsync(guid);
                return entity != null ? Ok(entity) : NotFound();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddEntity([FromBody] EntityVM entityVM)
        {
            try
            {
                await _entityService.AddEntityAsync(entityVM);
                return CreatedAtAction(nameof(GetEntityById), new { guid = Guid.NewGuid() }, entityVM);
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateEntityById(Guid guid, [FromBody] EntityVM entityVM)
        {
            try
            {
                var updatedEntity = await _entityService.UpdateEntityByIdAsync(guid, entityVM);
                return updatedEntity != null ? Ok(updatedEntity) : NotFound();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }

        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteEntityById(Guid guid)
        {
            try
            {
                await _entityService.DeleteEntityByIdAsync(guid);
                return NoContent();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }

}