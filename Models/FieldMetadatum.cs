using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class FieldMetadatum
    {
        public FieldMetadatum()
        {
            RuleConditions = new HashSet<RuleCondition>();
        }

        public int FieldId { get; set; }
        public string? FieldName { get; set; }
        public string? DataType { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<RuleCondition> RuleConditions { get; set; }
    }
}
