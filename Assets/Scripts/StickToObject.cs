using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToObject : MonoBehaviour {

    public GameObject Follow;
    public Vector3 Offset;
    private void Start()
    {
        if(Follow == null)
        {
            Follow = GameObject.Find("Player");
        }
    }
    private void FixedUpdate()
    {
        transform.position = Follow.transform.position+ Offset;
    }
}
