using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class UseCaseMaster
    {
        public UseCaseMaster()
        {
            RulesMasters = new HashSet<RulesMaster>();
            UseCaseRuleMappings = new HashSet<UseCaseRuleMapping>();
        }

        public int UseCaseId { get; set; }
        public string? UseCaseCode { get; set; }
        public string? UseCaseName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<RulesMaster> RulesMasters { get; set; }
        public virtual ICollection<UseCaseRuleMapping> UseCaseRuleMappings { get; set; }
    }
}
