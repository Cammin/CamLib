namespace CamLib.Editor
{
    public interface IEditorPrefInstance
    {
        void Initialize(string prefKey, string displayName, string icon);
        void DrawField();

    }
}