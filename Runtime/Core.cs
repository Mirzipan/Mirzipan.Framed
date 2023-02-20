using System;
using System.Collections.Generic;
using System.Linq;
using Mirzipan.Bibliotheca.Unity;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Framed.Scheduler;

namespace Mirzipan.Framed
{
    public class Core : Singleton<Core>, IModuleContainer
    {
        private readonly Dictionary<Type, CoreModule> _modulesByType = new Dictionary<Type, CoreModule>();

        private CoreState _state;
        private SchedulerModule _scheduler;

        public CoreState State => _state;
        public bool IsLoading => _state == CoreState.Loading;

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
            // TODO: get these from somewhere and sort topologically
            var modules = new List<CoreModule>();
            foreach (CoreModule entry in modules)
            {
                entry.Init(this);
            }
            
            foreach (CoreModule entry in modules)
            {
                entry.Load();
            }
        }

        #endregion Private
    }
}