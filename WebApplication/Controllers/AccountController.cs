using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Configurations;
using WebApplication.ViewModels.Inputs.Account;

namespace WebApplication.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        public AccountController(UserManager<User> userManager, SignInManager<User> signManager, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            _userManager = userManager;
            _signManager = signManager;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email.Trim().ToLower());

            if (user is null)
                return BadRequest(new { Message = "Usuário não encontrado em nossa base." });

            var loginResult = await _signManager.CheckPasswordSignInAsync(user, viewModel.Password, false);

            if (loginResult.Succeeded)
            {
                //Gerando a identidade do usuário
                var identity = new ClaimsIdentity(
                    new GenericIdentity(viewModel.Email, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    }
                );

                //Calculando quando o token irá expirar baseado na data de criação dele
                var createdDate = DateTime.Now;
                var expirationDate = createdDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                //Configurando como o token deve ser gerado
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = createdDate,
                    Expires = expirationDate
                });

                var accessToken = handler.WriteToken(securityToken);

                return Ok(new
                {
                    created = createdDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken
                });
            }
            else
            {
                return BadRequest(new { Message = "Usuário/Senha não estão coincidem." });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountViewModel viewModel)
        {
            var user = new User
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                IsEnabled = true,
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
                return Ok("Conta cadastrada com êxito");
            else
                return BadRequest(result.Errors);
        }
    }
}