using UnityEngine;

namespace Game
{
    public class GpuInstancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}