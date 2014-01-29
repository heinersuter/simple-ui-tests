namespace EnterpriseValidation
{
    public class SomeType : ValidatingViewModel
    {
        public string Value
        {
            get { return BackingFields.GetValue(() => Value); }
            set { BackingFields.SetValue(() => Value, value); }
        }
    }
}
