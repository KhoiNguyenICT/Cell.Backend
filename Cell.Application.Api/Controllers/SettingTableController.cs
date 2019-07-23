using Cell.Application.Api.Models.SettingTable;
using Cell.Model.Entities.SettingTableEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingTableController : CellController<SettingTable, SettingTableCreateModel, SettingTableUpdateModel, ISettingTableService>
    {
        public SettingTableController(ISettingTableService service) : base(service)
        {
        }
    }
}