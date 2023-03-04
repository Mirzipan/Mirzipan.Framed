using Mirzipan.Framed.Scenes;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion;
using Mirzipan.Infusion.Meta;
using UnityEngine;

namespace Mirzipan.Framed.Configurations
{
    [CreateAssetMenu(fileName = "NewCoreConfiguration", menuName = "Framed/Core Configuration", order = 1000)]
    public class CoreConfiguration: ConfigurationScriptableObject, IConfiguration
    {
        public override int Priority => int.MaxValue;

        [Inject]
        private readonly IInjectionContainer _container;

        public override void AddBindings()
        {
            _container.Bind(new SchedulerModule());
            _container.Bind(new SceneManagement());
        }
    }
}