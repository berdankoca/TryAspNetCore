using System;
using System.Collections.Generic;
using TryAspNetCore.Api.Core.Context;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Domain;
using TryAspNetCore.Api.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TryAspNetCore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IWriteRepository<EventContext, Event> _repository;
        public EventsController(ILogger<EventsController> logger, IWriteRepository<EventContext, Event> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Event>> Get()
        {
            _logger.LogInformation("Event Get log, 1");
            Serilog.Log.Information("Event Get log, 2");
            var events = _repository.FindBy(e => true);
            return new JsonResult(events);
        }

        [HttpGet("{id}")]
        public ActionResult<Event> Get(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
                return NotFound(null);

            return Ok(entity);
        }

        [HttpPost]
        public ActionResult<Event> Post([FromBody]RegisterEventDto eventInfo)
        {
            var entity = new Event
            {
                Id = Guid.NewGuid(),
                Title = eventInfo.Title
            };
            _repository.Add(entity);

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPost("{id}/RegisterMe")]
        public IActionResult RegisterMe(Guid id)
        {
            return new JsonResult("RegisterMe");
        }
    }
}
