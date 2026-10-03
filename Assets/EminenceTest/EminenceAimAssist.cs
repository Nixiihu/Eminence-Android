using UnityEngine;

namespace EminenceTest
{
    public class EminenceAimAssist : MonoBehaviour
    {
        public Camera aimCamera;
        public AimSettings settings = new AimSettings();

        public Transform targetHead;
        public Transform targetNeck;
        public Transform targetBody;

        [Header("Input gate")]
        [Tooltip("Assistance is applied only while this input gate is held.")]
        public bool requireAimInput = true;

        [Tooltip("Use the Unity primary fire/aim input for the development test.")]
        public string inputAxis = "Fire1";

        [Tooltip("Optional scope input. When enabled, either Fire1 or this button can activate assistance.")]
        public bool allowScopeInput = true;

        public string scopeButton = "Fire2";

        private float acquiredAt = -1f;

        public bool IsInputActive()
        {
            if (!requireAimInput)
                return true;

            bool fire = Input.GetButton(inputAxis);
            bool scope = allowScopeInput && Input.GetButton(scopeButton);

            return fire || scope;
        }

        public void Tick(float deltaTime)
        {
            // Critical behavior: never move the camera unless the player is
            // actively providing the configured aim/fire input.
            if (!settings.enabled || aimCamera == null || !IsInputActive())
            {
                acquiredAt = -1f;
                return;
            }

            Transform target = settings.targetPart switch
            {
                AimTargetPart.Head => targetHead,
                AimTargetPart.Neck => targetNeck,
                _ => targetBody
            };

            if (target == null)
            {
                acquiredAt = -1f;
                return;
            }

            Vector3 direction = target.position - aimCamera.transform.position;
            if (direction.sqrMagnitude < 0.0001f)
            {
                acquiredAt = -1f;
                return;
            }

            float angle = Vector3.Angle(
                aimCamera.transform.forward,
                direction.normalized
            );

            float maxFov = Mathf.Lerp(5f, 90f, settings.fovNormalized);

            if (angle > maxFov)
            {
                acquiredAt = -1f;
                return;
            }

            if (acquiredAt < 0f)
                acquiredAt = Time.time;

            if (Time.time - acquiredAt < settings.reactionDelay)
                return;

            Quaternion wanted =
                Quaternion.LookRotation(direction.normalized, Vector3.up);

            // Controlled test error. This is deliberately measurable and
            // does not attempt to hide the behavior from anti-cheat.
            float error = settings.aimError * 2f;

            wanted *= Quaternion.Euler(
                Mathf.Sin(Time.time * 3.1f) * error,
                Mathf.Cos(Time.time * 2.7f) * error,
                0f
            );

            float response = Mathf.Lerp(
                1f,
                20f,
                settings.smoothness
            );

            float dragFactor = Mathf.Lerp(
                0.25f,
                1f,
                settings.drag
            );

            float step =
                1f - Mathf.Exp(
                    -response * dragFactor * deltaTime
                );

            float maxStep =
                settings.maxTurnSpeed * deltaTime;

            Vector3 current =
                aimCamera.transform.eulerAngles;

            Vector3 desired =
                wanted.eulerAngles;

            float pitch =
                Mathf.Clamp(
                    Mathf.DeltaAngle(current.x, desired.x),
                    -maxStep,
                    maxStep
                );

            float yaw =
                Mathf.Clamp(
                    Mathf.DeltaAngle(current.y, desired.y),
                    -maxStep,
                    maxStep
                );

            if (settings.x)
                current.y += yaw * step;

            if (settings.y)
                current.x += pitch * step;

            if (settings.z)
                current.z =
                    Mathf.LerpAngle(
                        current.z,
                        desired.z,
                        step
                    );

            aimCamera.transform.rotation =
                Quaternion.Euler(current);
        }
    }
}
