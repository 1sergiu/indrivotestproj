using IndrivoTestProj.Data.Services;
using IndrivoTestProj.Data.ViewModels;
using IndrivoTestProj.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IndrivoTestProj.Data.Models;

namespace IndrivoTestProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifierController : ControllerBase
    {
        private readonly ClassifierService _classifierService;

        public ClassifierController(ClassifierService classifierService)
        {
            _classifierService = classifierService;
        }

        private ActionResult HandleException(Exception ex)
        {
            if (ex is GuidNotFoundException)
            {
                return NotFound(ex.Message);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }

        [HttpGet]
        public async Task<ActionResult<List<Classifier>>> GetAllClassifiersAsync()
        {
            var allClassifiers = await _classifierService.GetAllClassifiersAsync();
            return Ok(allClassifiers);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Classifier>> GetClassifierByIdAsync(Guid guid)
        {
            try
            {
                var classifier = await _classifierService.GetClassifierByIdAsync(guid);
                return Ok(classifier);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClassifierAsync([FromBody] ClassifierVM classifier)
        {
            await _classifierService.AddClassifierAsync(classifier);
            return NoContent();
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<Classifier>> UpdateClassifierByIdAsync(Guid guid, [FromBody] ClassifierVM classifier)
        {
            try
            {
                var updatedClassifier = await _classifierService.UpdateClassifierByIdAsync(guid, classifier);
                return Ok(updatedClassifier);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteClassifierByIdAsync(Guid guid)
        {
            try
            {
                await _classifierService.DeleteClassifierByIdAsync(guid);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}