using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guardskill : Photon.MonoBehaviour
{
    [Header("skill 1")]
    public GameObject trapball;
    public Transform FirePos;
    public Image abilityImage1;
    public float cooldown1 = 5f;
    bool isCooldown = false;
    public KeyCode ability1;

    public Vector2 direction;
    void Start()
    {
        abilityImage1.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            Ability1();
        }
    }
    public void changedir(Vector2 dir)
    {
        direction = dir;
    }

    public void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
            Ability1Effect();
        }

        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount  <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    public void Ability1Effect()
    {
        GameObject ball = PhotonNetwork.Instantiate(trapball.name, new Vector2(FirePos.transform.position.x + direction[0], FirePos.transform.position.y + direction[1]), Quaternion.identity, 0);
        ball.GetComponent<trapball>().Setdirection(direction);
    }
    /*
    public void Ability2()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage1.fillAmount = 1;
        }

        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }*/
}
