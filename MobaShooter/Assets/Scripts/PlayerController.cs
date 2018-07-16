using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    //SerializeField makes it editable from the Unity Editor
    private float speed = 5f;

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

        //Adding both movement axis together and normalizing them so they only serve for direction
        //Multiplying the direction by the speed to control the movespeed
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //Let the Motor move with the desired calculated velocity
        motor.Move(_velocity);

    //Calculate rotation as a Vector3

    }

}
