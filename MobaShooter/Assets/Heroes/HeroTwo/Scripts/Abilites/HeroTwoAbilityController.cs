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
        if (sprintTime == maxSprintTime)
        {
            playerController.moveSpeed = 7f;
        }
        sprintTime -= Time.deltaTime;

    }

    //Recall Ability
    public override void AbilityTwo()
    {

    }

    //Bomb Ultimate
    public override void Ultimate()
    {

    }

}
