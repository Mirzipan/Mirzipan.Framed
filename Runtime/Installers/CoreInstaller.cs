using Mirzipan.Clues;
using Mirzipan.Scheduler;
using Reflex.Core;
using UnityEngine;

namespace Mirzipan.Framed.Installers
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private SchedulerConfiguration _scheduler;
        [SerializeField]
        private DefinitionsConfiguration _definitions;

        public void InstallBindings(ContainerDescriptor descriptor)
        {
            double frameBudget = 1d / Mathf.Max(Application.targetFrameRate, 30) * _scheduler.FrameBudgetPercentage;
            var scheduler = new Updater(frameBudget);
            descriptor.AddInstance(scheduler);
            
            var ticker = new Ticker();
            descriptor.AddInstance(ticker);

            var definitions = new Definitions();
            definitions.LoadAtPath(_definitions.PathToLoadFrom);
            descriptor.AddInstance(definitions, definitions.GetType(), typeof(IDefinitions));
        }
    }
}