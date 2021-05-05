using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randombutton : Photon.MonoBehaviour
{
    public GameObject[] ButtonSetactivelist;
    // Start is called before the first frame update
    void Start()
    {
        {
            int i = Random.Range(0, 5);
            ButtonSetactivelist[i].SetActive(true);
        }
    }
}
