using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControls : Photon.MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject[] Setactivelist;
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

    //animation
    public Animator animator;

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
            for (int i = 0; i < Setactivelist.Length; i++)
            {
                Setactivelist[i].SetActive(true);
            }
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
                animator.SetFloat("moveX", 0);
                v.x = -speed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetFloat("moveX", 1);
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
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                v = v.normalized * speed;
                rb.velocity = v;
            }
            //else
                //rb.velocity = Vector2.zero;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with: " + collision.gameObject.name);
        if (collision.gameObject.name == "ExitBtn")
        {
            Debug.Log("Winning condition triggered");
            GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
            gameManager.WinningConditionTriggered();
        }

        if (collision.gameObject.tag == "Guard")
        {
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Debug.Log("Losing condition triggered");
                GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
                gameManager.LosingConditionTriggered();
            }
        }

        if ((collision.gameObject.tag == "trapball"))
        {
            Debug.Log("Losing condition triggered");
            GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
            gameManager.LosingConditionTriggered();
        }
    }
}