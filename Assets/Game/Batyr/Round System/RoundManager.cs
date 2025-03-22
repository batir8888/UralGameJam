using System.Linq;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Round_System
{
    public class RoundManager : MonoBehaviour
    {
        private UITimer _uiTimer;
        private Dynamite.Dynamite[] _dynamites;

        private void Start()
        {
            _uiTimer = ServiceLocator.ForSceneOf(this).Get<UITimer>();
            _uiTimer.TimerEnded += OnTimerEnded;
            StartRound();
        }

        private void OnDestroy()
        {
            _uiTimer.TimerEnded -= OnTimerEnded;
        }

        private void StartRound()
        {
            _uiTimer.StartTimer();
            Debug.Log("Round started");
        }

        private void EndRound()
        {
            _dynamites = FindObjectsOfType<Dynamite.Dynamite>();
            _dynamites.ToList().ForEach(d => d.Explode());
        }

        private void OnTimerEnded()
        {
            EndRound();
            Debug.Log("Timer ended");
        }
    }
}