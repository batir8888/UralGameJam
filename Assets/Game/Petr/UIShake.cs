using UnityEngine;
using DG.Tweening;

public class UIShake : MonoBehaviour
{
    private void Start()
    {
        transform.DOShakePosition(1000000, 3, 1, 90, false, false);
    }
}