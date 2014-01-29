namespace EnterpriseValidation
{
    using System.ComponentModel;
    using System.Text;
    using Commons;
    using Commons.Mvvm;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    public abstract class ValidatingViewModel : ViewModel, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get { return GetErrorText(columnName); }
        }

        public string Error
        {
            get { return GetErrorText(); }
        }

        private string GetErrorText()
        {
            var results = Validation.Validate(this);

            var error = new StringBuilder();
            foreach (var result in results)
            {
                if (result.Target == this)
                {
                    error.Append("- ");
                    error.AppendLine(result.Message);
                }
            }
            return error.ToString();
        }

        private string GetErrorText(string columnName)
        {
            var results = Validation.Validate(this);

            foreach (var result in results)
            {
                if (result.Target == this && result.Key == columnName)
                {
                    return result.Message;
                }
            }

            return string.Empty;
        }
    }
}
