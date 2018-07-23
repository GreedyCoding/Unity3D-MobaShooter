using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour {

    [SerializeField][Range(1f, 10f)]
    private float speed = 5f;

    [SerializeField][Range(1f, 10f)]
    private float jumpVelocity = 5f;

    [SerializeField][Range(0.01f, 15f)]
    private float mouseSensitivity = 5f;

    //Setting a reference to the PlayerMotor
    private PlayerMotor motor;

    private void Start() {

        //Get the playermotor component
        motor = GetComponent<PlayerMotor>();

    }

    private void Update() {

    //Calculate movement velocity as Vector3

        //Gets a vetcor based on the input(Keyboard and Controller)
        // W-UP    ( 1, 0, 0)
        // S-DOWN  (-1, 0, 0)
        // A-LEFT  ( 0, 0, 1)
        // D-RIGHT ( 0, 0,-1)
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        //transform.right/forward takes the current rotation into consideration
        Vector3 _moveHorizontal = transform.right * _xMovement;
        Vector3 _moveVertical = transform.forward * _zMovement;

        //Vector for jumping pointing up multiplied by the jumpVelocity
        Vector3 _jumpVector = Vector3.up * jumpVelocity;

        //Adding both movement axis together and normalizing them so they only serve for direction
        //Multiplying the direction by the speed to control the movespeed
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //Let the Motor move with the calculated velocity
        motor.Move(_velocity);

    //Calculate Horizontal rotation as a Vector3 for turning around
       
        //Gets a vector based on the horizontal input of the mouse
        float _yRotation = Input.GetAxisRaw("Mouse X");

        //Mulitply the vector by the mouseSensitivity variable to make the turnspeed adjustable
        Vector3 _rotation = new Vector3(0f, _yRotation, 0f) * mouseSensitivity;

        //Let the motor rotate the player by the calculated rotation
        motor.Rotate(_rotation);

    //Calculate vertical rotation as a Vector3 for turning the camera up and down

        //Gets a vector based on the horizontal input of the mouse
        float _xRotation = Input.GetAxisRaw("Mouse Y");

        //Mulitply the vector by the mouseSensitivity variable to make the turnspeed adjustable
        Vector3 _cameraRotation = new Vector3(_xRotation, 0f, 0f) * mouseSensitivity;

        //Let the motor rotate the camera by the calculated rotation
        motor.RotateCamera(_cameraRotation);

    //Jump when player presses the space key

        //If the space key is pressed and the player is below 1,1m he can jump 
        if (Input.GetButtonDown("Jump")) {

            //we set the velocity of the rigid
            motor.Jump(_jumpVector);

        }

    }

}
