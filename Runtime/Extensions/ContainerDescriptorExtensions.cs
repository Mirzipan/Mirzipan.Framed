using System;
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
            @this.AddSingleton(type, contracts);
            return @this;
        }
    }
}