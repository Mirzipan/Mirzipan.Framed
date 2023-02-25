using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion.Meta;
using Mirzipan.Scheduler;
// ReSharper disable UnassignedReadonlyField

namespace Mirzipan.Framed.Modules
{
    public abstract class CoreModule: Module<Core>, ICoreModule, IDisposerContainer
    {
        private Core _core;
        private CompositeDisposable _disposer;

        [Inject]
        protected readonly SchedulerModule Scheduler;

        protected override IModuleContainer Container => _core;
        
        CompositeDisposable IDisposerContainer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

        #region Lifecycle

        /// <summary>
        /// Initializer called by the core.
        /// </summary>
        /// <param name="core">Instance of the core</param>
        internal void Init(Core core)
        {
            _core = core;
            OnInit();
        }

        /// <summary>
        /// Called after all modules were initialized.
        /// </summary>
        internal void Load()
        {
            OnLoad();
        }

        /// <summary>
        /// Called when core is being unloaded.
        /// </summary>
        internal void Unload()
        {
            _disposer?.Dispose();
            OnUnload();
        }

        #endregion Lifecycle

        #region Protected

        /// <summary>
        /// Initialize this module. Other modules may not yet be available.
        /// </summary>
        protected abstract void OnInit();

        /// <summary>
        /// Load this module. All modules have already been initialized at this point.
        /// </summary>
        protected virtual void OnLoad()
        {
        }

        /// <summary>
        /// Unloading this module. Core is unloading and this module should do its own cleanup.
        /// </summary>
        protected virtual void OnUnload()
        {
        }

        #endregion Protected
        
        #region Scheduler

        protected IDisposable Schedule(double dueTime, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime, update).DisposeWith(this);
        }

        protected IDisposable Schedule(double dueTime, double period, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime, period, update).DisposeWith(this);
        }

        protected IDisposable Schedule(TimeSpan dueTime, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime.TotalSeconds, update).DisposeWith(this);
        }

        protected IDisposable Schedule(TimeSpan dueTime, TimeSpan period, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime.TotalSeconds, period.TotalSeconds, update).DisposeWith(this);
        }

        protected void Unschedule(DeferredUpdate update)
        {
            Scheduler.Unschedule(update);
        }

        #endregion Scheduler
    }
}