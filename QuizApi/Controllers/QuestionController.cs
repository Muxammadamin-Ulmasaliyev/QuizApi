using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Services;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _questionService.GetAll());
        }



        [Authorize]
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var question = await _questionService.Get(id);
            if (question != null)
            {
                return Ok(question);
            }
            return NotFound(new ResponseModel() { Status = "Error", Message = $"Question with id : {id} not found!" });
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdQuestion = await _questionService.Create(model);
            var routeValues = new { id = createdQuestion.Id };
            return CreatedAtRoute(routeValues, createdQuestion);
        }

        [Authorize("Admin")]
        [HttpPatch]
        [Route("{id:int:min(1)}/update")]
        public async Task<IActionResult> Update(int id, [FromForm] QuestionModel model)
        {
            model.Id = id;
            var updatedQuestion = await _questionService.Update(id, model);
            return Ok(updatedQuestion);
        }

        [Authorize("Admin")]
        [HttpDelete]
        [Route("{id:int:min(1)}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _questionService.Delete(id);
            if (deleteResult)
            {
                return NoContent();
            }
            return NotFound(new ResponseModel() { Status = "Error", Message = $"Question with id : {id} not found!" });
        }
    }
}
