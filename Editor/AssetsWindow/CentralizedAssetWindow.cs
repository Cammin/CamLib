using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace CamLib.Editor
{
    /// <summary>
    /// This window is a centralized method of convenience shortcuts that solves many hurdles when dealing with the project window.
    /// It has 3 parts:
    /// - SceneSwitcher: A tool to switch between specific scenes simply.
    /// - EditorPrefFields: A tool to display and edit EditorPrefs. This is especially powerful for any kind of bootstrapping control or skipping parts of a game.
    /// - AssetDisplay: A tool to display assets (especially prefabs), with buttons to open them or ping them.
    /// 
    /// It's up to you to extend this window with your scenes, assets, and prefs.
    /// todo explain how to do that
    /// </summary>
    public class CentralizedAssetWindow : EditorWindow
    {
        public SceneSwitcher Scenes = new SceneSwitcher();
        public EditorPrefFields Prefs = new EditorPrefFields();
        public AssetDisplay Assets = new AssetDisplay();

        private Vector2 Scroll;
        
        [MenuItem("Tools/CamLib/AssetWindow")]
        public static void GetAWindow()
        {
            CentralizedAssetWindow window = GetWindow<CentralizedAssetWindow>();
            window.titleContent = new GUIContent("Assets");
        }
        
        private void OnBecameVisible() => EditorApplication.delayCall += InitializeThings;

        /// <summary>
        /// IMPORTANT to prioritize performance on this. This would reload upon every recompile.
        /// </summary>
        private void InitializeThings()
        {
            Profiler.BeginSample("CentralizedAssetWindow.Init");
            {
                Profiler.BeginSample("Initialize.Scenes");
                Scenes.GetScenes();
                Profiler.EndSample();

                Profiler.BeginSample("Initialize.Scenes");
                Assets.Initialize();
                Profiler.EndSample();

                Profiler.BeginSample("Initialize.Scenes");
                Prefs.Initialize();
                Profiler.EndSample();
            }
            Profiler.EndSample();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                InitializeThings();
            }
            
            Scroll = GUILayout.BeginScrollView(Scroll);
            
            Scenes.OnGUI();
            EditorGUILayout.Space();
            Prefs.OnGUI();
            EditorGUILayout.Space();
            Assets.OnGUI();

            GUILayout.EndScrollView();
        }
    }
}