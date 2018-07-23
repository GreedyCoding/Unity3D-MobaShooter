using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    //With this reference we are able to enable up and down movement on the camera
    //Could be used if you dont want the player to look up and down by not referencing a camera
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpVelocity = Vector3.zero;

    private float fallMultiplier = 0.4f;
    private float distanceToGround;

    private Rigidbody rb;

    void Start() {

        //Get the rigidbody compenent
        rb = GetComponent<Rigidbody>();

        //Gets the distance to the ground from the player collider
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
            
    }

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
    
    #region Movement - Move(), PerformMovement();

    //Gets a movement vector
    public void Move(Vector3 _velocity) {

        velocity = _velocity;

    }

    //Performs movement if the velocity vector is not zero
    void PerformMovement() {

        if (velocity != Vector3.zero) {

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        }
    }

    #endregion

    #region Rotation - Rotate(), PerformRotation();

    //Gets a rotation vector
    public void Rotate(Vector3 _rotation) {

        rotation = _rotation;

    }

    //Performs movement if the velocity vector is not zero
    void PerformRotation() {

        if (rotation != Vector3.zero) {

            //Uses quarternion system for rotation
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (cam != null) {
                cam.transform.Rotate(-cameraRotation);
            }

        }
    }

    #endregion

    #region Camera Rotation - RotateCamera(), uses PerformRotation() to do the actual rotation;

    //Gets a rotation vector
    public void RotateCamera(Vector3 _cameraRotation) {

        cameraRotation = _cameraRotation;

    }

    #endregion

    //Gets the jump vector
    public void Jump(Vector3 _jumpVelocity) {

        jumpVelocity = _jumpVelocity;

    }

    void PerformJumping() {


        //If the jumpvelocity is not zero and the player is on the ground jump
        if (jumpVelocity != Vector3.zero && isGrounded()) {

            //Set the velocity of the rigidbody to the jumpvelocity
            rb.velocity = jumpVelocity;
            //Reset the jumpvelocity afterwards
            jumpVelocity = Vector3.zero;

        }

        if (rb.velocity.y < 0) {

            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;

        }

    }

}
