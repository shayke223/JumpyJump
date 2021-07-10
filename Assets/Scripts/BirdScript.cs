using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

    public float Speed;
    public float Timer;

    void Update () {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * Speed, transform.position.y, 7.71f);
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else gameObject.SetActive(false);

	}
}
