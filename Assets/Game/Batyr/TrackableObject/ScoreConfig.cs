using UnityEngine;

namespace Game.Batyr.TrackableObject
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "TrackableObject/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        public int baseValue = 100;
        public float baseMultiplier = 1;
    }
}