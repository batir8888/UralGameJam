using UnityEngine;

namespace Extensions
{
    public static class RigidBodyExtensions
    {
        public static void MakeKinematic(this Rigidbody rb) => rb.isKinematic = true;
        public static void MakeNonKinematic(this Rigidbody rb) => rb.isKinematic = false;
    }
}