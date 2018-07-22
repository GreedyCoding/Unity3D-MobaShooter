using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    //Reference to the player camera for the raycasting
    public Camera fpsCam;
    //Reference to the muzzle particle system that is played when shooting
    public ParticleSystem muzzleFlash;
    //Reference to the impact particle system which gets instantiated as a gameobject
    public GameObject impactAnimation;
    	
	void Update () {
		
        //If player pressed Mouse Button 1 shoot the gun
        if (Input.GetButtonDown("Fire1")) {

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

            //Every time we hit something we instantiate a particle system pointing out from the impact point
            GameObject impactAnimationGO = Instantiate(impactAnimation, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy the GameObject after 2 seconds
            Destroy(impactAnimationGO, 2f);


        }

    }

}
