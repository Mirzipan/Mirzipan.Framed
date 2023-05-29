using System;
using Mirzipan.Clues;
using Mirzipan.Extensions.Collections;
using Mirzipan.Framed.Configuration;
using Mirzipan.Framed.Reactive;
using Mirzipan.Scheduler;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed.Extensions
{
    public static class ContainerDescriptorExtensions
    {
        #region General

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

        public static ContainerDescriptor AddInstanceAsInterfacesAndSelf(this ContainerDescriptor @this, object instance)
        {
            var type = instance.GetType();
            var interfaces = type.GetInterfaces();
            if (interfaces.IsNullOrEmpty())
            {
                return @this.AddInstance(instance);
            }
            
            Array.Resize(ref interfaces, interfaces.Length + 1);
            interfaces[^1] = type;
            return @this.AddInstance(instance, interfaces);
        }

        public static ContainerDescriptor AddInstanceAsInterfaces(this ContainerDescriptor @this, object instance)
        {
            var type = instance.GetType();
            var interfaces = type.GetInterfaces();
            if (interfaces.IsNullOrEmpty())
            {
                return @this.AddInstance(instance);
            }
            
            return @this.AddInstance(instance, interfaces);
        }

        #endregion General

        #region Specific

        public static ContainerDescriptor AddScheduler(this ContainerDescriptor @this,
            SchedulerConfiguration configuration)
        {
            double frameBudget = 1d / Mathf.Max(Application.targetFrameRate, 30) * configuration.FrameBudgetPercentage;
            var scheduler = new Updater(frameBudget);
            @this.AddInstance(scheduler);
            
            var ticker = new Ticker();
            return @this.AddInstance(ticker);
        }

        public static ContainerDescriptor AddDefinitions(this ContainerDescriptor @this,
            DefinitionsConfiguration configuration)
        {
            var definitions = new Definitions();
            definitions.LoadAtPath(configuration.PathToLoadFrom);
            return @this.AddInstance(definitions, typeof(Definitions), typeof(IDefinitions));
        }
        
        public static ContainerDescriptor AddReactiveSystems(this ContainerDescriptor @this)
        {
            return @this.AddSingleton(typeof(ReactiveSystems));
        }

        #endregion Specific
    }
}