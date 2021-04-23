using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 guardpos;

    void Start()
    {
        guardpos = new Vector3 (0,0,0);
    }

    public void Guard_Pos_Update(Vector3 pos)
    {
        guardpos = pos;
    }
}
