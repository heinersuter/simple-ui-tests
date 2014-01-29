namespace CommonsTest
{
    using System.Collections.Generic;
    using System.Linq;
    using Commons;
    using Commons.Mvvm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ViewModelTest
    {
        public class MyViewModel : ViewModel
        {
            public string StringProperty
            {
                get { return BackingFields.GetValue(() => StringProperty); }
                set { BackingFields.SetValue(() => StringProperty, value); }
            }

            public string InitializedStringProperty
            {
                get { return BackingFields.GetValue(() => StringProperty, () => "initial value"); }
                set { BackingFields.SetValue(() => StringProperty, value); }
            }

            public MyViewModel ViewModelProperty
            {
                get { return BackingFields.GetValueAndObserve(() => ViewModelProperty, () => null); }
                set { BackingFields.SetValueAndObserve(() => ViewModelProperty, value); }
            }
        }

        [TestMethod]
        public void ViewModel_Initialized()
        {
            var vm = new MyViewModel();

            Assert.AreEqual(null, vm.StringProperty);
            Assert.AreEqual("initial value", vm.InitializedStringProperty);
            Assert.AreEqual(null, vm.ViewModelProperty);

            vm.ViewModelProperty = new MyViewModel { StringProperty = "inner vm" };
            Assert.AreEqual("inner vm", vm.ViewModelProperty.StringProperty);
        }

        [TestMethod]
        public void ViewModel_Observed()
        {
            var propertyNames = new List<string>();
            var vm = new MyViewModel();
            vm.PropertyChanged += (sender, args) => propertyNames.Add(args.PropertyName);
            vm.ViewModelProperty = new MyViewModel { StringProperty = "inner vm" };

            Assert.AreEqual(1, propertyNames.Count(s => s == "ViewModelProperty"));
            Assert.AreEqual(0, propertyNames.Count(s => s == "StringProperty"));

            vm.ViewModelProperty.StringProperty = "new value";
            Assert.AreEqual(2, propertyNames.Count(s => s == "ViewModelProperty"));
            Assert.AreEqual(0, propertyNames.Count(s => s == "StringProperty"));
        }
    }
}
