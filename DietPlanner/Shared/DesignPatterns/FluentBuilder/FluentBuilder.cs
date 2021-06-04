using System;

namespace DietPlanner.Shared.DesignPatterns.FluentBuilder
{
    public static class FluentBuilder<T>
        where T : class, new()
    {
        public static IBuilder<T> Init(T instance) => new Builder<T>(instance);
        public static IBuilder<T> Init() => new Builder<T>(new());
        public static IBuilder<T> Init<TFactory>()
            where TFactory : IBuilder<T> 
            => (IBuilder<T>)Activator.CreateInstance(typeof(TFactory), Activator.CreateInstance<T>());
    }
}
