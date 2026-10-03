using UnityEngine;

namespace EminenceTest
{
    public class EminenceTripleTap : MonoBehaviour
    {
        public float maxTapInterval = 0.35f;
        public bool panelOpen;

        private int taps;
        private float lastTap;

        void Update()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                RegisterTap();

            if (Input.GetMouseButtonDown(0))
                RegisterTap();
        }

        private void RegisterTap()
        {
            float now = Time.unscaledTime;

            if (now - lastTap > maxTapInterval)
                taps = 0;

            taps++;
            lastTap = now;

            if (taps >= 3)
            {
                panelOpen = !panelOpen;
                taps = 0;
                Debug.Log($"[EMINENCE TEST] Development panel: {(panelOpen ? "OPEN" : "CLOSED")}");
            }
        }
    }
}
