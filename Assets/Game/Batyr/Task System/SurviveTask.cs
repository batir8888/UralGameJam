using Game.Batyr.Inventory_System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Task_System
{
    public class SurviveTask : MonoBehaviour, ITask
    {
        [SerializeField] private Transform safeZone;
        [SerializeField] private float safeDistance = 6f;

        public bool IsCompleted()
        {
            return Vector3.Distance(transform.position, safeZone.position) <= safeDistance &&
                   ServiceLocator.ForSceneOf(this).Get<InventoryManager>().DynamitesCount <= 0;
        }

        public string GetDescription()
        {
            return "Выжить";
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(safeZone.position, safeDistance);
        }
    }
}