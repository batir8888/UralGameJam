using System.Collections;
using System.Linq;
using Game.Batyr.Inventory_System;
using Game.Batyr.Task_System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Round_System
{
    public class RoundManager : MonoBehaviour
    {
        private UITimer _uiTimer;
        private Dynamite.Dynamite[] _dynamites;
        private InventoryManager _inventoryManager;

        [SerializeField] private GameObject dynamitePrefab;
        [SerializeField] private Transform player;

        private void Start()
        {
            _inventoryManager = ServiceLocator.ForSceneOf(this).Get<InventoryManager>();
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
            StartCoroutine(SpawnDynamites());
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

        private IEnumerator SpawnDynamites()
        {
            if (_inventoryManager.DynamitesCount > 0)
            {
                while (_inventoryManager.DynamitesCount > 0)
                {
                    Instantiate(dynamitePrefab, player.position, Quaternion.identity);
                    _inventoryManager.ReduceDynamitesCount();
                }

                yield return new WaitForSeconds(0.75f);
            }
            
            _dynamites = FindObjectsOfType<Dynamite.Dynamite>();
            _dynamites.ToList().ForEach(d => d.Explode());
        }
    }
}