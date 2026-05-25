using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEnginePOC.Models
{
    public class RuleInput
    {
        public string UseCaseCode { get; set; }
        public Dictionary<string, string> Fields { get; set; }
        public List<Dictionary<string, string>> Inputs { get; set; }
    }
}
