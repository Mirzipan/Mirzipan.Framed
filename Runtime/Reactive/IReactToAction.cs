using Mirzipan.Heist;

namespace Mirzipan.Framed.Reactive
{
    public interface IReactToAction
    {
        int Priority { get; }
        void ReactTo(IAction action);
    }
}