namespace WpfColors.Common
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class PropertyHelper
    {
        /// <summary>
        /// Gets the property name as string.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property as lambda exporessen. E.g. <c>()=>PropertyName</c>.</param>
        /// <returns>The property name.</returns>
        public static string GetName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpression");
            }

            return memberExpression.Member.Name;
        }

        /// <summary>
        /// Creates an object on first access. On later calls, the initially created object is returned.
        /// </summary>
        /// <typeparam name="T">Type of the object to create.</typeparam>
        /// <param name="field">A reference to the object.</param>
        /// <param name="newInstanceCreateMethod">A method that initially creats the object.</param>
        /// <returns>The object.</returns>
        public static T CreateIfNeeded<T>(ref T field, Func<T> newInstanceCreateMethod) where T : class
        {
            if (field == null)
            {
                field = newInstanceCreateMethod.Invoke();
            }
            return field;
        }
    }
}
