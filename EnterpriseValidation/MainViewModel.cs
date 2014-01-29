namespace EnterpriseValidation
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using Microsoft.Practices.Prism.ViewModel;

    [HasSelfValidation]
    public class MainViewModel : ValidatingViewModel
    {
        public SomeType SomeValue
        {
            get { return BackingFields.GetValueAndObserve(() => SomeValue, () => new SomeType()); }
            set { BackingFields.SetValueAndObserve(() => SomeValue, value); }
        }

        public string Value
        {
            get { return BackingFields.GetValue(() => Value, () => "a"); }
            set { BackingFields.SetValue(() => Value, value); }
        }

        [SelfValidation]
        public void Check(ValidationResults results)
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                results.AddResult(new ValidationResult(
                    "No value allowed",
                    this,
                    PropertySupport.ExtractPropertyName(() => this.Value),
                    null,
                    null,
                    null));
            }

            if (string.IsNullOrWhiteSpace(SomeValue.Value))
            {
                results.AddResult(new ValidationResult(
                    "No value allowed",
                    this,
                    PropertySupport.ExtractPropertyName(() => SomeValue),
                    null,
                    null,
                    null));
            }
        }
    }
}
