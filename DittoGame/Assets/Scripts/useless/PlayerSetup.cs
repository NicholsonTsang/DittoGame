using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    //Camera sceneCamera;
    void Start()
    {
        print(isLocalPlayer);
        if ((!isLocalPlayer))
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        /*
        else
        {
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        */
    }

        void OnDisable()
    {
        /*
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }*/
    }
}
