using System;

namespace Phases.Umb.Examine.Search.Extension.Composing
{
    internal class ServiceLocator
    {
        private static Func<Type, object> _findService;

        public static T GetInstance<T>() => (T)_findService(typeof(T));

        public static void Configure(Func<Type, object> findService)
        {
            _findService = findService;
        }
    }
}