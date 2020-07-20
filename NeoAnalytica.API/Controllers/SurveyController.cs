using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeoAnalytica.AppCore.Entities;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Infrastructure;
using NeoAnalytica.Infrastructure.DTOs;

namespace NeoAnalytica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {

        private ISurveyService _surveyService;
        private IAuthService _authService;

        public SurveyController(
            ISurveyService surveyService,
            IAuthService authService)
        {
            _surveyService = surveyService;
            _authService = authService;
        }

        /// <summary>
        /// Creates new survey
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
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

            Survey entity = new Survey() { Name = newSurvey.SurveyName, Description = newSurvey.SurveyDescription, SurveyCategoryId = newSurvey.SurveyCategoryID, UserId = author.UserId };
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
        public async Task<IActionResult> GetAllSurveyCategories()
        {
            var categories = await _surveyService.GetAllSurveyCategories();
            if (categories.Any())
            {
                return Ok(categories);
            }

            return NoContent();
        }


    }
}
