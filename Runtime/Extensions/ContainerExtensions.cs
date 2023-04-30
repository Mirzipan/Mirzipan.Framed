using Reflex.Core;
using Reflex.Enums;
using Reflex.Injectors;
using UnityEngine;

namespace Mirzipan.Framed.Extensions
{
    public static class ContainerExtensions
    {
        public static void Inject(this Container @this, object instance)
        {
            AttributeInjector.Inject(instance, @this);
        }
        
        public static void Inject(this Container @this, Component instance, MonoInjectionMode injectionMode = MonoInjectionMode.Recursive)
        {
            foreach (var injectable in instance.GetInjectables(injectionMode))
            {
                AttributeInjector.Inject(injectable, @this);
            }
        }
        
    }
}