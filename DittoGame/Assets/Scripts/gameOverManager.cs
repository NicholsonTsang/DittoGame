using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void getBackOnClickHandle()
    {
        Debug.Log("GetBackOnclick handling");
        //GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
        //Debug.Log(gameManager == null);
        //gameManager.leaveRoom();
        //gameManager.toGameMenuScene();
        PhotonNetwork.LoadLevel("MainMenu");
    }

}
