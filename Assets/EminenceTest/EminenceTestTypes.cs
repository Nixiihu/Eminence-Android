using UnityEngine;

namespace EminenceTest
{
    public enum AimTargetPart { Head, Neck, Body }
    public enum TestProfile { Legit, Rage }
    public enum TestAction { FlagOnly, LogOnly, Ban }

    [System.Serializable]
    public class AimSettings
    {
        public bool enabled = false;
        public AimTargetPart targetPart = AimTargetPart.Head;
        [Range(0f, 1f)] public float smoothness = 0.35f;
        [Range(0f, 1f)] public float drag = 0.25f;
        [Range(0f, 1f)] public float aimError = 0.15f;
        [Range(0f, 1f)] public float fovNormalized = 0.20f;
        public float reactionDelay = 0.14f;
        public float maxTurnSpeed = 85f;
        public bool x = true;
        public bool y = true;
        public bool z = false;
    }

    [System.Serializable]
    public class LeadAimSettings
    {
        public bool enabled = false;
        public float projectileSpeed = 100f;
        [Range(0f, 2f)] public float predictionMultiplier = 1f;
    }
}
