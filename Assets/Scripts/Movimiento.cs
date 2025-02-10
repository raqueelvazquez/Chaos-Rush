using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;

    private bool isGrounded;
    private Rigidbody rb;
    private Animator animator;

    public RuntimeAnimatorController playerAnimator;  
    public RuntimeAnimatorController rootMotionAnimator; 

    private RuntimeAnimatorController currentAnimator;
    
    public Transform cameraHolder;

    private float rotationX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        animator.runtimeAnimatorController = playerAnimator;
        currentAnimator = playerAnimator;
    }

    void Update()
    {
        Move();
        RotateCamera();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        transform.position += moveDirection * speed * Time.deltaTime;

        float movementSpeed = moveDirection.magnitude;
        animator.SetFloat("Speed", movementSpeed);

        if (movementSpeed > 0.1f && currentAnimator != rootMotionAnimator)
        {
            animator.runtimeAnimatorController = rootMotionAnimator;
            animator.Play("Run");
            currentAnimator = rootMotionAnimator;
        }
        else if (movementSpeed <= 0.1f && currentAnimator != playerAnimator)
        {
            animator.runtimeAnimatorController = playerAnimator;
            animator.Play("Idle");
            currentAnimator = playerAnimator;
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
        cameraHolder.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
