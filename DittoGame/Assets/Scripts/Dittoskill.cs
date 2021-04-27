using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dittoskill : Photon.MonoBehaviour
{
    [Header("skill 1")]
    public Image abilityImage1;
    public float cooldown1 = 0.5f;
    bool isCooldown = false;
    public KeyCode ability1;
    void Start()
    {
        abilityImage1.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
    }

    //no need implement
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

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    //transform skill
    public void Ability1Effect()
    {
        //
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
