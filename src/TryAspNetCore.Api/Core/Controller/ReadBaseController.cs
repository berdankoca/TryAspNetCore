using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;
using TryAspNetCore.Api.Core.Repositories;

namespace TryAspNetCore.Core
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public abstract class ReadBaseController<TContext, T> : ControllerBase
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        protected readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IReadRepository<TContext, T> _readRepository;

        public ReadBaseController(IReadRepository<TContext, T> readRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _mapper = mapper;
            _readRepository = readRepository;
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
        }

        [HttpGet]
        public ActionResult<List<T>> Get()
        {
            //We have to implement paged result

            _logger.LogInformation("Get log");
            var events = _readRepository.FindBy(e => true);
            return new JsonResult(events);
        }

        [HttpGet("{id}")]
        public ActionResult<T> Get(Guid id)
        {
            _logger.LogInformation("GetById log");
            var entity = _readRepository.Get(id);
            if (entity == null)
                return NotFound(null);

            return Ok(entity);
        }
    }
}