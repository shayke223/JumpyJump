using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailListScript : MonoBehaviour {

    public PlayerScript Player;
  //  public GameManagerObject ManagerObject;
    public enum Shop {Trails,Characters,Effects}
    public Shop Type;
    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
      //  ManagerObject = GameObject.Find("GameManagerObject").GetComponent<GameManagerObject>();

        switch (Type)
        {
            case Shop.Trails:
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (Player.TrailSaved[i])
                    {
                        transform.GetChild(i).GetComponent<OptionScript>().Blocker.SetActive(true);
                    }
                }
                break;
            case Shop.Characters:
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (Player.CharSaved[i])
                    {
                        transform.GetChild(i).GetComponent<OptionScript>().Blocker.SetActive(true);
                    }
                }
                break;
        }

    }
}
