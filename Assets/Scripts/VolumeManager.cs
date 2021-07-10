using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public OneSound BGMSound;
    public Slider Slider;
    // public  ManagerObject;
    public bool All,BackgroundSound,Effects;
    public AudioManager AM;
//    public GameObject YNQuality;
    public Text NumberToChange;
    public int AfterQuality;
    private void Start()
    {

        if (BGMSound == null)
        {
            BGMSound = GameObject.Find("BGM").GetComponent<OneSound>();
        }
        if (AM == null)
        {
            AM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        }

        if (All)
        {
            Slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
            Slider.value = AudioManager.CurrentVol;
        }
        if (BackgroundSound)
        {
            Slider.value = AudioManager.BackgroundVolume;
            Slider.onValueChanged.AddListener(delegate { ChangeVolume2(); });
        }
        if (Effects)
        {
           // Slider.onValueChanged.AddListener(delegate { ChangeVolume3(); });
            Slider.value = AudioManager.EffectsVolume;
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = Slider.value;
        AudioManager.CurrentVol = Slider.value;
    }
    public void ChangeVolume2()
    {
        BGMSound.TheSource.volume = Slider.value;
        AudioManager.BackgroundVolume = Slider.value;
    }
    public void ChangeVolume3()
    {
        for (int i = 0; i < AM.sounds.Length; i++)
        {
            AM.sounds[i].source.volume = Slider.value;
        }
        AM.SoundLand.volume = Slider.value;
        AudioManager.EffectsVolume = Slider.value;

    }
    public void SetNewQuality()
    {
        QualitySettings.SetQualityLevel(AfterQuality, true);
        QualitySettings.pixelLightCount = 0;
        QualitySettings.shadows = 0;
        QualitySettings.shadowResolution = 0;
        QualitySettings.shadowProjection = 0;
        QualitySettings.shadowCascades = 0;
        QualitySettings.realtimeReflectionProbes = false;
        QualitySettings.billboardsFaceCameraPosition = false;
        QualitySettings.blendWeights = 0;
        QualitySettings.vSyncCount = 0;

    }
    public void ChangeQuality() // Not Set!
    {
      //  YNQuality.SetActive(true);
       
        if (Slider.value >= 0 && Slider.value <= 0.2f) { AfterQuality = 1; }
        if (Slider.value > 0.2f && Slider.value <= 0.4f) { AfterQuality = 2; }
        if (Slider.value > 0.4f && Slider.value <= 0.6f) { AfterQuality = 3; }
        if (Slider.value > 0.6f && Slider.value <= 0.8f) { AfterQuality = 4; }
        if (Slider.value > 0.8f && Slider.value <= 1f) { AfterQuality = 5; }
        NumberToChange = GameObject.Find("Quality level before").GetComponent<Text>();
        NumberToChange.text = QualitySettings.GetQualityLevel().ToString();
        NumberToChange = GameObject.Find("Quality level after").GetComponent<Text>();
        NumberToChange.text = AfterQuality.ToString();
        //   QualitySettings.SetQualityLevel(num,false);
       // print(AfterQuality);
    }
    public void StopMusic()
    {
        BGMSound.TheSource.Stop();
    }
    public void RecommendedSettings()
    {
        AudioManager.CurrentVol = 1;
        AudioManager.EffectsVolume = 1f;
        AudioManager.BackgroundVolume = 0.6f;

    }
    public void UpdateSlider() // For Recom settings
    {
        if (All)
        {
            Slider.value = AudioManager.CurrentVol;
        }
        if (BackgroundSound)
        {
            Slider.value = AudioManager.BackgroundVolume;
        }
        if (Effects)
        {
            Slider.value = AudioManager.EffectsVolume;
        }
    }
    }
