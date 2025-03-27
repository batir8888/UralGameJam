using Game.Batyr.Inventory_System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Task_System
{
    public class SurviveTask : MonoBehaviour, ITask
    {
        [SerializeField] private Transform dangerZone;
        [SerializeField] private float dangerDistance = 6f;

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        public bool IsCompleted()
        {
            return Vector3.Distance(transform.position, dangerZone.position) >= dangerDistance &&
                   ServiceLocator.ForSceneOf(this).Get<InventoryManager>().DynamitesCount <= 0;
        }

        public string GetDescription()
        {
            return "Выжить";
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(dangerZone.position, dangerDistance);
        }
    }
}