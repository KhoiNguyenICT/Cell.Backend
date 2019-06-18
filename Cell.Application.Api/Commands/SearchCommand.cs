using System;

namespace Cell.Application.Api.Commands
{
    public class SearchCommand
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string[] Sorts { get; set; }
        public string Query { get; set; }
    }

    public class SearchSettingFieldCommand: SearchCommand
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingActionCommand : SearchCommand
    {
        public Guid TableId { get; set; }
    }

    public class SearchSettingViewCommand : SearchCommand
    {
        public Guid TableId { get; set; }
    }
}