using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleConditionValue
    {
        public RuleConditionValue()
        {
            RuleConditionMappings = new HashSet<RuleConditionMapping>();
        }

        public int RuleConditionValueId { get; set; }
        public int RuleConditionId { get; set; }
        public string? FieldValue { get; set; }
        public int? RuleId { get; set; }

        public virtual RulesMaster? Rule { get; set; }
        public virtual RuleCondition RuleCondition { get; set; } = null!;
        public virtual ICollection<RuleConditionMapping> RuleConditionMappings { get; set; }
    }
}
