using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyText : MonoBehaviour {

    public PlayerScript Player;
    public Text Self;
   
    private void Start()
    {
        Self = GetComponent<Text>();
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        UpdateMoney();
    }

    public void UpdateMoney()
    {
       Self.text =  Player.TotalMoney.ToString();
  
    }

   
}
