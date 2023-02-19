using Mirzipan.Framed.Modules;

namespace Mirzipan.Framed
{
    public abstract class CoreModule: Module<Core>, ICoreModule
    {
        private Core _core;

        protected override IModuleContainer Container => _core;

        #region Lifecycle

        /// <summary>
        /// Initializer called by the core.
        /// </summary>
        /// <param name="core">Instance of the core</param>
        internal void Init(Core core)
        {
            _core = core;
        }

        /// <summary>
        /// Called after all modules were initialized.
        /// </summary>
        internal void Load()
        {
        }

        /// <summary>
        /// Called when core is being unloaded.
        /// </summary>
        internal void Unload()
        {
        }

        #endregion Lifecycle

        #region Protected

        /// <summary>
        /// Initialize this module. Other modules may not yet be available.
        /// </summary>
        protected abstract void OnInit();

        /// <summary>
        /// Load this module. All modules have already been initialized at this point.
        /// </summary>
        protected virtual void OnLoad()
        {
        }

        /// <summary>
        /// Unloading this module. Core is unloading and this module should do its own cleanup.
        /// </summary>
        protected virtual void OnUnload()
        {
        }

        #endregion Protected
    }
}