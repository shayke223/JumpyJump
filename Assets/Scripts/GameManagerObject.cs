using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerObject : MonoBehaviour {
    public bool BannerOn;
    public Color32[] Colors;
    [Header("Prefabs")]
    public GameObject PlusMoney; 
    [Header("Sprites")]
    public Sprite[] Characters;
    public int CurrentChar;
    public PlayerScript Player;
    public Color32[] SkyColors;

    public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);

    }
    public void ChangeCharacter()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        }
        Player.GetComponent<SpriteRenderer>().sprite = Characters[CurrentChar];
        Player.ShopAlert = 0;

    }

   public void ChangeScenePreview(string name)
    {
        if(PlayerPrefs.GetInt("Logins") == 1)
        {
            SceneManager.LoadSceneAsync("Preload Scene 4");

        }
        else
        SceneManager.LoadSceneAsync(name);

    }
}
