using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityServiceLocator;

public class Emotions : MonoBehaviour
{
    [SerializeField] private Sprite[] emojis;
    [SerializeField] Image face;

    private void Awake()
    {
        ServiceLocator.ForSceneOf(this).Register(this);
    }

    public void Anger(float time, Action callback = null)
    {
        transform.DOShakePosition(time, 7, 10, 90, false, false).OnComplete(() => callback?.Invoke());
        face.sprite = emojis[1];
    }

    public void Laugh(float time, Action callback = null)
    {
        transform.DOShakePosition(time, 12, 8, 90, false, false).OnComplete(() => callback?.Invoke());
        face.sprite = emojis[3];
    }

    public void Normal(float time, Action callback = null)
    {
        transform.DOShakePosition(time, 1, 1, 90, false, false).OnComplete(() => callback?.Invoke());
        face.sprite = emojis[0];
    }

    public void Calm(float time, Action callback = null)
    {
        transform.DOShakePosition(time, 2, 30, 90, false, false).OnComplete(() => callback?.Invoke());
        face.sprite = emojis[2];
    }
}