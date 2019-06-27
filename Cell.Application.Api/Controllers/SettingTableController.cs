using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.BasedTableAggregate;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingTableController : CellController<SettingTable>
    {
        private readonly ISettingTableRepository _settingTableRepository;
        private readonly IBasedTableRepository _basedTableRepository;
        private readonly ISettingFieldRepository _settingFieldRepository;
        private readonly ISettingActionRepository _settingActionRepository;

        public SettingTableController(
            IValidator<SettingTable> entityValidator,
            ISettingTableRepository settingTableRepository,
            IBasedTableRepository basedTableRepository,
            ISettingFieldRepository settingFieldRepository,
            ISettingActionRepository settingActionRepository) : base(entityValidator)
        {
            _settingTableRepository = settingTableRepository;
            _basedTableRepository = basedTableRepository;
            _settingFieldRepository = settingFieldRepository;
            _settingActionRepository = settingActionRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var settingTableItems = new List<SettingTableCommand>();
            var spec = SettingTableSpecs.SearchByQuery(command.Query);
            var queryable = _settingTableRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            foreach (var settingTable in items)
            {
                var countFieldItems = await _settingFieldRepository.Count(settingTable.Id);
                var countActionItems = await _settingActionRepository.Count(settingTable.Id);
                var settingTableCommand = settingTable.To<SettingTableCommand>();
                settingTableCommand.CountFieldItems = countFieldItems;
                settingTableCommand.CountActionItems = countActionItems;
                settingTableItems.Add(settingTableCommand);
            }
            return Ok(new QueryResult<SettingTableCommand>
            {
                Count = queryable.Count(),
                Items = settingTableItems
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingTable(Guid id)
        {
            var settingTable = await _settingTableRepository.GetByIdAsync(id);
            return Ok(settingTable);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingTableCommand command)
        {
            var spec = SettingTableSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingTableRepository.ExistsAsync(spec);
            var isValidator = await EntityValidator.ValidateAsync(command.To<SettingTable>());
            if (!isValidator.IsValid)
                throw new CellException(isValidator);
            if (isInvalid)
                throw new CellException("Setting table name must be unique");
            _settingTableRepository.Add(new SettingTable(
                command.Name,
                command.Description,
                command.Code,
                command.BasedTable,
                JsonConvert.SerializeObject(command.Settings)));
            await _settingTableRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingTableCommand command)
        {
            var settingTable = await _settingTableRepository.GetByIdAsync(command.Id);
            settingTable.Update(
                command.Name,
                command.Description,
                command.Code,
                JsonConvert.SerializeObject(command.Settings));
            await _settingTableRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("searchBasedTable")]
        public async Task<IActionResult> BasedTables()
        {
            var result = await _basedTableRepository.SearchBasedTable();
            return Ok(result);
        }

        [HttpPost("createBasedTable")]
        public async Task<IActionResult> CreateBasedTable(CreateBasedTable model)
        {
            var result = await _basedTableRepository.CreateBasedTable(model);
            return Ok(new { tableName = result });
        }

        [HttpPost("searchColumnFromBasedTable/{tableName}")]
        public async Task<IActionResult> SearchColumnFromBasedTable(string tableName)
        {
            var result = await _basedTableRepository.SearchColumnFromBasedTable(tableName);
            return Ok(result);
        }

        [HttpPost("searchUnusedColumnFromBasedTable/{tableId}")]
        public async Task<IActionResult> SearchUnusedColumnFromBasedTable(Guid tableId)
        {
            var result = await _basedTableRepository.SearchUnusedColumnFromBasedTable(tableId);
            return Ok(result);
        }
    }
}