using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControls : Photon.MonoBehaviour
{
    public GameObject PlayerCamera;
    public PhotonView photonView;
    public Text PlayerNameText;
    public SpriteRenderer sr;


    [SerializeField] float speed = 100;
    [SerializeField] int side;
    [SerializeField] float warningDis;
    public Rigidbody2D rb;
    public Vector2 v;
    public GameObject vision;
    public Vector3 localpos;
    public GameObject guard_pointer;
    public GameObject guard;

    // Start is called before the first frame update
    public void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            vision.SetActive(true);
            if (gameObject.tag == "Ditto")
                guard_pointer.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.red;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v = rb.velocity;
        localpos = new Vector3 (0, 0, 0);
        if (gameObject.tag == "Ditto")
            guard = GameObject.FindWithTag("Guard");
        warningDis = 20f;
    }
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                v.x = -speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                v.x = speed;
            }
            else
            {
                v.x = 0;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v.y = speed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                v.y = -speed;
            }
            else
            {
                v.y = 0;
            }
            rb.velocity = v;

            Vector3 pos = transform.position;
            vision.GetComponent<FieldOfView>().SetOrigin(pos);
            
            if (side == 0)
            {
                if (guard == null)
                {
                    guard = GameObject.FindWithTag("Guard");
                    guard_pointer.SetActive(true);
                }
                if (guard != null)
                {
                    Vector3 enermyPos = guard.transform.position;
                    guard_pointer.GetComponent<Pointer>().TargetUpdate(enermyPos);
                    double distance = Math.Sqrt((pos[0] - enermyPos[0]) * (pos[0] - enermyPos[0]) + (pos[1] - enermyPos[1]) * (pos[1] - enermyPos[1]));
                    if (distance < warningDis)
                    {
                        guard_pointer.GetComponent<Pointer>().show();
                    }
                    else
                    {
                        guard_pointer.GetComponent<Pointer>().hide();
                    }
                }

            }
            
            if (side == 1)
            {
                //playersInfo.GetComponent<PlayerInfo>().Guard_Pos_Update(pos);
                //Debug.Log(playersInfo.GetComponent<PlayerInfo>().guardpos);
            }

        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}