using System;

namespace Mirzipan.Framed.Extensions
{
    public static class DisposableExtensions
    {
        public static IDisposable DisposeWith(this IDisposable @this, IContainDisposer container)
        {
            container.Disposer.Add(@this);
            return @this;
        }
    }
}