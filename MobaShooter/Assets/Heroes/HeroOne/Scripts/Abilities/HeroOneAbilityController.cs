using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOneAbilityController : AbilityController {

    //REFERENCES
    PlayerController playerController;
    Rigidbody rb;

    //BLINK
    float blinkDistance = 10f;
    
    //RECALL
    bool isRewinding;
    float recallTime = 3f;
    int framesToRecall;
    int frameCount = 0;
    int recordInterval = 3;

    List<PointInTime> pointsInTime;

    public void Start()
    {
        //Initializing the list for our points in time
        pointsInTime = new List<PointInTime>();
        //Calculating the frames we have to recall by dividing by fixedDeltaTime which is time between every physics iteration
        framesToRecall = (int)Mathf.Round(recallTime / Time.fixedDeltaTime) / recordInterval;
    }

    public void FixedUpdate()
    {
        if (!isRewinding)
        {
            //While we are not rewinding we record the our positional data every 3 frames
            if (frameCount == recordInterval)
            {
                Record();
            }

            frameCount++;
        }
        else
        {
            //If isRewinding is true we rewind and start recording again after rewinding
            Rewind();
        }
    }

    //Blink Ability
    public override void AbilityOne(float xMovement, float zMovement)
    {
        //Getting the current Y position so we can reassign it instead of using the y value of the ray hitpoint
        float previousY = transform.position.y;

        RaycastHit hit;
        Vector3 destination;

        //Calculating the point we want to hit if we are not intersecting anything
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
        //The logic for the recall(Record()/Rewind()) is happening in FixedUpdate
        //We are triggering the recall by setting isRewinding to true
        isRewinding = true;
    }

    //Bomb Ultimate
    public override void Ultimate()
    {

    }

    void Record()
    {

        //If we are recording more frames then we want to call back to we delete the last frame so we can keep inserting new points at 0
        if (pointsInTime.Count > framesToRecall)
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        //Recording our points in time every physics frame by inserting them into our pointsInTime list
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
        
        //After we recorded this frame we reset the framecount because we do not record every frame
        frameCount = 0;
    }

    void Rewind()
    {
        //If there are no more points we stop rewinding by setting is rewinding to false
        if (pointsInTime.Count == 0)
        {
            isRewinding = false;
            return;
        }
        //Linear interpolation between the current position and the last recorded position to have a smooth transition
        transform.position = Vector3.Lerp(transform.position, pointsInTime[0].position, Time.fixedDeltaTime);
        //Setting the current rotation to the last recorded rotation
        transform.rotation = pointsInTime[0].rotation;
        //Then removing the last pointInTime from our list
        pointsInTime.RemoveAt(0);
    }

}
