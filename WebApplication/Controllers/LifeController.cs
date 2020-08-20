using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using WebApplication.ViewModels.Inputs.CreateFeedback;
using WebApplication.ViewModels.Inputs.Life;
using WebApplication.ViewModels.Inputs.Visitant;
using WebApplication.ViewModels.Output.Life;
using WebApplication.ViewModels.Output.Visitant;

namespace WebApplication.Controllers
{
    [Route("api/lifes")]
    [Authorize("Bearer")]
    public class LifeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILifeService _lifeService;
        private readonly IFeedbackService _feedbackService;
        private readonly IVisitantService _visitantService;
        private readonly IMapper _mapper;

        public LifeController(UserManager<User> userManager, ILifeService lifeService, IFeedbackService feedbackService, IVisitantService visitantService, IMapper mapper)
        {
            _userManager = userManager;
            _lifeService = lifeService;
            _feedbackService = feedbackService;
            _visitantService = visitantService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLifeViewModel viewModel)
        {
            var life = new Life
            {
                FullName = viewModel.FullName,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                Age = viewModel.Age,
                BaptismOtherChurch = viewModel.BaptismOtherChurch,
                BaptismMinister = viewModel.BaptismMinister
            };

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            life.IntegratorId = user.Id;

            life.Steps.Add(new ProgressStepsLife
            {
                Step = StepsPropheticWay.Visitor,
                DoneDate = DateTime.Now,
            });

            life.Steps.Add(new ProgressStepsLife
            {
                Step = viewModel.TypeConversion,
                DoneDate = DateTime.Now,
            });

            if (viewModel.BaptismToday)
            {
                life.Steps.Add(new ProgressStepsLife
                {
                    Step = StepsPropheticWay.Baptism,
                    DoneDate = DateTime.Now,
                });
            }

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
            var lifes = await _lifeService.Get(pageIndex, pageLimit);
            var mappedLifes = _mapper.Map<List<PreviewLifeViewModel>>(lifes);
            
            return Ok(mappedLifes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var lifes = await _lifeService.FindByIdAs<DetailsLifeViewModel>(id);
            return Ok(lifes);
        }

        [HttpPut("add-life-step")]
        public async Task<IActionResult> AddLifeStep([FromBody] AddStepLifeViewModel viewModel)
        {
            var life = await _lifeService.FindById(viewModel.LifeId);
            life.Steps.Add(new ProgressStepsLife{
                CreatedDate = DateTime.Now,
                Step = viewModel.Step,
                DoneDate = viewModel.Date
            });

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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLifeViewModel viewModel)
        {
            var life = await _lifeService.FindById(viewModel.Id);

            life.FullName = viewModel.FullName;
            life.Phone = viewModel.Phone;
            life.Email = viewModel.Email;
            life.Age = viewModel.Age;
            life.IntegratorId = viewModel.IntegratorId;

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

        [HttpPost("life-lost/{id}")]
        public async Task<IActionResult> LifeLost(long id)
        {
            var life = await _lifeService.FindById(id);
            life.IsLost = true;
            try
            {
                await _lifeService.Save(life);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("new-feedback")]
        public async Task<IActionResult> NewFeedback([FromBody] CreateFeedbackViewModel viewModel)
        {
            var feedback = new Feedback{
                LifeId = viewModel.LifeId,
                Content = viewModel.Content
            };

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            feedback.IntegratorId = user.Id;

            try
            {
                await _feedbackService.Save(feedback);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("create-visitant")]
        public async Task<IActionResult> CreateVisitant([FromBody] CreateVisitantViewModel viewModel)
        {
            var visitant = new Visitant
            {
                FullName = viewModel.FullName,
                Phone = viewModel.Phone,
                FrequentOtherChurch = viewModel.FrequentOtherChurch,
                Companion = viewModel.Companion
            };

            try
            {
                await _visitantService.Save(visitant);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("list-visitants")]
        public async Task<IActionResult> ListVisitants(int pageIndex = 1, int pageLimit = 10)
        {
            var visitants = await _visitantService.GetAs<PreviewVisitantViewModel>(pageIndex, pageLimit);
            
            return Ok(visitants);
        }
    }
}