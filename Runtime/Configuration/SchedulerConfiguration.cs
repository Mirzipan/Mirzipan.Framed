using UnityEngine;

namespace Mirzipan.Framed.Configuration
{
    [CreateAssetMenu(fileName = "SchedulerConfiguration", menuName = "Framed/Scheduler Configuration", order = 10000)]
    public class SchedulerConfiguration : ScriptableObject
    {
        [Range(0, 1)]
        [Tooltip("How much of the frame may be used up by the scheduler. 0 - none, 1 - whole frame")]
        [SerializeField]
        private double _frameBudgetPercentage = 0.8d;
        public double FrameBudgetPercentage => _frameBudgetPercentage;
    }
}