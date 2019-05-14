using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TryAspNetCore.Core;
using TryAspNetCore.Api.Core;
using TryAspNetCore.EventManagement;
using TryAspNetCore.EntityFrameworkCore.Context;
using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.Api.Controllers
{
    public class EventsController : WriteBaseController<EventContext, Event, EventDto>
    {
        private readonly IEventRepository _repository;
        public EventsController(IEventRepository repository, ILoggerFactory loggerFactory, IMapper mapper)
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
