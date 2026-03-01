using UnityEngine;
using UnityEngine.InputSystem;

public class SpacePlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public Vector2 boundsX = new Vector2(-10f, 10f);
    public Vector2 boundsY = new Vector2(-5f, 5f); // For 2D/top-down
    public Vector2 boundsZ = new Vector2(-10f, 10f); // For 3D forward/back

    //private PlayerInputActions input;
    private Vector2 moveInput;

    private void Awake()
    {
        //input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        //input.Player.Enable();
        //input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        //input.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        //input.Player.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Top-down 2D: X/Y movement
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f) * moveSpeed * Time.deltaTime;

        transform.position += move;

        // Clamp position inside bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundsX.x, boundsX.y),
            Mathf.Clamp(transform.position.y, boundsY.x, boundsY.y),
            transform.position.z
        );
    }
}
