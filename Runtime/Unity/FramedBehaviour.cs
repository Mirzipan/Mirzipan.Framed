using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Scheduler;
using UnityEngine;

namespace Mirzipan.Framed.Unity
{
    public class FramedBehaviour: MonoBehaviour, IDisposerContainer
    {
        private CompositeDisposable _disposer;
        
        // TODO: this will be injected at some point
        private SchedulerModule _scheduler;

        CompositeDisposable IDisposerContainer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

        private SchedulerModule Scheduler => _scheduler;

        #region Lifecycle

        protected virtual void Start()
        {
            OnCoreLoading();
            
            // TODO: wait for core if not loaded yet
            if (Core.Instance.IsLoading)
            {
                return;
            }

            OnCoreLoaded();
            
            _scheduler = Core.Instance.Scheduler;
        }

        protected void OnDestroy()
        {
            _disposer?.Dispose();
        }

        #endregion Lifecycle

        #region Loading

        protected virtual void OnCoreLoading()
        {
            
        }

        protected virtual void OnCoreLoaded()
        {
            
        }

        #endregion Loading

        #region EventBus

        protected void Publish<TEvent>(TEvent @event)
        {
            // TODO: figure out event bus
        }

        protected IObservable<TEvent> Receive<TEvent>()
        {
            // TODO: figure out event bus
            return null;
        }

        #endregion EventBus

        #region Scheduler

        protected IDisposable Schedule(double dueTime, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime, update).DisposeWith(this);
        }

        protected IDisposable Schedule(double dueTime, double period, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime, period, update).DisposeWith(this);
        }

        protected IDisposable Schedule(TimeSpan dueTime, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime.TotalSeconds, update).DisposeWith(this);
        }

        protected IDisposable Schedule(TimeSpan dueTime, TimeSpan period, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime.TotalSeconds, period.TotalSeconds, update).DisposeWith(this);
        }

        protected void Unschedule(DeferredUpdate update)
        {
            _scheduler.Unschedule(update);
        }

        #endregion Scheduler
    }
}