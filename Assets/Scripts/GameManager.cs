using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GameManager : MonoBehaviour
{
    [Header("Challege")]
    public static int StaticChallengeScore; // Report the score to the leaderboard
    public int ChooseChallenge;
    public ChallengeScript[] AllChallenges;
    public PlayerScript Player;
    public bool ChallengeMode;
    public GameObject Flag;
    public Text ChallengeTextTarget;
    public Text HeadLine;

    [HideInInspector]
    public int HowManyLevels;
    private int RandomTroophy;

    public GameObject[] Alerts; // 1- shield, 2- wings, 3- Daily something

    public GameObject[] ObjRestart;
    public static bool Restart;

    [Header("Gradient Colors")]
    public Gradient[] GColors;


    [Header("Save and Load")]
    public GameObject TrailList;
    public GameObject CharList;

    public BuyProduct YesButtonAnswer;

    public GameManagerObject ManagerObject;
    public int PurchaseOption;

    public VolumeManager SliderData;

    public int AdType;

    //   public GameObject Inventory;
    public BrickText ClonePrefab;
    public GameObject ChallengeComplete;
    [Header("ads")]
    public bool CanReward;
    public GameObject Obst;
    public Text Helper;

    public static bool newBestScore;
    public GameObject GooglePlayUI;
    public void SetChallengeScore()
    {
        StaticChallengeScore = ChooseChallenge;
    }
    public void CheckAlerts()
    {

        //Shiekds
        if (Player.CheckIfNewReward())
        {
            if (Alerts[2] != null)
            {
                Alerts[2].SetActive(true);
            }

        }
        else
        {
            if (Alerts[2] != null)
            {
                Alerts[2].SetActive(false);
            }
        }
        if (Player.ShieldsCount > 0)
        {
            if (Alerts[0] != null)
            {
                Alerts[0].SetActive(true);
            }
        }
        else
        {
            if (Alerts[0] != null)
            {
                Alerts[0].SetActive(false);
            }
        }
        //wingss
        if (Player.WingsCount > 0)
        {
            if (Alerts[1] != null)
            {
                Alerts[1].SetActive(true);
            }
        }
        else
        {
            if (Alerts[1] != null)
            {
                Alerts[1].SetActive(false);
            }
        }
        if (Player.FireCount > 0)
        {
            if (Alerts[3] != null)
            {
                Alerts[3].SetActive(true);
            }
        }
        else
        {
            if (Alerts[3] != null)
            {
                Alerts[3].SetActive(false);
            }
        }
        if(Player.ShopAlert > 0)
        {
            if (Alerts[4] != null)
            {
                Alerts[4].SetActive(true);
            }
        }
        else if (Alerts[4] != null)
        {
            Alerts[4].SetActive(false);
        }
        if (newBestScore)
        {
            if (Alerts[5] != null)
            {
                Alerts[5].SetActive(true);
            }
        }
        else if (Alerts[5] != null)
        {
            Alerts[5].SetActive(false);
        }
    }

  
    private void Awake()
    {
        CanReward = true;
           ManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();

     //   if (TrailList.activeSelf)
       // {
       // //    TrailSaved = new bool[TrailList.transform.childCount];
      //  }

        if (Restart)
        {
          //  Destroy(ObjRestart[2]);
            //  PlayerScript.BrickLevel++;
            // ObjRestart[0].SetActive(false);
            Destroy(ObjRestart[0]);
            ObjRestart[1].SetActive(true);
            ObjRestart[1].GetComponent<Animator>().Play("DontMove");
            //Player.AM.Play("Complete");
            Player.CanPlay = true;
            Player.BrickLevelText.text = PlayerScript.BrickLevel.ToString();
        }
    }
    private void Start()
    {

        AudioSource audio = GetComponent<AudioSource>();

     


    }
    public void TurnOfLBAlert()
    {
        newBestScore = false;
        CheckAlerts();
    }
    public void PurchaseTrail()
    {
        if (Player.TotalMoney >= YesButtonAnswer.price)
        {
            if (YesButtonAnswer.number <= Player.TrailSaved.Length) // if i dont deviate from the array
            {
                CoinToCoinAnim("New Trail CTC");
                PlayerScript.TotalTrails++;
                Player.TrailSaved[YesButtonAnswer.number] = true;
                Player.AM.Play("Purchase");
                Player.Trail.colorGradient = GColors[YesButtonAnswer.number];
                TrailList.transform.GetChild(YesButtonAnswer.number).GetComponent<OptionScript>().Blocker.SetActive(true);
                Player.TotalMoney -= YesButtonAnswer.price;
                Player.CurrentTrail = YesButtonAnswer.number;
                CheckAlerts();
                Player.Save();
            }
        }
        else Player.AM.Play("Error");
    }
    public void PurchaseChar()
    {
        if (Player.TotalMoney >= YesButtonAnswer.price)
        {
            if (YesButtonAnswer.number <= CharList.transform.childCount) // if i dont deviate from the array
            {
                CoinToCoinAnim("Buy New Char");
                PlayerScript.TotalCharacters++;
                Player.CharSaved[YesButtonAnswer.number] = true;
                Player.AM.Play("Purchase");
                CharList.transform.GetChild(YesButtonAnswer.number).GetComponent<OptionScript>().Blocker.SetActive(true);
                Invoke("ChangeCharacter", 0.05f);
               // print(YesButtonAnswer.price);
                Player.TotalMoney -= YesButtonAnswer.price;
                Player.Save();
            }
        }
        else Player.AM.Play("Error");
    }
    public void ChoosePurchase()
    {
        switch (PurchaseOption)
        {
            case 0:
                PurchaseTrail();
             
                break;
            case 1:
                PurchaseChar();
              
                break;
            case 2:
                BuyEffect();
               
                break;
        }
        Player.MoneyText.UpdateMoney();
    }
    public void SetChoise(int num)
    {
        PurchaseOption = num;
    }
    public void ChangeCharacter()
    {
        if (Player.CharSaved[YesButtonAnswer.number])
        {
            ManagerObject.CurrentChar = YesButtonAnswer.number;
            ManagerObject.ChangeCharacter();
            Player.ChangeColliderSize();
            Player.Save();
        }
    }
    public void ChangeCharacterWithoutPurchase(int num)
    {
        if (Player.CharSaved[num])
        {
            ManagerObject.CurrentChar = num;
            ManagerObject.ChangeCharacter();
            Player.ChangeColliderSize();
            Player.Save();
        }
    }
    public void BuyEffect()
    {
        if (Player.TotalMoney >= YesButtonAnswer.price)
        {
            switch (YesButtonAnswer.number)
            {
                case 0:
                    Player.ShieldsCount++;
                    CoinToCoinAnim("Buy Shield");
                    break;
                case 1:
                    Player.ShieldsCount += 5;
                    CoinToCoinAnim("Buy Shield");
                    break;
                case 2:
                    Player.ShieldsCount += 10;
                    CoinToCoinAnim("Buy Shield");
                    break;
                case 3:
                    Player.WingsCount++;
                    CoinToCoinAnim("Buy Wings");
                    break;
                case 4:
                    Player.WingsCount+= 5;
                    CoinToCoinAnim("Buy Wings");
                    break;
                case 5:
                    Player.WingsCount += 10;
                    CoinToCoinAnim("Buy Wings");
                    break;
                case 6:
                    Player.FireCount += 1;
                    CoinToCoinAnim("Buy Fire");
                    break;
                case 7:
                    Player.FireCount += 5;
                    CoinToCoinAnim("Buy Fire");
                    break;
                case 8:
                    Player.FireCount += 10;
                    CoinToCoinAnim("Buy Fire");
                    CheckAlerts();
                    break;   
            }
            Player.TotalMoney -= YesButtonAnswer.price;
            Player.AM.Play("Purchase");
            CheckAlerts();
            Player.UpdateInventory();
            Player.Save();
        }
        else Player.AM.Play("Error");
    }
    public void  CoinToCoinAnim(string name)
    {
        // if (!Player.CoinToCoin.activeSelf)
        // {
        /*
             Player.CoinToCoin.SetActive(true);
             Player.CoinToCoin.GetComponent<Animator>().Play(name);
             StartCoroutine(Player.SetActiveWithDelay(Player.CoinToCoin, 1.2f, false));
         //}
         */
        Player.UseCoinToCoin(name, 1.5f);
    }

    public void ResetChallenge()
    {
        ChooseChallenge = 0;
        Player.Save();
    }
    public void SetChallengeBricks()
    {

       
        HowManyLevels = AllChallenges[ChooseChallenge].NextBrick.Length;
        if (ChallengeTextTarget != null)
        {
            ChallengeTextTarget.text = HowManyLevels.ToString();
        }

            for (int i = 0; i < HowManyLevels; i++)
        {
            BrickText Clone = Instantiate(ClonePrefab, transform.position + (AllChallenges[ChooseChallenge].Distances[i]) * 1.4f, Quaternion.identity);
            //  Clone.GetComponent<SpriteRenderer>();
            // Clone.SetShape(AllChallenges[ChooseChallenge].NextBrick[i]);
            Clone.ChallengeMode = true;
            Clone.NumFromChallenge = AllChallenges[ChooseChallenge].NextBrick[i];
            Clone.SetShape(AllChallenges[ChooseChallenge].NextBrick[i]);

            // Check who is moving
                for (int j = 0; j < AllChallenges[ChooseChallenge].Moving.Length; j++)
                {

                    if (AllChallenges[ChooseChallenge].Moving[j] == i)
                    {
                       // AllChallenges[ChooseChallenge].Moving[j] = 0;
                        Clone.Moving = true;

                       //Clone.StartTheMove();
                       j = AllChallenges[ChooseChallenge].Moving.Length;
                    }

            }
            // Check who is obstacle

            for (int k = 0; k < AllChallenges[ChooseChallenge].Obstacle.Length;k++)
            {

                if (AllChallenges[ChooseChallenge].Obstacle[k] == i)
                {
                    // AllChallenges[ChooseChallenge].Moving[j] = 0;
                    Instantiate(Obst, Clone.transform.position + Vector3.up * 6.3f, Quaternion.identity, Clone.transform);

                    //Clone.StartTheMove();
                    k = AllChallenges[ChooseChallenge].Obstacle.Length;
                }

            }
            // Check who is obstacle end

            if (i == AllChallenges[ChooseChallenge].NextBrick.Length - 1) {
                Instantiate(Flag, Clone.transform.position + Vector3.up * 5.9f, Quaternion.identity, Clone.transform);
            }
            HeadLine.text = ChooseChallenge.ToString();

        }

    }

    // ---------------------------------------------------------------------------- Ads
    public void ShowAd30sec(int num)
    {
        AdType = num;
        if (Advertisement.IsReady())
        {
            CanReward = false;
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleResult30 });
        }
    }
    public void HandleResult30(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                if (!CanReward)
                {
                    switch (AdType)
                    {
                        case 0:
                            Player.Revive();
                            break;
                        case 1:
                            Player.DoubleMoney();
                            break;
                        case 2:
                            Player.ShieldsCount++;
                            Player.UpdateInventory();
                            break;
                        case 3:
                            Player.WingsCount++;
                            Player.UpdateInventory();
                            break;
                    }
                    Player.Save();
                  
                    CanReward = true;
                    CancelInvoke("CanRewardAgain");
                    Invoke("CanRewardAgain", 0.5f);
                }

                //  Debug.Log("Win30");
                break;
            case ShowResult.Skipped:
                CanReward = true;
                CancelInvoke("CanRewardAgain");
                Invoke("CanRewardAgain", 0.5f);
           
                //  Debug.Log("Skip");
                break;
            case ShowResult.Failed:
                CanReward = true;
                CancelInvoke("CanRewardAgain");
                Invoke("CanRewardAgain", 0.5f);
      
                //  Debug.Log("Fail");
                break;
        }
    }
    public void CanRewardAgain()
    {
        CanReward = false;
    }

    public void StartAppear()
    {
        StartCoroutine(WaitBeforeStart());
    }
    public IEnumerator WaitBeforeStart()
    {
        yield return new WaitForSeconds(0.1f);  // Change to 3 when needed to show flag!
        StartCoroutine(TextAppear(AllChallenges[ChooseChallenge].TheHelper.Length));
    }
    public IEnumerator TextAppear(int theLength)
    {
        int counter = 0;
        while(counter < theLength)
        {
            counter++;
            Helper.text = AllChallenges[ChooseChallenge].TheHelper.Substring(0, counter);
            yield return new WaitForSeconds(0.04f);
        }
        Destroy(Helper, 3);
    }
    public void ShowLBIfConnect() // Open UI Google Play if not connected today.
    {

            if (PlayerScript.FirstTimeLoginToday == false)
            {
                GooglePlayUI.SetActive(true);
            }
        
    }
}
