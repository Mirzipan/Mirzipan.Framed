using System;
using System.Collections;
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

        protected virtual IEnumerator Start()
        {
            OnCoreLoading();
            
            if (Core.Instance.IsLoading)
            {
                yield return null;
            }

            // TODO: this should also work with more limited scopes later
            Core.Instance.Container.Inject(this);
            
            OnCoreLoaded();

            yield return new WaitForEndOfFrame();

            OnGameReady();
        }

        protected void OnDestroy()
        {
            _disposer?.Dispose();
        }

        #endregion Lifecycle

        #region Loading

        /// <summary>
        /// Called before we wait for the core loading.
        /// </summary>
        protected virtual void OnCoreLoading()
        {
            
        }

        /// <summary>
        /// First method to be called once the core has finished loading.
        /// </summary>
        protected virtual void OnCoreLoaded()
        {
            
        }

        /// <summary>
        /// Called when all bindings have been resolved.
        /// </summary>
        protected virtual void OnGameReady()
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

        protected IDisposable AddUpdate(double dueTime, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime, update).DisposeWith(this);
        }

        protected IDisposable AddUpdate(double dueTime, double period, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime, period, update).DisposeWith(this);
        }

        protected IDisposable AddUpdate(TimeSpan dueTime, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime.TotalSeconds, update).DisposeWith(this);
        }

        protected IDisposable AddUpdate(TimeSpan dueTime, TimeSpan period, DeferredUpdate update)
        {
            return Scheduler.Schedule(dueTime.TotalSeconds, period.TotalSeconds, update).DisposeWith(this);
        }

        protected void RemoveUpdate(DeferredUpdate update)
        {
            Scheduler.Unschedule(update);
        }

        #endregion Scheduler
    }
}