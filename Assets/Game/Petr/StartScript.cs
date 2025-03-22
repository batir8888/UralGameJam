using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Game.Batyr.Dynamite;

public class StartScript : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip soundEffect;
    [SerializeField] float time_move = 0.5f;
    [SerializeField] float fadeTime = 0.3f;
    [SerializeField] Image darkPanel;

    private Dynamite[] dynamites;

    void Start()
    {
        // Находим все объекты с компонентом Dynamite в сцене
        dynamites = FindObjectsOfType<Dynamite>();

        audioSource = GetComponent<AudioSource>();

        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Initialize dark panel if not assigned
        if (darkPanel == null)
        {
            Debug.LogWarning("Dark panel not assigned. Please assign a UI Image to the darkPanel field.");
        }
        else
        {
            // Make sure the panel starts transparent
            Color panelColor = darkPanel.color;
            panelColor.a = 0f;
            darkPanel.color = panelColor;
        }
    }

    public void StartButton()
    {
        PlaySound(soundEffect);
        transform.DOMoveY(0, time_move);

        // Вызываем Explode() для всех найденных объектов Dynamite
        foreach (var dynamite in dynamites)
        {
            dynamite.Explode();
        }
        
        
        // Fade in the dark panel
        if (darkPanel != null)
        {
            darkPanel.gameObject.SetActive(true);
            darkPanel.DOFade(1, fadeTime);
        }
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
