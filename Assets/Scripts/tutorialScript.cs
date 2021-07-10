using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorialScript : MonoBehaviour {

    public Sprite[] sprites;
    public int CurrentTut;
    public GameObject Circles;
    public GameObject LastButton;
    public Image ThePicture;
    public Text TheText;
    public string[] Details;

    // Use this for initialization
    void Start () {
        Circles = GameObject.Find("Hold all");
        LastButton = GameObject.Find("LastButton");
        ThePicture = GameObject.Find("ThePicture").GetComponent<Image>();
        TheText = GameObject.Find("TheText").GetComponent<Text>();
        SetCircles();

    }
	
    public void GoNext(int num)
    {
        if (num == 1)
        {
            if (CurrentTut < sprites.Length - 1) { CurrentTut++; }
        }
        if (num == -1)
        {
            if (CurrentTut > 0) { CurrentTut--; }
        }
        SetCircles();
        
    }
    public void SetCircles()
    {
        for(int i = 0; i < Circles.transform.childCount; i++)
        {
            if(i == CurrentTut) { Circles.transform.GetChild(i).GetComponent<Image>().color = Color.gray; }
            else Circles.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        if(CurrentTut == sprites.Length-1)
        {
            LastButton.SetActive(true);
        }
        else
            LastButton.SetActive(false);
        ThePicture.sprite = sprites[CurrentTut];
        TheText.text = Details[CurrentTut];


    }
    public void EndTuto()
    {
       
        PlayerPrefs.SetInt("Logins", 2);
    }
}
