using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    //Reference to the explosionEffect that gets created
    public GameObject explosionEffect;

    public float impactArea = 5f;
    public float impactForce = 3000f;
    public float damage = 45f;
    public float speed = 2f;

    //Setting up a rigidbody variable to store the rigidbody of the rocket
    Rigidbody rb;

    private void Start() {

        //Getting the Rigidbody when the Rocket is created
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate() {
        //Using transform.up Vector for the calculation because the object is rotated around the X axis
        rb.AddForce(transform.up.normalized * speed, ForceMode.VelocityChange);
        
    }


    //Checks for collision, is a Unity function
    private void OnCollisionEnter(Collision collision) {

        //If the rocket collides with something we let the rocket explode
        ExplodeRocket();

    }

    private void ExplodeRocket() {

        //Instantiate an effect for the explosion the the current position and rotation
        GameObject explosionGO = Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get all colliders by using the creating an OverlapSphere at the current position and radius of the impactArea
        //Physics.OverlapSphere returns an array of colliders it collided with
        Collider[] colliders = Physics.OverlapSphere(transform.position, impactArea);

        //Loop through all the objects in the colliders array
        foreach(Collider nearbyObject in colliders) {

            //Get rigidbody of the nearbyObject
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            //If the nearbyObject we collided with has a rigidbody
            if (rb != null ) {

                //And its is not the Player
                if(rb.name != "Player") {

                    //Add an explosionforce 
                    rb.AddExplosionForce(impactForce, transform.position, impactArea, 0f, ForceMode.Acceleration);

                }

            }

            //Get target component of the nearbyObject
            Target target = nearbyObject.GetComponent<Target>();

            //If the target has a target component
            if (target != null) {

                //Call TakeDamage on the target component;
                target.TakeDamage(damage);

            }


        }

        //After we have done everything we destroy the rocket and the explosion effect
        Destroy(gameObject);
        Destroy(explosionGO, 0.5f);

    }

}
