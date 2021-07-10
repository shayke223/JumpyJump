using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Starter : MonoBehaviour {
    public bool PlayedAnimation;
    public Animator anim;
    public GameObject PressBar;
    public PlayerScript Player;
    public Image TapObject;
    public GameObject Shop;

    public GameObject TapInstructionObj;
    public static int TimesToExplain;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("TapObjectClickable", 0.25f);
    }

    public void TapToPlay()
    {
        if (!Player.ChallengeMode)
        {
            if (TimesToExplain < 3)
            {
                TimesToExplain++;
                TapInstructionObj.SetActive(true);
                TapInstructionObj.GetComponent<Animator>().Play("HowToClick");
                Destroy(TapInstructionObj, 4);

            }
        }
        if (!PlayedAnimation)
        {
            if (!Player.ChallengeMode)
            {
                Invoke("PlayerCanPlayAgain", 0.5f);
            }
            else
            {
                Player.GameManager.StartAppear();
   
            }
                PressBar.SetActive(true);
                anim.Play("FadeOut");
                PlayedAnimation = true;
         
            // Destroy(Shop);

        }
        CheckSpecialSkills();
    }
    public void Begone() // Disappear
    {
     //   gameObject.SetActive(false);
        Destroy(gameObject,0.6f);
    }
    public void PlayerCanPlayAgain()
    {
        Player.CanPlay = true;
    }
    public void TapObjectClickable()
    {
        TapObject.raycastTarget = true;
    }
    public void CheckSpecialSkills()
    {
        switch (Player.GameManagerObject.CurrentChar)
        {
            case 14: // Ninja
                if (!Player.ChallengeMode)
                {
                    Player.UISpeed.SetActive(true);
                    Player.UISpeed.GetComponent<Animator>().Play("effect run");
                    Player.Invoke("FadeRun", 1.4f);
                    Player.ExtraDistance = 0;
                    Player.CurrentBrick = 2;
                    Player.Platforms[Player.CurrentBrick].transform.position = Player.LastBrick.transform.position + Player.TheOffset + Vector3.right * 40;


                    StartCoroutine(Player.FlyFast());
                }
                break;
            case 15: // Captain America
            Player.Shield.SetActive(true);
            PlayerScript.ShieldOn = true;
            break;
        }
    }
}
