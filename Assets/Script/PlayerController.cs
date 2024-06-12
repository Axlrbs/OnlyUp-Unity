using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float mouseSensitivityX;

    [SerializeField]
    private float mouseSensitivityY;

    private PlayerMotor motor;
    public Rigidbody rb;

    private bool isOnGround = true;


    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
    }


    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();

        // Configure Rigidbody
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        // Calculer la vélocité du mouvement du joueur
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // Application de la vélocité
        motor.Move(velocity);

        // Rotation du joueur
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;
        motor.Rotate(rotation);

        // Rotation de la caméra
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRot * mouseSensitivityY;
        motor.RotateCamera(cameraRotationX);

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Saut
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
            isOnGround = false;
            ResetTriggers();
            playerAnim.SetTrigger("JumpTrigger");
        }

        // Gérer les animations avec des triggers
        else if (isOnGround)
        {
            if (zMove > 0)
            {
                ResetTriggers();
                playerAnim.SetTrigger("RunTrigger");
            }
            else if (xMove < 0)
            {
                ResetTriggers();
                playerAnim.SetTrigger("LeftTrigger");
            }
            else if (xMove > 0)
            {
                ResetTriggers();
                playerAnim.SetTrigger("RightTrigger");
            }
            else if (zMove < 0)
            {
                ResetTriggers();
                playerAnim.SetTrigger("BackTrigger");
            }
            else
            {
                ResetTriggers();
                playerAnim.SetTrigger("GroundTrigger");
            }

        }
    }

    private void ResetTriggers()
    {
        playerAnim.ResetTrigger("RunTrigger");
        playerAnim.ResetTrigger("LeftTrigger");
        playerAnim.ResetTrigger("RightTrigger");
        playerAnim.ResetTrigger("BackTrigger");
        playerAnim.ResetTrigger("JumpTrigger");
        playerAnim.ResetTrigger("GroundTrigger");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}