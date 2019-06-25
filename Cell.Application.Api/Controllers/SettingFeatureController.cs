using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingFeatureController : CellController
    {
        private readonly ISettingFeatureRepository _settingFeatureRepository;

        public SettingFeatureController(ISettingFeatureRepository settingFeatureRepository)
        {
            _settingFeatureRepository = settingFeatureRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SettingFeatureSpecs.SearchByQuery(command.Query);
            var queryable = _settingFeatureRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFeatureCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFeatureCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingFeatureCommand command)
        {
            var settingFeature = command.To<SettingFeature>();
            await _settingFeatureRepository.InsertNodeBeforeAnother(settingFeature, command.Parent);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFeatureCommand command)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(command.Id);
            settingFeature.Update(
                command.Name,
                command.Description,
                command.Icon,
                JsonConvert.SerializeObject(command.Settings));
            await _settingFeatureRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingFeature(Guid id)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(id);
            return Ok(settingFeature.To<SettingFeatureCommand>());
        }

        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree()
        {
            var result = await _settingFeatureRepository.GetTreeAsync();
            return Ok(result.To<List<SettingFeatureCommand>>());
        }

        [HttpPost("insertFirstRootNode")]
        public async Task<IActionResult> InsertFirstRootNode(SettingFeatureCommand command)
        {
            await _settingFeatureRepository.InsertFirstRootNode(command.To<SettingFeature>());
            return Ok();
        }

        [HttpPost("insertLastRootNode")]
        public async Task<IActionResult> InsertLastRootNode(SettingFeatureCommand command)
        {
            await _settingFeatureRepository.InsertLastRootNode(command.To<SettingFeature>());
            return Ok();
        }

        [HttpPost("insertRootNodeBeforeAnother/{refNodeId}")]
        public async Task<IActionResult> InsertRootNodeBeforeAnother(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertRootNodeBeforeAnother(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }

        [HttpPost("insertRootNodeAfterAnother/{refNodeId}")]
        public async Task<IActionResult> InsertRootNodeAfterAnother(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertRootNodeAfterAnother(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }

        [HttpPost("insertFirstChildNode/{refNodeId}")]
        public async Task<IActionResult> InsertFirstChildNode(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertFirstChildNode(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }

        [HttpPost("insertLastChildNode/{refNodeId}")]
        public async Task<IActionResult> InsertLastChildNode(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertLastChildNode(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }

        [HttpPost("insertNodeBeforeAnother/{refNodeId}")]
        public async Task<IActionResult> InsertNodeBeforeAnother(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertNodeBeforeAnother(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }

        [HttpPost("insertNodeAfterAnother/{refNodeId}")]
        public async Task<IActionResult> InsertNodeAfterAnother(SettingFeatureCommand command, Guid refNodeId)
        {
            await _settingFeatureRepository.InsertNodeAfterAnother(command.To<SettingFeature>(), refNodeId);
            return Ok();
        }
    }
}