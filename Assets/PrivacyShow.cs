using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShow : MonoBehaviour {

    public int LoginsCounter;
    public GameObject PPObject;
	// Use this for initialization
	void Start () {
        LoginsCounter = PlayerPrefs.GetInt("Logins");
        if(LoginsCounter == null)
        {
            LoginsCounter = 0;
        }
        if(LoginsCounter == 0)
        {
            PPObject.SetActive(true);
        }
    }
	

}
