using System;

namespace Cell.Model.Models.SecurityGroup
{
    public class SecurityGroupUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}