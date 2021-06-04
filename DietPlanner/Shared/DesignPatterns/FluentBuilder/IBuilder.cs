using System;
using System.Linq.Expressions;

namespace DietPlanner.Shared.DesignPatterns.FluentBuilder
{
    public interface IBuilder<T>
    {
        IBuilder<T> GiveAValue<P>(Expression<Func<T, P>> property, P value);
        IBuilder<T> Init<P>(Expression<Func<T, P>> propertyExpression);
        IBuilder<T> Use(Action<T> init);
        IBuilder<T> Use<P>(Expression<Func<T, P>> propertyExpression, Action<P> action);
        IBuilder<T> Cast<TBuilder>(Action<TBuilder> action) where TBuilder : IBuilder<T>;
        T Take();
    }
}
