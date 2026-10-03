using UnityEngine;

namespace EminenceTest
{
    public class EminenceTestController : MonoBehaviour
    {
        public TestProfile profile = TestProfile.Legit;

        public EminenceAimAssist aimAssist;
        public EminenceNoRecoil noRecoil;
        public EminenceLeadAim leadAim;
        public EminenceWeaponSwitchTest weaponSwitch;

        [Header("Anti-cheat development policy")]
        public TestAction detectedAction = TestAction.FlagOnly;

        void Start()
        {
            ApplyProfile();
        }

        void Update()
        {
            float dt = Time.deltaTime;

            aimAssist?.Tick(dt);
            noRecoil?.Tick(dt);
        }

        public void ApplyProfile()
        {
            if (aimAssist == null || noRecoil == null || leadAim == null) return;

            EminenceTestProfiles.Apply(
                profile,
                aimAssist.settings,
                noRecoil,
                leadAim
            );

            Debug.Log($"[EMINENCE TEST] Profile: {profile}");
        }

        public void SetProfile(TestProfile newProfile)
        {
            profile = newProfile;
            ApplyProfile();
        }

        public void RunWeaponSwitchTest()
        {
            weaponSwitch?.StartSwitchTest(profile);
            SimulateDetectedModule("FastSwitching");
        }

        public void SimulateDetectedModule(string module)
        {
            Debug.Log($"[AC TEST] Detected module: {module}");

            if (detectedAction == TestAction.FlagOnly)
                Debug.Log($"[AC TEST] FLAG ONLY: {module}");
            else if (detectedAction == TestAction.LogOnly)
                Debug.Log($"[AC TEST] LOG ONLY: {module}");
            else
                Debug.Log($"[AC TEST] BAN POLICY TEST: {module}");
        }
    }
}
