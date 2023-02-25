using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Scheduler;
using Mirzipan.Infusion.Meta;
using Mirzipan.Scheduler;
using UnityEngine;
// ReSharper disable UnassignedReadonlyField

namespace Mirzipan.Framed.Unity
{
    public class FramedBehaviour: MonoBehaviour, IDisposerContainer
    {
        private CompositeDisposable _disposer;
        
        [Inject]
        protected readonly SchedulerModule Scheduler;

        CompositeDisposable IDisposerContainer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

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
            
            Core.Instance.Container.Inject(this);
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