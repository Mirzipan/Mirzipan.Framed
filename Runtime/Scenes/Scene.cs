using System;
using System.Collections.Generic;
using Mirzipan.Framed.Configurations;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Framed.Unity;
using Mirzipan.Infusion;
using UnityEngine;

namespace Mirzipan.Framed.Scenes
{
    public class Scene: FramedBehaviour, IModuleContainer, IScene
    {
        private readonly Dictionary<Type, SceneModule> _modulesByType = new();
        private InjectionContainer _container;

        private CoreState _state;
        [SerializeField]
        private ConfigurationContext _configurationContext;

        public string Name { get; set; }
        public IInjectionContainer Container => _container;
        public CoreState State => _state;
        public ConfigurationContext ConfigurationContext => _configurationContext;

        #region Lifecycle

        protected override void OnCoreLoaded()
        {
            InitContainer();
            InitModules();
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

        #region Private

        private void InitContainer()
        {
            _container = new InjectionContainer(Core.Instance.Container);
            _container.Bind(typeof(IInjectionContainer), _container);

            if (_configurationContext)
            {
                _container.Inject(_configurationContext);
                _configurationContext.AddBindings();
            }

            _container.InjectAll();
        }

        private void InitModules()
        {
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

        #endregion Private
    }
}