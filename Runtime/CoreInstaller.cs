﻿using Mirzipan.Framed.Configuration;
using Mirzipan.Framed.Extensions;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private SchedulerConfiguration _scheduler;
        [SerializeField]
        private DefinitionsConfiguration _definitions;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            descriptor.AddScheduler(_scheduler);
            descriptor.AddDefinitions(_definitions);
        }

        public void OnContainerBuilt(Container container)
        {
        }
    }
}