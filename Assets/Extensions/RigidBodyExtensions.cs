using UnityEngine;

namespace Extensions
{
    public static class RigidBodyExtensions
    {
        public static void MakeKinematic(this Rigidbody rb) => rb.isKinematic = true;
        public static void MakeNonKinematic(this Rigidbody rb) => rb.isKinematic = false;

        public static void SetAllZeroVelocity(this Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}