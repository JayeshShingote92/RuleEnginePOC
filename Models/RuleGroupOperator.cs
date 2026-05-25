using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleGroupOperator
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public string? Operator { get; set; }

        public virtual RulesMaster Rule { get; set; } = null!;
    }
}
