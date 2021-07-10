using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour {

    public GameObject[] Open, Close;

    public void OpenAndCloseAll()
    {
        int LengthOne = Open.Length;
        int LengthTwo = Close.Length;
        if (LengthOne > 0)
        {
            for (int i = 0; i < LengthOne; i++)
            {
                Open[i].SetActive(true);
            }
        }
        if (LengthTwo > 0)
        {
            for (int j = 0; j < LengthTwo; j++)
            {
                if (Close[0] != null)
                {
                    Close[j].SetActive(false);
                }
            }
        }
    }
}
