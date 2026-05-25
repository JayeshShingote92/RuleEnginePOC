using System;
using System.Collections.Generic;

namespace RuleEnginePOC.Models
{
    public partial class UseCaseRuleMapping
    {
        public int Id { get; set; }
        public int? UseCaseId { get; set; }
        public int? RuleId { get; set; }

        public virtual RulesMaster? Rule { get; set; }
        public virtual UseCaseMaster? UseCase { get; set; }
    }
}
