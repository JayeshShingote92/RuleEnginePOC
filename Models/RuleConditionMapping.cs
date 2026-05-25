using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RuleConditionMapping
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int? RuleId { get; set; }
        public int? RuleConditionValueId { get; set; }

        public virtual RuleConditionGroup Group { get; set; } = null!;
        public virtual RulesMaster? Rule { get; set; }
        public virtual RuleConditionValue? RuleConditionValue { get; set; }
    }
}
