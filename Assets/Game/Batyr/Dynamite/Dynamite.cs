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

        private readonly Collider[] _colliders = new Collider[250];

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
                rb.AddExplosionForce(dynamiteConfig.explosionForce + Random.Range(-5f, 5f),
                    transform.position,
                    dynamiteConfig.explosionRadius,
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