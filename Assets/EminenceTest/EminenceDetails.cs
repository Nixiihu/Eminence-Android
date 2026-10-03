using UnityEngine;

namespace EminenceTest
{
    public class EminenceDetails : MonoBehaviour
    {
        public float FPS { get; private set; }

        void Update()
        {
            FPS = 1f / Mathf.Max(Time.unscaledDeltaTime, 0.0001f);
        }

        void OnGUI()
        {
            GUI.Label(new Rect(10, 45, 300, 25), $"FPS: {FPS:0}");
            GUI.Label(new Rect(10, 70, 300, 25), $"Frame: {Time.unscaledDeltaTime * 1000f:0.00} ms");
        }
    }
}
