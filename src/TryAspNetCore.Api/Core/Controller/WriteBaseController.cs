using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;
using TryAspNetCore.Api.Core.Repositories;

namespace TryAspNetCore.Core
{
    public abstract class WriteBaseController<TContext, T, TDto> : ReadBaseController<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        private readonly IMapper _mapper;
        private readonly IWriteRepository<TContext, T> _writeRepository;

        public WriteBaseController(IWriteRepository<TContext, T> writeRepository, ILoggerFactory loggerFactory, IMapper mapper)
            : base(writeRepository, loggerFactory, mapper)
        {
            _mapper = mapper;
            _writeRepository = writeRepository;
        }

        [HttpPost]
        public ActionResult<T> Post([FromBody]TDto record)
        {
            _logger.LogInformation("Post log");
            var entity = _mapper.Map<T>(record);
            entity.Id = Guid.NewGuid();
            _writeRepository.Add(entity);

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }
    }
}