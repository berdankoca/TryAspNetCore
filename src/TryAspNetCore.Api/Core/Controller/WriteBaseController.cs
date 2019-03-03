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
        private readonly IWriteRepository<TContext, T> _writeRepository;

        public WriteBaseController(IWriteRepository<TContext, T> writeRepository, ILoggerFactory loggerFactory)
            : base(writeRepository, loggerFactory)
        {
            _writeRepository = writeRepository;
        }

        [HttpPost]
        public ActionResult<T> Post([FromBody]TDto entity)
        {
            _logger.LogInformation("Post log");
            //Use automapper
            // var entity = new Event
            // {
            //     Id = Guid.NewGuid(),
            //     Title = eventInfo.Title
            // };
            // _writeRepository.Add(entity);

            return CreatedAtAction("Get", new { id = 1 }, entity);
            // return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }
    }
}