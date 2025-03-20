using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip soundEffect;
    [SerializeField] float time_move = 0.5f;
    [SerializeField] float fadeTime = 0.3f;
    [SerializeField] Image darkPanel;

    void Start()
    {
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

    public void Start_Button()
    {
        PlaySound(soundEffect);
        transform.DOMoveY(0, time_move);
        
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
