using RuleEnginePOC.DTO;
using RuleEnginePOC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEnginePOC.Services
{
    public class RuleEngineService
    {
        private readonly RuleEngineContext _context;

        public RuleEngineService(RuleEngineContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> Evaluate(RuleInput input)
        {
            var useCase = _context.UseCaseMasters
                .FirstOrDefault(x => x.UseCaseCode == input.UseCaseCode);

            if (useCase == null)
                return null;

            var rules = _context.RulesMasters
                .Where(r => r.UseCaseId == useCase.UseCaseId)
                .ToList();

            foreach (var rule in rules)
            {
                var groups = _context.RuleConditionGroups
                    .Where(g => g.RuleId == rule.RuleId)
                    .OrderBy(g => g.GroupId)
                    .ToList();

                // Build all groups once
                var groupObjects = groups
                    .Select(g => BuildGroup(g.GroupId, rule.RuleId))
                    .Where(g => g != null)
                    .ToList();

                var groupEvaluations = EvaluateGroups(groupObjects, input.Inputs);

                bool finalResult = false;
                bool firstGroup = true;

                foreach (var groupEval in groupEvaluations)
                {
                    bool groupResult = groupEval.InputResults.Any(x => x.IsMatch);

                    if (firstGroup)
                    {
                        finalResult = groupResult;
                        firstGroup = false;
                    }
                    else
                    {
                        var groupOperator = _context.RuleGroupOperators
                            .FirstOrDefault(x => x.RuleId == rule.RuleId);

                        if (groupOperator?.Operator == "AND")
                            finalResult = finalResult && groupResult;
                        else
                            finalResult = finalResult || groupResult;
                    }
                }

                if (finalResult)
                    return GetRuleActions(rule.RuleId);
            }

            return null;
        }

        private RuleGroup BuildGroup(int groupId, int ruleId)
        {
            // 1. Get mappings for the group
            var mappings = _context.RuleConditionMappings
                .Where(x => x.GroupId == groupId && x.RuleId == ruleId)
                .ToList();

            if (!mappings.Any())
                return null;

            var valueIds = mappings.Select(x => x.RuleConditionValueId).ToList();

            // 2. Load condition values
            var conditionValues = _context.RuleConditionValues
                .Where(v => valueIds.Contains(v.RuleConditionValueId))
                .ToList();

            var conditionIds = conditionValues
                .Select(v => v.RuleConditionId)
                .Distinct()
                .ToList();

            // 3. Load conditions
            var conditions = _context.RuleConditions
                .Where(c => conditionIds.Contains(c.RuleConditionId))
                .ToDictionary(c => c.RuleConditionId);

            // 4. Load fields
            var fieldIds = conditions.Values
                .Select(c => c.FieldId)
                .Distinct()
                .ToList();

            var fields = _context.FieldMetadata
                .Where(f => fieldIds.Contains(f.FieldId))
                .ToDictionary(f => f.FieldId);

            // 5. Group values by RuleConditionId
            var valuesByCondition = conditionValues
                .GroupBy(v => v.RuleConditionId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(v => v.FieldValue).ToList()
                );

            // 6. Build group object
            var group = new RuleGroup
            {
                GroupId = groupId,
                Conditions = new List<GroupCondition>()
            };

            foreach (var conditionEntry in valuesByCondition)
            {
                var condition = conditions[conditionEntry.Key];
                var field = fields[condition.FieldId];

                group.Conditions.Add(new GroupCondition
                {
                    FieldName = field.FieldName,
                    Operator = condition.Operator,
                    Values = conditionEntry.Value
                });
            }

            return group;
        }

        private List<GroupEvaluation> EvaluateGroups(List<RuleGroup> groups, List<Dictionary<string, string>> inputs)
        {
            var results = new List<GroupEvaluation>();

            foreach (var group in groups)
            {
                var groupResult = new GroupEvaluation
                {
                    GroupId = group.GroupId
                };

                for (int i = 0; i < inputs.Count; i++)
                {
                    var input = inputs[i];

                    var inputEvaluation = new InputEvaluation
                    {
                        InputIndex = i,
                        InputFields = input
                    };

                    bool groupMatch = true;

                    foreach (var condition in group.Conditions)
                    {
                        string inputValue = null;

                        input.TryGetValue(condition.FieldName, out inputValue);

                        bool result = false;

                        if (inputValue != null)
                        {
                            result = EvaluateCondition(
                                condition.FieldName,
                                inputValue,
                                condition.Operator,
                                condition.Values
                            );
                        }

                        inputEvaluation.Conditions.Add(new ConditionEvaluation
                        {
                            FieldName = condition.FieldName,
                            InputValue = inputValue,
                            Operator = condition.Operator,
                            ExpectedValues = condition.Values,
                            Result = result
                        });

                        if (!result)
                            groupMatch = false;
                    }

                    inputEvaluation.IsMatch = groupMatch;

                    groupResult.InputResults.Add(inputEvaluation);
                }

                results.Add(groupResult);
            }

            return results;
        }

        private bool EvaluateCondition(string fieldName, string input, string op, List<string> values)
        {
            foreach (var value in values)
            {
                switch (op)
                {
                    case "=":
                        if (input == value) return true;
                        break;

                    case "!=":
                        if (input != value) return true;
                        break;

                    case ">":
                        if (decimal.Parse(input) > decimal.Parse(value)) return true;
                        break;

                    case "<":
                        if (decimal.Parse(input) < decimal.Parse(value)) return true;
                        break;

                    case ">=":
                        if (decimal.Parse(input) >= decimal.Parse(value)) return true;
                        break;

                    case "<=":
                        if (decimal.Parse(input) <= decimal.Parse(value)) return true;
                        break;
                }
            }

            return false;
        }

        private Dictionary<string, string> GetRuleActions(int ruleId)
        {
            return _context.RuleActions.Where(a => a.RuleId == ruleId).ToDictionary(a => a.ActionKey, a => a.ActionValue);
        }

    }
}
