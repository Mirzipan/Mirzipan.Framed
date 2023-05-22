using Mirzipan.Heist;

namespace Mirzipan.Framed.Reactive
{
    public interface IReactToCommand
    {
        int Priority { get; }
        void ReactTo(ICommand command);
    }
}