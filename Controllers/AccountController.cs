using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TasksApi.DTOs;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO user)
        {
            try
            {
                IdentityUser newUser = new IdentityUser
                {
                    Email = user.Email,
                  
                    UserName = user.Email
                };
                var result = await userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    // add any roles or claims to user 
                    return Ok($"The user [{user.Email} is registerd]");
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginuser)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == loginuser.Email.ToLower());
            if (user is null)
            {
                return Unauthorized("invalid User Name");

            }
            var signUser = await signInManager.CheckPasswordSignInAsync(user, loginuser.Password, false);
            if (!signUser.Succeeded)
            {
                return Unauthorized("Invalid user name or password");
            }
            return Ok(new { Email = loginuser.Email, Token = GenerateToken(user) });
        }




        private string GenerateToken(IdentityUser user)
        {
            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
        
            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(config["JWT:skey"])
                );
            // بدي اعمل ساينيغ للبيانات من خلال الكي والالوجريثم
            var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = signCred,
                Issuer = config["JWT:iss"],
                Audience = config["JWT:aud"],
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(Claims)
                // ingo aboout user how loged cliam that clerfiy the user we can store his email the user type
            };
            // what it will memo in payload in token
            var TokenHadler = new JwtSecurityTokenHandler();
            var token = TokenHadler.CreateToken(tokenDiscriptor);

            return TokenHadler.WriteToken(token);
        }
    }
}
