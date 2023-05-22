using Mirzipan.Framed.Reactive;
using Mirzipan.Scheduler;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed.Unity
{
    public class FramedTicker : MonoBehaviour, IStartable
    {
        [Inject]
        private Container _container;
        [Inject]
        private Ticker _ticker;
        [Inject]
        private ReactiveSystems _reactiveSystems;
        
        public void Start()
        {
        }
    }
}