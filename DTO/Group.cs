using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEnginePOC.DTO
{
    public class GroupCondition
    {
        public int GroupId { get; set; }
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public List<string> Values { get; set; }
    }

    public class RuleGroup
    {
        public int GroupId { get; set; }
        public List<GroupCondition> Conditions { get; set; } = new();
    }
}
