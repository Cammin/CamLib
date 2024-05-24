using UnityEditor;
using UnityEngine;

namespace CamLib.Editor
{
    public static class InternalEditorFunctions
    {
        public static void ClearConsole() => LogEntries.Clear();
        public static void PlayPreviewClip(AudioClip clip, int startSample = 0, bool loop = false) => AudioUtil.PlayPreviewClip(clip, startSample, loop);
        public static void StopAllPreviewClips() => AudioUtil.StopAllPreviewClips();
        public static bool IsPreviewClipPlaying(AudioClip clip) => AudioUtil.IsPreviewClipPlaying();
    }
}