using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Game.Batyr.Dynamite;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip soundEffect;
    [SerializeField] private float time_move = 0.5f;
    [SerializeField] private float fadeTime = 0.3f;
    [SerializeField] private Image darkPanel;

    private Dynamite[] dynamites;

    private void Start()
    {
        dynamites = FindObjectsOfType<Dynamite>();

        audioSource = GetComponent<AudioSource>();

        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (darkPanel == null)
        {
            Debug.LogWarning("Dark panel not assigned. Please assign a UI Image to the darkPanel field.");
        }
        else
        {
            Color panelColor = darkPanel.color;
            panelColor.a = 0f;
            darkPanel.color = panelColor;
        }
    }

    public void StartButton()
    {
        PlaySound(soundEffect);
        transform.DOMoveY(0, time_move);

        foreach (var dynamite in dynamites)
        {
            dynamite.Explode();
        }


        if (darkPanel != null)
        {
            darkPanel.gameObject.SetActive(true);
            darkPanel.DOFade(1, fadeTime).OnComplete(() => SceneManager.LoadScene("SampleScene"));
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