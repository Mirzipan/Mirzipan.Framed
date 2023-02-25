using Mirzipan.Infusion;

namespace Mirzipan.Framed
{
    public interface IConfiguration
    {
        bool IsEnabled { get; }
        void AddBindings(IInjectionContainer container);
    }
}