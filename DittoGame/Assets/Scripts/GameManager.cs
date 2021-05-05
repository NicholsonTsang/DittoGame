using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour
{
    public GameObject[] spawnpoint;
    public GameObject DittoPrefabs;
    public GameObject GuardPrefabs;
    public GameObject GameCanvas;
    public GameObject SceneCamera;

    public Button dittoBtn;
    public Button guardBtn;
    private bool prefabGenerated = false;

    private int myPlayRole = 0; // 1 for Ditto, 2 for Guard


    public void Awake()
    {
        GameCanvas.SetActive(true);
    }

    public void SpawnPlayer()
    {
        myPlayRole = 1;
        PhotonView photonView = PhotonView.Get(this);
        Debug.Log(photonView);
        photonView.RPC("disableSelectedRole", PhotonTargets.All, 1);
        //GameCanvas.SetActive(false);
        //SceneCamera.SetActive(false);
        //PhotonNetwork.Instantiate(DittoPrefabs.name, spawnpoint[0].transform.position, Quaternion.identity, 0);
    }

    public void SpawnPlayer2()
    {
        myPlayRole = 2;
        PhotonView photonView = PhotonView.Get(this);
        Debug.Log(photonView);
        photonView.RPC("disableSelectedRole", PhotonTargets.All, 2);
        //GameCanvas.SetActive(false);
        //SceneCamera.SetActive(false);
        //PhotonNetwork.Instantiate(GuardPrefabs.name, spawnpoint[1].transform.position, Quaternion.identity, 0);
    }

    [PunRPC]
    void disableSelectedRole(int role)
    {
        Debug.Log("Running the disable selected role Pun RPC");
        if (role == 1)
        {
            dittoBtn.interactable = false;
        }
        else if (role == 2)
        {
            guardBtn.interactable = false;
        }
    }

    private void Update()
    {
        if (dittoBtn.interactable == false && guardBtn.interactable == false)
        {
            GameCanvas.SetActive(false);
            SceneCamera.SetActive(false);
            if (myPlayRole == 1 && prefabGenerated == false)
            {
                PhotonNetwork.Instantiate(DittoPrefabs.name, spawnpoint[0].transform.position, Quaternion.identity, 0);
                prefabGenerated = true;
            }
            else if (myPlayRole == 2 && prefabGenerated == false)
            {
                PhotonNetwork.Instantiate(GuardPrefabs.name, spawnpoint[1].transform.position, Quaternion.identity, 0);
                prefabGenerated = true;
            }
        }
    }






    [PunRPC]
    void tellOpponentYourRole(int role)
    {
        Debug.Log(string.Format("Opponent Role is {0}", role));
    }

    public void testfunction()
    {
        Debug.Log("Running test function");
        dittoBtn.interactable = false;
        guardBtn.interactable = false;
    }

    public void WinningConditionTriggered()
    {
        Debug.Log("Go to Game Over Scene");
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("toGameOverScene", PhotonTargets.All);
    }

    public void LosingConditionTriggered()
    {
        Debug.Log("Go to Game Over Scene");
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("toGameOverScene2", PhotonTargets.All);
    }

    [PunRPC]
    void toGameOverScene()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("DittoWin");
    }

    [PunRPC]
    void toGameOverScene2()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("GuardWin");
    }

}
