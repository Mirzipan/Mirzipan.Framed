using Mirzipan.Bibliotheca.Unity;
using Mirzipan.Scheduler.Unity;
using UnityEngine;

namespace Mirzipan.Framed
{
    public class Core : Singleton<Core>
    {
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
            
            _scheduler.Dispose();
            _scheduler = null;
        }

        #endregion Lifecycle

        #region Private

        private void InitInternals()
        {
            double frameBudget = 1d / Application.targetFrameRate * .5f;
            _scheduler = new Scheduler.Scheduler(new RealTime(), frameBudget);
        }

        #endregion Private
    }
}