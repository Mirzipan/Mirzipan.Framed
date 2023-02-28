using System;
using System.Collections.Generic;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Framed.Scenes;
using Mirzipan.Infusion;

namespace Mirzipan.Framed.Unity
{
    public class Scene: FramedBehaviour, IModuleContainer
    {
        private readonly Dictionary<Type, SceneModule> _modulesByType = new();
        private InjectionContainer _container;

        private CoreState _state;
        private IConfiguration _configuration;

        public string Name { get; set; }
        public IInjectionContainer Container => _container;
        public CoreState State => _state;

        #region Lifecycle

        protected override void OnCoreLoaded()
        {
            _container = new InjectionContainer(Core.Instance.Container);
            _container.Bind(typeof(IInjectionContainer), _container);

            _configuration?.AddBindings(_container);

            _container.InjectAll();
            
            var modules = new List<SceneModule>();
            modules.AddRange(_container.ResolveAll<SceneModule>());
            int count = modules.Count;
            
            for (int i = 0; i < count; i++)
            {
                SceneModule entry = modules[i];
                _modulesByType[entry.GetType()] = entry;
            }            
            
            for (var i = 0; i < count; i++)
            {
                modules[i].Init(this);
            }

            for (var i = 0; i < count; i++)
            {
                modules[i].Load();
            }
        }

        #endregion Lifecycle

        #region Queries

        public TSceneModule Get<TSceneModule>() where TSceneModule : class, ISceneModule => Get(typeof(TSceneModule)) as TSceneModule;

        public SceneModule Get(Type moduleType)
        {
            if (_state != CoreState.Loaded)
            {
                throw new SceneNotLoadedException("Scene has either not finished loading or has been unloaded.");
            }

            return _modulesByType.TryGetValue(moduleType, out var result) ? result : null;
        }

        IModule IModuleContainer.Get(Type moduleType) => Get(moduleType);

        #endregion Queries

    }
}