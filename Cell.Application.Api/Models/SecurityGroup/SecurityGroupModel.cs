using System;
using System.Collections.Generic;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SecurityGroup
{
    public class SettingGroupModel : BaseModel
    {
        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public string Settings { get; set; }

        public Guid Status { get; set; }

        public List<SettingGroupModel> Children { get; set; }
    }
}