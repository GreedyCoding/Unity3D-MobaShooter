using System.Collections;
using UnityEngine;

public class HeroOneGun : MonoBehaviour {

    //REFERENCE FIELDS
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleFlashTwo;
    public GameObject impactAnimation;
    public GameObject rocket;
    public Transform firePoint;

    //GUN
    private float nextTimeToShoot = 0f;         //Stores the time the gun can shoot the next time, 0 so you can instantly shoot
    public static float currentAmmo = 40;       //CurrentAmmo is same as MaxAmmo so gun is fully loaded on start of the game
    public static float maxAmmo = 40;          
    private float reloadTime = 1f;              
    public float damage = 5f;
    public float fireRate = 30f;
    public float impactForce = 200f;

    void Update ()
    {
        //If player pressed Mouse Button 1 and its already time to fire again, shoot the gun
        if (Input.GetKey(GameManager.GM.shootKey) && Time.time >= nextTimeToShoot && currentAmmo > 0)
        {
            //Then add the 1/firerate to the current time to calculate the next time to shoot
            nextTimeToShoot = Time.time + (1f / fireRate);
            Shoot();
            currentAmmo -= 1;
            Debug.Log(currentAmmo);
        }

        //If our ammo is 0 we start the couroutine for reloading
        if (currentAmmo == 0)
        {
            StartCoroutine(Reload(reloadTime));
        }
	}

    void Shoot()
    {
        //Raycast variable that will hold the information about the positon the ray hits
        RaycastHit hit;

        //Every time we shoot we display the muzzle flash animation
        muzzleFlash.Play();
        muzzleFlashTwo.Play();

        //Shoot a ray from the camera postion straight forward and store the hitinfo in the hit variable
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            //If the ray hit something and the Target component exists we store thsi component in the targe var
            Target target = hit.transform.GetComponent<Target>();

            //If there is a target component we let the target take damage
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //If the hit component has a rigidbody add a force to it
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //Every time we hit something we instantiate a impact animation pointing out from the impact point
            GameObject impactAnimationGO = Instantiate(impactAnimation, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy the impact animation GameObject after 1 seconds
            Destroy(impactAnimationGO, 1f);
        }
    }

    IEnumerator Reload(float _reloadTime)
    {
        yield return new WaitForSeconds(_reloadTime);
        currentAmmo = maxAmmo;
    }

}
