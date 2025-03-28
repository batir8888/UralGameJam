using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Task_System
{
    public class RetrieveSafeTask : MonoBehaviour, ITask
    {
        [SerializeField] private Transform safeZone;
        [SerializeField] private float safeDistance = 6f;

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        public bool IsCompleted() => Vector3.Distance(transform.position, safeZone.position) > safeDistance;

        public string GetDescription()
        {
            return "Вытащить сейф";
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(safeZone.position, safeDistance);
        }
    }
}