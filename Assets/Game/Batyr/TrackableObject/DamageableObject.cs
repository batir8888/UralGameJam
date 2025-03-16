using Game.Batyr.ScoreManager;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.TrackableObject
{
    public class DamageableObject : TrackableObject
    {
        [Header("Начисление очков"), SerializeField]
        private ScoreConfig scoreConfig;

        protected override void OnTrackingComplete(float distance)
        {
            int score = Mathf.RoundToInt(scoreConfig.baseValue + distance * scoreConfig.baseMultiplier);
            ServiceLocator.ForSceneOf(this).Get<IScoreManager>().AddScore(score);
        }
    }
}