using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Emotions : MonoBehaviour
{
    [SerializeField] private Sprite[] emojis;
    [SerializeField] Image face;
   
    // Start is called before the first frame update
    void Start()
    {
        //Calm(4, ()=>Normal(4, ()=>Anger(4, ()=>Laugh(4, ()=>Normal(4)))));
    }

    public void Anger(float time, Action callback = null){
        transform.DOShakePosition(time, 7, 10, 90, false, false).OnComplete(()=>callback.Invoke());
        face.sprite = emojis[1];
    }

    public void Laugh(float time, Action callback = null){
        transform.DOShakePosition(time, 12, 8, 90, false, false).OnComplete(()=>callback.Invoke());
        face.sprite = emojis[3];
    }

    public void Normal(float time, Action callback = null){
        transform.DOShakePosition(time, 1, 1, 90, false, false).OnComplete(()=>callback.Invoke());
        face.sprite = emojis[0];
    }

    public void Calm(float time, Action callback = null){
        transform.DOShakePosition(time, 2, 30, 90, false, false).OnComplete(()=>callback.Invoke());
        face.sprite = emojis[2];
    }

}
