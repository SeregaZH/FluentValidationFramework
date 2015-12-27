namespace FluentValidation.Validation.Models
{
    public class ValidatorContainerBase
    {
        public ValidatorContainerBase(long priority)
        {
            Priority = priority;
        }
        
        public long Priority { get; private set; }
    }
}
