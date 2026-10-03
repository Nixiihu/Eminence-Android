using UnityEngine;

namespace EminenceTest
{
    public class EminenceNoRecoil : MonoBehaviour
    {
        public bool enabledForTest;
        [Range(0f, 1f)] public float reduction = 0.25f;
        public Transform cameraTransform;

        private Vector2 accumulatedRecoil;

        public void RegisterRecoil(float pitch, float yaw)
        {
            accumulatedRecoil += new Vector2(pitch, yaw);
        }

        public void Tick(float deltaTime)
        {
            if (!enabledForTest || cameraTransform == null) return;
            if (accumulatedRecoil.sqrMagnitude < 0.000001f) return;

            float amount = Mathf.Clamp01(reduction);
            cameraTransform.Rotate(
                -accumulatedRecoil.x * amount,
                -accumulatedRecoil.y * amount,
                0f,
                Space.Self
            );

            accumulatedRecoil = Vector2.Lerp(
                accumulatedRecoil,
                Vector2.zero,
                12f * deltaTime
            );
        }
    }
}
