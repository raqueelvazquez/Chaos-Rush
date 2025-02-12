using UnityEngine;

public class Movimiento : MonoBehaviour
{
    // Variables para configurar la velocidad, fuerza de salto y sensibilidad del ratón
    public float speed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;

    // Variable para verificar si el personaje está en el suelo
    private bool isGrounded;

    private Rigidbody rb;
    private Animator animator;

    // Controladores de animación
    public RuntimeAnimatorController playerAnimator; // Animación en estado normal
    public RuntimeAnimatorController rootMotionAnimator; // Animación cuando se mueve
    
    private RuntimeAnimatorController currentAnimator; // Variable para controlar el animador actual
    
    // Referencia al objeto que sostiene la cámara
    public Transform cameraHolder;

    // Variable para controlar la rotación de la cámara en el eje X
    private float rotationX = 0f; 

    void Start()
    {
        // Obtener el componente Rigidbody del objeto
        rb = GetComponent<Rigidbody>();  
        // Obtener el componente Animator del objeto
        animator = GetComponent<Animator>();  

        // Bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;  

        // Asignar la animación inicial
        animator.runtimeAnimatorController = playerAnimator;
        currentAnimator = playerAnimator;
    }

    void Update()
    {
        // Llamamos a las funciones de movimiento, rotación y salto en cada frame
        Move();
        RotateCamera();
        Jump();
    }

    void Move()
    {
        // Obtener la entrada del jugador (WASD o flechas)
        float moveX = Input.GetAxis("Horizontal");  
        float moveZ = Input.GetAxis("Vertical");  

        // Calcular la dirección del movimiento en función de la rotación del personaje
        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Aplicar el movimiento al personaje (directamente sobre la posición)
        transform.position += moveDirection * speed * Time.deltaTime; 

        // Obtener la magnitud del movimiento (para animaciones)
        float movementSpeed = moveDirection.magnitude;
        animator.SetFloat("Speed", movementSpeed); 

        // Cambiar animaciones dinámicamente según la velocidad del personaje
        if (movementSpeed > 0.1f && currentAnimator != rootMotionAnimator)
        {
            animator.runtimeAnimatorController = rootMotionAnimator; // Cambiar a animación de movimiento
            animator.Play("Run"); // Reproducir animación de correr
            currentAnimator = rootMotionAnimator;
        }
        else if (movementSpeed <= 0.1f && currentAnimator != playerAnimator)
        {
            animator.runtimeAnimatorController = playerAnimator; // Cambiar a animación en reposo
            animator.Play("Idle"); // Reproducir animación de espera
            currentAnimator = playerAnimator;
        }
    }

    void RotateCamera()
    {
        // Obtener la entrada del ratón para girar la cámara
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; 

        // Rotar el personaje horizontalmente según el movimiento del mouse
        transform.Rotate(Vector3.up * mouseX); 

        // Ajustar la rotación vertical de la cámara con límites
        rotationX -= mouseY; 
        rotationX = Mathf.Clamp(rotationX, -60f, 60f); // Limitar la rotación en el eje X
        cameraHolder.localRotation = Quaternion.Euler(rotationX, 0f, 0f); 
    }

    void Jump()
    {
        // Verificar si el jugador presionó espacio y está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Aplicar una fuerza de salto en el eje Y
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            
            // Indicar que el personaje está en el aire
            isGrounded = false; 

            // Activar la animación de salto
            animator.SetTrigger("Jump"); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el personaje colisionó con el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Permitir que vuelva a saltar
        }
    }
}