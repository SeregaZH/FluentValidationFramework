using System;
using FluentValidation.Validation.Rules;

namespace FluentValidation.Validation.Models
{
  [Serializable]
  public class ValidationResult : IValidationResult
  {
    private readonly ValidatorDescriptor _validatorDescriptor;
    private readonly bool _isValid ;

    public ValidationResult(bool isValid, ValidatorDescriptor validatorDescriptor)
    {
      _validatorDescriptor = validatorDescriptor;
      _isValid = isValid;
    }

    public bool IsValid()
    {
      return _isValid;
    }

    public Guid Id
    {
      get
      {
        return _validatorDescriptor.Id;
      }
    }

    public string Key
    {
      get
      {
        return _validatorDescriptor.Key;
      }
    }

    public string ErrorMessage
    {
      get
      {
        return _validatorDescriptor.ErrorMessage;
      }
    }

    public string Description
    {
      get
      {
        return _validatorDescriptor.Description;
      }
    }
  }
}
