using UnityEngine;
using DG.Tweening;

public class Start_script : MonoBehaviour
{
    // Ссылка на компонент AudioSource
    private AudioSource audioSource;

    // Звуковой клип для воспроизведения
    public AudioClip soundEffect;
    [SerializeField] float time_move = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

        // Если компонент отсутствует, добавляем его
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Start_Button()
    {
        PlaySound(soundEffect);
        //transform.DOMoveY(0, 2, false).SetDelay(3);
        transform.DOMoveY(0, time_move, false);
    }

    // Метод для воспроизведения звука
    public void PlaySound()
    {
        // Проверяем, что у нас есть звуковой клип и компонент AudioSource
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
        else
        {
            Debug.LogWarning("Звуковой клип не назначен или отсутствует компонент AudioSource");
        }
    }

    // Перегруженный метод для воспроизведения указанного звукового клипа
    public void PlaySound(AudioClip clip)
    {
        // Проверяем, что у нас есть звуковой клип и компонент AudioSource
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