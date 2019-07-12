using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Core.Constants;

namespace Cell.Application.Api.Controllers
{
    public class SettingFeatureController : CellController<SettingFeature, SettingFeatureCommand>
    {
        private readonly ISettingFeatureRepository _settingFeatureRepository;
        private readonly ISettingTreeRepository<SettingFeature> _treeRepository;

        public SettingFeatureController(
            IValidator<SettingFeature> entityValidator,
            ISettingFeatureRepository settingFeatureRepository,
            ISettingTreeRepository<SettingFeature> treeRepository) : base(entityValidator)
        {
            _settingFeatureRepository = settingFeatureRepository;
            _treeRepository = treeRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingFeatureCommand command)
        {
            await ValidateModel(command);
            var settingFeature = command.To<SettingFeature>();
            var any = await _settingFeatureRepository.AnyAsync();
            if (any)
            {
                await _treeRepository.InsertNodeBeforeAnother(settingFeature, command.Parent, ConfigurationKeys.SettingFeature);
            }
            else
            {
                await _treeRepository.InsertFirstRootNode(settingFeature, ConfigurationKeys.SettingFeature);
            }

            return Ok();
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename([FromBody] SettingFeatureCommand command)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(command.Id);
            settingFeature.Rename(command.Name);
            await _settingFeatureRepository.CommitAsync();
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

        [HttpPost("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _treeRepository.RemoveNode(id);
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
    }
}