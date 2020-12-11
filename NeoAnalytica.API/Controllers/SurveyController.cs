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
using NeoAnalytica.Infrastructure.Interfaces;

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
        private IQuestionService _questionService;


        public SurveyController(
            ISurveyService surveyService,
            IAuthService authService,
            IQuestionService questionService,
            IHttpContextAccessor httpContextAccessor)
        {
            _surveyService = surveyService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _questionService = questionService;
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
        [ServiceFilter(typeof(CheckToken))]
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateNewSurvey([FromBody] SurveyRequest newSurvey)
        {
            // TODO: move this to validation
            if (string.IsNullOrEmpty(newSurvey.SurveyName))
            {
                return BadRequest("Suvey name cannot be empty!");
            }
            var userId = (int)_httpContextAccessor.HttpContext.Items["userId"];
            SurveyEntity entity = new SurveyEntity() { Name = newSurvey.SurveyName, Description = newSurvey.SurveyDescription, SurveyCategoryId = newSurvey.SurveyCategoryID, UserId = userId };
            var result = await _surveyService.InsertSurveyAsync(entity);
            return CreatedAtAction("GetSurvey", new { surveyId = result.SurveyId }, result);
        }

        /// <summary>
        /// Retrieves survey entity based on surveyId supplied
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [HttpGet("{surveyId}")]
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
        /// Deletes survey based on supplied surveyId
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [HttpDelete("{surveyId}")]
        public async Task<ActionResult> DeleteSurvey(int surveyId)
        {
            var survey = await _surveyService.GetSurveyById(surveyId);
            if (survey == null)
            {
                return NotFound();
            }
            await _surveyService.DeleteAsync(surveyId);
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

        [ServiceFilter(typeof(CheckToken))]
        [HttpPost("{surveyId}/questions")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionRequest newQuestionRequest, int surveyId)
        {
            var userId = (int)_httpContextAccessor.HttpContext.Items["userId"];
            var survey = await _surveyService.GetSurveyByIdAndUserId(surveyId, userId);
            if (survey == null)
            {
                return BadRequest($"Survey with ID : {newQuestionRequest.SurveyID} does not exist.");
            }
            var newQuestion = new QuestionEntity()
            {
                Text = newQuestionRequest.Text,
                SurveyID = newQuestionRequest.SurveyID,
                AnswerOptional = newQuestionRequest.AnswerOptional,
                QuestionTypeID = newQuestionRequest.QuestionТypeID,
                Answers = MapAnswers(newQuestionRequest.PossibleAnswers)
            };
            var result = await _questionService.InsertQuestionAsync(newQuestion);
            return CreatedAtAction("GetQuestion", new { questionId = result }, result);
        }

       

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            var question = await _questionService.GetQuestionById(questionId);
            if (question != null)
            {
                return Ok(question);
            }

            return NoContent();
        }

        [ServiceFilter(typeof(CheckToken))]
        [HttpPut("{surveyId}/questions")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateQuestion([FromBody] QuestionRequest updateQuestionRequest, int surveyId)
        {
            var userId = (int)_httpContextAccessor.HttpContext.Items["userId"];
            var survey = await _surveyService.GetSurveyByIdAndUserId(surveyId, userId);
            if (survey == null)
            {
                return BadRequest($"Survey with ID : {updateQuestionRequest.SurveyID} does not exist.");
            }
            var updateQuestion = new QuestionEntity()
            {
                Text = updateQuestionRequest.Text,
                AnswerOptional = updateQuestionRequest.AnswerOptional,
                QuestionTypeID = updateQuestionRequest.QuestionТypeID,
                Answers = MapAnswers(updateQuestionRequest.PossibleAnswers)
            };
            await _questionService.UpdateAsync(updateQuestion);
            return Ok();
        }



        #region helpers
        private List<AnswerEntity> MapAnswers(List<QuestionItem> possibleAnswers)
        {
            List<AnswerEntity> answerEntities = new List<AnswerEntity>();
            foreach (var answer in possibleAnswers)
            {
                answerEntities.Add(new AnswerEntity() { AnswerText = answer.Text });
            }
            return answerEntities;
        }
        #endregion
    }
}
