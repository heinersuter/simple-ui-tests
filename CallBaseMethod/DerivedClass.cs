namespace CallBaseMethod
{
    using System;

    public class DerivedClass : BaseClass
    {
        public void DoSomethingWrong()
        {
            base.DoSomething();
        }

        public override void DoSomething()
        {
            base.DoSomething();
            Console.WriteLine("Do something of defived class");
        }
    }
}
