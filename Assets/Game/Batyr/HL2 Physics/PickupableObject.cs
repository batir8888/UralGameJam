using Extensions;
using UnityEngine;

namespace Game.Batyr.HL2Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickupableObject : MonoBehaviour
    {
        public bool isPickedUp { get; private set; }

        private Rigidbody _rb;

        [SerializeField] private float defaultDrag;
        [SerializeField] private float grabbedDrag;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public Rigidbody GetRigidbody() => _rb;

        public void PickUp()
        {
            isPickedUp = true;
            _rb.useGravity = false;
            _rb.SetAllZeroVelocity();
            _rb.drag = grabbedDrag;
        }

        public void Throw(Vector3 force)
        {
            isPickedUp = false;
            _rb.useGravity = true;
            _rb.drag = defaultDrag;
            _rb.AddForce(force, ForceMode.VelocityChange);
        }
    }
}