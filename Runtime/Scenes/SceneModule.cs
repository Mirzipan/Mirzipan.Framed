using Mirzipan.Bibliotheca.Disposables;
using Mirzipan.Framed.Modules;
using Mirzipan.Framed.Unity;

namespace Mirzipan.Framed.Scenes
{
    public abstract class SceneModule: Module<Scene>, ISceneModule, IDisposerContainer
    {
        private Scene _scene;
        private CompositeDisposable _disposer;
        
        protected override IModuleContainer Container => _scene;
        
        CompositeDisposable IDisposerContainer.Disposer
        {
            get => _disposer ??= new CompositeDisposable();
            set => _disposer = value;
        }

        #region Lifecycle

        /// <summary>
        /// Initializer called by the core.
        /// </summary>
        /// <param name="scene">Instance of the core</param>
        internal void Init(Scene scene)
        {
            _scene = scene;
            OnInit();
        }

        /// <summary>
        /// Called after all modules were initialized.
        /// </summary>
        internal void Load()
        {
            OnLoad();
        }

        /// <summary>
        /// Called when core is being unloaded.
        /// </summary>
        internal void Unload()
        {
            _disposer?.Dispose();
            OnUnload();
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