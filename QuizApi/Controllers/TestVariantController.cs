using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Services;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestVariantController : ControllerBase
    {

        private readonly ITestVariantService _testVariantService;
        public TestVariantController(ITestVariantService testVariantService)
        {
            _testVariantService = testVariantService;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _testVariantService.GetAll());
        }

        [Authorize]
        [HttpGet]
        [Route("{testVariantId:int:min(1)}/get-questions")]
        public async Task<IActionResult> GetQuestions(int testVariantId)
        {
            Random random = new Random();
            var testVariant = await _testVariantService.Get(testVariantId);
            var questions = testVariant.Questions;
            var shuffledList = questions.OrderBy(q => random.Next()).ToList();
            var questionsForFrontend = shuffledList.Take(10).ToList();
            testVariant.NumberOfQuestions = questionsForFrontend.Count;
            testVariant.Questions = questionsForFrontend;
            return Ok(testVariant);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var testVariant = await _testVariantService.Get(id);
            if (testVariant != null)
            {
                return Ok(testVariant);
            }
            return NotFound(new ResponseModel() { Status = "Error", Message = $"Test variant with id : {id} not found!" });
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] TestVariantModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTestVariant = await _testVariantService.Create(model);
            var routeValues = new { id = createdTestVariant.Id };
            return CreatedAtRoute(routeValues, createdTestVariant);
        }

        [Authorize("Admin")]
        [HttpPatch]
        [Route("{id:int:min(1)}/update")]
        public async Task<IActionResult> Update(int id, [FromForm] TestVariantModel model)
        {
            model.Id = id;
            var updatedTestVariant = await _testVariantService.Update(id, model);
            return Ok(updatedTestVariant);
        }
        [Authorize("Admin")]
        [HttpDelete]
        [Route("{id:int:min(1)}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _testVariantService.Delete(id);
            if (deleteResult)
            {
                return NoContent();
            }
            return NotFound(new ResponseModel() { Status = "Error", Message = $"Test variant with id : {id} not found! OR This TestVariatn have existing Questions, first delete all questions than retry!!!" });
        }
    }
}
