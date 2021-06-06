using System;

namespace DietPlanner.Shared.DesignPatterns.FluentFactory
{
    public static class FluentFactory<T>
        where T : class, new()
    {
        public static IBuilder<T> Init(T instance) => new Builder<T>(instance);
        public static IBuilder<T> Init() => new Builder<T>(new());
        public static IBuilder<T> Init<TBuilder>()
            where TBuilder : IBuilder<T> 
            => (IBuilder<T>)Activator.CreateInstance(typeof(TBuilder), Activator.CreateInstance<T>());
    }
}
