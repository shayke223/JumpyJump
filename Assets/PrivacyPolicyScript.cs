using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyScript : MonoBehaviour
{

    public Text Vbox;
    public Image OkButton;
    public static bool Agree;
    public Color32 TheColor;

    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 0;
        if (Vbox == null)
        {
            Text Vbox = GameObject.Find("V Text").GetComponent<Text>();
        }
        if (OkButton == null)
        {
            Image OkButton = GameObject.Find("OK Button").GetComponent<Image>();
        }
      
    }

    public void OpenPP()
    {
        Application.OpenURL("https://yumaapps.blogspot.com/2018/09/jumpy-jump-privacy-policy.html");
    }
    public void Continue()
    {
        if (Agree)
        {

            PlayerPrefs.SetInt("Logins", 1);
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void IAgree()
    {
        if (Agree)
        {
            Agree = false;
            OkButton.color = Color.grey;
            OkButton.raycastTarget = false;
            Vbox.text = "";
        }
        else
        {
            Agree = true;
            OkButton.color = TheColor;
            OkButton.raycastTarget = true;
            Vbox.text = "X";
        }
    }
}
