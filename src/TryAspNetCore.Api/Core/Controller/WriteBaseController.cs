using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;
using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.Api.Core
{
    public abstract class WriteBaseController<TContext, T, TDto> : ReadBaseController<TContext, T, TDto>
        where TContext : BaseContext
        where T : BaseEntity, new()
        where TDto : BaseEntity, new()
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
        public ActionResult<TDto> Post([FromBody]TDto record)
        {
            _logger.LogInformation("Post log");
            var entity = _mapper.Map<T>(record);
            entity.Id = Guid.NewGuid();
            _writeRepository.Add(entity);

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public ActionResult<TDto> Put(Guid id, [FromBody]TDto record)
        {
            _logger.LogInformation("Put log");
            var entity = _writeRepository.Get(id);
            if (entity == null)
                return NotFound();

            _mapper.Map(record, entity);
            _writeRepository.Update(entity);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _logger.LogInformation("Delete log");
            var entity = _writeRepository.Get(id);
            if (entity == null)
                return NotFound();

            _writeRepository.Delete(entity);

            return Ok();
        }
    }
}