using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity, TModelCreate, TModelUpdate, TService> : ControllerBase
        where TEntity : Entity
        where TService : IService<TEntity>
    {
        private readonly TService _service;

        public CellController(TService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        protected virtual async Task<IActionResult> Create(TModelCreate model)
        {
            var entity = model.To<TEntity>();
            var result = await _service.AddAsync(entity);
            await _service.CommitAsync();
            return Ok(result);
        }

        [HttpPost("update")]
        protected virtual async Task<IActionResult> Update(TModelUpdate model)
        {
            var entity = model.To<TEntity>();
            _service.Update(entity);
            await _service.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        protected virtual async Task<IActionResult> Delete(Guid id)
        {
            _service.Delete(id);
            await _service.CommitAsync();
            return Ok();
        }
    }
}