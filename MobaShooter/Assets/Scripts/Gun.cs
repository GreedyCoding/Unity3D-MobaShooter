using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    	
	void Update () {
		
        if (Input.GetButtonDown("Fire1")) {

            Shoot();

        }

	}

    void Shoot() {

        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range)) {
            Debug.Log(hitInfo.transform.name);
        }

    }

}
