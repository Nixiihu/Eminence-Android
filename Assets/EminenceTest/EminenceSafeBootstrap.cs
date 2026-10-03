
using UnityEngine;

namespace EminenceTest
{
    // Development-only bootstrap. It deliberately starts with every gameplay
    // test disabled and never creates a separate process/window.
    public sealed class EminenceSafeBootstrap : MonoBehaviour
    {
        [SerializeField] private EminenceTestController controller;

        private void Awake()
        {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            enabled = false;
            return;
#endif
            try
            {
                if (controller == null)
                    controller = GetComponent<EminenceTestController>();

                if (controller == null)
                {
                    Debug.Log("[EMINENCE TEST] No controller found; harness disabled safely.");
                    enabled = false;
                    return;
                }

                // Never activate gameplay modifiers during startup.
                if (controller.aimAssist != null)
                    controller.aimAssist.settings.enabled = false;

                if (controller.noRecoil != null)
                    controller.noRecoil.enabledForTest = false;

                if (controller.leadAim != null)
                    controller.leadAim.settings.enabled = false;

                if (controller.weaponSwitch != null)
                    controller.weaponSwitch.enabledForTest = false;

                Debug.Log("[EMINENCE TEST] Safe initialization complete. Modules OFF.");
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("[EMINENCE TEST] Initialization failed safely: " + ex.Message);
                enabled = false;
            }
        }
    }
}
