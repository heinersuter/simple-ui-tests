namespace Commons.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Commons.Mvvm;

    public class DialogBasicsViewModel : ViewModel
    {
        private static readonly Stack<Window> _parentWindows = new Stack<Window>();
        private bool _closedByCommand;

        public string Title
        {
            get { return BackingFields.GetValue(() => Title, () => Application.Current.MainWindow.Title); }
            set { BackingFields.SetValue(() => Title, value); }
        }

        public ViewModel Content
        {
            get { return BackingFields.GetValue(() => Content); }
            set { BackingFields.SetValue(() => Content, value); }
        }

        public Action<bool?> Callback
        {
            get { return BackingFields.GetValue(() => Callback); }
            set { BackingFields.SetValue(() => Callback, value); }
        }
        
        internal Action CloseDelegate { get; set; }

        public void ShowDialog()
        {
            var dialog = new DialogView { DataContext = this, Owner = GetParentWindow() };
            _parentWindows.Push(dialog);
            dialog.ShowDialog();
        }

        internal void OnDialogClosing()
        {
            if (_closedByCommand)
            {
                return;
            }
            Close(false);
        }

        protected internal void Close(bool? result)
        {
            if (Callback != null)
            {
                Callback.Invoke(result);
            }
            if (result == null)
            {
                return;
            }
            _parentWindows.Pop(); 
            if (CloseDelegate != null)
            {
                _closedByCommand = true;
                CloseDelegate.Invoke();
            }
        }

        private static Window GetParentWindow()
        {
            if (_parentWindows.Count > 0)
            {
                return _parentWindows.Peek();
            }
            return Application.Current.MainWindow;
        }
    }
}
