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

        [HttpGet]
        public async Task<ActionResult<List<Classifier>>> GetAllClassifiers()
        {
            var allClassifiers = await _classifierService.GetAllClassifiersAsync();
            return Ok(allClassifiers);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<Classifier>> GetClassifierById(Guid guid)
        {
            try
            {
                var classifier = await _classifierService.GetClassifierByIdAsync(guid);
                return Ok(classifier);
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClassifier([FromBody] ClassifierVM classifier)
        {
            await _classifierService.AddClassifierAsync(classifier);
            return NoContent();
        }

        [HttpPut("{guid}")]
        public async Task<ActionResult<Classifier>> UpdateClassifierById(Guid guid, [FromBody] ClassifierVM classifier)
        {
            try
            {
                var updatedClassifier = await _classifierService.UpdateClassifierByIdAsync(guid, classifier);
                return Ok(updatedClassifier);
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteClassifierById(Guid guid)
        {
            try
            {
                await _classifierService.DeleteClassifierByIdAsync(guid);
                return NoContent();
            }
            catch (GuidNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}