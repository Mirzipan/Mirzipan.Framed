using Mirzipan.Bibliotheca.Unity;
using Mirzipan.Scheduler;
using Reflex.Attributes;
using UnityEngine;

namespace Mirzipan.Framed.Unity
{
    public class FramedScheduler : Singleton<FramedScheduler>
    {
        [Inject]
        private Updater _updater;
        [Inject]
        private Ticker _ticker;

        #region Lifecycle

        private void FixedUpdate()
        {
            _ticker?.Tick();
            _updater?.Tick(Time.fixedUnscaledTimeAsDouble);
        }

        #endregion Lifecycle
    }
}