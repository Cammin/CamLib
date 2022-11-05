using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CamLib
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] private ThreadPriority _loadPriority = ThreadPriority.Normal;
        [SerializeField] private Object _loadingScreenScene = null;
        
        public Object LoadingScreenScene => _loadingScreenScene;
        public ThreadPriority LoadPriority => _loadPriority;
        
        public event Action<float> OnProgressMade;

        private static readonly LoadSceneParameters LoadingScreen = new LoadSceneParameters
        {
            loadSceneMode = LoadSceneMode.Additive,
            localPhysicsMode = LocalPhysicsMode.None
        };
        
        private static readonly LoadSceneParameters NewScreen = new LoadSceneParameters
        {
            loadSceneMode = LoadSceneMode.Additive,
            localPhysicsMode = LocalPhysicsMode.None
        };

        public IEnumerator LoadScene(string newScene)
        {
            ThreadPriority originalPriority = Application.backgroundLoadingPriority;
            Application.backgroundLoadingPriority = LoadPriority;

            string currentScene = SceneManager.GetActiveScene().name;
            
            //load the loading screen,
            //wait for loading screen to fade itself perhaps
            //Unload the previous scene,
            //load the new scene,
            //unload the loading screen

            //load loadingScreen

            
            yield return Load(LoadingScreenScene.name, LoadingScreen);
            yield return Unload(currentScene);
            
            yield return Load(newScene, NewScreen);
            yield return Unload(LoadingScreenScene.name);
            
            //set loading priority back to original speed
            Application.backgroundLoadingPriority = originalPriority;
        }


        private IEnumerator Load(string scene, LoadSceneParameters loadParams)
        {
            Debug.Log($"Load {scene}");
            yield return AsyncScene(SceneManager.LoadSceneAsync(scene, loadParams));
            Debug.Log($"Loaded {scene}");
        }

        private IEnumerator Unload(string scene)
        {
            Debug.Log($"Unload {scene}");
            yield return AsyncScene(SceneManager.UnloadSceneAsync(scene));
            Debug.Log($"Unloaded {scene}");
        }

        private IEnumerator AsyncScene(AsyncOperation operation)
        {
            float previousProgress = 0;
            while (!operation.isDone)
            {
                if (Math.Abs(operation.progress - previousProgress) > 0.01f)
                {
                    Debug.Log($"|    {operation.progress}");
                    OnProgressMade?.Invoke(operation.progress);
                    previousProgress = operation.progress;
                }

                yield return null;
            }
            OnProgressMade?.Invoke(1);
        }
    }
}
