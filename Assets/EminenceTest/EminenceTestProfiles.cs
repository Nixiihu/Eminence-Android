using UnityEngine;

namespace EminenceTest
{
    public static class EminenceTestProfiles
    {
        public static void Apply(TestProfile profile, AimSettings aim, EminenceNoRecoil recoil, EminenceLeadAim lead)
        {
            if (profile == TestProfile.Legit)
            {
                aim.enabled = false;
                aim.smoothness = 0.35f;
                aim.drag = 0.25f;
                aim.aimError = 0.15f;
                aim.fovNormalized = 0.20f;
                aim.reactionDelay = 0.14f;
                aim.maxTurnSpeed = 85f;

                recoil.enabledForTest = false;
                recoil.reduction = 0.25f;

                lead.settings.enabled = false;
                lead.settings.predictionMultiplier = 1f;
            }
            else
            {
                aim.enabled = true;
                aim.smoothness = 0.98f;
                aim.drag = 1f;
                aim.aimError = 0.005f;
                aim.fovNormalized = 0.85f;
                aim.reactionDelay = 0.01f;
                aim.maxTurnSpeed = 720f;

                recoil.enabledForTest = true;
                recoil.reduction = 1f;

                lead.settings.enabled = true;
                lead.settings.predictionMultiplier = 1f;
            }
        }
    }
}
