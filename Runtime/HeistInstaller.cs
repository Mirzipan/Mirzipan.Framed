using Mirzipan.Framed.Extensions;
using Mirzipan.Heist.Reflex;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed
{
    public class HeistInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerDescriptor descriptor)
        {
            descriptor.AddMetadataIndexers();
            
            descriptor.AddLoopbackQueue();
            descriptor.AddClientProcessor();
            descriptor.AddServerProcessor();

            descriptor.AddReactiveSystems();
        }

        public void OnContainerBuilt(Container container)
        {
        }
    }
}