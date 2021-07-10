using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProduct : MonoBehaviour {


    public int number;
    public Animator anim;
    public int price;

    public void SetNumber(int newNum) // Take number from the Shop Product and move it to Yes or No question
    {
        number = newNum;
        anim.Play("AreYouSureAppear");
    }
    public void SetPrice(int newNum)
    {
        price = newNum;
    }
}
