using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTutorial : MonoBehaviour {

    // Use this for initialization
    public bool isGrounded;
    public GameObject GroundCheck;
    public LayerMask WhatIsGround;
    private float JumpSpeed;
    public float rotateSpeed = 1000;
    public Rigidbody2D rb;
    public bool CanPlay;
    public float JumpLimit;
    public GameObject Accelerate;
    public AudioManager AM;
    public float BeginSpeedJump;
    private float RatioBar;
    public Image PressBar;
    public GameManagerObject GameManagerObject;
    public GameObject LastPos;
    public GameObject FinishTutorial;
    private bool CanFinish;
    public Transform[] LastPositions;

    public float TimerBrick;
    public GameObject TheDisBrick;
    public bool ShieldOn;
    public GameObject Shield;
    public GameObject ShieldObject;
    public Sprite NewChar;
    public GameObject DarkAppear;
    public int FakeHP = 2;

    public GameObject LastBrick;

    public void FinishTutorialButton()
       
    {
        PlayerPrefs.SetInt("Logins",2);
        GameManagerObject.ChangeScene("Preload Scene");
    }
    void Start () {
        DarkAppear.SetActive(true);
        Destroy(DarkAppear, 2);
        CanFinish = true;
        rb = GetComponent<Rigidbody2D>();
        AM = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        GameManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();
        CanPlay = true;
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if(TimerBrick > 0) { TimerBrick -= Time.deltaTime; } else
        {
            if (TheDisBrick.activeSelf)
            {
                TheDisBrick.SetActive(false);
                TimerBrick = 1;
            }
            else
            {
                TheDisBrick.SetActive(true);
                TimerBrick = 4;
            }

           
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

                    //AM.sounds[0].pitch = Random.Range(0.95f, 1.05f);
                    AM.Play("Blip");
                    rb.gravityScale = 5;
                    Jump();
                    Accelerate.SetActive(false);
                }

            }
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space"))
            {
                rb.gravityScale = 5;
            }
        }
    }

    public void Jump()
    {

        rb.velocity = new Vector3(JumpSpeed / 1.5f, BeginSpeedJump + JumpSpeed, 0); // Minimum speed to jump included.
        JumpSpeed = 0;
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundTutorial"))
        {

            if (LastBrick != other.gameObject)
            {
                LastBrick = other.gameObject;
                FakeHP = 6;

            }
            else
            {

                FakeHP -= 1;
                if (FakeHP < 0)
                {
                    rb.velocity = new Vector3(-2, 5, 0); FakeHP = 6;
                }
            }
        }
            if (other.CompareTag("TutorialTrophy"))
            {
                AM.Play("Price");
                other.gameObject.SetActive(false);
            }
            if (other.CompareTag("Killer B"))
            {

                if (Shield.activeSelf)
                {
                    Destroy(other.gameObject);
                    Shield.SetActive(false);
                    gameObject.GetComponent<SpriteRenderer>().sprite = NewChar;
                }
                else
                {
                    transform.position = LastPositions[3].transform.position;

                }
                AM.Play("Boom");
            }


            if (other.CompareTag("ReTutorial"))
            {
                if (CanFinish)
                {
                    FinishTutorial.SetActive(true);
                    AM.Play("Complete");
                    CanPlay = false;
                    CanFinish = false;
                }
            }
            if (other.CompareTag("Tutorial Die"))
            {
                int num = other.GetComponent<TutorialDie>().Number;
                transform.position = LastPositions[num].transform.position;

            }

            if (other.CompareTag("Troophy"))
            {
                other.gameObject.SetActive(false);
                AM.Play("Price");
                Shield.SetActive(true);
            }
        }
   
}
