using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSound : MonoBehaviour {

    public AudioClip BGMClip;
    public AudioSource TheSource;
    public static OneSound instance;

    private void Awake()
    {
       
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);

        TheSource.clip = BGMClip;
        TheSource.Play();
       
    }
    public void SetVolume()
    {
        TheSource.volume = AudioManager.BackgroundVolume;
    }

}
