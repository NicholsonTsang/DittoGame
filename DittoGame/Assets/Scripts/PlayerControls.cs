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
    private bool q = false;
    private bool w = false;
    private bool e = false;
    private bool isMoving = true;

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
            if (Input.GetKeyDown(KeyCode.Q) && w == false && e == false)
            {
                if (animator.GetBool("transform") == false)
                {
                    // animator.SetTrigger("trigger");
                    animator.SetBool("transform", true);
                    animator.SetBool("q", true);
                    q = true;
                    isMoving = false;
                }
                else
                {
                    //animator.ResetTrigger("trigger");
                    animator.SetBool("transform", false);
                    animator.SetBool("q", false);
                    q = false;
                    isMoving = true;
                }
            }

            else if (Input.GetKeyDown(KeyCode.W) && q == false && e == false)
            {
                if (animator.GetBool("transform") == false)
                {
                    //animator.SetTrigger("trigger");
                    animator.SetBool("transform", true);
                    animator.SetBool("w", true);
                    w = true;
                    isMoving = false;
                }
                else
                {
                    //animator.ResetTrigger("trigger");
                    animator.SetBool("transform", false);
                    animator.SetBool("w", false);
                    w = false;
                    isMoving = true;
                }
            }

            else if (Input.GetKeyDown(KeyCode.E) && q == false && w == false)
            {
                if (animator.GetBool("transform") == false)
                {
                    //animator.SetTrigger("trigger");
                    animator.SetBool("transform", true);
                    animator.SetBool("e", true);
                    e = true;
                    isMoving = false;
                }
                else
                {
                    //animator.ResetTrigger("trigger");
                    animator.SetBool("transform", false);
                    animator.SetBool("e", false);
                    e = false;
                    isMoving = true;
                }
            }

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
            if (isMoving == true)
                rb.velocity = v;
            else
                rb.velocity = Vector2.zero;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide with: " + collision.gameObject.name);
        if (collision.gameObject.name == "ExitBtn")
        {
            Debug.Log("Winning condition triggered");
            GameManager gameManager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
            gameManager.WinningConditionTriggered();
        }
    }
}