using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCM.ViewModels;
using CCM.Data.Enums;
using CCM.Business.Repositories;
using CCM.Data.Models;
using AutoMapper;
using CCM.Business.Utilities;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace CCM.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Camp")]
    public class CampController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<CampController> _logger;

        public CampController(CCMContext context, IMapper mapper, ILogger<CampController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        [Route("/api/Camps/{state?}")]
        public async Task<IEnumerable<CampVm>> GetCamps(CampState? state)
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var camps = await uow.CampRepository.GetCamps(state);
            return _mapper.Map<IEnumerable<CampVm>>(camps);
        }

        [HttpGet]
        [Route("{id}", Name = "CampGet")]
        public async Task<IActionResult> GetCamp(int id)
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var camp = await uow.CampRepository.GetCampById(id);
            if (camp == null)
            {
                return NotFound($"Could not find Camp of id: {id}");
            }
            return new ObjectResult(_mapper.Map<CampVm>(camp));
        }

        [HttpPost]
        public async Task<IActionResult> PostCamp([FromBody]CampVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.State = EnumUtilities.GetEnumDescription(CampState.Upcoming);
                    UnitOfWork uow = new UnitOfWork(_context);
                    var camp = _mapper.Map<CampVm, Camp>(model);
                    uow.CampRepository.AddCamp(camp);
                    if (await uow.SaveAsync())
                    {
                        var newUri = Url.Link("CampGet", new { id = camp.Id });
                        return Created(newUri, _mapper.Map<Camp, CampVm>(camp));
                    }

                }
                catch (Exception x)
                {
                    _logger.LogError($"Throw exeption while creating a Camp: {x}");
                }
            }
            return BadRequest("Could not create Camp");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutCamp(int id, [FromBody]CampVm model)
        {
            if (ModelState.IsValid && id == model.Id)
            {
                try
                {
                    UnitOfWork uow = new UnitOfWork(_context);
                    var existingCamp = await uow.CampRepository.GetCampById(id);
                    if (existingCamp == null)
                    {
                        return NotFound($"Could not find Camp of id: {id}");
                    }

                    _mapper.Map(model, existingCamp);

                    if (await uow.SaveAsync())
                    {
                        return Ok();
                    }
                    else
                    {
                        _logger.LogWarning("Could not update Camp");
                    }
                }
                catch (Exception x)
                {
                    _logger.LogError($"Throw exeption while updating Camp: {x}");
                }
            }
            return BadRequest("Could not update Camp");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCamp(int id)
        {
            try
            {
                UnitOfWork uow = new UnitOfWork(_context);
                var existingCamp = await uow.CampRepository.GetCampById(id);
                if (existingCamp == null)
                {
                    return NotFound($"Could not find Camp of id: {id}");
                }
                uow.CampRepository.DeleteCamp(existingCamp);

                if (await uow.SaveAsync())
                {
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("Could not delete Camp");
                }

            }
            catch (Exception x)
            {
                _logger.LogError($"Throw exeption while deleting Camp: {x}");
            }

            return BadRequest("Could not delete the Camp!");
        }

        [HttpGet]
        [Route("/CampStates")]
        public List<KeyValuePair<string, int>> GetCampStates()
        {
            var list = EnumUtilities.ListTheEnum<CampState>();
            return list.ToList();
        }
    }
}