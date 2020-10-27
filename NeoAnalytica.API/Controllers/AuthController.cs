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
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Infrastructure;
using NeoAnalytica.Infrastructure.DTOs;
using NeoAnalytica.Infrastructure.Interfaces;

namespace NeoAnalytica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IEmailService _emailService;
        public AuthController(
            IAuthService authService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Register([FromBody] UserCredentials credentials)
        {
            //validate request 
            credentials.Email = credentials.Email.ToLower();

            if (await _authService.UserExists(credentials.Email))
            {
                return BadRequest("Username already exists!");
            }

            var user = new ApplicationUser { UserName = credentials.Email, Email = credentials.Email };
            var result = await _userManager.CreateAsync(user, credentials.Password);
            if(result.Succeeded)
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token = emailToken, email = user.Email }, Request.Scheme);
                string content = $"To confirm your registration please click on the following link {confirmationLink}";
                await _emailService.SendEmail(new Message(new List<string> { credentials.Email }, "Welcome to NeoAnalytica", content, null));
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Authenticates a user with user  credentials
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);

            if (result == null)
                return Unauthorized();

            if (result.Succeeded)
            {
                var userId = await _authService.GetAndUpdateUserLoginInfoAsync(credentials.Email, DateTime.UtcNow);
                string token = _authService.CreateToken(credentials, userId);
                return Ok(new
                {
                    Email = credentials.Email,
                    Token = token
                });
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpGet]
        public async Task<string> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return "Error";
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
                return $"Thank you for confirming email";
            return result.Errors.FirstOrDefault().Code.ToString();
        }


        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _authService.GetUserAsync(username);
            if(user != null)
            {
                return Ok(user);
            }

            return NoContent();
            
        }
    }

}
