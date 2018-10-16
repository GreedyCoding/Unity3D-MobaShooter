using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOneAbilityController : AbilityController {

    private float blinkDistance = 10f;

    //Blink
    public override void AbilityOne()
    {
        RaycastHit hit;
        //Destination we want to hit if we are not intersecting anything
        Vector3 destination = transform.position + transform.forward * blinkDistance;

        //Casting a linecast ray to the destination to check if we are intersecting anything
        if (Physics.Linecast(transform.position, destination, out hit))
        {
            //If we hit anything we are going to set the blinkdestination to the distance of the hit -1
            destination = transform.position + transform.forward * (hit.distance - 1f);
        }

        if (Physics.Raycast(destination, -Vector3.up, out hit))
        {
            destination = hit.point;
            destination.y = 1f;
            transform.position = destination;
        }

    }

    //Recall


    //Bomb
}
