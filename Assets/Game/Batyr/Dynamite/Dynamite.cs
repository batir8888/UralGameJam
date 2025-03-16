using Extensions;
using UnityEngine;

namespace Game.Batyr.Dynamite
{
    public class Dynamite : MonoBehaviour
    {
        private readonly Collider[] _colliders = new Collider[250];

        [SerializeField] private DynamiteConfig dynamiteConfig;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            Explode();
        }

        private void Explode()
        {
            Physics.OverlapSphereNonAlloc(transform.position, dynamiteConfig.explosionRadius, _colliders);

            foreach (var hit in _colliders)
            {
                if (hit == null || !hit.TryGetComponent<Rigidbody>(out var rb)) continue;
                if (rb == null) continue;
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

            Destroy(gameObject);
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