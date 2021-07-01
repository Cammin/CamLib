using UnityEngine;

namespace CamLib
{
    public class DevBuildQuitter : MonoBehaviour
    {
        private void Update()
        {
            if (!Debug.isDebugBuild)
            {
                enabled = false;
                return;
            }
            
            TryTestQuit();
        }

        private static void TryTestQuit()
        {
#if ENABLE_INPUT_SYSTEM
            bool pressed = UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame;
#else
            bool pressed = Input.GetKeyDown(KeyCode.Escape);
#endif
            if (!pressed)
            {
                return;
            }

            Debug.Log("Application.Quit");
            Application.Quit();
        }
    }
}
