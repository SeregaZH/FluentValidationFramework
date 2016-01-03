namespace FluentValidation.Validation.Configuration
{
    public abstract class ValidatorsConfigBase
    {
        public ValidatorsConfigBase(string rulesetName)
        {
            RulesetName = rulesetName;
        }

        public string RulesetName { get; private set; }
    }
}
