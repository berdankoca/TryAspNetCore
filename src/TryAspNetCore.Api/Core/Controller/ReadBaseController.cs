using System;
using System.Collections.Generic;
using System.Linq;
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
    public abstract class ReadBaseController<TContext, T, TDto> : ControllerBase
        where TContext : BaseContext
        where T : BaseEntity, new()
        where TDto : BaseEntity, new()
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
        public ActionResult<PagedResult> Get([FromQuery]int start = 0, [FromQuery]int count = 20)
        {
            _logger.LogInformation("Get log");
            //We have to implement async pattern
            var totalCount = _readRepository.FindBy(e => true).Count();
            var entities = _readRepository.FindBy(e => true)
                .Skip(start)
                .Take(count);

            var result = new PagedResult
            {
                Items = _mapper.Map<List<TDto>>(entities),
                TotalCount = totalCount
            };
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public ActionResult<TDto> Get(Guid id)
        {
            _logger.LogInformation("GetById log");
            var entity = _readRepository.Get(id);
            if (entity == null)
                return NotFound(null);
            var result = _mapper.Map<TDto>(entity);
            return Ok(result);
        }
    }
}