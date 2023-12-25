using IndrivoTestProj.Data.Services;
using IndrivoTestProj.Data.ViewModels;
using IndrivoTestProj.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllClassifiers()
        {
            var allClassifiers = await _classifierService.GetAllClassifiersAsync();
            return Ok(allClassifiers);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetClassifierById(Guid guid)
        {
            try
            {
                var classifierId = await _classifierService.GetClassifierByIdAsync(guid);
                return Ok(classifierId);
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddClassifier([FromBody] ClassifierVM classifier)
        {
            await _classifierService.AddClassifierAsync(classifier);
            return Ok();
        }

        [HttpPut("{guid}")]
        public async Task<IActionResult> UpdateClassifierById(Guid guid, [FromBody] ClassifierVM classifier)
        {
            try
            {
                var updateClassifier = await _classifierService.UpdateClassifierByIdAsync(guid, classifier);
                return Ok(updateClassifier);
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteClassifierById(Guid guid)
        {
            try
            {
                await _classifierService.DeleteClassifierByIdAsync(guid);
                return Ok();
            }
            catch (GuidNotFoundException ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}