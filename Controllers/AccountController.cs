using HanamiAPI.Dtos;
using HanamiAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HanamiAPI.Controllers
{
    [Route("auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<PessoaComAcesso> _userManager;
        private readonly SignInManager<PessoaComAcesso> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<PessoaComAcesso> userManager, SignInManager<PessoaComAcesso> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new PessoaComAcesso { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                return Ok(new { token });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await GenerateJwtToken(user);
                    return Ok(new { token });
                }

                ModelState.AddModelError(string.Empty, "Usuário Inválido.");
                return BadRequest(ModelState);
            }

            if (result.IsLockedOut)
            {
                return Forbid();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.UserName
            });
        }

        private async Task<string> GenerateJwtToken(PessoaComAcesso user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var secretKey = _configuration["Jwt:SecretKey"];
            if (secretKey == null)
            {
                throw new InvalidOperationException("Jwt:SecretKey não está configurado no arquivo de configuração.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.UserName))
            {
                tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            }

            if (!string.IsNullOrEmpty(user.Email))
            {
                tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                tokenClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: tokenClaims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
