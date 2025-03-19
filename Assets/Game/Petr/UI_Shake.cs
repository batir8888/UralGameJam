using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Shake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOShakePosition(10, 4, 7, 60, false, true);
        transform.DOShakePosition(1000000, 3, 1, 90, false, false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }
}
