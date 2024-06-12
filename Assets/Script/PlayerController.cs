using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    
    [SerializeField]
    private float mouseSensitivityX;
    
    [SerializeField]
    private float mouseSensitivityY;

    private PlayerMotor motor;

    private Vector3 playerVelocity;

    public Rigidbody rb;

    private bool isOnGround = true;


    private void Awake(){
        playerAnim = GetComponent<Animator>();
    }


    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // calculer la vélocité du mvt du joueur
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // application de la vélocité
        motor.Move(velocity);
        
        // rotation du joueur
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);
        
        // rotation de la caméra
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * mouseSensitivityY;

        motor.RotateCamera(cameraRotationX);

        if(Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetButtonDown("Jump")&& isOnGround)
        {
            rb.AddForce(new Vector3(0,8,0),ForceMode.Impulse);
            isOnGround = false;
        }

        if(Input.GetButtonDown("Z"))
        {

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isOnGround = true;
    }
}
