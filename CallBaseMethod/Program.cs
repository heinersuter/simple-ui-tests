namespace CallBaseMethod
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var x = new DerivedClass();
            x.DoSomething();
            x.DoSomethingWrong();

            Console.Read();
        }
    }
}
