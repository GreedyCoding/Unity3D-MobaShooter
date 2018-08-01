using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private float range = 1000f;

    RaycastHit hit;

    private void FixedUpdate() {


        Transform transform = GetComponent<Transform>();
        Physics.Raycast(transform.position, transform.forward, out hit, range);
        transform.position += hit.transform.position;
        Debug.Log(hit);

    }

}
