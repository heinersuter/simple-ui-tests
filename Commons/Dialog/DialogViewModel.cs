namespace Commons.Dialog
{
    using Commons.Mvvm;

    public class DialogViewModel : DialogBasicsViewModel
    {
        public DelegateCommand OkCommand
        {
            get { return BackingFields.GetCommand(() => OkCommand, Ok, CanOk); }
        }

        protected virtual void Ok()
        {
            Close(true);
        }

        protected virtual bool CanOk()
        {
            return true;
        }

        public bool IsOkVisible
        {
            get { return BackingFields.GetValue(() => IsOkVisible, () => true); }
            set { BackingFields.SetValue(() => IsOkVisible, value); }
        }

        public DelegateCommand ApplyCommand
        {
            get { return BackingFields.GetCommand(() => ApplyCommand, Apply, CanApply); }
        }

        protected virtual void Apply()
        {
            Close(null);
        }

        protected virtual bool CanApply()
        {
            return true;
        }

        public bool IsApplyVisible
        {
            get { return BackingFields.GetValue(() => IsApplyVisible); }
            set { BackingFields.SetValue(() => IsApplyVisible, value); }
        }

        public DelegateCommand CancelCommand
        {
            get { return BackingFields.GetCommand(() => CancelCommand, Cancel, CanCancel); }
        }

        protected virtual void Cancel()
        {
            Close(false);
        }

        protected virtual bool CanCancel()
        {
            return true;
        }

        public bool IsCancelVisible
        {
            get { return BackingFields.GetValue(() => IsCancelVisible, () => true); }
            set { BackingFields.SetValue(() => IsCancelVisible, value); }
        }
    }
}
