using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buffer : MonoBehaviour {

    public Image Circle;

    public float Speed;
    private float Counter = 0;
    public void Awake()
    {
        Circle = GetComponent<Image>();
       
    }
    public IEnumerator FillIn()
    {
        Circle.raycastTarget = false;

        Counter = 0.0f;
        while (Counter < 1)
        {

            Circle.fillAmount = Counter;
            Counter += Speed* Time.deltaTime;
            yield return null;
        }
 
        Circle.fillAmount = 1;
        Circle.raycastTarget = true;
    }
}
