using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleAction
    {
        public int ActionId { get; set; }
        public int? RuleId { get; set; }
        public string? ActionKey { get; set; }
        public string? ActionValue { get; set; }
        public string? DataType { get; set; }

        public virtual RulesMaster? Rule { get; set; }
    }
}
