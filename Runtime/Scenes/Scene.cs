using System;
using System.Collections.Generic;
using Mirzipan.Framed.Configurations;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Infusion;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Mirzipan.Framed.Scenes
{
    public class Scene: MonoBehaviour, IModuleContainer, IScene
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

        public void Init(IInjectionContainer parent)
        {
            _state = CoreState.Loading;
            
            InitContainer(parent);
            InitModules();

            _state = CoreState.Loaded;
        }

        public void Unload()
        {
            _state = CoreState.Unloading;

            using var obj = ListPool<SceneModule>.Get(out var modules);
            modules.AddRange(_container.ResolveAll<SceneModule>());
            int count = modules.Count;
            for (var i = 0; i < count; i++)
            {
                modules[i].Unload();
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

        #region Private

        private void InitContainer(IInjectionContainer parent)
        {
            _container = parent.CreateChild(Name) as InjectionContainer;
            if (_container == null)
            {
                return;
            }

            if (_configurationContext)
            {
                _container.Inject(_configurationContext);
                _configurationContext.AddBindings();
            }
        }

        private void InitModules()
        {
            using var obj = ListPool<SceneModule>.Get(out var modules);
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