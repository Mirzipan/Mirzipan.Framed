using Mirzipan.Framed.Scenes;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion;
using UnityEngine;

namespace Mirzipan.Framed
{
    [CreateAssetMenu(fileName = "NewCoreConfiguration", menuName = "Framed/Core Configuration", order = 1000)]
    public class CoreConfiguration: ScriptableObject, IConfiguration
    {
        public bool IsEnabled => true;
        
        public void AddBindings(IInjectionContainer container)
        {
            // TODO: move this somewhere else still
            container.Bind(new SchedulerModule());
            container.Bind(new SceneManagement());
        }
    }
}