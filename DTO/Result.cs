
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEnginePOC.DTO
{
    public class ConditionEvaluation
    {
        public string FieldName { get; set; }

        public string InputValue { get; set; }

        public string Operator { get; set; }

        public List<string> ExpectedValues { get; set; }

        public bool Result { get; set; }
    }

    public class InputEvaluation
    {
        public int InputIndex { get; set; }

        public Dictionary<string, string> InputFields { get; set; }

        public List<ConditionEvaluation> Conditions { get; set; } = new();

        public bool IsMatch { get; set; }
    }
    public class GroupEvaluation
    {
        public int GroupId { get; set; }

        public List<InputEvaluation> InputResults { get; set; } = new();

        public bool GroupMatch => InputResults.Any(x => x.IsMatch);
    }
}
