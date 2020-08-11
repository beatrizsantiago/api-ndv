using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApplication.ViewModels.Inputs.Life;
using WebApplication.ViewModels.Output.Life;

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
                Birthday = viewModel.Birthday,
                IsBaptismOhterChurch = viewModel.IsBaptismOhterChurch,
                MinisterBaptism = viewModel.MinisterBaptism
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

        [HttpGet]
        public async Task<IActionResult> List(int pageIndex = 1, int pageLimit = 10)
        {
            var lifes = await _lifeService.GetAs<PreviewLifeViewModel>(pageIndex, pageLimit);
            return Ok(lifes);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLifeViewModel viewModel)
        {
            var life = await _lifeService.FindById(viewModel.Id);

            life.Name = viewModel.Name;
            life.Phone = viewModel.Phone;
            life.Email = viewModel.Email;
            life.Birthday = viewModel.Birthday;
            life.IsBaptismOhterChurch = viewModel.IsBaptismOhterChurch;
            life.MinisterBaptism = viewModel.MinisterBaptism;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var life = await _lifeService.FindById(id);
            try
            {
                await _lifeService.Delete(life);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}