using UnityEngine;

namespace EminenceTest
{
    public class EminenceLeadAim : MonoBehaviour
    {
        public LeadAimSettings settings = new LeadAimSettings();

        public Vector3 Predict(Vector3 shooterPosition, Vector3 targetPosition, Vector3 targetVelocity)
        {
            if (!settings.enabled || settings.projectileSpeed <= 0f)
                return targetPosition;

            float distance = Vector3.Distance(shooterPosition, targetPosition);
            float travelTime = distance / settings.projectileSpeed;

            return targetPosition +
                   targetVelocity * travelTime * settings.predictionMultiplier;
        }
    }
}
