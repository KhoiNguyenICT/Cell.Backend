using System;
using System.Collections.Generic;
using System.Linq;
using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.BasedTableAggregate;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingTableController : CellController
    {
        private readonly ISettingTableRepository _settingTableRepository;
        private readonly IBasedTableRepository _basedTableRepository;

        public SettingTableController(
            ISettingTableRepository settingTableRepository, 
            IBasedTableRepository basedTableRepository)
        {
            _settingTableRepository = settingTableRepository;
            _basedTableRepository = basedTableRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SettingTableSpecs.SearchByQuery(command.Query);
            var queryable = _settingTableRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingTableCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingTableCommand>>()
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
            if (isInvalid)
            {
                throw new CellException("Setting table name must be unique");
            }

            var settingTable = command.To<SettingTable>();
            _settingTableRepository.Add(new SettingTable(
                settingTable.Name,
                settingTable.Description,
                settingTable.Code,
                settingTable.BasedTable,
                settingTable.Settings));
            await _settingTableRepository.CommitAsync();
            return Ok(settingTable.To<SettingTableCommand>());
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