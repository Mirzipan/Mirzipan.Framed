using Mirzipan.Clues;
using Mirzipan.Framed.Configuration;
using Mirzipan.Framed.Reactive;
using Mirzipan.Scheduler;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed.Reflex
{
    public static class ContainerDescriptorExtensions
    {
        public static ContainerDescriptor AddScheduler(this ContainerDescriptor @this,
            SchedulerConfiguration configuration)
        {
            double frameBudget = 1d / Mathf.Max(Application.targetFrameRate, 30) * configuration.FrameBudgetPercentage;
            var scheduler = new Updater(frameBudget);
            @this.AddInstance(scheduler);
            
            var ticker = new Ticker();
            @this.AddInstance(ticker);
            
            return @this;
        }

        public static ContainerDescriptor AddDefinitions(this ContainerDescriptor @this,
            DefinitionsConfiguration configuration)
        {
            var definitions = new Definitions();
            definitions.LoadAtPath(configuration.PathToLoadFrom);
            @this.AddInstance(definitions, typeof(Definitions), typeof(IDefinitions));
            
            return @this;
        }
        
        public static ContainerDescriptor AddReactiveSystems(this ContainerDescriptor @this)
        {
            @this.AddSingleton(typeof(ReactiveSystems));
            return @this;
        }
    }
}