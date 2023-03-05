using Mirzipan.Infusion;

namespace Mirzipan.Framed.Scenes
{
    public interface IScene
    {
        string Name { get; set; }
        void Init(IInjectionContainer parent);
        void Unload();
    }
}