using UnityEngine;
using DG.Tweening;

public class UIShake : MonoBehaviour
{
    void Start()
    {
        transform.DOShakePosition(1000000, 3, 1, 90, false, false);
    }
}