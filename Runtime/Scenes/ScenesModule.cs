using System.Collections.Generic;
using Mirzipan.Framed.Modules;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirzipan.Framed.Scenes
{
    public class ScenesModule: CoreModule
    {
        private readonly Dictionary<string, IScene> _scenesByName = new();
        private readonly List<IScene> _loadedScenes = new();

        public IReadOnlyList<IScene> LoadedScenes => _loadedScenes;
        
        #region Lifecycle

        protected override void OnInit()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        #endregion Lifecycle

        #region Public

        public void LoadScene(string name)
        {
            var scene = SceneManager.GetSceneByName(name);
            if (!scene.IsValid())
            {
                Debug.LogError($"Scene with name `{name}` not found.");
                return;
            }

            SceneManager.LoadSceneAsync(scene.buildIndex, LoadSceneMode.Additive);
        }

        public void UnloadScene(string name)
        {
            if (_scenesByName.TryGetValue(name, out var scene))
            {
                UnloadScene(scene);
            }
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
            scene.Unload();
            
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

        #region Bindings

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            var roots = scene.GetRootGameObjects();

            int index = 0;
            Scene framedScene = null;
            while (!framedScene && index < roots.Length)
            {
                GameObject root = roots[index];
                framedScene = root.GetComponent<Scene>();
                ++index;
            }

            if (!framedScene)
            {
                return;
            }

            _scenesByName[scene.name] = framedScene;
            _loadedScenes.Add(framedScene);
            framedScene.Init(Core.Instance.Container);
        }

        private void OnSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
        {
            _scenesByName.Remove(scene.name);
        }

        #endregion Bindings
    }
}