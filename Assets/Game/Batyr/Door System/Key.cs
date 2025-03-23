using Extensions;
using Game.Batyr.HL2Physics;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Batyr.Door_System
{
    public class Key : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out DoorByKey doorByKey)) return;
            if (other.gameObject.TryGetComponent(out PickupableObject pickupableObject)) return;
            var po = doorByKey.AddComponent<PickupableObject>();
            po.GetRigidbody().MakeNonKinematic();
        }
    }
}