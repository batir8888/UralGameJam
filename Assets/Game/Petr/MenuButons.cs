using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using Unity.VisualScripting;


public class MenuButons : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Slider sliderSFX;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderVoice;

    [SerializeField] AudioMixer mixerSFX;
    [SerializeField] AudioMixer mixerMusic; 
    [SerializeField] AudioMixer mixerVoice;
    
    public void ExitGame(){
        Application.Quit();
    }

    public void OpenPanel(){
        panel.SetActive(true);
    }

    public void ClosePanel(){
        panel.SetActive(false);
    }

    float ConvertD(float value){
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
    }

    void Update()
    {
        mixerSFX.SetFloat("Volume", ConvertD(sliderSFX.value));
        mixerMusic.SetFloat("Volume", ConvertD(sliderMusic.value));
        mixerVoice.SetFloat("Volume", ConvertD(sliderVoice.value));
    }

}
