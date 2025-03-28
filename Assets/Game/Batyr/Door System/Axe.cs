using System.Media;
using Extensions;
using Game.Batyr.HL2Physics;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Batyr.Door_System
{
    public class Axe : MonoBehaviour
    {
        [SerializeField] AudioClip axeSound;

        AudioSource aud;

        void Start(){
            aud = GetComponent<AudioSource>();
        }
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out DoorByAxe doorByAxe)) return;
            if (other.gameObject.TryGetComponent(out PickupableObject pickupableObject)) return;
            var po = doorByAxe.AddComponent<PickupableObject>();
            po.GetRigidbody().MakeNonKinematic();
            aud.PlayOneShot(axeSound);
        }
    }
}