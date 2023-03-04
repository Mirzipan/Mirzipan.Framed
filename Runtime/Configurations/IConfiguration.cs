namespace Mirzipan.Framed.Configurations
{
    public interface IConfiguration
    {
        bool IsEnabled { get; }
        int Priority { get; }
        void AddBindings();
    }
}