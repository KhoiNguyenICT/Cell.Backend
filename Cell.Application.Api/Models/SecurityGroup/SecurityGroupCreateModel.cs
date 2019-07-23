using System;

namespace Cell.Application.Api.Models.SecurityGroup
{
    public class SecurityGroupCreateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Parent { get; set; }
        public Guid Status { get; set; }
    }
}