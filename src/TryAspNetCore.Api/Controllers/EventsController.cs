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
using AutoMapper;

namespace TryAspNetCore.Api.Controllers
{
    public class EventsController : WriteBaseController<EventContext, Event, EventDto>
    {
        private readonly IWriteRepository<EventContext, Event> _repository;
        public EventsController(IWriteRepository<EventContext, Event> repository, ILoggerFactory loggerFactory, IMapper mapper)
            : base(repository, loggerFactory, mapper)
        {
            _repository = repository;
        }

        [HttpPost("{id}/RegisterMe")]
        public IActionResult RegisterMe(Guid id)
        {
            return new JsonResult("RegisterMe");
        }
    }
}
