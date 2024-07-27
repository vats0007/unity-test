using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float gravity = -9.81f; // Gravity force
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 2f; // Jump height
    public Transform cam;
    public Animator animator;
    private float turnSmoothVelocity;
    private bool isGrounded;
    private Vector3 verticalVelocity; // To track vertical movement
    [SerializeField]private bool isInverted = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isInverted)
        {
            isInverted = !isInverted;
            gravity = -gravity;
        }

        if (isGrounded)
        {
            animator.SetBool("IsGrounded", true);
            verticalVelocity.y = -2f;

            // Detect jump input
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
                animator.SetTrigger("Jump"); // Set jump animation trigger
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            
        }
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDirection.normalized);
            animator.SetBool("IsMoving", true);
        }
        else 
        {
            animator.SetBool("IsMoving", false);
        }
    }

}
