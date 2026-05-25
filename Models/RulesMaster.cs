using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class RulesMaster
    {
        public RulesMaster()
        {
            RuleActions = new HashSet<RuleAction>();
            RuleConditionGroups = new HashSet<RuleConditionGroup>();
            RuleConditionMappings = new HashSet<RuleConditionMapping>();
            RuleConditionValues = new HashSet<RuleConditionValue>();
            RuleGroupOperators = new HashSet<RuleGroupOperator>();
            UseCaseRuleMappings = new HashSet<UseCaseRuleMapping>();
        }

        public int RuleId { get; set; }
        public string? RuleName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UseCaseId { get; set; }

        public virtual UseCaseMaster? UseCase { get; set; }
        public virtual ICollection<RuleAction> RuleActions { get; set; }
        public virtual ICollection<RuleConditionGroup> RuleConditionGroups { get; set; }
        public virtual ICollection<RuleConditionMapping> RuleConditionMappings { get; set; }
        public virtual ICollection<RuleConditionValue> RuleConditionValues { get; set; }
        public virtual ICollection<RuleGroupOperator> RuleGroupOperators { get; set; }
        public virtual ICollection<UseCaseRuleMapping> UseCaseRuleMappings { get; set; }
    }
}
