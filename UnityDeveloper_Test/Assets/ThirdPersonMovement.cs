using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform hologram;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravity = -9.81f;
    public Transform groundCheckPoint;
    public LayerMask groundLayer;
    public float checkRadius = 0.1f;
    public Vector3 jumpDir;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector3 gravityDirection = -Vector3.down;
    private Vector3 hologramDirection;
    private Quaternion hologramRotation;

    void Start()
    {

    }

    void Update()
    {
        isGrounded = IsGrounded();
        Debug.Log(isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            transform.Translate(speed * Time.deltaTime * direction);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetBool("IsGrounded", isGrounded);
        HandleGravity();
    }

    private void HandleGravity()
    {
        hologramDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.U))
        {
            hologramRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            hologramDirection = new Vector3(0, 0, -1);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            hologramRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            hologramDirection = new Vector3(0, 0, 1);
        }
        else if (Input.GetKey(KeyCode.H))
        {
            hologramRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            hologramDirection = new Vector3(1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            hologramRotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            hologramDirection = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.N))
        {
            hologramRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            hologramDirection = new Vector3(0, -1, 0);
            jumpDir = new Vector3(0f, jumpForce, 0f);
        }
        else if (Input.GetKey(KeyCode.M))
        {
            hologramRotation = Quaternion.Euler(new Vector3(-180f, 0f, 0f));
            hologramDirection = new Vector3(0, 1, 0);
            jumpDir = new Vector3(0f, -jumpForce, 0f);
        }

        if (hologramDirection != Vector3.zero)
        {
            hologram.gameObject.SetActive(true);
            hologram.SetPositionAndRotation(transform.position + new Vector3(0, 1f, 0f), hologramRotation);
        }
        else
        {
            hologram.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return) && hologramDirection != Vector3.zero)
        {
            gravityDirection = hologramDirection;
            transform.position = hologram.position;
            transform.rotation = hologram.rotation;
            if (IsGrounded())
            {
                transform.forward = hologram.forward;
            }

            Physics.gravity = gravityDirection * Mathf.Abs(gravity);

            velocity = gravityDirection * Mathf.Abs(velocity.y);
        }
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckPoint.position, checkRadius, groundLayer);
    }
}
