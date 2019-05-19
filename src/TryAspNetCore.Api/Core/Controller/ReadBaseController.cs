using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;
using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.Api.Core
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
        public async Task<ActionResult<PagedResult>> Get([FromQuery]int start = 0, [FromQuery]int count = 20)
        {
            _logger.LogInformation("Get log");
            var totalCount = _readRepository.FindBy(e => true)
                .CountAsync();
            var entities = await _readRepository.FindBy(e => true)
                .Skip(start)
                .Take(count)
                .ToListAsync();

            var result = new PagedResult
            {
                Items = _mapper.Map<List<TDto>>(entities),
                TotalCount = totalCount.Result
            };
            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> Get(Guid id)
        {
            _logger.LogInformation("GetById log");
            var entity = await _readRepository.GetByIdAsync(id);
            if (entity == null)
                return NotFound(null);
            var result = _mapper.Map<TDto>(entity);
            return Ok(result);
        }
    }
}