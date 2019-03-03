using System;
using System.Collections.Generic;
using TryAspNetCore.Api.Core.Context;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Domain;
using TryAspNetCore.Api.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TryAspNetCore.Core;

namespace TryAspNetCore.Api.Controllers
{
    public class EventsController : WriteBaseController<EventContext, Event, EventDto>
    {
        private readonly IWriteRepository<EventContext, Event> _repository;
        public EventsController(IWriteRepository<EventContext, Event> repository, ILoggerFactory loggerFactory)
            : base(repository, loggerFactory)
        {
            _repository = repository;
        }

        // [HttpGet]
        // public ActionResult<List<Event>> Get()
        // {
        //     _logger.LogInformation("Event Get log, 1");
        //     Serilog.Log.Information("Event Get log, 2");
        //     var events = _repository.FindBy(e => true);
        //     return new JsonResult(events);
        // }

        // [HttpGet("{id}")]
        // public ActionResult<Event> Get(Guid id)
        // {
        //     var entity = _repository.Get(id);
        //     if (entity == null)
        //         return NotFound(null);

        //     return Ok(entity);
        // }

        // [HttpPost]
        // public ActionResult<Event> Post([FromBody]RegisterEventDto eventInfo)
        // {
        //     var entity = new Event
        //     {
        //         Id = Guid.NewGuid(),
        //         Title = eventInfo.Title
        //     };
        //     _repository.Add(entity);

        //     return CreatedAtAction("Get", new { id = entity.Id }, entity);
        // }

        [HttpPost("{id}/RegisterMe")]
        public IActionResult RegisterMe(Guid id)
        {
            return new JsonResult("RegisterMe");
        }
    }
}
