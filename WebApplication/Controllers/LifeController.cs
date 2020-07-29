using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApplication.ViewModels.Inputs.Life;

namespace WebApplication.Controllers
{
    [Route("api/lifes")]
    [Authorize("Bearer")]
    public class LifeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILifeService _lifeService;

        public LifeController(UserManager<User> userManager, ILifeService lifeService)
        {
            _userManager = userManager;
            _lifeService = lifeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLifeViewModel viewModel)
        {
            var life = new Life
            {
                Name = viewModel.Name,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Birthday = viewModel.Birthday
            };

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            life.IntegradorId = user.Id;

            try
            {
                await _lifeService.Save(life);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}