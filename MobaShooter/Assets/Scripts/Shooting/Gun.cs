using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactAnimation;
    	
	void Update () {
		
        if (Input.GetButtonDown("Fire1")) {

            Shoot();

        }

	}

    void Shoot() {
        
        RaycastHit hitInfo;

        muzzleFlash.Play();

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range)) {

            Target target = hitInfo.transform.GetComponent<Target>();

            if (target != null) {

                target.TakeDamage(damage);

            }

            GameObject impactAnimationGO = Instantiate(impactAnimation, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactAnimationGO, 1f);


        }

    }

}
