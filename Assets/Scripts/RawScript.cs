using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawScript : MonoBehaviour {

    [HideInInspector]
    public int MyID;
    [HideInInspector]
    public int Progress;
    public int Target;
   // [HideInInspector]
    public int Status;
    public bool Rewarded;

    public Text ProgressText, StatusText,TargetText;
    public GameObject ButtonReward;

    public enum WhatKind { Levels, Trails, Characters, Confetti, Challenges,Birds}
    public WhatKind Kind;
    public PlayerScript Player;
    public bool Taken;

    public enum RewardKind { Cash, Other }
    public RewardKind TheReward;

    public enum Itemkind { None, Wings, Shields,Trail,Character, Fire }
    public Itemkind Item;
    
    public int Number; // Cash or Item
    private void Awake()
    {
        UpdateSelf();
    }
    public void UpdateSelf()
    {
        // Do i need details or im just a level;
        switch (Kind)
        {
            case WhatKind.Levels:
                Progress = PlayerScript.BestScore;
                break;
            case WhatKind.Trails:
                Progress = PlayerScript.TotalTrails;

                break;
            case WhatKind.Characters:
                Progress = PlayerScript.TotalCharacters;
                break;
            case WhatKind.Confetti:
                Progress = PlayerScript.ConfettiCounter;
                break;
            case WhatKind.Challenges:
                Progress = PlayerScript.ChallengesComplete;
                break;
            case WhatKind.Birds:
                Progress = PlayerScript.BirdsCounter;
                break;
        }

       Rewarded = PlayerScript.Rewarded[MyID];


        TargetText.text = Target.ToString();
        if (!Rewarded && Progress >= Target)
        {
          //  Rewarded = true;
            Status = 2;
            ProgressText.color = Color.green;
            TargetText.color = Color.green;
        }

        if (Target / 2 < Progress && Status == 0)
            {
                ProgressText.color = Color.yellow;
            }
        
        ProgressText.text = Progress.ToString();
        if (Rewarded)
        {
          
            Status = 1;
        }
        switch (Status)
        {
            case 0:
                StatusText.text = "Waiting";
                StatusText.color = Color.red;
                ProgressText.color = Color.white;
                TargetText.color = Color.white;
                Taken = true;
                break;
            case 1:
                StatusText.text = "Finished";
                StatusText.color = Color.green;
                ProgressText.color = Color.green;
                TargetText.color = Color.green;
                ButtonReward.SetActive(false);
                Taken = true;
                break;
            case 2:
                ButtonReward.SetActive(true);
                ProgressText.color = Color.green;
                TargetText.color = Color.green;
                Taken = false;
                break;
        }
        UpdateParent();
    }
    public void UpdateParent()
    {
       
        transform.parent.GetComponent<AchivScript>().Status[MyID] = Status;

    }
    public void GiveReward()
    {
        PlayerScript.Rewarded[MyID] = true;
        Status = 1;
        Rewarded = true;
        UpdateSelf();
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
       

        switch (TheReward) // If cash so add the number of Cash, if Item, add the item (Array number for char and trails and amount for shield and wings)
        {
            case RewardKind.Cash:
                Player.GameManager.CoinToCoinAnim("CoinToCoin");
                Player.TotalMoney += Number;
                Player.UsePlusMoney(Number);
                transform.parent.GetComponent<AchivScript>().Player.MoneyText.UpdateMoney();
                break;

            case RewardKind.Other:
                switch (Item) {
                    case Itemkind.Character:
                      //  Player.CharSaved[Number] = true;
                        Player.TotalMoney += 600;
                        Player.OpenChest();
                        break;
                    case Itemkind.Trail:
                        Player.GameManager.CoinToCoinAnim("New Trail CTC");
                        Player.TrailSaved[Number] = true;
                        break;
                    case Itemkind.Wings:
                        Player.GameManager.CoinToCoinAnim("Buy Wings");
                        Player.WingsCount += Number;
                       
                        break;
                    case Itemkind.Shields:
                        Player.GameManager.CoinToCoinAnim("Buy Shield");
                        Player.ShieldsCount += Number;
                        break;
                    case Itemkind.Fire:
                        Player.GameManager.CoinToCoinAnim("Buy Fire");
                        Player.FireCount += Number;
                        break;

                }
                break;
        }
        Player.Save();
    }
}
