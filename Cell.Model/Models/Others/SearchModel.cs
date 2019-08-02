using System;

namespace Cell.Model.Models.Others
{
    public class SearchModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string[] Sorts { get; set; }
        public string Query { get; set; }
    }

    public class SearchSettingFieldModel : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingActionModel : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingViewModel : SearchModel
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingFieldInstanceModel : SearchModel
    {
        public Guid ParentId { get; set; }
    }

    public class SearchSettingActionInstanceModel : SearchModel
    {
        public Guid ParentId { get; set; }
    }

    public class SearchSettingFormModel : SearchModel
    {
        public Guid? TableId { get; set; }
    }

    public class SearchSettingFilterModel : SearchModel
    {
        public Guid? TableId { get; set; }
    }

    public class SearchSettingReportModel : SearchModel
    {
        public Guid? TableId { get; set; }
    }

    public class SearchSettingApiModel : SearchModel
    {
        public Guid? TableId { get; set; }
    }
}