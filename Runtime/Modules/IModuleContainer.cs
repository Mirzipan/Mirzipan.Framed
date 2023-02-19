using System;

namespace Mirzipan.Framed.Modules
{
    public interface IModuleContainer
    {
        IModule Get(Type moduleType);
    }
}