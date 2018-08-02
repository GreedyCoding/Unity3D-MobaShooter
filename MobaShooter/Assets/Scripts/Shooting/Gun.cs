using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 3f;
    public float impactForce = 2000f;

    //Reference to the player camera for the raycasting
    public Camera fpsCam;
    //Reference to the muzzle particle system that is played when shooting
    public ParticleSystem muzzleFlash;
    //Reference to the impact particle system which gets instantiated as a gameobject
    public GameObject impactAnimation;
    //Reference to the rocket we are shooting as alternate fire
    public GameObject rocket;
    //Reference to the firepoint
    public Transform firePoint;

    //Stores the time the gun can shoot the next time, 0 so you can instantly shoot
    private float nextTimeToShoot = 0f;

    //Next time to shoot a rocket
    private float nextTimeToRocket = 0f;
    //Rocket Cooldown
    public float rocketCooldown = 6f;
    //Rocket Prefab is rotated around the x axis we rotate it by this value so it is facing away from the player
    private Vector3 rocketOffsetRotation = new Vector3(90f, 0f, 0f);


    void Update () {
		
        //If player pressed Mouse Button 1 and its already time to fire again, shoot the gun
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot) {

            //Then add the 1/firerate to the current time to calculate the next time to shoot
            nextTimeToShoot = Time.time + (1f / fireRate);
            Shoot();

        }

        //If player pressed Mouse Button 2 and its already time to shoot a rocket again, shoot it
        if (Input.GetButton("Fire2") && Time.time >= nextTimeToRocket) {

            //Add the rocketcooldown to the current time to calculate the next time to fire a rocket
            nextTimeToRocket = Time.time + rocketCooldown;
            ShootRocket();

        }

	}

    void Shoot() {
        
        //Raycast variable that will hold the information about the positon the ray hits
        RaycastHit hit;

        //Every time we shoot we display the muzzle flash animation
        muzzleFlash.Play();

        //Shoot a ray from the camera postion straight forward and store the hitinfo in the hit variable
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {

            //If the ray hit something and the Target component exists we store thsi component in the targe var
            Target target = hit.transform.GetComponent<Target>();

            //If there is a target component we let the target take damage
            if (target != null) {

                target.TakeDamage(damage);

            }

            //If the hit component has a rigidbody add a force to it
            if (hit.rigidbody != null) {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }

            //Every time we hit something we instantiate a impact animation pointing out from the impact point
            GameObject impactAnimationGO = Instantiate(impactAnimation, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy the impact animation GameObject after 1 seconds
            Destroy(impactAnimationGO, 1f);


        }

    }

    void ShootRocket() {

        RaycastHit hit;
        //Casting a Raycast from the camera to determine the impact point of the rocket
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        //Facing the Firepoint to the impact point beacuse we add a force pointing forward
        firePoint.transform.LookAt(hit.point);

        //Play the muzzleFlash animation
        muzzleFlash.Play();
        //Instantiate a rocket (positoion is still buggy)
        GameObject rocketGO = Instantiate(rocket, firePoint.position, firePoint.rotation * Quaternion.Euler(rocketOffsetRotation));
        //Call destroy after 10 seconds to clear rockets who may still fly in space
        Destroy(rocketGO, 10f);

    }

}
