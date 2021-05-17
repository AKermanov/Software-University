namespace MasterInjection
{
    using System;

    public interface IMyServiceProvider
    {
        void Add<TSource, TDestination>()
            where TDestination : TSource;

        object CreateInstance(Type type);

        T CreateInstance<T>();
    }
}