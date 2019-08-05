using System;

namespace Cell.Model.Models.SecurityGroup
{
    public class SecurityGroupOutputByAccountIdModel
    {
        public Guid Id { get; set; }
        public int IndexLeft { get; set; }
        public int IndexRight { get; set; }
    }
}