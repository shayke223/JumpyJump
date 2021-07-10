using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControl : MonoBehaviour {

    public GameObject LosePanel;
public void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}
