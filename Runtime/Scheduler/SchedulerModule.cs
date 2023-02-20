using System;
using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Modules;
using Mirzipan.Scheduler;
using Mirzipan.Scheduler.Unity;
using UnityEngine;

namespace Mirzipan.Framed.Scheduler
{
    public class SchedulerModule: CoreModule
    {
        private Mirzipan.Scheduler.Scheduler _scheduler;

        #region Lifecycle

        protected override void OnInit()
        {
            double frameBudget = 1d / Application.targetFrameRate * .5f;
            _scheduler = new Mirzipan.Scheduler.Scheduler(new RealTime(), frameBudget);
        }

        protected override void OnUnload()
        {
            _scheduler.Dispose();
            _scheduler = null;
        }

        #endregion Lifecycle

        #region Public

        public IDisposable Schedule(double dueTime, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime, update).DisposeWith(this);
        }

        public IDisposable Schedule(double dueTime, double period, DeferredUpdate update)
        {
            return _scheduler.Schedule(dueTime, period, update).DisposeWith(this);
        }

        public void Unschedule(DeferredUpdate update)
        {
            _scheduler.Unschedule(update);
        }

        #endregion Public
    }
}