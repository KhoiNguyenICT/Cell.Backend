using System;
using Cell.Common.SeedWork;
using Cell.Helpers.Models;
using Cell.Model;
using Cell.Model.Entities.DynamicEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class DynamicController : CellController<Entity>
    {
        private readonly IDynamicService _dynamicService;

        public DynamicController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<Entity> entityValidator,
            IDynamicService dynamicService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _dynamicService = dynamicService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(DynamicSearchModel model)
        {
            var result = await _dynamicService.SearchAsync(model);
            return Ok(result);
        }

        [HttpPost("{tableId}/{id}")]
        public async Task<IActionResult> GetById(Guid tableId, Guid id)
        {
            var result = await _dynamicService.GetSingleAsync(tableId, id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(WriteModel model)
        {
            await _dynamicService.InsertAsync(model);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WriteModel model)
        {
            await _dynamicService.UpdateAsync(model);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Guid tableId, Guid id)
        {
            await _dynamicService.DeleteAsync(tableId, id);
            return Ok();
        }
    }
}