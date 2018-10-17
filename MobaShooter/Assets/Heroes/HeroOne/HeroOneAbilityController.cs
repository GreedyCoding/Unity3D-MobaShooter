using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOneAbilityController : AbilityController {

    PlayerController playerController;
    Rigidbody rb;

    List<PointInTime> pointsInTime;

    //Blink
    float blinkDistance = 10f;

    //Recall
    public bool isRewinding;
    float recallTime = 3f;

    public void Start()
    {
        pointsInTime = new List<PointInTime>();
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (pointsInTime.Count > Mathf.Round(recallTime / Time.fixedDeltaTime))
        { 
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        Debug.Log(pointsInTime.Count);

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    //Blink Ability
    public override void AbilityOne(float xMovement, float zMovement)
    {
        float previousY = transform.position.y;
        RaycastHit hit;
        Vector3 destination;
        //Destination we want to hit if we are not intersecting anything
        if (zMovement < 0)
        {
            destination = transform.position + -transform.forward * blinkDistance;
        }
        else
        {
            destination = transform.position + transform.forward * blinkDistance;
        }

        //Casting a linecast ray to the destination to check if we are intersecting anything
        if (Physics.Linecast(transform.position, destination, out hit))
        {
            //If we hit anything we are going to set the blinkdestination to the distance of the hit -1
            destination = transform.position + transform.forward * (hit.distance - 1f);
        }

        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            destination.y = previousY;
            transform.position = destination;
        }

    }

    //Recall Ability
    public override void AbilityTwo()
    {
        for (int i = 0; i < 180; i++)
        {
            transform.position = pointsInTime[0].position;
            transform.rotation = pointsInTime[0].rotation;
            pointsInTime.RemoveAt(0);
        }
    }

    //Bomb Ultimate
    public override void Ultimate()
    {

    }

}
