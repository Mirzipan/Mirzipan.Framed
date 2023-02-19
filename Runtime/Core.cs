using System;
using System.Collections.Generic;
using System.Linq;
using Mirzipan.Bibliotheca.Unity;
using Mirzipan.Extensions.Collections;
using Mirzipan.Framed.Exceptions;
using Mirzipan.Framed.Modules;
using Mirzipan.Scheduler.Unity;
using UnityEngine;

namespace Mirzipan.Framed
{
    public class Core : Singleton<Core>, IModuleContainer
    {
        private readonly Dictionary<Type, CoreModule> _modulesByType = new Dictionary<Type, CoreModule>();

        private CoreState _state;
        private Scheduler.Scheduler _scheduler;

        public CoreState State => _state;
        public bool IsLoading => _state == CoreState.Loading;

        public Scheduler.Scheduler Scheduler => _scheduler;

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

            _scheduler.Dispose();
            _scheduler = null;
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
            double frameBudget = 1d / Application.targetFrameRate * .5f;
            _scheduler = new Scheduler.Scheduler(new RealTime(), frameBudget);

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