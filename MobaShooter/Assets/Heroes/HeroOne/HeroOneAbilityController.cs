using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOneAbilityController : AbilityController {

    private float blinkDistance = 10f;

    //Blink
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

    //Recall


    //Bomb
}
