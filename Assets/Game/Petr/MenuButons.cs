using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuButons : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderVoice;

    [SerializeField] private AudioMixer mixerSfx;
    [SerializeField] private AudioMixer mixerMusic;
    [SerializeField] private AudioMixer mixerVoice;

    private void Start()
    {
        LoadSettings();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        SaveSettings();
    }

    private float ConvertD(float value)
    {
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
    }

    private void Update()
    {
        mixerSfx.SetFloat("Volume", ConvertD(sliderSfx.value));
        mixerMusic.SetFloat("Volume", ConvertD(sliderMusic.value));
        mixerVoice.SetFloat("Volume", ConvertD(sliderVoice.value));
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("SFX_Volume", sliderSfx.value);
        PlayerPrefs.SetFloat("Music_Volume", sliderMusic.value);
        PlayerPrefs.SetFloat("Voice_Volume", sliderVoice.value);
    }

    private void LoadSettings()
    {
        sliderSfx.value = PlayerPrefs.GetFloat("SFX_Volume");
        sliderMusic.value = PlayerPrefs.GetFloat("Music_Volume");
        sliderVoice.value = PlayerPrefs.GetFloat("Voice_Volume");
    }

    private void OnDestroy()
    {
        SaveSettings();
    }
}