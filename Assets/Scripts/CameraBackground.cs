using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour {
    public Transform Player;
    public Vector3 Offset;
    public float Speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, Player.position + Offset, Speed * Time.deltaTime);
    }
    }
