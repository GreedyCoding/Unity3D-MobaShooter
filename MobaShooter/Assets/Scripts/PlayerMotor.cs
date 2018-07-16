using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;

    private Rigidbody rb;

    void Start() {

        rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate() {

        PerformMovement();

    }


    //Gets a movement vector
    public void Move(Vector3 _velocity) {

        velocity = _velocity;

    }

    void PerformMovement() {

        if (velocity != Vector3.zero) {

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        }
    }

}
