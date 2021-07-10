using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour {

    public Image Circle;

    public float Counter;


	void Update () {
        if (Counter > 0)
        {
            Counter -= Time.deltaTime/2.5f;
            Circle.fillAmount = Counter;
        }
    }
}
