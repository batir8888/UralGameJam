using UnityEngine;

namespace Game.Batyr.Dynamite
{
    public class DynamiteThrower : MonoBehaviour
    {
        [SerializeField] private GameObject dynamitePrefab;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private float throwTorque = 10f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ThrowDynamite(transform.position + transform.forward, transform.forward);
            }
        }

        private void ThrowDynamite(Vector3 position, Vector3 direction)
        {
            GameObject dynamite = Instantiate(dynamitePrefab, position, Quaternion.identity);
            dynamite.TryGetComponent<Rigidbody>(out var component);
            component.AddForce(direction * throwForce, ForceMode.Impulse);
            component.AddTorque(transform.right * throwTorque, ForceMode.Impulse);
        }
    }
}