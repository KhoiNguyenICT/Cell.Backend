using System;

namespace Cell.Application.Api.Models
{
    public class SearchModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string[] Sorts { get; set; }
        public string Query { get; set; }
    }

    public class SearchSettingFieldCommand : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingActionCommand : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingViewCommand : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingFieldInstanceCommand : SearchModel
    {
        public Guid ParentId { get; set; }
    }

    public class SearchSettingActionInstanceCommand : SearchModel
    {
        public Guid ParentId { get; set; }
    }

    public class SearchSettingFormCommand : SearchModel
    {
        public Guid? TableId { get; set; }
    }

    public class SearchSettingFilterCommand : SearchModel
    {
        public Guid? TableId { get; set; }
    }

    public class SearchSettingReportCommand : SearchModel
    {
        public Guid? TableId { get; set; }
    }
}