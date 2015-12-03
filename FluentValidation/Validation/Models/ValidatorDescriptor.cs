using System;

namespace FluentValidation.Validation.Models
{
  public sealed class ValidatorDescriptor
  {
    public ValidatorDescriptor(Guid id, string key, string errorMessage, string description)
    {
      Id = id;
      Key = key;
      ErrorMessage = errorMessage;
      Description = description;
    }

    public Guid Id { get; private set; }

    public string Key { get; private set; }

    public string ErrorMessage { get; private set; }

    public string Description { get; private set; }
  }
}
