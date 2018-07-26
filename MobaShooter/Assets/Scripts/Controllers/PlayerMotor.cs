using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {

    //With this reference we are able to enable up and down movement on the camera
    //If you dont want the player to look up and down do not reference a camera
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpVelocity = Vector3.zero;

    [SerializeField]
    private float fallMultiplier = 0.4f;
    private float distanceToGround;

    private Rigidbody rb;

    void Start() {

        //Get the rigidbody compenent
        rb = GetComponent<Rigidbody>();

        //Gets the distance to the ground from the player collider
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
            
    }

    //Every fixed Update we perform all our movement
    private void FixedUpdate() {

        PerformMovement();
        PerformRotation();
        PerformJumping();

    }

    private bool isGrounded() {

        //Shooting a raycast downwards to check if the ground is there
        //returns true if we hit the ground with the ray
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);

    }
    
    public void Move(Vector3 _velocity) {

        //Sets the move velocity to the input move velocity input from the playercontroller
        velocity = _velocity;

    }

    void PerformMovement() {

        //Performs movement if the velocity vector is not zero
        if (velocity != Vector3.zero) {

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        }
    }

    public void RotatePlayer(Vector3 _rotation) {

        //Sets the rotation to the input rotation input from the playercontroller
        rotation = _rotation;

    }

    public void RotateCamera(Vector3 _cameraRotation) {

        //Gets a rotation vector for the camera from the playercontroller
        cameraRotation = _cameraRotation;

    }

    void PerformRotation() {

        if (rotation != Vector3.zero) {

            //Rigidbody uses quarternion system so we need to convert our angle into a quaternion
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            //If there is a camera attached to the Player we perfrom camerarotation
            if (cam != null) {
                cam.transform.Rotate(-cameraRotation);
            }

        }
    }


    public void Jump(Vector3 _jumpVelocity) {

        //Sets the jumpvelocity to the input jumpvelocity input from the playercontroller
        jumpVelocity = _jumpVelocity;

    }

    void PerformJumping() {


        //If the jumpvelocity is not zero and the player is on the ground jump
        if (jumpVelocity != Vector3.zero && isGrounded()) {

            //Set the velocity of the rigidbody to the jumpvelocity
            rb.velocity = jumpVelocity;
            //Reset the jumpvelocity afterwards so the player doesnt go flying
            jumpVelocity = Vector3.zero;

        }

        //If the rigidbody has a velocity lower then 0(the player is falling) we add a multiplier to gravity
        if (rb.velocity.y < 0) {

            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;

        }

    }

}
