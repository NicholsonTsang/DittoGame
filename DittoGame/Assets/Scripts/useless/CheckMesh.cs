using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMesh : MonoBehaviour
{
    public MeshCollider mc;
    // Start is called before the first frame update
    void Start()
    {
        mc = GetComponent<MeshCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (mc.isTrigger == true)
        {
            print("yes");
        }
    }
}
