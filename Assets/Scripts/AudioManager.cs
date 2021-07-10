using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

   // public static AudioManager instance;
    public static float CurrentVol;
    public static float BackgroundVolume;
    public static float EffectsVolume;

    public AudioSource SoundLand;

    void Awake()
    {

        //  if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
        //    DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            AudioSource creatSource =  gameObject.AddComponent<AudioSource>();
            creatSource.clip = sounds[i].clip;
            sounds[i].source = creatSource;
            sounds[i].source.pitch = sounds[i].pitch;
        }
    
    }
    public void updateLandSound()
    {
        SoundLand.pitch = sounds[1].pitch;
    }

    public void PlayLandSound()
    {
        updateLandSound();
        SoundLand.clip = sounds[1].clip;
        SoundLand.Play();
    }
    public void Play(string name)
    {
        
        for(int i = 0; i < sounds.Length; i++)
        {
            if (name == sounds[i].name)
            {

                sounds[i].source.Play();

            }
        }

    }
    public void SetVolume() // After Load
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.volume = EffectsVolume;
        }
        SoundLand.volume = EffectsVolume;
    }

        //-------------------------------------------------------------------------------------- Fade start

        public void StartFade(bool FadeInOrOut, float Speed) // true for in
    {
        if (FadeInOrOut)
        {
            StartCoroutine(FadeInVol(Speed));
        }
       else
        StartCoroutine(FadeOutVol(Speed));

    }
    public IEnumerator FadeOutVol(float Speed)
    {
      //  Speed = Speed * CurrentVol;
        while (AudioListener.volume > 0)
        {
            AudioListener.volume -= Time.deltaTime* Speed;
            yield return null;
            
        }
        AudioListener.volume = 0;

    }
    public IEnumerator FadeInVol(float Speed)
    {
        //Speed = Speed * CurrentVol;
        AudioListener.volume = 0;
        while (AudioListener.volume < CurrentVol)
        {
            AudioListener.volume += Time.deltaTime * Speed;
            yield return null;

        }
        AudioListener.volume = CurrentVol;
    }
    //-------------------------------------------------------------------------------------- Fade Ends
}
