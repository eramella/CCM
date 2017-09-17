using AutoMapper;
using CCM.Business.Repositories;
using CCM.Business.Utilities;
using CCM.Data.Models;
using CCM.Data.Enums;
using CCM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using CCM.Data.Contracts;

namespace CCM.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Sessions")]
    public class SessionsController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<SessionsController> _logger;
        private IUserResolverService _userResolver;

        public SessionsController(CCMContext context, IMapper mapper, ILogger<SessionsController> logger, IUserResolverService userResolver)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userResolver = userResolver;
        }

        [HttpGet]
        public async Task<List<SessionVm>> GetSessions(int campId)
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var list = await uow.SessionRepository.GetSessions(campId);
            return _mapper.Map<List<Session>,List<SessionVm>>(list);
        }

        [HttpGet]
        [Route("GetLevels")]
        public List<KeyValuePair<string,int>> GetSessionLevels()
        {
            var list = EnumUtilities.ListTheEnum<SessionLevel>();
            return list.ToList();
        }

        [HttpGet]
        [Route("Tags")]
        public async Task<List<TagVm>> GetTags()
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var tags = await uow.TagRepository.GetTags();
            return _mapper.Map<List<Tag>,List<TagVm>>(tags);
        }

        [HttpGet("{id}", Name = "SessionGet")]
        public async Task<IActionResult> GetSession(string id)
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var session = await uow.SessionRepository.GetSessionById(id);
            if (session == null)
            {
                return NotFound($"Could not find Session of id: {id}");
            }
            return new ObjectResult(_mapper.Map<Session, SessionVm>(session));
        }

        [HttpPost]
        public async Task<IActionResult> PostSession([FromBody]SessionVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork uow = new UnitOfWork(_context);
                    string sessionId = Guid.NewGuid().ToString();
                    var session = new Session()
                    {
                        Id = sessionId,
                        CampId = model.CampId,
                        Description = model.Description,
                        Level = EnumUtilities.EnumFromInt<SessionLevel>(model.Level),
                        Title = model.Title,
                        TagSessions = await uow.TagRepository.GenerateTagSessions(model.Tags, sessionId),
                        UserId = _userResolver.GetUserId()
                    };
                    uow.SessionRepository.AddSession(session);
                    if (await uow.SaveAsync())
                    {
                        var newUri = Url.Link("SessionGet", new { id = session.Id });
                        return Created(newUri, _mapper.Map<Session, SessionVm>(session));
                    }
                }
                catch (Exception x)
                {
                    _logger.LogError($"Throw exception while creating a Session: {x}");
                }
            }
            return BadRequest("Could not create a Camp");
        }
    }
}