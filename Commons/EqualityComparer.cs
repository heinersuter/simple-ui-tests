namespace Commons
{
    using System;
    using System.Collections.Generic;

    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _comparableFunction;

        public EqualityComparer(Func<T, object> comparableFunction)
        {
            this._comparableFunction = comparableFunction;
        }

        public bool Equals(T x, T y)
        {
            return Equals(_comparableFunction(x), _comparableFunction(y));
        }

        public int GetHashCode(T obj)
        {
            var comparable = _comparableFunction(obj);
            return (!Equals(comparable, default(T))) ? comparable.GetHashCode() : 0;
        }
    }
}
