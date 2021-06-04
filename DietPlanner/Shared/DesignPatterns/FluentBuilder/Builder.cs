using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DietPlanner.Shared.DesignPatterns.FluentBuilder
{
    public class Builder<T> : IBuilder<T>
    {
        protected readonly T _instance;
        public Builder(T instance) => _instance = instance;
        private static PropertyInfo GetPropertyInfo<P>(Expression<Func<T, P>> propertyExpression)
        {
            PropertyInfo propertyInfo;
            if (propertyExpression.Body is MemberExpression body)
                propertyInfo = body.Member as PropertyInfo;
            else
                propertyInfo = (((UnaryExpression)propertyExpression.Body).Operand as MemberExpression).Member as PropertyInfo;
            return propertyInfo;
        }

        public IBuilder<T> GiveAValue<P>(Expression<Func<T, P>> propertyExpression, P value)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpression);
            propertyInfo.SetValue(_instance, value);
            return this;
        }
        public IBuilder<T> Init<P>(Expression<Func<T, P>> propertyExpression) => GiveAValue(propertyExpression, Activator.CreateInstance<P>());
        public IBuilder<T> Use(Action<T> init)
        {
            init(_instance);
            return this;
        }
        public IBuilder<T> Use<P>(Expression<Func<T, P>> propertyExpression, Action<P> action)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyExpression);
            P property = (P)propertyInfo.GetValue(_instance);
            action(property);
            return this;
        }
        public IBuilder<T> Cast<TBuilder>(Action<TBuilder> action) where TBuilder : IBuilder<T>
        {
            if (this is TBuilder factory)
                action(factory);
            return this;
        }
        public T Take() => _instance;
    }
}
