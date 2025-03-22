using Game.Batyr.Inventory_System;
using Game.Batyr.Phrases_System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.Dynamite
{
    public class DynamiteThrower : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private PhraseSystem _phraseSystem;

        [SerializeField] private GameObject dynamitePrefab;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private float throwTorque = 10f;

        private void Start()
        {
            _inventoryManager = ServiceLocator.ForSceneOf(this).Get<InventoryManager>();
            _phraseSystem = ServiceLocator.ForSceneOf(this).Get<PhraseSystem>();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1) || _inventoryManager.DynamitesCount <= 0) return;
            ThrowDynamite(transform.position + transform.forward, transform.forward);
            _phraseSystem.TriggerPhrase(PhraseSystem.PlayerAction.ThrowDynamite);
        }

        private void ThrowDynamite(Vector3 position, Vector3 direction)
        {
            GameObject dynamite = Instantiate(dynamitePrefab, position, Quaternion.identity);
            _inventoryManager.ReduceDynamitesCount();
            dynamite.TryGetComponent<Rigidbody>(out var component);
            component.AddForce(direction * throwForce, ForceMode.Impulse);
            component.AddTorque(transform.right * throwTorque, ForceMode.Impulse);
        }
    }
}