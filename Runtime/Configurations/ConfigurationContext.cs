using System.Collections.Generic;
using System.Linq;
using Mirzipan.Infusion;
using Mirzipan.Infusion.Meta;
using UnityEngine;
using UnityEngine.Pool;

namespace Mirzipan.Framed.Configurations
{
    public class ConfigurationContext: MonoBehaviour
    {
        [Inject]
        private readonly IInjectionContainer _container;
        
        [SerializeField]
        private List<ConfigurationScriptableObject> _scriptableObjects = new();

        [SerializeField]
        private List<ConfigurationComponent> _components = new();

        private readonly List<Configuration> _configurations = new();

        /// <summary>
        /// Manually add a configuration from code.
        /// </summary>
        /// <param name="configuration"></param>
        public void AddConfiguration(Configuration configuration)
        {
            _configurations.Add(configuration);
        }

        public void AddBindings()
        {
            using var obj = ListPool<IConfiguration>.Get(out var configurations);
            
            configurations.AddRange(_scriptableObjects.Where(e => e.IsEnabled));
            configurations.AddRange(_components.Where(e => e.IsEnabled));
            configurations.AddRange(_configurations.Where(e => e.IsEnabled));
            configurations.Sort(ConfigurationComparer.Instance);

            foreach (IConfiguration entry in configurations)
            {
                _container.Inject(entry);
                entry.AddBindings();
            }
        }
    }
}