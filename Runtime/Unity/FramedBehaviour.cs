using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Extensions;
using Mirzipan.Scheduler;
using UnityEngine;
using UnityEngine.TerrainUtils;

namespace Mirzipan.Framed.Unity
{
    public class FramedBehaviour: MonoBehaviour, IContainDisposer
    {
        private CompositeDisposable _disposer;
        private Scheduler.Scheduler _scheduler;

        CompositeDisposable IContainDisposer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

        private Scheduler.Scheduler Scheduler => _scheduler;

        #region Lifecycle

        protected virtual void Start()
        {
            // TODO: wait for core if not loaded yet
            if (Core.Instance.IsLoading)
            {
                return;
            }
            
            _scheduler = Core.Instance.Scheduler;
        }

        protected void OnDestroy()
        {
            _disposer?.Dispose();
        }

        #endregion Lifecycle

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
            return _scheduler.Schedule(dueTime, update).DisposeWith(this);
        }

        protected IDisposable Schedule(TimeSpan dueTime, TimeSpan period, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime, period, update).DisposeWith(this);
        }

        protected void Unschedule(DeferredUpdate update)
        {
            _scheduler.Unschedule(update);
        }

        #endregion Scheduler
    }
}