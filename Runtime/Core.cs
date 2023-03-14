using System;
using System.Collections.Generic;
using System.Linq;
using Mirzipan.Bibliotheca.Unity;
using Mirzipan.Framed.Configurations;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mirzipan.Framed
{
    public class Core : Singleton<Core>, IModuleContainer
    {
        private readonly Dictionary<Type, CoreModule> _modulesByType = new();
        private InjectionContainer _container;

        private CoreState _state;
        private SchedulerModule _scheduler;

        [SerializeField]
        private ConfigurationContext _configurationContext;

        public CoreState State => _state;
        public bool IsLoading => _state == CoreState.Loading;

        public InjectionContainer Container => _container;
        public SchedulerModule Scheduler => _scheduler ??= Get<SchedulerModule>();

        #region Lifecycle

        protected override void Awake()
        {
            base.Awake();

            _state = CoreState.Loading;

            // TODO: async
            InitInternals();

            _state = CoreState.Loaded;
        }

        private void OnDestroy()
        {
            _state = CoreState.Unloading;
            _container.Dispose();
            _container = null;

            var modules = _modulesByType.Values.ToList();
            foreach (CoreModule entry in modules)
            {
                entry.Unload();
            }
        }

        #endregion Lifecycle

        #region Queries

        public TCoreModule Get<TCoreModule>() where TCoreModule : class, ICoreModule => Get(typeof(TCoreModule)) as TCoreModule;

        public CoreModule Get(Type moduleType)
        {
            if (_state != CoreState.Loaded)
            {
                throw new CoreNotLoadedException("Core has either not finished loading or has been unloaded.");
            }

            return _modulesByType.TryGetValue(moduleType, out var result) ? result : null;
        }

        IModule IModuleContainer.Get(Type moduleType) => Get(moduleType);

        #endregion Queries

        #region Private

        private void InitInternals()
        {
            InitContainer();
            InitModules();
        }

        private void InitContainer()
        {
            _container = new InjectionContainer("CoreContainer");

            if (_configurationContext)
            {
                _container.Inject(_configurationContext);
                _configurationContext.AddBindings();
            }
        }

        private void InitModules()
        {
            var modules = new List<CoreModule>();
            modules.AddRange(_container.ResolveAll<CoreModule>());
            int count = modules.Count;

            for (int i = 0; i < count; i++)
            {
                CoreModule entry = modules[i];
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