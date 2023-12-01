using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5.0f;
    public float turnSpeed = 120.0f;

    private Transform playerTransform;
    private Transform cameraTransform;
    private PauseManager pauseManager;

    public bool estaAtacando;
    private void Start()
    {
        playerTransform = transform;
        cameraTransform = Camera.main.transform;
        pauseManager = FindObjectOfType<PauseManager>(); // Asegúrate de asignar el PauseManager adecuado.
    }

    private void Update()
    {
        // Movimiento del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            // Rotar el jugador en la dirección del movimiento
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            playerTransform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Mover al jugador en la dirección del movimiento
            Vector3 move = playerTransform.forward * moveSpeed * Time.deltaTime;
            
            if(estaAtacando == false)
            {
                playerTransform.position += move;
            }
        }

        animator.SetFloat("velocidad", moveDirection == Vector3.zero ? 0 : 1);

    }
}
