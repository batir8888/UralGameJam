using Extensions;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game.Batyr.Dynamite
{
    [RequireComponent(typeof(DynamiteSfx))]
    public class Dynamite : MonoBehaviour
    {
        public UnityEvent onExploded;

        private readonly Collider[] _colliders = new Collider[800];

        [SerializeField] private DynamiteConfig dynamiteConfig;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.F)) return;
            Explode();
        }

        private void Explode()
        {
            Physics.OverlapSphereNonAlloc(transform.position, dynamiteConfig.explosionRadius, _colliders);

            foreach (var hit in _colliders)
            {
                if (!hit || !hit.TryGetComponent<Rigidbody>(out var rb)) continue;
                if (!rb) continue;
                rb.MakeNonKinematic();
                rb.AddExplosionForce(dynamiteConfig.explosionForce + Random.Range(10f, 35f),
                    transform.position,
                    dynamiteConfig.explosionRadius + Random.Range(0.3f, 0.7f),
                    dynamiteConfig.upwardsModifier,
                    ForceMode.Impulse);

                if (hit.TryGetComponent<TrackableObject.TrackableObject>(out var trackableObject))
                {
                    trackableObject.StartTracking();
                }
            }

            Destroy(this);
        }

        private void OnDestroy()
        {
            onExploded.Invoke();
            onExploded.RemoveAllListeners();
        }

        private void OnDrawGizmosSelected()
        {
            if (dynamiteConfig == null) return;

            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
            Gizmos.DrawSphere(transform.position, dynamiteConfig.explosionRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dynamiteConfig.explosionRadius);
        }
    }
}