using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [SerializeField] private float gravityScale = 1;
    [SerializeField] private float walkSpeed = 1;
    [SerializeField] private float sprintSpeed = 2;
    [SerializeField] private float acceleration = .5f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float currentSpeed;
    private Vector3 velocityXZ; //moveInput
    private float velocityY;

    private bool isGrounded;
    private bool isSprint;
    public bool IsSprint => isSprint;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        CheckIsGrounded();
        ResetVelocityY();
        CalculateAcceleration();
    }

    public void Move()
    {
        CalculateVelocityXZ();
        CalculateVelocityY();
        Vector3 velocity = new Vector3(velocityXZ.x, velocityY, velocityXZ.z);

        characterController.Move(velocity);
    }

    private void CalculateVelocityXZ()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 xDir = moveDirection.x * cameraTransform.right;
        Vector3 zDir = moveDirection.z * cameraTransform.forward;
        Vector3 direction = xDir + zDir;
        direction.y = 0;

        if (moveDirection.magnitude > .01f)
            velocityXZ = direction.normalized * currentSpeed * Time.deltaTime;
        else velocityXZ = Vector3.zero;
    }

    private void CalculateVelocityY()
    {
        velocityY += Physics.gravity.y * gravityScale * Time.deltaTime;
    }

    private void ResetVelocityY()
    {
        if (isGrounded && velocityY < 0)
            velocityY = -2;
    }

    private void CalculateAcceleration()
    {
        if (moveDirection.magnitude > .01f)
        {
            if (isSprint)
                currentSpeed += acceleration * Time.deltaTime;
            else
                currentSpeed -= acceleration * Time.deltaTime;

            currentSpeed = Mathf.Clamp(currentSpeed, walkSpeed, sprintSpeed);
        }
        else
            currentSpeed = 0;
    }

    public void SaveMoveDirection(Vector2 inputDirection)
    {
        moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void SetSprint(bool isSprint) => this.isSprint = isSprint;

    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        isGrounded = Physics.CheckSphere(transform.position, .5f, groundLayer);
    }
}
