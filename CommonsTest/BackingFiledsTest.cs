namespace CommonsTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Commons;
    using Commons.Mvvm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BackingFiledsTest
    {
        public class ViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged = delegate { };

            private object _value;

            public object Value
            {
                get { return _value; }
                set
                {
                    if (_value != value)
                    {
                        _value = value;
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Value"));
                    }
                }
            }
        }

        public string StringProperty { get; set; }

        public int IntProperty { get; set; }

        public DateTime DateTimeProperty { get; set; }

        public DateTime? NullableDateTimeProperty { get; set; }

        public List<string> StringListProperty { get; set; }

        public ViewModel ViewModelProperty { get; set; }

        public DelegateCommand CommandProperty { get; set; }

        [TestMethod]
        public void GetValue_NotInitialized_DefaultValue()
        {
            var backingFileds = new BackingFields(null);

            Assert.AreEqual(null, backingFileds.GetValue(() => StringProperty));
            Assert.AreEqual(0, backingFileds.GetValue(() => IntProperty));
            Assert.AreEqual(default(DateTime), backingFileds.GetValue(() => DateTimeProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => NullableDateTimeProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => StringListProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => ViewModelProperty));
        }

        [TestMethod]
        public void GetValue_Initialized_SameValue()
        {
            var backingFileds = new BackingFields(null);

            Assert.AreEqual("a", backingFileds.GetValue(() => StringProperty, () => "a"));
            Assert.AreEqual(7, backingFileds.GetValue(() => IntProperty, () => 7));
            Assert.AreEqual(new DateTime(2222, 2, 2), backingFileds.GetValue(() => DateTimeProperty, () => new DateTime(2222, 2, 2)));
            Assert.AreEqual(new DateTime(2345, 6, 7), backingFileds.GetValue(() => NullableDateTimeProperty, () => new DateTime(2345, 6, 7)));
            Assert.AreEqual("b", backingFileds.GetValue(() => StringListProperty, () => new List<string> { "b" })[0]);
            Assert.AreEqual("c", backingFileds.GetValue(() => ViewModelProperty, () => new ViewModel { Value = "c" }).Value);
        }

        [TestMethod]
        public void SetValue_DefalutValue_SameValue()
        {
            var backingFileds = new BackingFields(null);

            backingFileds.SetValue(() => StringProperty, null);
            backingFileds.SetValue(() => IntProperty, 0);
            backingFileds.SetValue(() => DateTimeProperty, default(DateTime));
            backingFileds.SetValue(() => NullableDateTimeProperty, null);
            backingFileds.SetValue(() => StringListProperty, null);
            backingFileds.SetValue(() => ViewModelProperty, null);

            Assert.AreEqual(null, backingFileds.GetValue(() => StringProperty));
            Assert.AreEqual(0, backingFileds.GetValue(() => IntProperty));
            Assert.AreEqual(default(DateTime), backingFileds.GetValue(() => DateTimeProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => NullableDateTimeProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => StringListProperty));
            Assert.AreEqual(null, backingFileds.GetValue(() => ViewModelProperty));
        }

        [TestMethod]
        public void SetValue_NotDefalutValue_SameValue()
        {
            var backingFileds = new BackingFields(null);

            Assert.IsTrue(backingFileds.SetValue(() => StringProperty, "a"));
            Assert.IsTrue(backingFileds.SetValue(() => IntProperty, 7));
            Assert.IsTrue(backingFileds.SetValue(() => DateTimeProperty, new DateTime(2222, 2, 2)));
            Assert.IsTrue(backingFileds.SetValue(() => NullableDateTimeProperty, new DateTime(2345, 6, 7)));
            Assert.IsTrue(backingFileds.SetValue(() => StringListProperty, new List<string> { "b" }));
            Assert.IsTrue(backingFileds.SetValue(() => ViewModelProperty, new ViewModel { Value = "c" }));

            Assert.AreEqual("a", backingFileds.GetValue(() => StringProperty));
            Assert.AreEqual(7, backingFileds.GetValue(() => IntProperty));
            Assert.AreEqual(new DateTime(2222, 2, 2), backingFileds.GetValue(() => DateTimeProperty));
            Assert.AreEqual(new DateTime(2345, 6, 7), backingFileds.GetValue(() => NullableDateTimeProperty));
            Assert.AreEqual("b", backingFileds.GetValue(() => StringListProperty)[0]);
            Assert.AreEqual("c", backingFileds.GetValue(() => ViewModelProperty).Value);

            Assert.IsFalse(backingFileds.SetValue(() => StringProperty, "a"));
            Assert.IsFalse(backingFileds.SetValue(() => IntProperty, 7));
            Assert.IsFalse(backingFileds.SetValue(() => DateTimeProperty, new DateTime(2222, 2, 2)));
            Assert.IsFalse(backingFileds.SetValue(() => NullableDateTimeProperty, new DateTime(2345, 6, 7)));
            Assert.IsFalse(backingFileds.SetValue(() => StringListProperty, backingFileds.GetValue(() => StringListProperty)));
            Assert.IsFalse(backingFileds.SetValue(() => ViewModelProperty, backingFileds.GetValue(() => ViewModelProperty)));
        }

        [TestMethod]
        public void GetInitializedAndSet_Observer_AttachedAndDetached()
        {
            var viewModel1 = new ViewModel { Value = "a" };
            var viewModel2 = new ViewModel { Value = "r" };
            var counter = 0;

            var backingFileds = new BackingFields(
                s =>
                {
                    if (s == "ViewModelProperty")
                    {
                        counter++;
                    }
                });

            backingFileds.GetValueAndObserve(() => ViewModelProperty, () => viewModel1);
            Assert.AreEqual(0, counter);

            viewModel1.Value = "b";
            Assert.AreEqual(1, counter);

            backingFileds.SetValueAndObserve(() => ViewModelProperty, viewModel2);
            Assert.AreEqual(2, counter);

            viewModel1.Value = "c";
            Assert.AreEqual(2, counter);

            viewModel2.Value = "s";
            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public void GetInitializedAndSet_EventHandler_AttachedAndDetached()
        {
            var viewModel1 = new ViewModel { Value = "a" };
            var viewModel2 = new ViewModel { Value = "r" };
            var counter1 = 0;
            var counter2 = 0;

            var backingFileds = new BackingFields(
                s =>
                {
                    if (s == "ViewModelProperty")
                    {
                        counter1++;
                    }
                });

            PropertyChangedEventHandler handler = (source, args) =>
                {
                    if (args.PropertyName == "Value")
                    {
                        counter2++;
                    }
                };

            backingFileds.GetValueAndObserve(() => ViewModelProperty, () => viewModel1, handler);
            Assert.AreEqual(0, counter1);
            Assert.AreEqual(0, counter2);

            viewModel1.Value = "b";
            Assert.AreEqual(0, counter1);
            Assert.AreEqual(1, counter2);

            backingFileds.SetValueAndObserve(() => ViewModelProperty, viewModel2, handler);
            Assert.AreEqual(1, counter1);
            Assert.AreEqual(1, counter2);

            viewModel1.Value = "c";
            Assert.AreEqual(1, counter1);
            Assert.AreEqual(1, counter2);

            viewModel2.Value = "s";
            Assert.AreEqual(1, counter1);
            Assert.AreEqual(2, counter2);
        }

        [TestMethod]
        public void GetValue_Initialized_OnlyOnce()
        {
            var counter = 0;
            Func<ViewModel> init = () => { counter++; return new ViewModel(); };
            var backingFileds = new BackingFields(null);
            
            backingFileds.GetValue(() => ViewModelProperty, init);
            Assert.AreEqual(1, counter);

            backingFileds.GetValue(() => ViewModelProperty, init);
            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void GetCommand()
        {
            var counter = 0;
            var backingFileds = new BackingFields(null);

            var command = backingFileds.GetCommand(() => CommandProperty, () => { counter++; });
            command.Execute(null);
            Assert.AreEqual(1, counter);

            command.Execute(null);
            Assert.AreEqual(2, counter);
        }

        [TestMethod]
        public void SetValueAndObserve_SetNull_NoException()
        {
            var backingFileds = new BackingFields(null);

            backingFileds.SetValueAndObserve(() => ViewModelProperty, new ViewModel());
            backingFileds.SetValueAndObserve(() => ViewModelProperty, null);
        }
    }
}
