using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterList : MonoBehaviour {

    public GameManagerObject ManagerObject;
    public GameObject LastSelected;
    private void Awake()
    {
        ManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();
        SelectChar(ManagerObject.CurrentChar);
   


    }
    public void SelectChar(int num)
    {
      if(LastSelected != null) {LastSelected.GetComponent<CharButton>().Selector.SetActive(false); }
        transform.GetChild(num).GetComponent<CharButton>().Selector.SetActive(true);
      LastSelected = transform.GetChild(num).gameObject;
        ManagerObject.CurrentChar = num;
       ManagerObject.ChangeCharacter();
        PlayerScript.MyChar = num;
    }
}
