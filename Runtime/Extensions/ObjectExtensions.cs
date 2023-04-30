using System.Collections.Generic;
using Reflex.Core;
using Reflex.Injectors;

namespace Mirzipan.Framed.Extensions
{
    public static class ObjectExtensions
    {
        public static void Inject(this object @this, Container container)
        {
            AttributeInjector.Inject(@this, container);
        }
        
        internal static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}