using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;
    public float Speed;
    public Vector3 offset;
    public bool CanFollow;
    public Camera Cam;

    public float PreviewSpeed;
    public float SaveSpeed;

    public bool ChallengeMode;
    public bool ShowStageOnce;

	void Start () {
        ShowStageOnce = true;
        Cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (CanFollow)
        {
            if (Player != null)
                transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, Speed * Time.deltaTime);
        }
    }
    public void useCameraShake(float Power,float Timer)
    {
        StartCoroutine(CameraShake(Power, Timer));
    }
    public IEnumerator CameraShake(float Power, float Timer)
    {
        Vector3 FirstPos = transform.position;
        CanFollow = false;
        float Counter = 0.0f;
        while(Counter < Timer)
        {
            float RandomizerY = Random.Range(-Power, Power);
            float RandomizerX = Random.Range(-Power, Power);
            transform.position = new Vector3(transform.position.x + RandomizerX, transform.position.y + RandomizerY, transform.position.z);
            Counter += Time.deltaTime;
            yield return null;
            transform.position = FirstPos;
        }
        CanFollow = true;
       
    }
    public IEnumerator CameraZoomIn(float TargetZoom)
    {
        while (Cam.orthographicSize > TargetZoom)
        {
            Cam.orthographicSize -= Time.deltaTime * 8;
            yield return null;
        }
        Cam.orthographicSize = TargetZoom;
    }

public IEnumerator CameraZoomOut(float TargetZoom)
{
    while (Cam.orthographicSize < TargetZoom)
    {
        Cam.orthographicSize += Time.deltaTime * 8;
        yield return null;
    }
    Cam.orthographicSize = TargetZoom;
}


    public void StartShowStage()
    {
        if (ShowStageOnce)
        {
            ShowStageOnce = false;
            StartCoroutine(ShowStage());
           
        }
    }
    public IEnumerator ShowStage()
    {/*
        SaveSpeed = Speed;
        Speed = PreviewSpeed;
        Player.GetComponent<PlayerScript>().CanPlay = false;
        Player = GameObject.Find("Flag(Clone)");

        float Timer = 1;
        while (Timer > 0)
        {

            Timer -= Time.deltaTime;
            yield return null;

        }
        Player = GameObject.Find("Player");
        Player.GetComponent<PlayerScript>().CanPlay = true;
        StartCoroutine(BackToPlayer());
        */
        yield return new WaitForSeconds(0.25f);
        Player.GetComponent<PlayerScript>().CanPlay = true;
    }
    public IEnumerator BackToPlayer()
    {
        Player = GameObject.Find("Player");

        float Timer = 2;
        while (Timer > 0)
        {
            Timer -= Time.deltaTime;
            yield return null;

        }
        if (Player == null)
        {
            Player = GameObject.Find("Player");         
        }
        Player.GetComponent<PlayerScript>().CanPlay = true;
        Speed = SaveSpeed;
    }


}
