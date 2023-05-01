using System;
using Mirzipan.Extensions.Collections;
using Reflex.Core;

namespace Mirzipan.Framed.Extensions
{
    public static class ContainerDescriptorExtensions
    {
        public static ContainerDescriptor AddSingletonAsSelf(this ContainerDescriptor @this, Type type, params Type[] contracts)
        {
            if (contracts == null)
            {
                return @this.AddSingleton(type);
            }
            
            Array.Resize(ref contracts, contracts.Length + 1);
            contracts[^1] = type;
            return @this.AddSingleton(type, contracts);
        }

        public static ContainerDescriptor AddSingletonAsInterfacesAndSelf(this ContainerDescriptor @this, Type type)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.IsNullOrEmpty())
            {
                return @this.AddSingleton(type);
            }
            
            Array.Resize(ref interfaces, interfaces.Length + 1);
            interfaces[^1] = type;
            return @this.AddSingleton(type, interfaces);
        }

        public static ContainerDescriptor AddSingletonAsInterfaces(this ContainerDescriptor @this, Type type)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.IsNullOrEmpty())
            {
                return @this.AddSingleton(type);
            }
            
            return @this.AddSingleton(type, interfaces);
        }
    }
}