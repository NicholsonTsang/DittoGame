using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dittoskill : Photon.MonoBehaviour
{
    public Animator animator;
    public Text PlayerNameText;
    public Rigidbody2D rb;
    public BoxCollider2D collider;

    [Header("skill 1")]
    public Image abilityImage1;
    public float cooldown1 = 0.5f;
    bool isCooldown1 = false;
    public KeyCode ability1;

    [Header("skill 2")]
    public Image abilityImage2;
    public float cooldown2 = 0.5f;
    bool isCooldown2 = false;
    public KeyCode ability2;

    [Header("skill 3")]
    public Image abilityImage3;
    public float cooldown3 = 0.5f;
    bool isCooldown3 = false;
    public KeyCode ability3;

    private bool q = true;
    private bool w = true;
    private bool e = true;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Ability1();
            Ability2();
            Ability3();
        }
    }

    //no need implement
    public void Ability1()
    {
        if (q && Input.GetKey(ability1) && isCooldown1 == false)
        {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
            Ability1Effect();
        }

        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }

    //transform skill Q
    public void Ability1Effect()
    {
        if (animator.GetBool("transform") == false)
        {
            // animator.SetTrigger("trigger");
            animator.SetBool("transform", true);
            animator.SetBool("q", true);
            photonView.RPC("HideName", PhotonTargets.AllBuffered);
            w = false;
            e = false;
            //isMoving = false;
        }
        else
        {
            //animator.ResetTrigger("trigger");
            animator.SetBool("transform", false);
            animator.SetBool("q", false);
            photonView.RPC("ShowName", PhotonTargets.AllBuffered);
            w = true;
            e = true;
        }
    }
  
    public void Ability2()
    {
        if (w && Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            Ability2Effect();
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    //transform skill W
    public void Ability2Effect()
    {
        if (animator.GetBool("transform") == false)
        {
            //animator.SetTrigger("trigger");
            animator.SetBool("transform", true);
            animator.SetBool("w", true);
            photonView.RPC("HideName", PhotonTargets.AllBuffered);
            q = false;
            e = false;
        }
        else
        {
            //animator.ResetTrigger("trigger");
            animator.SetBool("transform", false);
            animator.SetBool("w", false);
            photonView.RPC("ShowName", PhotonTargets.AllBuffered);
            q = true;
            e = true;
        }
    }

    public void Ability3()
    {
        if (e && Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
            Ability3Effect();
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }

    //transform skill E
    public void Ability3Effect()
    {
        if (animator.GetBool("transform") == false)
        {
            //animator.SetTrigger("trigger");
            animator.SetBool("transform", true);
            animator.SetBool("e", true);
            photonView.RPC("HideName", PhotonTargets.AllBuffered);
            q = false;
            w = false;
        }
        else
        {
            //animator.ResetTrigger("trigger");
            animator.SetBool("transform", false);
            animator.SetBool("e", false);
            photonView.RPC("ShowName", PhotonTargets.AllBuffered);
            q = true;
            w = true;
        }
    }

    [PunRPC]
    private void ShowName()
    {
        PlayerNameText.gameObject.SetActive(true);
        gameObject.layer = 7;
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider.size = new Vector2(5.55f, 4.8f);
    }

    [PunRPC]
    private void HideName()
    {
        PlayerNameText.gameObject.SetActive(false);
        gameObject.layer = 0;
        rb.bodyType = RigidbodyType2D.Static;
        collider.size = new Vector2(7f, 8f);
    }
}
