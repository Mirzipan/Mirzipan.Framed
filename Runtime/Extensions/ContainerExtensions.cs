using Reflex.Core;
using Reflex.Injectors;

namespace Mirzipan.Framed.Extensions
{
    public static class ContainerExtensions
    {
        public static void Inject(this Container @this, object instance)
        {
            AttributeInjector.Inject(instance, @this);
        }
    }
}