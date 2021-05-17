namespace MasterInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MyServiceProvider : IMyServiceProvider
    {
        private readonly Dictionary<Type, Type> container;

        public MyServiceProvider()
        {
            this.container = new Dictionary<Type, Type>();
        }

        public void Add<TSource, TDestination>()
            where TDestination : TSource
        {
            this.container[typeof(TSource)] = typeof(TDestination);
            this.container[typeof(TDestination)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (type is null)
            {
                return default;
            }

            if (!this.container.ContainsKey(type))
            {
                return null;
            }

            type = this.container[type];
            var constructor = type.GetConstructors().OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

            if (constructor is null)
            {
                return default;
            }

            var parameters = constructor.GetParameters();
            var paramVal = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterValue = CreateInstance(parameter.ParameterType);
                paramVal.Add(parameterValue);
            }

            var obj = constructor.Invoke(paramVal.ToArray());
            return obj;

        }

        public T CreateInstance<T>()
             => (T)CreateInstance(typeof(T));
    }
}
