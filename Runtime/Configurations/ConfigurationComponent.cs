using Mirzipan.Infusion;
using Mirzipan.Infusion.Meta;
using UnityEngine;

namespace Mirzipan.Framed.Configurations
{
    public abstract class ConfigurationComponent: MonoBehaviour, IConfiguration
    {
        [Inject]
        protected IInjectionContainer Container { get; set; }
        
        [SerializeField]
        private int _priority;
        
        public bool IsEnabled => enabled;
        public int Priority => _priority;

        protected virtual void Start()
        {
            // Just to get Unity to show the enabled checkbox
        }

        public abstract void AddBindings();
    }
}