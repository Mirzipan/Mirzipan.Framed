using System.Collections.Generic;
using Mirzipan.Framed.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirzipan.Framed.Scenes
{
    public class SceneManagement: CoreModule
    {
        private readonly Dictionary<string, IScene> _scenesByName = new();
        private readonly List<IScene> _loadedScenes = new();

        public IReadOnlyList<IScene> LoadedScenes => _loadedScenes;
        
        #region Lifecycle

        protected override void OnInit()
        {
        }

        #endregion Lifecycle

        #region Public

        public void LoadScene(string name)
        {
            
        }

        public void UnloadScene(string name)
        {
            
        }

        public void UnloadScene(IScene scene)
        {
            UnloadSceneAsync(scene);
        }

        #endregion Public

        #region Private

        private void UnloadSceneAsync(IScene scene)
        {
            _loadedScenes.Remove(scene);
            // TODO: set state to unloading
            
            var unloadAsync = SceneManager.UnloadSceneAsync(((MonoBehaviour)scene).gameObject.scene);
            if (unloadAsync.isDone)
            {
                Completed(unloadAsync);
            }
            else
            {
                unloadAsync.completed += Completed;
            }

            void Completed(AsyncOperation asyncOperation)
            {
            }
        }

        #endregion Private
    }
}