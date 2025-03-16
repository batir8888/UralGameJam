using Game.Batyr.ScoreManager;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.TrackableObject
{
    public class PenaltyObject : TrackableObject
    {
        [Header("Начисление штрафных очков"), SerializeField]
        private ScoreConfig scoreConfig;

        protected override void OnTrackingComplete(float distance)
        {
            int penalty = Mathf.RoundToInt(scoreConfig.baseValue + distance * scoreConfig.baseMultiplier);
            ServiceLocator.ForSceneOf(this).Get<IScoreManager>().AddPenalty(penalty);
        }
    }
}