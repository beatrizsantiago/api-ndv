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
using WebApplication.ViewModels.Inputs.Integration;
using WebApplication.ViewModels.Output.Integration;

namespace WebApplication.Controllers
{
    [Route("api/integration")]
    [Authorize("Bearer")]
    public class LifeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILifeService _lifeService;
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public LifeController(UserManager<User> userManager, ILifeService lifeService, IFeedbackService feedbackService, IMapper mapper)
        {
            _userManager = userManager;
            _lifeService = lifeService;
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpPost("lifes")]
        public async Task<IActionResult> CreateLife([FromBody] CreateLifeViewModel viewModel)
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

        [HttpGet("lifes")]
        public async Task<IActionResult> ListLifes(int pageIndex = 1, int pageLimit = 10)
        {
            var lifes = await _lifeService.Get(pageIndex, pageLimit);
            var mappedLifes = _mapper.Map<List<PreviewLifeViewModel>>(lifes);
            
            return Ok(mappedLifes);
        }

        [HttpGet("lifes/{id}")]
        public async Task<IActionResult> DetailsLife(long id)
        {
            var lifes = await _lifeService.FindByIdAs<DetailsLifeViewModel>(id);
            return Ok(lifes);
        }

        [HttpPut("lifes/steps")]
        public async Task<IActionResult> AddStepLife([FromBody] AddStepLifeViewModel viewModel)
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

        [HttpPut("lifes")]
        public async Task<IActionResult> UpdateLife([FromBody] UpdateLifeViewModel viewModel)
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

        [HttpDelete("lifes/{id}")]
        public async Task<IActionResult> DeleteLife(long id)
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

        [HttpPost("lifes/lost/{id}")]
        public async Task<IActionResult> LostLife(long id)
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

        [HttpPost("lifes/feedbacks")]
        public async Task<IActionResult> NewFeedbackLife([FromBody] CreateFeedbackViewModel viewModel)
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

        [HttpGet("lifes/report")]
        public async Task<IActionResult> ReportLife(DateTime? initDate, DateTime? endDate)
        {
            var report = await _lifeService.ReportStepsByPeriod(initDate??DateTime.Now.AddMonths(-1), endDate??DateTime.Now);
            return Ok(report);
        }

        [HttpPost("visitants")]
        public async Task<IActionResult> CreateVisitant([FromBody] CreateVisitantViewModel viewModel)
        {
            var visitant = new Life
            {
                FullName = viewModel.FullName,
                Phone = viewModel.Phone,
                FrequentOtherChurch = viewModel.FrequentOtherChurch,
                Companion = viewModel.Companion
            };

            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            visitant.IntegratorId = user.Id;

            visitant.Steps.Add(new ProgressStepsLife
            {
                Step = StepsPropheticWay.Visitor,
                DoneDate = DateTime.Now,
            });

            try
            {
                await _lifeService.Save(visitant);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("visitants")]
        public async Task<IActionResult> ListVisitants(int pageIndex = 1, int pageLimit = 10)
        {
            var visitants = await _lifeService.GetAs<PreviewVisitantViewModel>(pageIndex, pageLimit);
            
            return Ok(visitants);
        }
    }
}