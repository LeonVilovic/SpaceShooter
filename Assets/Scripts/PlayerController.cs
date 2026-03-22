using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction fireAction;

    public Vector2 moveVector;

    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;
    //private Rigidbody2D rigidbody2D;

    bool isAttacking;

    [Header("Movement")]
    public float moveSpeed = 3f;
    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");

        animator =  GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Attack();
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    private void Move()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        Vector3 move = moveVector * moveSpeed * Time.deltaTime;

        transform.position += move;
    }

    private void Attack()
    {
        if (fireAction.WasPressedThisFrame())
        {
            if (moveVector.y > 0)
            {
                animator.Play("Slash3");
            }
            if (moveVector.y < 0)
            {
                animator.Play("Slash2");
            }
            if (moveVector.y == 0)
            {
                animator.Play("Slash1");
            }
        }
        if ((animatorStateInfo.IsName("Slash1") || animatorStateInfo.IsName("Slash2") || animatorStateInfo.IsName("Slash3")) && animatorStateInfo.normalizedTime >= 1f)
        {
            animator.Play("Idle");
        }
    }
}
