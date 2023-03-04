using Mirzipan.Infusion;
using Mirzipan.Infusion.Meta;

namespace Mirzipan.Framed.Configurations
{
    public abstract class Configuration: IConfiguration
    {
        [Inject]
        protected IInjectionContainer Container { get; set; }
        
        public virtual bool IsEnabled => true;
        public virtual int Priority => 0; 
        
        public abstract void AddBindings();
    }
}