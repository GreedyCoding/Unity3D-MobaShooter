using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    //With this we are able to disable up and down movement on the camera
    //Could be used if you dont want the player to loot up and down
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

    void Start() {

        rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate() {

        PerformMovement();
        PerformRotation();

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

}
