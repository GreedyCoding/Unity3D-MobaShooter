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

    //Stores the time the gun can shoot the next time, 0 so you can instantly shoot
    private float nextTimeToFire = 0f;
    	
	void Update () {
		
        //If player pressed Mouse Button 1 and its already time to fire again, shoot the gun
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) {

            //Then add the 1/firerate to the current time to calculate the next time to shoot
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

        }

	}

    void Shoot() {
        
        //Raycast variavle that will hold the information about the positon the ray hits
        RaycastHit hit;

        //Every time we shoot we display the muzzle flash animation
        muzzleFlash.Play();

        //Shoot a ray from the camera postion straight forward and store the hitinfo in the hit variable
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {

            //If the ray hit something we get the Target component if it exists
            Target target = hit.transform.GetComponent<Target>();

            //If there is a target component we let the target take damage
            if (target != null) {

                target.TakeDamage(damage);

            }

            //If the hit component has a rigidbody we add a force to it
            if (hit.rigidbody != null) {

                hit.rigidbody.AddForce(-hit.normal * impactForce);

            }

            //Every time we hit something we instantiate a particle system pointing out from the impact point
            GameObject impactAnimationGO = Instantiate(impactAnimation, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy the GameObject after 2 seconds
            Destroy(impactAnimationGO, 2f);


        }

    }

}
