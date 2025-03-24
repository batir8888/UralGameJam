using System.Linq;
using Game.Batyr.Task_System;
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
            Debug.Log(
                $"{ServiceLocator.ForSceneOf(this).Get<RetrieveSafeTask>().GetDescription()} -> {ServiceLocator.ForSceneOf(this).Get<RetrieveSafeTask>().IsCompleted()}");
            Debug.Log(
                $"{ServiceLocator.ForSceneOf(this).Get<TurnOffGasTask>().GetDescription()} -> {ServiceLocator.ForSceneOf(this).Get<TurnOffGasTask>().IsCompleted()}");
            // Debug.Log(
            //     $"{ServiceLocator.ForSceneOf(this).Get<RescueCatTask>().GetDescription()} -> {ServiceLocator.ForSceneOf(this).Get<RescueCatTask>().IsCompleted()}");
            Debug.Log(
                $"{ServiceLocator.ForSceneOf(this).Get<TurnOffElectricity>().GetDescription()} -> {ServiceLocator.ForSceneOf(this).Get<TurnOffElectricity>().IsCompleted()}");
        }

        private void OnTimerEnded()
        {
            EndRound();
            Debug.Log("Timer ended");
        }
    }
}