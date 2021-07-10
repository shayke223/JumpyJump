using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchivScript : MonoBehaviour {

    public PlayerScript Player;

    public int[] Progress; // Need to be saved
    public int[] Status; // 0 - Not done, 1 - Done, 2 - Has a gift
    public int[] WhatGift;
    // public static bool[] Rewarded; // Need to be saved

    public int RewardUntaken;

    private void Awake()
    {
        bool[] Rewarded = new bool[transform.childCount];
        Progress = new int[transform.childCount];
        Status = new int[transform.childCount];
        WhatGift = new int[transform.childCount];


        for (int i = 0; i < transform.childCount; i++) // it doesnt matter if its deatils or something else length
        {

            transform.GetChild(i).GetComponent<RawScript>().MyID = i;
           // transform.GetChild(i).GetComponent<RawScript>().Target = Target[i];
            transform.GetChild(i).GetComponent<RawScript>().Progress = Progress[i];
            transform.GetChild(i).GetComponent<RawScript>().Status = Status[i];
            transform.GetChild(i).GetComponent<RawScript>().Rewarded = Rewarded[i];
        }
    }
    public void UpdateAchiv()
    {
        for (int i = 0; i < transform.childCount; i++) // it doesnt matter if its deatils or something else length
        {
            transform.GetChild(i).GetComponent<RawScript>().UpdateSelf();

        }
    }
    public int HowManyNotTaken()
    {
        int Counter = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<RawScript>().Taken == false)
            {
                Counter++;
            }
        }
       
        return Counter;
    }
}
