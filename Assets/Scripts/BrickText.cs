using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickText : MonoBehaviour {

    //public string Name;
    [Header("Same Size Sprites")]
    public Sprite[] Style;
    public Color32[] AllColors;
    public SpriteRenderer SR;
    private int Randomizer,Randomizer2;
    public bool Moving;
    private int Dir = 1;
    public float Speed;
    public float Timer;
    public float DestroyTimer;
    public bool CanMove;

    public Animator anim;
    public Vector2[] colSize;
    public Vector2[] BlockSize;
    public BoxCollider2D Col;
    public BoxCollider2D BlockCol;

    public int NumFromChallenge;
    public bool ChallengeMode;

    public bool Check;
  
    //if Shake
 
    public PlayerScript Player;
 

    private void Start()
    {


        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        Speed = 1;
        Timer = 2;
        Col = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //  DestroyTimer = 2f;
        Starter();    
        SetShape(1);
        //Randomizer = Random.Range(0, Style.Length);
        if (!ChallengeMode)
        {
            SR.sprite = Style[1];
        }

    }
    void Update()
    {
        if (Moving)
        {
            if (Timer > 0) { Timer -= Time.deltaTime; }else { Timer = 2; Dir *= -1; }
            transform.position = new Vector3(transform.position.x + Dir * Time.fixedDeltaTime * Speed, transform.position.y, transform.position.z);
        }

    }
    public IEnumerator MovingObject()
    {
        float Counter = 0.0f;
        while(Counter < Timer)
        {
            if (CanMove)
            {
                transform.position = new Vector3(transform.position.x + Dir * Time.fixedDeltaTime * Speed, transform.position.y, transform.position.z);
            }
            Counter += Time.deltaTime;
            yield return null;
        }
        Dir *= -1;
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CanMove = false;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            CanMove = true;
        }
    }

    public IEnumerator SelfDestroy()
    {
       
        while(DestroyTimer > 0)
        {
            DestroyTimer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    public void Starter()
    {
        //  StartCoroutine(Appear());
        anim.Play("Appear");
        CanMove = true;
 
        Randomizer2 = Random.Range(0, AllColors.Length);

        SR.color = AllColors[Randomizer2];
  
    }
    public void SetShape(int num)
    {
        if (ChallengeMode) { num = NumFromChallenge; }
        SR.sprite = Style[num];
        Col.size = colSize[num];
        BlockCol.size = BlockSize[num];

    }
    public IEnumerator Appear()
    {
        float Alpha = 0.0f;

        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        while (Alpha < 1)
        {
            tmp.a = Alpha;
            Alpha += Time.deltaTime / 10;
            yield return null;
        }
        tmp.a = 1;
    }
    public void StartTheMove()
    {
            StartCoroutine(MovingObject());
     
    }
}
