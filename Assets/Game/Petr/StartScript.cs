using UnityEngine;
using DG.Tweening;

public class StartScript : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip soundEffect;
    [SerializeField] float time_move = 0.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Start_Button()
    {
        PlaySound(soundEffect);
        transform.DOMoveY(0, time_move);
    }

    public void PlaySound()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogWarning("Звуковой клип не назначен или отсутствует компонент AudioSource");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Звуковой клип не назначен или отсутствует компонент AudioSource");
        }
    }
}