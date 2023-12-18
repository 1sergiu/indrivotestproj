using IndrivoTestProj.Data.Services;
using IndrivoTestProj.Data.ViewModels;
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

        [HttpGet("get-all-classifiers")]
        public async Task<IActionResult> GetAllClassifiers()
        {
            var allClassifiers = await _classifierService.GetAllClassifiersAsync();
            return Ok(allClassifiers);
        }

        [HttpGet("get-classifier-by-id")]
        public async Task<IActionResult> GetClassifierById(Guid guid)
        {
            var classifierId = await _classifierService.GetClassifierByIdAsync(guid);
            return Ok(classifierId);
        }

        [HttpPost("add-type")]
        public async Task<IActionResult> AddClassifier([FromBody] ClassifierVM classifier)
        {
            await _classifierService.AddClassifierAsync(classifier);
            return Ok();
        }

        [HttpPut("update-classifier-by-id/{guid}")]
        public async Task<IActionResult> UpdateClassifierById(Guid guid, [FromBody] ClassifierVM classifier)
        {
            var updateClassifier = await _classifierService.UpdateClassifierByIdAsync(guid, classifier);
            return Ok(updateClassifier);
        }

        [HttpDelete("delete-classifier-by-id/{guid}")]
        public async Task<IActionResult> DeleteClassifierById(Guid guid)
        {
            await _classifierService.DeleteClassifierByIdAsync(guid);
            return Ok();
        }
    }
}