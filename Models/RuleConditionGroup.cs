using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleConditionGroup
    {
        public RuleConditionGroup()
        {
            RuleConditionMappings = new HashSet<RuleConditionMapping>();
        }

        public int GroupId { get; set; }
        public int RuleId { get; set; }
        public int? GroupOrder { get; set; }

        public virtual RulesMaster Rule { get; set; } = null!;
        public virtual ICollection<RuleConditionMapping> RuleConditionMappings { get; set; }
    }
}
