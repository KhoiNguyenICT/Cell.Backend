using Cell.Helpers.Models;
using Cell.Model.Entities.DynamicEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicController : ControllerBase
    {
        private readonly IDynamicService _dynamicService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"]);

        public DynamicController(
            IDynamicService dynamicService,
            IHttpContextAccessor httpContextAccessor)
        {
            _dynamicService = dynamicService;
            _httpContextAccessor = httpContextAccessor;
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
            model.Data.Add("CREATED_BY", CurrentAccountId);
            await _dynamicService.InsertAsync(model);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(WriteModel model)
        {
            model.Data.Add("MODIFIED_BY", CurrentAccountId);
            await _dynamicService.UpdateAsync(model);
            return Ok();
        }

        [HttpPost("delete/{tableId}/{id}")]
        public async Task<IActionResult> Delete(Guid tableId, Guid id)
        {
            await _dynamicService.DeleteAsync(tableId, id);
            return Ok();
        }
    }
}