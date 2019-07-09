using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;
using Cell.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Core.Constants;

namespace Cell.Application.Api.Controllers
{
    public class SettingAdvancedController : CellController<SettingAdvanced, SettingAdvancedCommand>
    {
        private readonly ISettingAdvancedRepository _settingAdvancedRepository;
        private readonly ISettingTreeRepository<SettingAdvanced> _treeRepository;

        public SettingAdvancedController(
            IValidator<SettingAdvanced> entityValidator,
            ISettingAdvancedRepository settingAdvancedRepository,
            ISettingTreeRepository<SettingAdvanced> treeRepository) : base(entityValidator)
        {
            _settingAdvancedRepository = settingAdvancedRepository;
            _treeRepository = treeRepository;
        }

        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree()
        {
            var result = await _settingAdvancedRepository.GetTreeAsync();
            return Ok(result.To<List<SettingAdvancedCommand>>());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingAdvancedCommand command)
        {
            await ValidateModel(command);
            var settingAdvanced = command.To<SettingAdvanced>();
            var any = await _settingAdvancedRepository.AnyAsync();
            if (any)
            {
                await _treeRepository.InsertNodeBeforeAnother(settingAdvanced, command.Parent, ConfigurationKeys.SettingAdvancedTable);
            }
            else
            {
                await _treeRepository.InsertFirstRootNode(settingAdvanced, ConfigurationKeys.SettingAdvancedTable);
            }

            return Ok();
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename([FromBody] SettingAdvancedCommand command)
        {
            var settingFeature = await _settingAdvancedRepository.GetByIdAsync(command.Id);
            settingFeature.Rename(command.Name);
            await _settingAdvancedRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingAdvancedCommand command)
        {
            await ValidateModel(command);
            var settingFeature = await _settingAdvancedRepository.GetByIdAsync(command.Id);
            settingFeature.Update(
                command.Name,
                command.Description);
            await _settingAdvancedRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _treeRepository.RemoveNode(id);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var settingAdvanced = await _settingAdvancedRepository.GetByIdAsync(id);
            return Ok(settingAdvanced.To<SettingAdvancedCommand>());
        }

        [HttpPost("insertFirstRootNode")]
        public async Task<IActionResult> InsertFirstRootNode(SettingAdvancedCommand command)
        {
            await _treeRepository.InsertFirstRootNode(command.To<SettingAdvanced>(), ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertLastRootNode")]
        public async Task<IActionResult> InsertLastRootNode(SettingAdvancedCommand command)
        {
            await _treeRepository.InsertLastRootNode(command.To<SettingAdvanced>(), ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertRootNodeBeforeAnother/{refNodeId}")]
        public async Task<IActionResult> InsertRootNodeBeforeAnother(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertRootNodeBeforeAnother(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertRootNodeAfterAnother/{refNodeId}")]
        public async Task<IActionResult> InsertRootNodeAfterAnother(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertRootNodeAfterAnother(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertFirstChildNode/{refNodeId}")]
        public async Task<IActionResult> InsertFirstChildNode(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertFirstChildNode(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertLastChildNode/{refNodeId}")]
        public async Task<IActionResult> InsertLastChildNode(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertLastChildNode(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertNodeBeforeAnother/{refNodeId}")]
        public async Task<IActionResult> InsertNodeBeforeAnother(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertNodeBeforeAnother(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }

        [HttpPost("insertNodeAfterAnother/{refNodeId}")]
        public async Task<IActionResult> InsertNodeAfterAnother(SettingAdvancedCommand command, Guid refNodeId)
        {
            await _treeRepository.InsertNodeAfterAnother(command.To<SettingAdvanced>(), refNodeId, ConfigurationKeys.SettingAdvancedTable);
            return Ok();
        }
    }
}