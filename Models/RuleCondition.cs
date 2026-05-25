using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleCondition
    {
        public RuleCondition()
        {
            RuleConditionValues = new HashSet<RuleConditionValue>();
        }

        public int RuleConditionId { get; set; }
        public int FieldId { get; set; }
        public string Operator { get; set; } = null!;

        public virtual FieldMetadatum Field { get; set; } = null!;
        public virtual ICollection<RuleConditionValue> RuleConditionValues { get; set; }
    }
}
