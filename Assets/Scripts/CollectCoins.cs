using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour {

    public AudioManager AM;

    private void Awake()
    {
        if (AM == null)
        {
            AM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        }
    }
    public void PlaySound(string sound)
    {
        AM.Play(sound);
    }
}
