using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public static bool FirstTimeLoginToday;
    public GameObject Blank;
    public Vector2 FirstPosition;
    public GameObject SettingsUI;

    public int WingsCount, ShieldsCount,FireCount;
    [HideInInspector]
    public SceneControl SceneManager;
    [HideInInspector]
    public GameObject GroundCheck;
    [HideInInspector]
    public LayerMask WhatIsGround;

    private bool isGrounded;
    private Rigidbody2D rb;
    private float GravityScale;
    private float JumpSpeed;
    [HideInInspector]
    public GameObject Accelerate;

    public float rotateSpeed;

    public float BeginSpeedJump;
    public float JumpLimit;
    [HideInInspector]
    public GameManagerObject GameManagerObject;
    public static int BrickLevel;
    [HideInInspector]
    public Text BrickLevelText;

    public GameObject LastBrick;
    [HideInInspector]
    public GameObject BeforeLastBrick;

    public GameObject[] NextBrick;

    [Header("Bar")]
    [HideInInspector]
    public Image PressBar;
    public Image Coins100;
    public Image ComboBar;
    [Header("Particles")]
    public GameObject Confetti;
    //  [HideInInspector]
    public GameObject Land;
 
    public GameObject Collect;
    
    public GameObject Die;
    [HideInInspector]
    public int FakeHP = 2;
    public static float CountPitcher;
    [Header("Troophies")]
    //  [HideInInspector]
    public GameObject[] Troophy;

    public int CurrentTro;

    [Header("Killers")]
   
    public GameObject[] Obstacles;
    [Header("Texts")]
    public GameObject OpenInv;
    public Text ShieldCounterText, WingsCounterText,FireCounterText;
    [HideInInspector]
    public MoneyText MoneyText;
    public static float ObstacleIntensity;
    public float ExtraDistance = 0;
    [Header("Ability")]
    [HideInInspector]
    public GameObject Shield, Wings;

    [HideInInspector]
    public Transform LastPosition;

    [Header("Collider")]
    private BoxCollider2D MyCol;
    [Header("Camera")]
    [HideInInspector]
    public CameraFollow Cam;

    private bool Rising;
    private bool Reviving;
    [HideInInspector]
    public GameManager GameManager;
    [HideInInspector]
    public GameObject Buffer;
    public GameObject PlayBar;
    [Header("new char")]
    public Animator NewCharAvail;
    public bool Vibrate;
    public bool ChallengeMode;
    public bool CanChangeScene;
    public bool CanPlay;

    public static bool ShieldOn;
    public static bool WingsOn;
    public AudioManager AM;
    private float Pitcher;
    [Header("GC")]
    private float RandomX, RandomY, RandomObstacleChance;
    private int RandomBrick, RandomTroophy, RandomObs, RandomDir, Randomizer;
    public Vector3 TheOffset;


    public List<BrickText> Platforms = new List<BrickText>();
    public BrickText ThePlatform;
    public int CurrentBrick;
    public int CurrentBrickKind;

    public Vector2 ColliderSize;
    private bool PlayLoseSound;

    // public GameObject[,] Trophs;
    [Header("Checking")]
    public bool AlwaysTrophy;

    public GameObject ChangeSceneParticle;
    public static int MyChar;
    public GameObject GoodAndWrong;

    [Header("Ads")]
    public static int AdCounter;
    public AdScript Ad;

    [Header("Trail")]
    public TrailRenderer Trail;

    //private float ratio;
    public static float RatioBar;
    [Header("Save for Achiv")]
    public static int TotalTrails;
    public static int TotalCharacters;
    public static int BestScore;
    public static int ChallengesComplete;
    public AchivScript AchDetails;

    public int SaveIntTrail, SaveIntChar, SaveIntBestScore;
    public AchivScript Achiv;
    public static bool[] Rewarded;

    public bool[] TrailSaved;
    public bool[] CharSaved;
    public int TotalMoney;
    public GameObject AchivBoard;

    public GameObject[] CoinToCoin;
    public int CoinToCoinNum;
    

    public static bool FireMode;
    public GameObject Fire;

    public int CurrentTrail;

    public OneSound BGMObject;
    public GameObject RewardPanel;

    public List<int> RandomArray = new List<int>();
    public bool FinishAllChar;

    public Image CharRewardPicture;

    public int ShopAlert;

    public static int ConfettiCounter;
    public static int ProgresSky;

    [Header ("Rate")]

    public GameObject RateUsObject;
    public static int RateMSGCounter;
    public int AlreadyRated;

    public int ReGameLevel;

    public SpriteRenderer GradientSky;
    public static int ThisStageCoins;
    public Text DoubleCoinsText;
    public bool CanGetDouble;
    public Animator ExtraMoneyAnimate;

    public Vector2 CubeSize;
    public Vector2 CharsSize;

    public Vector3 GroundCheckNewPos;
    public bool CanCompleteChallenge;

    public int FirstTimePlaying; // 0 = Yes, 1 = no

    public static int BirdsCounter;
    public BirdScript Bird;
    public int ScreenShots;
    [Header("plus money")]
  //  public Text PlusMoneyText;
    public GameObject[] PlusMoneyHolder;
    public int currentMoneyHolder;
    public GameObject InternetPanel;
    // Object fall;
    public GameObject KillerObj;
    public Text TimerToDisText;
    public float isCountingShake;
    public bool isShaking;
    public ClockScript Clock;
    public int Logins;
    public static float ShakeChance;
    public GameObject ShopUI;

    public GameObject UISpeed;
    public GameObject QuitGameQ;
    public GameObject Inventory;

    public bool Tutorial;
    public int[] ArrangeRandoms;
    public bool SetNewPlaces;


    public int FollowInstagram;
    public int FacebookShare;
    // ----------------------------------------------- Parameters End

    public void AlreadyRate()
    {
        AlreadyRated = 1;
       
        print("Saved");
        Save();
    }
    public GameObject UICompleteChallenge;

    private void Awake()
    {     
        
        Logins = PlayerPrefs.GetInt("Logins");

        if (ChallengeMode)
        {
            if (!Tutorial)
            {
                UICompleteChallenge.SetActive(true);
            }
        }
        CanCompleteChallenge = true;
        CanGetDouble = true;
        TrailSaved = new bool[15];
        CharSaved = new bool[17];

        CharSaved[0] = true; // First Char

        // Load();

        Trail = GetComponent<TrailRenderer>();

        Ad = GameObject.Find("Admob").GetComponent<AdScript>();
        if (AM == null)
        {
            AM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        }

        // Check interstatial

        // Check interstatial End
        CurrentBrickKind = 1; // Middle size
        if (!ChallengeMode)
        {
            if (!Tutorial)
            {
                for (int i = 1; i < Platforms.Count; i++)
                {
                    Platforms[i] = Instantiate(ThePlatform);
                    Platforms[i].gameObject.SetActive(true);
                }

                CurrentBrick = 0;
                LastBrick = Platforms[1].gameObject;
            }
        }
    }
    public void ShowAd()
    {
        AdCounter++;
        if (AdCounter % 3 == 0)
        {
            Ad.showInterstitialAd();

        }
      //  Ad.showInterstitialAd();
    }
    public void FollowOnInstagram()
    {
        FollowInstagram = PlayerPrefs.GetInt("Instagram");
        if(FollowInstagram == 0)
        {
            Application.OpenURL("https://www.instagram.com/yumaapps/");
            PlayerPrefs.SetInt("Instagram",1);
            TotalMoney += 400;
            UseCoinToCoin("FastCoin", 1);
            UsePlusMoney(400);
            MoneyText.UpdateMoney();
            Save();
        }
    }
    public void ShareOnFacebook()
    {
        FacebookShare = PlayerPrefs.GetInt("Facebook");
        if (FacebookShare == 0)
        {
            PlayerPrefs.SetInt("Facebook", 1);
            OpenShare();
            TotalMoney += 800;
            UseCoinToCoin("FastCoin", 1);
            UsePlusMoney(800);
            MoneyText.UpdateMoney();
            Save();
        }
    }
    public void OpenShare()
    {
        string GameLinkDownload = "https://play.google.com/store/apps/details?id=com.YumaApps.JumpyJump";
        string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + GameLinkDownload ;
        Application.OpenURL(facebookshare);

    }
    void Start()
    {
        
      //  FirstTimeLoginToday = true;
        //InternetPanel = GameObject.Find("Internet");
        GameManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();

        if (GameManager == null)
            GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
        MyCol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // Set image sprite depend on "MyChar"
       // GameManagerObject.CurrentChar = MyChar;
      

        // Set image sprite depend on "MyChar" End

        // CountPitcher = 0;
        if (!ChallengeMode)
        {
            if (!Tutorial)
            {
                Platforms[0].transform.position = new Vector3(transform.position.x - 7, transform.position.y - 10, transform.position.y);
                Platforms[1].transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.y);
                Platforms[2].transform.position = new Vector3(transform.position.x + 7, transform.position.y - 10, transform.position.y);
            }
        }

        Pitcher = 1;
        if (!GameManager.Restart)
        {
            Blank.SetActive(true);
            Blank.GetComponent<Animator>().Play("StartAppear");
            StartCoroutine(SetActiveWithDelay(Blank, 0.4f, false));
            //Intensity shake
            ShakeChance = 0.3f;
            ThisStageCoins = 0;
            ProgresSky = 0;
            RateMSGCounter++;
            ObstacleIntensity = 0f;
            BrickLevel = 0;
            AM.Play("Opening");
            Rewarded = new bool[33];
            if (FireMode) { FireMode = false; }
            if (ShieldOn)
            {
                ShieldOn = false;
                Shield.SetActive(false);
            }
            if (WingsOn)
            {
                WingsOn = false;
                Wings.SetActive(false);
            }
        }
        else
        {
            GradientSky.color = GameManagerObject.SkyColors[ProgresSky];
            GameManager.Restart = false;
            if (FireMode) { Fire.SetActive(true); }
            StartCoroutine(SetFireOff(15));
            UpdateBar(false);
            StartCoroutine(AppearFromFade());
            Blank.SetActive(true);
            Blank.GetComponent<Animator>().Play("BlankAppear");
            StartCoroutine(SetActiveWithDelay(Blank, 0.9f, false));
        }


        GravityScale = rb.gravityScale;

        if (ShieldOn) { Shield.SetActive(true); }
        if (WingsOn) { Wings.SetActive(true); }

        UpdateInventory();
 
        // ChangeTrail();
        Load();
        ChallengesComplete = GameManager.ChooseChallenge;
        if (ChallengeMode)
        {
            if (!Tutorial)
            {
                GameManager.SetChallengeBricks();
            }
        }
        GameManager.ChangeCharacterWithoutPurchase(GameManagerObject.CurrentChar);
        //-------------------- Set Volume From Load
        AM.SetVolume();
        BGMObject.SetVolume();
        AudioListener.volume = AudioManager.CurrentVol;
        //-------------------- Set Volume From Load End

        ChangeTrail(CurrentTrail);
        GameManager.CheckAlerts();
        //    TotalMoney = 500;
        UpdateComboBar();
        if (!GameManager.Restart)
        {
          
            if ((RateMSGCounter + 1) % 10 == 0 && AlreadyRated == 0) { RateUsObject.SetActive(true); } // Ask to rate
        }
        ChangeColliderSize();

        if (Logins == 2)
        {
            PlayerPrefs.SetInt("Logins", 3);

            if (!ChallengeMode)
            {

                //    print(QualitySettings.GetQualityLevel());
                CharSaved[0] = true;
                TrailSaved[0] = true;

                FirstTimePlaying++;
                AudioManager.CurrentVol = 1;
                AudioManager.BackgroundVolume = 0.6f;
                AudioManager.EffectsVolume = 1f;
                SettingsUI.SetActive(true);
                Save();
            }
    
        }


        /*
        if (FirstTimePlaying == 0)
        {

            if (!ChallengeMode)
            {
                FirstTimePlaying++;
                Save();
                GameManagerObject.ChangeScene("Preload Scene 2");
            }
        }

        

                if (FirstTimePlaying == 1)
                  {
          
            if (!ChallengeMode)
            {

                //    print(QualitySettings.GetQualityLevel());
                CharSaved[0] = true;
                TrailSaved[0] = true;
                
                FirstTimePlaying++;
                AudioManager.CurrentVol = 1;
                AudioManager.BackgroundVolume = 1;
                AudioManager.EffectsVolume = 0.3f;
                SettingsUI.SetActive(true);
              
            }
        //    Save();
        }
                */
        //internet
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            InternetPanel.SetActive(true);
        }
        GameManager.SetChallengeScore();
       
    }
    public void CheckInternetAgain()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            InternetPanel.SetActive(false);
        }
    }
    public void Save()
    {
        // Achiv.SetActive(true);
        //  SaveIntBestScore = BestScore;
        // SaveIntChar = TotalCharacters;
        //  SaveIntTrail = TotalTrails;

        SaveManager.SavePlayer(this);
    }

    public void Load()
    {

        bool[] LoadedInfo = SaveManager.LoadPlayerBools();

        for (int i = 0; i < Rewarded.Length; i++)
        {
            Rewarded[i] = LoadedInfo[i];
        }

        int[] LoadedInfo2 = SaveManager.LoadPlayerInts();
        BestScore = LoadedInfo2[0];
        TotalTrails = LoadedInfo2[1];
        TotalCharacters = LoadedInfo2[2];
        ShieldsCount = LoadedInfo2[3];
        WingsCount = LoadedInfo2[4];
        TotalMoney = LoadedInfo2[5];
        FireCount = LoadedInfo2[6];
        GameManagerObject.CurrentChar = LoadedInfo2[7];
        CurrentTrail = LoadedInfo2[8];
        ShopAlert = LoadedInfo2[9];
        ConfettiCounter = LoadedInfo2[10];
        AlreadyRated = LoadedInfo2[11];
        GameManager.ChooseChallenge = LoadedInfo2[12];
        ChallengesComplete = LoadedInfo2[13];
        FirstTimePlaying = LoadedInfo2[14];
        BirdsCounter = LoadedInfo2[15];


        bool[] LoadedInfo3 = SaveManager.LoadPlayerTrails();
        for (int i = 0; i < TrailSaved.Length; i++)
        {
            TrailSaved[i] = LoadedInfo3[i];
        }

        bool[] LoadCharInfo = SaveManager.LoadPlayerChars();

        for (int i = 0; i < CharSaved.Length; i++)
        {
            CharSaved[i] = LoadCharInfo[i];
        }


        float[] LoadedInfo4 = SaveManager.LoadPlayerFloats();
        AudioManager.CurrentVol = LoadedInfo4[0];
        AudioManager.BackgroundVolume = LoadedInfo4[1];
        AudioManager.EffectsVolume = LoadedInfo4[2];
    }
    public void ResetAll()
    {
        
        BirdsCounter = 0;
        GameManager.ChooseChallenge = 0;
        FirstTimePlaying = 0;
        AlreadyRated = 0;
        BestScore = 0;
        TotalCharacters = 0;
        TotalTrails = 0;
        for (int i = 0; i < Rewarded.Length; i++)
        {
            Rewarded[i] = false;
        }
        for (int i = 0; i < TrailSaved.Length; i++)
        {
            TrailSaved[i] = false;
        }
        for (int i = 0; i < CharSaved.Length; i++)
        {
            CharSaved[i] = false;
        }
        CharSaved[0] = true;
        TrailSaved[0] = true;
        ConfettiCounter = 0;
        ShieldsCount = 0;
        WingsCount = 0;
        FireCount = 0;
        TotalMoney = 0;
        Achiv.UpdateAchiv();
        UpdateInventory();
        MoneyText.UpdateMoney();
        PlayerPrefs.SetInt("Logins", 0);
        PlayerPrefs.SetInt("FacebookShare", 0);
        Save();

    }
    public void ShowWhatGood()
    {
        GoodAndWrong.SetActive(true);
        Destroy(GoodAndWrong, 3);
    }
   public void QuitGame()
    {
        Application.Quit();
    }
    public void SetTimeScale(float thetime)
    {
        Time.timeScale = thetime;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ChallengeMode)
            {
                if (!AchivBoard.activeSelf)
                {
                    if (!SettingsUI.activeSelf)
                    {
                        if (!ShopUI.activeSelf)
                        {
                            if (!Inventory.activeSelf)
                            {
                                if (!SceneManager.LosePanel.activeSelf)
                                {
                                    QuitGameQ.SetActive(true);
                                    Time.timeScale = 0;
                                    //Application.Quit();
                                }
                            }
                        }
                    }
                }
            }
            if (SceneManager.LosePanel.activeSelf)
            {
                if (!ChallengeMode)
                {
                    ShowAd();
                    GameManagerObject.ChangeScene("Preload Scene");
                 
                }
                else
                {
                    ShowAd();
                    GameManagerObject.ChangeScene("Preload Scene 3");
                }
            }
            AchivBoard.SetActive(false);
            SettingsUI.SetActive(false);
            ShopUI.SetActive(false);
            if (Inventory.activeSelf)
            {
                OpenInv.SetActive(true);
                Inventory.SetActive(false);
                CanPlay = true;
            }
            //SceneManager.LosePanel.SetActive(false);
            AM.Play("Click");
        }
        if (isShaking)
        {
            if (isCountingShake >= 0) { isCountingShake -= Time.deltaTime; TimerToDisText.text = isCountingShake.ToString("F0"); }
            else
            {
                transform.parent = null;
                isShaking = false; LastBrick.SetActive(false);
                Clock.gameObject.SetActive(false);
                GameObject killer = Instantiate(KillerObj, LastBrick.transform.position + Vector3.down * 8, Quaternion.identity);
                //shaking1
            }
        }
        /*
        if (Input.GetKeyDown("r"))
        {
            // QualitySettings.SetQualityLevel(1);
            Application.CaptureScreenshot("SS" + ScreenShots + ".png");
            ScreenShots++;
        }
            if (Input.GetKeyDown("a"))
        {
            Blank.SetActive(true);
            Blank.GetComponent<Animator>().Play("BlankAppear");
        }
        */
        isGrounded = Physics2D.OverlapCircle(GroundCheck.transform.position, 0.1f, WhatIsGround);
        if (!isGrounded) // If the player on the air, so rotate him until he reaches the peak
        {

            JumpSpeed = 0;
            if (rb.velocity.y >= 0)
            {

                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
            }
            else
            {
                // float dist = Vector3.Distance(Groundddd.transform.position, transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 100);
            }
        }
        if (CanPlay)
        {
            if (isGrounded)
            {

                if (Input.touchCount > 0 || (Input.GetMouseButton(0)) || Input.GetKey("space"))
                {
                    UpdateBar(true);
                    rb.gravityScale = 0;
                    if (JumpSpeed < JumpLimit)
                    {
                        JumpSpeed += 18 * Time.deltaTime;
                    }
                    Accelerate.SetActive(true);


                }

                if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space")) //When release touch, everything happend
                {
                    
                    AM.sounds[0].pitch = Random.Range(0.95f, 1.05f);
                    AM.Play("Blip");
                    rb.gravityScale = GravityScale;
                    Jump();
                    Accelerate.SetActive(false);
                }

            }
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space"))
            {
                rb.gravityScale = GravityScale;
            }
        }
    }
    public void Jump()
    {
        rb.velocity = new Vector3(JumpSpeed / 1.5f, BeginSpeedJump + JumpSpeed, 0); // Minimum speed to jump included.
        JumpSpeed = 0;
    }
    public IEnumerator SetActiveWithDelay(GameObject obj, float delay, bool TorF)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(TorF);
        // CanPlay = CanPlayAfter;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

            if (other.CompareTag("Ground"))  //Open new Brick with option to troophy
        {
            if (MyCol.enabled)
            {

               // transform.SetParent(other.transform);
                LastPosition.transform.position = transform.position;
                LastPosition.transform.parent = other.transform;
               // LastPosition.transform.SetParent(other.transform);

                LastPosition.transform.position = Vector3.Lerp(LastPosition.transform.position, other.transform.position, 25 * Time.deltaTime); //prevent death rehearsal
                rb.velocity = new Vector3(0, 0, 0);


                if (LastBrick != other.gameObject) // If im standing on a new brick, so do something
                {
                    if ((BrickLevel + 1) % ReGameLevel == 0) // Hahaha
                    {
                        ProgresSky++;
                        if(ProgresSky >= GameManagerObject.SkyColors.Length)
                        {
                            ProgresSky = 0;
                        }
                        StartCoroutine(RiseToChangeScene());


                    }
                    if (FireMode)
                    {
                        Cam.useCameraShake(0.5f, 0.2f);
                        /*
                        CoinToCoin.SetActive(true);
                        CoinToCoin.GetComponent<Animator>().Play("FastCoin");
                        StartCoroutine(SetActiveWithDelay(CoinToCoin, 1f, false));
                        */
                        UseCoinToCoin("FastCoin", 1);
                      
                        TotalMoney += 3;
                        ThisStageCoins += 3;
                        UsePlusMoney(3);
                        MoneyText.UpdateMoney();
                    }

                    if (CurrentBrick == Platforms.Count - 1) { CurrentBrick = 0; } else { CurrentBrick++; }
                    if (Pitcher >= 1.15f)
                    {
                        /*/CoinToCoin 
                        CoinToCoin.SetActive(true);
                        CoinToCoin.GetComponent<Animator>().Play("CoinToCoin");
                        StartCoroutine(SetActiveWithDelay(CoinToCoin, 2, false));
                        */
                        UseCoinToCoin("CoinToCoin", 2);
                        UsePlusMoney(10);
                        AM.Play("Extra"); AM.sounds[1].pitch = 0.95f; Pitcher = 0.95f; CountPitcher = -1;

                        //GameObject con = Instantiate(Confetti, transform.position + Vector3.up*10 + Vector3.right*2, Quaternion.identity);
                        Confetti.SetActive(true);
                        Confetti.transform.position = new Vector2(transform.position.x + 2, transform.position.y + 10);
                        TotalMoney += 10;
                        ThisStageCoins += 10;
                        ConfettiCounter++;
                        MoneyText.UpdateMoney();
                        StartCoroutine(SetActiveWithDelay(Confetti, 5, false));

                        //     Destroy(con, 4);
                    }
                    else
                    {
                        //  AM.Play("Complete");
                        AM.PlayLandSound();
                    }
                    AM.sounds[1].pitch += 0.05f;
                    Pitcher += 0.05f;
                    CountPitcher++;
                    UpdateComboBar();
                    if (Vibrate)
                    {
                        Handheld.Vibrate();
                    }
                    FakeHP = 3; // FakeHP its for testing how much times the player on the edge and then push him left.
                                 //  Invoke("NewDestroy", 3); // after Jump to new brick, destroy the last one.
                                 // StartCoroutine(LastBrick.GetComponent<BrickText>().SelfDestroy());
                    BeforeLastBrick = LastBrick;
                    LastBrick = other.gameObject; //Set the current brick
                    BrickLevel++;
                    ObstacleIntensity += 0.01f;
                    if(ObstacleIntensity > 0.92f) { ObstacleIntensity = 0.92f; }
                    BrickLevelText.text = BrickLevel.ToString();

                    //--------------------------------------------------------------------------------Gap between floors
                    //Shaking
                    ShakeChance += 0.02f;
                    if(ShakeChance > 0.92f) { ShakeChance = 0.92f; }
                    if (!ChallengeMode)
                    {
                        float randomIfShake = Random.Range(ShakeChance, 1f);
                        if (BrickLevel > 1 && randomIfShake > 0.95f)
                        {
                           
                                
                                Clock.gameObject.SetActive(true);
                                isCountingShake = 2.5f;
                                isShaking = true;

                                other.GetComponent<Animator>().Play("Shake");
                                //Clock.gameObject.SetActive(true);
                                Clock.Counter = 1;


                            
                        }
                        else
                        {
                            isCountingShake = 3;
                            isShaking = false;
                            Clock.gameObject.SetActive(false);
          
                        }
                    }

                    CurrentBrickKind = RandomBrick; //Before setting a new Random, Remember the last one.
                    RandomBrick = Random.Range(0, 4); // Which Brick its gonna be
                    if (RandomBrick == 3)
                    {
                        RandomBrick = 1;
                        if (ObstacleIntensity < 0.3f)
                        {
                            // If its not on hard mode so set normal brick instead of moving brick.
                        }
                    }
                    ExtraDistance = 0;

                    if (!ChallengeMode)
                    {
                        if (!Tutorial)
                        {
                            Platforms[CurrentBrick].SetShape(RandomBrick);
                        }
                    }
                    if (CurrentBrickKind == 2)
                    {
                        switch (RandomBrick)
                        {
                            case 0:
                                ExtraDistance = 1f;

                                break;
                            case 1:
                                ExtraDistance = 2f;

                                break;
                            case 2:
                                ExtraDistance = 3f;

                                break;
                        }

                    }
                    else
                    if (RandomBrick == 2)
                    {
                        switch (CurrentBrick)
                        {
                            case 0:
                                ExtraDistance = 1f;

                                break;
                            case 1:
                                ExtraDistance = 2f;

                                break;
                            case 2:
                                ExtraDistance = 3f;

                                break;
                        }


                    }
                    transform.parent = other.transform;

                    RandomX = Random.Range(4.5f + ExtraDistance, 4.5f + ExtraDistance); //Where the next Brick gonna be
                    RandomY = Random.Range(-2, 3); //Where the next Brick gonna be
                    TheOffset = new Vector3(1.4f * RandomX, 1.4f * RandomY, 0);

                    //--------------------------------------------------------------------------------Gap between floors End
                    if (!ChallengeMode)
                    {
                      
                        GameObject RememberLast = Platforms[CurrentBrick].gameObject;
                        Platforms[CurrentBrick].gameObject.SetActive(true);


                            Platforms[CurrentBrick].transform.position = LastBrick.transform.position + TheOffset;
                       
                            Platforms[CurrentBrick].Starter();



                        RandomTroophy = Random.Range(0, 5); // Chance of 25% to get troophy;
                        if (AlwaysTrophy) { RandomTroophy = 3; }
                        if (RandomTroophy == 3)
                        { 
                            
                                SetNewPlaces = true;
                                RandomTroophy = Random.Range(0, Troophy.Length); //Here i use the same random to test what troophy gonna be.

                                if (RandomBrick == 0) { RandomTroophy = Random.Range(0, 4); } // if it is small brick so the price should be money
                            
                            Troophy[RandomTroophy].SetActive(true);
                          
                                Troophy[RandomTroophy].transform.position = LastBrick.transform.position + TheOffset + Vector3.up * 5.9f;
                            
                            // GameObject Tro = Instantiate(Troophy[RandomTroophy], LastBrick.transform.position + TheOffset + Vector3.up * 5.9f, Quaternion.identity);
                            //Destroy(Tro, 10);
                            // OddTroophy.SetActive(true);
                            //  OddTroophy.transform.position = LastBrick.transform.position + TheOffset + Vector3.up * 5.9f;
                        }


                        if (RandomBrick != 0)
                        {

                            RandomObstacleChance = Random.Range(ObstacleIntensity, 1);
                            if (RandomObstacleChance > 0.9f && BrickLevel > 15) // The chance slightly raise.
                            {
                                RandomObs = Random.Range(0, Obstacles.Length);
                                RandomDir = Random.Range(-1, 2);
                                if (SetNewPlaces)
                                {
                                    SetNewPlaces = false;
                                    RandomDir = ArrangeRandoms[ Random.Range(0, ArrangeRandoms.Length)];
                                }
                                

                                GameObject ObstaclePrefab = Instantiate(Obstacles[RandomObs], LastBrick.transform.position + TheOffset + Vector3.up * 6.3f + (Vector3.right * RandomDir), Quaternion.identity);
                                //Destroy(ObstaclePrefab, 20);
                            }
                        }
   
                    }

                }
                else { AM.sounds[1].pitch = 1; Pitcher = 1; CountPitcher = 0; UpdateComboBar(); }
                // AM.updateLandSound();

                //------------------------------------------------------------------------------Complete Challenge
                /*
                if (ChallengeMode)
                {
                    if (BrickLevel == GameManager.AllChallenges[GameManager.ChooseChallenge].NextBrick.Length) // If i am on the last stage
                    {
                        CompleteChallenge();
                    }
                }
                */
                //------------------------------------------------------------------------------Complete Challenge End
                if (!isShaking)
                {
                    if (!Tutorial)
                    {
                        other.GetComponent<Animator>().Play("Whitening");
                    }
                }
                else
                {
                    transform.parent = null;
                }
                //GameObject Particles = Instantiate(Land, GroundCheck.transform.position + Vector3.up / 3, Quaternion.identity);
                FakeHP -= 1; if (FakeHP < 0) { rb.velocity = new Vector3(-2, 5, 0); FakeHP = 6; } // If im on the edge and cant get away
                Land.SetActive(true);
                StartCoroutine(SetActiveWithDelay(Land, 0.5f, false));
                Land.transform.position = GroundCheck.transform.position + Vector3.up / 3;
                //Destroy(Particles, 2);





                float RandomBirdChance = Random.Range(0, 1f);

                //Bird Instantiate
                if(RandomBirdChance > 0.92f)
                {
                    if (!Bird.gameObject.activeSelf)
                    {
                        Bird.gameObject.SetActive(true);
                        Bird.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 5,7.71f);
                        Bird.Timer = 6;
                       
                        
                    }
                }
            }
        }
        if (other.CompareTag("Killer"))
        {
            if (!WingsOn)
            {
                if (CanChangeScene)
                {
       
                   
                
                    // BGMObject.TheSource.volume = 0;

                    Clock.gameObject.SetActive(false);
                    AM.Play("Lose");
                    PlayLoseSound = false;
                    CanPlay = false;
                    Cam.Player = null;
                    //rb.gravityScale = 0;
                    Invoke("SetZero", 0.3f);
                    Invoke("ShowLose", 0.5f);
                    MyCol.enabled = false;
                    CanChangeScene = false;
                    if (!ChallengeMode)
                    {
                        //  GameManagerObject.ChangeScene("Preload Scene");
                    }
                    //  else GameManagerObject.ChangeScene("Challenges");
                }
            }
            else
            {
                //CancelInvoke("NewDestroy");
                //  StopCoroutine(LastBrick.GetComponent<BrickText>().SelfDestroy());
                if (Rising == false)
                {

                    AM.Play("RiseUp");
                    Rising = true;
                    StartCoroutine(RiseUp());
                }

            }
        }
        if (other.CompareTag("Troophy"))
        {
          
                GiveTroophy(other.gameObject);
            
            AM.Play("Price");

            Collect.transform.position = transform.position + new Vector3(0, 2f, 0);
            StartCoroutine(SetActiveWithDelay(Collect, 1, false));
            Collect.SetActive(true);
            
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Killer B")) //Obs
        {
            AM.Play("Boom");
            if (Reviving == true || FireMode) { Destroy(other.gameObject); GameObject BoomParticle = Instantiate(Die, transform.position, Quaternion.identity); }
            else if (MyCol.enabled)
            {
               // AM.Play("Boom");
                PlayLoseSound = true;
                Cam.useCameraShake(0.5f, 0.5f);

                GameObject BoomParticle = Instantiate(Die, transform.position, Quaternion.identity);
                Destroy(BoomParticle, 3);


                if (!ShieldOn)
                {
                    isShaking = false;
                    Clock.gameObject.SetActive(false);
                    Collect.SetActive(true);
                    StartCoroutine(SetActiveWithDelay(Collect, 1, false));
                    Collect.transform.position = transform.position + new Vector3(0, 2f, 0);

                    CanPlay = false;

                    //gameObject.SetActive(false);
                   // AM.Play("Boom");
                    Invoke("ShowLose", 1.5f);
                    Destroy(other.gameObject);
                }
                else
                {
                    Destroy(other.gameObject);
                    Shield.GetComponent<Animator>().Play("Remove");
                    Invoke("ShieldOff", 1);
                }
            }
        }
        if (other.CompareTag("SetFire"))
        {

            AM.Play("Fire");

            Fire.SetActive(true);
            FireMode = true;
            StartCoroutine(SetFireOff(20));
            Cam.useCameraShake(1, 0.2f);
            other.gameObject.SetActive(false);

        }
        if (other.CompareTag("Flag"))
        {
            CompleteChallenge();


        }
        if (other.CompareTag("Bird"))
        {

            GameObject TakeBird = Instantiate(Die, transform.position, Quaternion.identity);
            Destroy(TakeBird, 3);
            AM.Play("BirdSound");
            UseCoinToCoin("CoinToCoin", 2);
            UsePlusMoney(15);
            TotalMoney += 15;
            MoneyText.UpdateMoney();
            other.gameObject.SetActive(false);
            BirdsCounter++;
      
        }
        if (other.CompareTag("Fly"))
        {
            UISpeed.SetActive(true);
            UISpeed.GetComponent<Animator>().Play("effect run");
            Invoke("FadeRun", 1.4f);
            StartCoroutine(FlyFast());
            ExtraDistance = 0;
            Platforms[CurrentBrick].transform.position = LastBrick.transform.position + TheOffset + Vector3.right * 40;
            for (int i = 0; i < Troophy.Length; i++)
            {
                Troophy[i].SetActive(false);
            }
            other.gameObject.SetActive(false);
        }
        }
        public void FadeRun()
    {
        UISpeed.GetComponent<Animator>().Play("FadeRun");
        StartCoroutine(SetActiveWithDelay(UISpeed, 0.5f, false));
    }
        public void CompleteChallenge()
    {
        if (CanCompleteChallenge)
        {
            CanCompleteChallenge = false;
            GameManager.ChallengeComplete.SetActive(true);
            /*
            CoinToCoin.SetActive(true);
            CoinToCoin.GetComponent<Animator>().Play("CoinToCoin");
            StartCoroutine(SetActiveWithDelay(CoinToCoin, 2, false));
            */
            UseCoinToCoin("CoinToCoin", 2);
            TotalMoney += 20;
            UsePlusMoney(20);
            MoneyText.UpdateMoney();
            SetState("Can't Play");
            AM.Play("ChallengeComplete");
            if (GameManager.ChooseChallenge < GameManager.AllChallenges.Length - 1) // Next Stage only if available!
            {
                GameManager.ChooseChallenge++;
            }
            else print("Finished All Challenges");
            Save();
        }
    }
    public void FadeVolume(bool FadeInOrOut)
    {
        AM.StartFade(FadeInOrOut,3);
       // AM.Stop("BGM");
       
    }
    public IEnumerator SetFireOff(float time)
    {
        yield return new WaitForSeconds(time);
        FireMode = false;
        Fire.SetActive(false);

    }
    public void UpdateBar(bool CalculateRatio)
    {
        if (CalculateRatio)
        {
            RatioBar = JumpSpeed / JumpLimit;
        }
        if (RatioBar > 0 && RatioBar < 0.5f) { PressBar.color = GameManagerObject.Colors[0]; }
        if (RatioBar >= 0.5f && RatioBar < 0.8f) { PressBar.color = GameManagerObject.Colors[1]; }
        if (RatioBar >= 0.8f) { PressBar.color = GameManagerObject.Colors[2]; }
        PressBar.rectTransform.localScale = new Vector3(RatioBar, 1, 1);

    }
    public void UpdateComboBar()
    {
        float ratioCombo = CountPitcher / 5;
        ComboBar.rectTransform.localScale = new Vector3(ratioCombo, 1, 1);
    }
    public void ShowLose()
    {
        for (int i = 0; i < Platforms.Count; i++)
        {

        }
        if (BrickLevel > BestScore) { BestScore = BrickLevel;GameManager.newBestScore = true; GameManager.CheckAlerts(); }
     //   print(BestScore);
        Save();
        if (PlayLoseSound)
        {
            AM.Play("Lose");
        }
        if (!ChallengeMode)
        {
            ShieldCounterText.transform.parent.gameObject.SetActive(false);
            OpenInv.SetActive(true);
        }
        PlayBar.GetComponent<Animator>().Play("PressBar Dissapear");
        SceneManager.LosePanel.SetActive(true);
        SceneManager.LosePanel.GetComponent<Animator>().Play("Panel Appear");
        StartCoroutine(Buffer.GetComponent<Buffer>().FillIn());
        if (!ChallengeMode)
        {
            if (CanGetDouble) { ExtraMoneyAnimate.Play("Extra Coins"); }
            if (ThisStageCoins == 0) { ThisStageCoins = 20; }
            DoubleCoinsText.text = ThisStageCoins.ToString();
              Update100CoinsBar();
        }
    }
    public void Update100CoinsBar()
    {

        float ratio = TotalMoney / 600f;
        if (ratio >= 1)
        {
            ratio = 1;
            //  NewCharAvail.GetComponent<Animator>().Play("New Character available");
            NewCharAvail.SetBool("CanGet", true);
        }
        else
        {
            NewCharAvail.SetBool("CanGet", false);
        }
        Coins100.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }
    public void ShieldOff()
    {
        Shield.SetActive(false);
        ShieldOn = false;
    }
    public void GiveTroophy(GameObject other)
    {
        int TroNum = other.GetComponent<TroophyScript>().TroophyNumber;
        switch (TroNum)

        {
            case 0: // Money
                
                Randomizer = Random.Range(2, 6);

                TotalMoney += Randomizer;
                ThisStageCoins += Randomizer;
                MoneyText.UpdateMoney();

                UseCoinToCoin("CoinToCoin", 2);
                UsePlusMoney(Randomizer);
                // Destroy(GainText, 2);
                break;
            case 1: // Shield
                if (!FireMode)
                {
                    Shield.SetActive(true);
                    Shield.GetComponent<Animator>().Play("Appear");
                    ShieldOn = true;
                    UpdateInventory();
                }
                break;
            case 2: // Wings
                Wings.SetActive(true);
                WingsOn = true;
                UpdateInventory();
                break;



        }
    }
    public void ExtraInv(int num)
    {

        switch (num)
        {
            case 0:
                ShieldsCount++;
                break;
            case 1:
                WingsCount++;
                break;
        }

        UpdateInventory();
        Save();

    }
    public IEnumerator RiseUp()
    {
        isShaking = false;
        Clock.gameObject.SetActive(false);
        StartCoroutine(Cam.CameraZoomIn(7));
        Cam.offset = new Vector3(0, 1.2f, -1);
        float Counter = 0.0f;
        rb.gravityScale = 0;
        CanPlay = false;
        rb.velocity = new Vector3(0, 0, 0);
        while (Counter < 2)
        {
            Counter += Time.deltaTime;
            transform.position += new Vector3(0, 6 * Time.deltaTime, 0);
            yield return null;

        }
        StartCoroutine(GoBack());
        LastBrick.gameObject.SetActive(true);

    }
    public IEnumerator GoBack()
    {
        isShaking = false;
        Clock.gameObject.SetActive(false);
        MyCol.enabled = false;
        float Counter = 0.0f;
        while (Counter < 1)
        {
            Counter += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, LastPosition.transform.position + Vector3.up * 4, 4 * Time.deltaTime);
            yield return null;
        }
        Wings.SetActive(false);
        WingsOn = false;
        rb.gravityScale = GravityScale;
        Cam.offset = new Vector3(2.5f, 0.82f, -1);
        MyCol.enabled = true;
        StartCoroutine(DetectKillers());
        Rising = false;
        if (OpenInv.activeSelf)
        {
            CanPlay = true;
        }
        StartCoroutine(Cam.CameraZoomOut(10));
        Accelerate.SetActive(false);
    }
    public void UpdateInventory()
    {
        FireCounterText.text = FireCount.ToString();
        ShieldCounterText.text = ShieldsCount.ToString();
        WingsCounterText.text = WingsCount.ToString();
    }
    public void NewDestroy() // Can cancell the invoke after
    {
        //   Destroy(BeforeLastBrick, 4);
    }
    public void SetState(string State)
    {
        switch (State)
        {
            case "Can Play":
                Invoke("SetCanPlay", 0.25f);
                break;
            case "Can't Play":
                CanPlay = false;
                break;
        }
    }
    public void SetCanPlay() // For invoke
    {
        CanPlay = true;
    }
    public IEnumerator DetectKillers()
    {
        Reviving = true;
        float Counter = 0.0f;
        while (Counter < 2)
        {
            Counter += Time.deltaTime;
            yield return null;
        }
        Reviving = false;
    }
    public void Admin()
    {
        Instantiate(Troophy[1], transform.position, Quaternion.identity);
        Instantiate(Troophy[2], transform.position, Quaternion.identity);
    }
    public void Revive()
    {

        LastBrick.gameObject.SetActive(true);
        isCountingShake = 2.8f;
        PlayBar.GetComponent<Animator>().Play("PressBar Appear");
        
        Cam.Player = gameObject;
        CanChangeScene = true;
        MyCol.enabled = true;
        rb.gravityScale = GravityScale;
        if (!ChallengeMode)
        {
            if (OpenInv.activeSelf)
            {
                CanPlay = true;
            }
        }else CanPlay = true;
        transform.position = LastPosition.transform.position + Vector3.up * 5;
        CanGetAgain();
        //  StartCoroutine(RiseUp());
    }
    public void SetZero()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void UseItem(int num)
    {
        if (!Rising && isGrounded)
        {

            switch (num)
            {
                case 0:
                    if (ShieldsCount > 0 && !ShieldOn)
                    {
                        if (!FireMode)
                        {
                            ShieldsCount--;
                            Troophy[1].SetActive(true);
                            Troophy[1].transform.position = transform.position;
                            ShieldCounterText.transform.parent.gameObject.SetActive(false);
                            SetState("Can Play");
                            OpenInv.SetActive(true);
                            Save();
                        }
               
                    }
                    break;
                case 1:
                    if (WingsCount > 0 && !WingsOn)
                    {

                        Troophy[2].SetActive(true);
                        Troophy[2].transform.position = transform.position;
                        WingsCount--;
                        ShieldCounterText.transform.parent.gameObject.SetActive(false);
                        OpenInv.SetActive(true);
                        SetState("Can Play");
                        Save();
                    }
                    break;
                case 2:
                    if (FireCount > 0 && !FireMode)
                    {

                        Troophy[3].SetActive(true);
                        Troophy[3].transform.position = transform.position;
                        FireCount--;
                        FireCounterText.transform.parent.gameObject.SetActive(false);
                        OpenInv.SetActive(true);
                        SetState("Can Play");
                        Save();
                    }
                    break;

            }
            GameManager.CheckAlerts();
        }
    }
    public void CollectGar()
    {
        System.GC.Collect();

    }
    public void Restart()
    {
       
        GameManager.Restart = true;
        GameManagerObject.ChangeScene("Preload Scene");
        // LastBrick = null;
        MoneyText.UpdateMoney();

    }
    public IEnumerator WaitAndReset()
    {

        Instantiate(ChangeSceneParticle, transform.position, Quaternion.identity);
        float Timer = 1f;

        while (Timer > 0.1f)
        {
            transform.localScale = new Vector3(Timer, Timer, 0);
            Timer -= Time.deltaTime;
            yield return null;
        }
        //   Land.SetActive(true);
        //   Land.transform.position = GroundCheck.transform.position;
        Save();
        Restart();
    }
    public IEnumerator RiseToChangeScene()
    {
        AM.Play("Teleport");
        // StartCoroutine(Cam.CameraZoomIn(7));
        //   Cam.offset = new Vector3(0, 1.2f, -1);
        float Counter = 0.0f;
        rb.gravityScale = 0;
        CanPlay = false;
        rb.velocity = new Vector3(0, 0, 0);
        while (Counter < 0.5f)
        {
            Counter += Time.deltaTime;

            transform.position += new Vector3(0, 8 * Time.deltaTime, 0);
            yield return null;

        }


        StartCoroutine(WaitAndReset());


    }
    public IEnumerator AppearFromFade()
    {
        AM.Play("Teleport");
        Instantiate(ChangeSceneParticle, transform.position, Quaternion.identity);

        transform.localScale = new Vector3(0, 0, 0);
        float Counter = 0.0f;

        while (Counter < 1.3f)
        {
            transform.localScale = new Vector3(Counter, Counter, 1);
            Counter += Time.deltaTime;
            yield return null;

        }
        transform.localScale = new Vector3(1.3f, 1.3f, 1);
    }
    public void ReCalculate()
    {
        GameManager.Restart = false;
    }
    public void ChangeTrail(int num)
    {
        Trail.colorGradient = GameManager.GColors[num];
        CurrentTrail = num;
        Save();

    }
    public void PlaySound(string SoundName)
    {
        AM.Play(SoundName);
    }
    public bool CheckIfNewReward()
    {
        AchivBoard.SetActive(true);


        bool YesOrNo = false;
        for (int i = 0; i < Rewarded.Length; i++)
        {
            if (Achiv.HowManyNotTaken() > 0) { YesOrNo = true; }
        }
        AchivBoard.SetActive(false);
        return YesOrNo;
    }
    public void AddMoney()
    {
        TotalMoney += 300;
        MoneyText.UpdateMoney();
    }
    public void OpenSite(string adress)
    {
        Application.OpenURL(adress);
    }
    public void OpenChest()
    {

        if (TotalMoney >= 600)
        {

            RandomCharAvailable();
            if (!FinishAllChar)
            {
                NewCharAvail.SetBool("CanGet", false);
                PlayerScript.TotalCharacters++;
                int ran = Random.Range(0, RandomArray.Count);
                TotalMoney -= 600;
                RewardPanel.SetActive(true);
                AM.Play("NewChar");
                CharSaved[RandomArray[ran]] = true;
                CharRewardPicture.GetComponent<Animator>().Play("AppearReward");
                CharRewardPicture.sprite = GameManagerObject.Characters[RandomArray[ran]];

                ShopAlert = 1;
                if(BGMObject == null)
                {
                    BGMObject = GameObject.Find("BGM").GetComponent<OneSound>();
                }
                BGMObject.TheSource.volume = AudioManager.BackgroundVolume;
                Save();
            }
        }

    }
    public void RandomCharAvailable()
    {
        int counter = 0;
        RandomArray.Clear();
        for(int i = 0; i< CharSaved.Length; i++)
        {
            if (!CharSaved[i])
            {
                RandomArray.Add(i);
          
            }
            else counter++;
        }
        if (counter == CharSaved.Length) { FinishAllChar = true; print("Full"); } else FinishAllChar = false;
    }

    public void DoubleMoney()
    {
        if (CanGetDouble)
        {
            CanGetDouble = false;
            /*
            CoinToCoin.SetActive(true);
            CoinToCoin.GetComponent<Animator>().Play("FastCoin");
            StartCoroutine(SetActiveWithDelay(CoinToCoin, 1f, false));
            */
            UseCoinToCoin("FastCoin", 1);
            UsePlusMoney(ThisStageCoins);
            TotalMoney += ThisStageCoins;
            MoneyText.UpdateMoney();
            if(ExtraMoneyAnimate != null)
                ExtraMoneyAnimate.Play("NoExtra");
            Save();
        }
    }
    public void CanGetAgain()
    {
        if (!CanGetDouble)
        {
            ThisStageCoins = 0;
        }
        CanGetDouble = true;
    }
    public void ChangeColliderSize()
    {
        if (GameManagerObject.CurrentChar != 0)
        {

            MyCol.size = CharsSize;
            GroundCheck.transform.localPosition = GroundCheckNewPos;
            MyCol.offset = new Vector2 (0.01f, -0.09f);
        }
        else
        {

            MyCol.size = new Vector2(0.7132814f, 0.7074121f);
            GroundCheck.transform.localPosition = new Vector2(-0.012f, -0.324f);
            MyCol.offset = new Vector2(0, 0);
        }
    }
    public void UseCoinToCoin(string name,float timeToUnactive)
    {
        if (CoinToCoin[CoinToCoinNum] == null)
            return;
        CoinToCoin[CoinToCoinNum].SetActive(true);
        CoinToCoin[CoinToCoinNum].GetComponent<Animator>().Play(name);
        StartCoroutine(SetActiveWithDelay(CoinToCoin[CoinToCoinNum], timeToUnactive, false));
        if(CoinToCoinNum < CoinToCoin.Length-1)
        {
            CoinToCoinNum++;
        }else
        {
            CoinToCoinNum = 0;
        }
     
    }
    public void UsePlusMoney(int num)
    {
        if (PlusMoneyHolder[currentMoneyHolder] == null)
            return;
        int counter = 0;
        while (PlusMoneyHolder[currentMoneyHolder].activeSelf)
        {
            if (currentMoneyHolder < PlusMoneyHolder.Length-1) { currentMoneyHolder++; } else currentMoneyHolder = 0;
            counter++;
            if (counter == PlusMoneyHolder.Length) { break; }
        }

        if (counter != PlusMoneyHolder.Length)
        {
            if ((!PlusMoneyHolder[currentMoneyHolder].activeSelf))
            {
               
                PlusMoneyHolder[currentMoneyHolder].GetComponent<Animator>().Play("Normal");
                PlusMoneyHolder[currentMoneyHolder].GetComponent<Animator>().Play("Show Price"); // it is a wrong name, dont worry.
                PlusMoneyHolder[currentMoneyHolder].SetActive(true);
                PlusMoneyHolder[currentMoneyHolder].transform.GetChild(0).GetComponent<Text>().text = num.ToString();
                StartCoroutine(SetActiveWithDelay(PlusMoneyHolder[currentMoneyHolder], 1, false));
            }
        }
    }
    public IEnumerator FlyFast()
    {
     
        float TheCounter = 1.5f;
        Accelerate.SetActive(false);
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
        AM.Play("Fly");
        CanPlay = false;
     //   bool Rise = false;
        if (BrickLevel - 6 <= 35 * (ProgresSky + 1) && BrickLevel >= 35 * (ProgresSky + 1) - 6) {
           
            BrickLevel += 35 * (ProgresSky + 1) - BrickLevel - 1; 
            ObstacleIntensity += (35 * (ProgresSky + 1) - BrickLevel - 1)* 0.01f;
           
        }
        else { BrickLevel += 2; ObstacleIntensity += 0.02f; }

        while (TheCounter > 0)
        {
            TheCounter -= Time.deltaTime;
            
            transform.position = Vector3.Lerp(transform.position, Platforms[CurrentBrick].transform.position + Vector3.up * 10 + Vector3.right, Time.smoothDeltaTime*3);
            yield return null;

        }
        rb.gravityScale = GravityScale;
        CanPlay = true;
      
    }

}