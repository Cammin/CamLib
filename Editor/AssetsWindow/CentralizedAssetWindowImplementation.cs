using System;

namespace CamLib.Editor
{
    /// <summary>
    /// Derive an implementation of this class to tell the window what to have in it.
    /// Only have one implementation of this class in your project.
    /// </summary>
    public abstract class CentralizedAssetWindowImplementation
    {
        /// <summary>
        /// Directories only.
        /// ex. "Assets/Scenes"
        /// </summary>
        public virtual string[] SceneFolders { get; } = new[] { "Assets/Scenes" };
        
        /// <summary>
        /// There's 4 objects to choose from based on data type.
        /// ex.
        /// new EditorPrefInstanceInt("MyInt", "My Int", "d_VideoPlayer Icon")
        /// new EditorPrefInstanceFloat("MyFloat", "My Float", "d_VideoPlayer Icon")
        /// new EditorPrefInstanceString("MyString", "My String", "d_VideoPlayer Icon")
        /// new EditorPrefInstanceBool("MyBool", "My Bool", "d_VideoPlayer Icon")
        /// </summary>
        public virtual IEditorPrefInstance[] Prefs { get; } = Array.Empty<IEditorPrefInstance>();
        
        /// <summary>
        /// ex. "Assets/Prefabs/Player.prefab"
        /// ex. "Assets/Scripts/Game.asmdef"
        /// </summary>
        public virtual string[] AssetDisplayPaths { get; } = Array.Empty<string>(); 
    }
}