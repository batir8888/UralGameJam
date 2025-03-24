using Game.Batyr.Phrases_System;
using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.HL2Physics
{
    public class PhysicsPickupSystem : MonoBehaviour
    {
        private PickupableObject _heldObject;
        private Camera _camera;
        private PhraseSystem _phraseSystem;

        [SerializeField] private float pickupRange = 3f;
        [SerializeField] private float throwForce = 25f;
        [SerializeField] private float smoothSpeed = 10f;
        [SerializeField] private Transform holdPosition;

        private void Awake()
        {
            _camera = Camera.main;
            _phraseSystem = ServiceLocator.ForSceneOf(this).Get<PhraseSystem>();
        }

        private void Update()
        {
            if (!_heldObject && Input.GetKeyDown(KeyCode.E))
                TryPickup();

            if (_heldObject && Input.GetMouseButtonDown(0))
                ThrowObject();
        }

        private void FixedUpdate()
        {
            if (!_heldObject) return;
            Rigidbody rb = _heldObject.GetRigidbody();
            Vector3 newPosition = Vector3.Lerp(rb.transform.position,
                holdPosition.position,
                smoothSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }

        private void TryPickup()
        {
            Vector3 origin = _camera.transform.position;
            Vector3 direction = _camera.transform.forward;
            if (!Physics.Raycast(origin, direction, out RaycastHit hit, pickupRange)) return;
            if (!hit.collider.TryGetComponent<PickupableObject>(out var pickupableObject)) return;
            _heldObject = pickupableObject;
            _heldObject.PickUp();
        }

        private void ThrowObject()
        {
            if (!_heldObject) return;
            _heldObject.Throw(_camera.transform.forward * throwForce);
            _phraseSystem.TriggerPhrase(PhraseSystem.PlayerAction.ThrowObject);
            _heldObject = null;
        }
    }
}