using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Heist.Commands;
using Mirzipan.Heist.Processors;
using Mirzipan.Scheduler;
using Reflex.Attributes;
using UnityEngine;
// ReSharper disable UnassignedReadonlyField
// ReSharper disable MemberCanBePrivate.Global

namespace Mirzipan.Framed.Unity
{
    public class FramedBehaviour: MonoBehaviour, IDisposerContainer
    {
        private CompositeDisposable _disposer;

        [Inject]
        protected readonly IClientProcessor Client;
        [Inject]
        protected readonly Updater Updater;
        [Inject]
        protected readonly Ticker Ticker;

        CompositeDisposable IDisposerContainer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

        #region Lifecycle

        protected void OnDestroy()
        {
            _disposer?.Dispose();
        }

        #endregion Lifecycle

        #region Client

        protected ValidationResult Validate(IAction action)
        {
            return Client.Validate(action);
        }

        protected void Process(IAction action)
        {
            Client.Process(action);
        }

        #endregion Client

        #region Updater

        private bool SchedulerExists()
        {
            return Updater != null;
        }

        protected void AddUpdate(double dueTime, DeferredUpdate update)
        {
            if (!SchedulerExists())
            {
                return;
            }
            
            Updater.Schedule(dueTime, update).DisposeWith(this);
        }

        protected void AddUpdate(double dueTime, double period, DeferredUpdate update)
        {
            if (!SchedulerExists())
            {
                return;
            }

            Updater.Schedule(dueTime, period, update).DisposeWith(this);
        }

        protected void AddUpdate(TimeSpan dueTime, DeferredUpdate update)
        {
            if (!SchedulerExists())
            {
                return;
            }

            Updater.Schedule(dueTime.TotalSeconds, update).DisposeWith(this);
        }

        protected void AddUpdate(TimeSpan dueTime, TimeSpan period, DeferredUpdate update)
        {
            if (!SchedulerExists())
            {
                return;
            }

            Updater.Schedule(dueTime.TotalSeconds, period.TotalSeconds, update).DisposeWith(this);
        }

        protected void RemoveUpdate(DeferredUpdate update)
        {
            if (!SchedulerExists())
            {
                return;
            }

            Updater.Unschedule(update);
        }

        #endregion Updater

        #region Ticker

        private bool TickerExists()
        {
            return Ticker != null;
        }

        protected void AddTick(TickUpdate update)
        {
            if (!TickerExists())
            {
                return;
            }

            Ticker.Add(update).DisposeWith(this);
        }

        protected void AddTick(TickUpdate update, int priority)
        {
            if (!TickerExists())
            {
                return;
            }

            Ticker.Add(update, priority).DisposeWith(this);
        }

        protected void RemoveTick(TickUpdate update)
        {
            if (!TickerExists())
            {
                return;
            }
            
            Ticker.Remove(update);
        }

        #endregion Ticker
    }
}