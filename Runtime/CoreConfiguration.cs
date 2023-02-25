using Mirzipan.Framed.Definitions;
using Mirzipan.Framed.Localization;
using Mirzipan.Framed.Models;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion;
using UnityEngine;

namespace Mirzipan.Framed
{
    public class CoreConfiguration: ScriptableObject, IConfiguration
    {
        public bool IsEnabled => true;
        
        public void AddBindings(IInjectionContainer container)
        {
            // TODO: move this somewhere else still
            container.Bind(new SchedulerModule());
            container.Bind(new DefinitionModule());
            container.Bind(new LocalizationModule());
            container.Bind(new ModelModule());
        }
    }
}