using UnityEngine;
using System.Collections;

namespace EminenceTest
{
    public class EminenceWeaponSwitchTest : MonoBehaviour
    {
        public bool enabledForTest;
        public float switchInterval = 0.08f;
        public string[] weaponNames = { "Rifle", "Sniper" };

        public void StartSwitchTest(TestProfile profile)
        {
            if (!enabledForTest) return;
            StartCoroutine(SwitchRoutine(profile));
        }

        private IEnumerator SwitchRoutine(TestProfile profile)
        {
            int count = profile == TestProfile.Rage ? 30 : 6;
            float interval = profile == TestProfile.Rage ? 0.025f : 0.18f;

            for (int i = 0; i < count; i++)
            {
                string weapon = weaponNames[i % weaponNames.Length];
                Debug.Log($"[EMINENCE TEST] Weapon switch -> {weapon}");
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
