using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GuardControll : Photon.MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject[] Setactivelist;
    public PhotonView photonView;
    public Text PlayerNameText;
    public SpriteRenderer sr;


    [SerializeField] float speed = 100;
    [SerializeField] int side;
    public Rigidbody2D rb;
    public Vector2 v;
    public GameObject vision;
    public Vector3 localpos;
    public GameObject button;
    public GameObject firepos;

    // Start is called before the first frame update
    public void Awake()
    {
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            vision.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
            for (int i = 0; i < Setactivelist.Length; i++)
            {
                Setactivelist[i].SetActive(true);
            }
            button = GameObject.FindWithTag("button");
            if (button != null)
                button.SetActive(false);
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
        localpos = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                v.x = -speed;
                photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
                GetComponent<Guardskill>().changedir(new Vector2(-1,0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                v.x = speed;
                photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
                GetComponent<Guardskill>().changedir(new Vector2(1, 0));
            }
            else
            {
                v.x = 0;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                v.y = speed;
                GetComponent<Guardskill>().changedir(new Vector2(0, 1));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                v.y = -speed;
                GetComponent<Guardskill>().changedir(new Vector2(0, -1));
            }
            else
            {
                v.y = 0;
            }
            v = v.normalized * speed;
            rb.velocity = v;

            Vector3 pos = transform.position;
            vision.GetComponent<FieldOfView>().SetOrigin(pos);

          

   

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

    [PunRPC]
    private void fireleft()
    {
        firepos.transform.position = new Vector3(-20, 0, 0);
    }

    [PunRPC]
    private void fireright()
    {
        firepos.transform.position = new Vector3(20, 0, 0);
    }

    [PunRPC]
    private void fireup()
    {
        firepos.transform.position = new Vector3(0, 20, 0);
    }

    [PunRPC]
    private void firedown()
    {
        firepos.transform.position = new Vector3(0, -20, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with: " + collision.gameObject.name);
        if ((collision.gameObject.name == "ExitBtn") && (gameObject.tag == "Ditto"))
        {
            Debug.Log("Winning condition triggered");
            GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
            gameManager.WinningConditionTriggered();
        }

    }
}