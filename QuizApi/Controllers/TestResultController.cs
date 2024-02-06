using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApi.Domain;
using QuizApi.Domain.Entities;
using QuizApi.MapProfiles;
using QuizApi.Models;

namespace QuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;

        public TestResultController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var testResultEntities = await _appDbContext.TestResults.Include(tr => tr.AppUser).Include(tr => tr.TestVariant).OrderBy(tr => tr.TotalScore).ToListAsync();
            var testResultModels = new List<TestResultModel>();
            foreach (var entity in testResultEntities)
            {
                testResultModels.Add(Mapper.Map(entity));
            }
            return Ok(testResultModels);
        }

        [Authorize]
        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> GetAllForUser()
        {
            var user = await GetCurrentUser();
            var testResultEntities = await _appDbContext.TestResults.OrderBy(tr => tr.TotalScore).Where(tr => tr.UserId == user.Id).ToListAsync();
            var testResultModels = new List<TestResultModel>();
            foreach (var entity in testResultEntities)
            {
                testResultModels.Add(Mapper.Map(entity));
            }
            return Ok(testResultModels);
        }

        [Authorize]
        [HttpGet]
        [Route("testVariant/{testVariantId:int:min(1)}/get-all")]
        public async Task<IActionResult> GetAll(int testVariantId)
        {
            var testResultEntities = await _appDbContext.TestResults.OrderBy(tr => tr.TotalScore).Where(tr => tr.TestVariantId == testVariantId).ToListAsync();
            var testResultModels = new List<TestResultModel>();
            foreach (var entity in testResultEntities)
            {
                testResultModels.Add(Mapper.Map(entity));
            }
            return Ok(testResultModels);
        }

        [Authorize("Admin")]
        [HttpGet]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var testResultEntity = await _appDbContext.TestResults.FindAsync(id);
            if (testResultEntity != null)
            {

                return Ok(Mapper.Map(testResultEntity));
            }
            return NotFound(new ResponseModel() { Status = "Error", Message = $"Test result with id : {id} not found!" });
        }

        [Authorize]
        [HttpPost]
        [Route("save-result")]
        public async Task<IActionResult> SaveResult([FromForm] TestResultModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await GetCurrentUser();

            var testVariant = await _appDbContext.TestVariants.FindAsync(model.TestVariantId);
           // model.UserId = user.Id;
            model.AppUser = user;
            model.TestVariantName = testVariant.Name;
           
            var testResultEntity = Mapper.Map(model);
            await _appDbContext.TestResults.AddAsync(testResultEntity);
            await _appDbContext.SaveChangesAsync();
            var createdTestResult = Mapper.Map(testResultEntity);
            var routeValues = new { id = createdTestResult.Id };
            return CreatedAtRoute(routeValues, createdTestResult);
        }

        [Authorize("Admin")]
        [HttpDelete]
        [Route("{id:int:min(1)}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemToDelete = await _appDbContext.TestResults.FindAsync(id);
            if(itemToDelete == null)
            {
                return NotFound(new ResponseModel() { Status = "Error", Message = $"Test result with id : {id} not found!" });
            }
            var deleteResult = _appDbContext.TestResults.Remove(itemToDelete);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
        [NonAction]
        public async Task<AppUser> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return user;
        }
    }
}
