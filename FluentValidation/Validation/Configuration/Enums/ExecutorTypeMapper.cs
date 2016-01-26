namespace FluentValidationFramework.Validation.Configuration.Enums
{
    internal static class ExecutorTypeMapper
    {
        internal static ValidatorExecutorTypes Map(ExecutorTypes type)
        {
            return (ValidatorExecutorTypes)(int)type;
        }
    }
}
