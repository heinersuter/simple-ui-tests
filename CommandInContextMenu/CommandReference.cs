using System;
using System.Windows;
using System.Windows.Input;

namespace CommandInContextMenu {
    public class CommandReference : FrameworkElement, ICommand {

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(CommandReference),
            new PropertyMetadata(OnCommandChanged));

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(CommandReference),
            new PropertyMetadata(OnCommandParameterChanged));

        public object CommandParameter {
            get { return GetValue(CommandParameterProperty); }
            set {SetValue(CommandParameterProperty, value);}
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return Command != null && Command.CanExecute(parameter);
        }

        public void Execute(object parameter) {
            Command.Execute(parameter);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var commandReference = (CommandReference)d;
            var oldCommand = e.OldValue as ICommand;
            var newCommand = e.NewValue as ICommand;

            if (oldCommand != null) {
                //oldCommand.CanExecuteChanged -= commandReference.CanExecuteChanged;
                oldCommand.CanExecuteChanged -= commandReference.CanExecuteChanged;
            }
            if (newCommand != null) {
                //newCommand.CanExecuteChanged += commandReference.CanExecuteChanged;
                newCommand.CanExecuteChanged += commandReference.OnCanExecuteChanged;
            }
        }

        private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var commandReference = (CommandReference)d;
            commandReference.OnCanExecuteChanged(d, EventArgs.Empty);
        }

        private void OnCanExecuteChanged(object sender, EventArgs eventArgs) {
            if (CanExecuteChanged != null) {
                CanExecuteChanged.Invoke(this, eventArgs);
            }
        }
    }
}

