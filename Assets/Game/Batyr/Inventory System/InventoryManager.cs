using System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Inventory_System
{
    public class InventoryManager : MonoBehaviour
    {
        public event Action<int> DynamiteCountChanged;

        [field: SerializeField] public int DynamitesCount { get; private set; }

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(this);
        }

        public void ReduceDynamitesCount()
        {
            DynamitesCount--;
            DynamiteCountChanged?.Invoke(DynamitesCount);
        }
    }
}