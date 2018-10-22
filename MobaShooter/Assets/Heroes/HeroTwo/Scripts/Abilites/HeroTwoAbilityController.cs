using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTwoAbilityController : AbilityController {

    PlayerController playerController;

    float sprintTime = 3f;
    float maxSprintTime = 3f;

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    //Sprint Ability
    public override void AbilityOne(float xMovement, float zMovement)
    {
        playerController.moveSpeed = 7f;
        StartCoroutine("StopSprinting");
    }

    //Heal Ability
    public override void AbilityTwo()
    {

    }

    //AutoAim Ultimate
    public override void Ultimate()
    {

    }

    IEnumerator StopSprinting()
    {
        yield return new WaitForSeconds(3f);
        playerController.moveSpeed = 5f;
    }

}
