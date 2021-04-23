using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnpoint;
    public GameObject DittoPrefabs;
    public GameObject GuardPrefabs;
    public GameObject GameCanvas;
    public GameObject SceneCamera;


    public void Awake()
    {
        GameCanvas.SetActive(true);
    }

    public void SpawnPlayer()
    {
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
        PhotonNetwork.Instantiate(DittoPrefabs.name, spawnpoint[0].transform.position, Quaternion.identity, 0);
    }

    public void SpawnPlayer2()
    {
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(false);
        PhotonNetwork.Instantiate(GuardPrefabs.name, spawnpoint[1].transform.position, Quaternion.identity, 0);
    }

}
