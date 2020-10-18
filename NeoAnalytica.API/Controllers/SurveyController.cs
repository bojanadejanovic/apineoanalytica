using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NeoAnalytica.API.Filters;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Infrastructure;
using NeoAnalytica.Infrastructure.DTOs;

namespace NeoAnalytica.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {

        private ISurveyService _surveyService;
        private IAuthService _authService;
        private IHttpContextAccessor _httpContextAccessor;

        public SurveyController(
            ISurveyService surveyService,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor)
        {
            _surveyService = surveyService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Retrieves all surveys for authenticated user
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(CheckToken))]
        [ProducesResponseType(typeof(IEnumerable<SurveyEntity>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<SurveyEntity>> GetAllSurveys(int pageNumber, int pageSize = 10)
        {
            var page = new Pager(pageNumber, pageSize);
            var userId = (int)_httpContextAccessor.HttpContext.Items["userId"];
            var ret = await _surveyService.GetAllSurveys(page, userId);
            return ret;
        }

        /// <summary>
        ///  Creates new survey
        /// </summary>
        /// <param name="newSurvey"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateNewSurvey([FromBody] SurveyRequest newSurvey)
        {
            // TODO: move this to validation
            if (string.IsNullOrEmpty(newSurvey.SurveyName))
            {
                return BadRequest("Suvey name cannot be empty!");
            }
            var author = await _authService.GetUserAsync(newSurvey.Username);
            if (author == null)
            {
                return BadRequest("User not found!");
            }

            SurveyEntity entity = new SurveyEntity() { Name = newSurvey.SurveyName, Description = newSurvey.SurveyDescription, SurveyCategoryId = newSurvey.SurveyCategoryID, UserId = author.UserId };
            var result = await _surveyService.CreateNewSurvey(entity);
            return new JsonResult(result);
        }

        /// <summary>
        /// Retrieves survey entity based on surveyId supplied
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId)
        {
            var survey = await _surveyService.GetSurveyById(surveyId);
            if (survey != null)
            {
                return Ok(survey);
            }

            return NoContent();
        }

        /// <summary>
        /// Retrieves all survey categories
        /// </summary>
        /// <returns></returns>
        [HttpGet("categories/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSurveyCategories()
        {
            var categories = await _surveyService.GetAllSurveyCategories();
            if (categories.Any())
            {
                return Ok(categories);
            }

            return NoContent();
        }

        [HttpPost("question/add")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddQuestions([FromBody] QuestionRequest newQuestions)
        {
            // TODO: move this to validation
            var survey = _surveyService.GetSurveyById(newQuestions.SurveyId);
            if(survey == null)
            {
                return BadRequest("Survey not found!");
            }

            await _surveyService.AddQuestionsToSurvey(newQuestions);
            return Ok();
        }


    }
}
