using Mirzipan.Infusion;
using Mirzipan.Infusion.Meta;
using UnityEngine;

namespace Mirzipan.Framed.Configurations
{
    public abstract class ConfigurationScriptableObject: ScriptableObject, IConfiguration
    {
        [Inject]
        protected IInjectionContainer Container { get; set; }

        [SerializeField]
        private bool _enabled = true;
        [SerializeField]
        private int _priority;
        
        public virtual bool IsEnabled => _enabled;
        public virtual int Priority => _priority;
        
        public abstract void AddBindings();
    }
}