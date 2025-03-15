using UnityEngine;

namespace Game.Batyr.Dynamite
{
    [CreateAssetMenu(fileName = "Dynamite", menuName = "Dynamite/DynamiteCfg")]
    public class DynamiteConfig : ScriptableObject
    {
        public float explosionRadius = 5f;
        public float explosionForce = 700f;
        public float upwardsModifier = 1f;
    }
}