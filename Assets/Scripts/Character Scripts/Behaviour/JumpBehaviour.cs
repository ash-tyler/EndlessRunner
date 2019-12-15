using UnityEngine;

public class JumpBehaviour : ControllableCharacterBehaviour
{
    #region Inspector Fields
    [Header("Jump Settings")]
    public float jumpVelocity = 5f;
    public float fallGravityMultiplier = 2.5f;
    public float jumpReleasedGravityMultiplier = 2f;
    #endregion

    public bool TriggerJump { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        PlayerGroundDetection.Instance.OnGroundedChange += HandleIsGroundedChange;
    }

    private void Update()
    {
        if (canAct == false)
        {
            return;
        }

        if (behaviourButton.GetButtonDown)
        {
            TriggerJump = true;
        }

        //Ensures Jump animation corresponds to Y velocity (Falling when negative, rising when positive)
        anim.SetFloat("YVelocity", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        if (canAct == false)
        {
            return;
        }

        if (TriggerJump && PlayerGroundDetection.Instance.IsGrounded)
        {
            Jump();
        }

        HandleGravityScale();
    }


    #region Jumping Functions
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        rb.velocity += Vector2.up * (jumpVelocity / 2);
        TriggerJump = false;
        IsJumping = true;
        anim.SetBool("Jump", true);
    }

    private void HandleGravityScale()
    {
        //When falling
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravityMultiplier;
        }

        //When rising and not holding jump
        else if (rb.velocity.y > 0 && !behaviourButton.GetButton)
        {
            rb.gravityScale = jumpReleasedGravityMultiplier;
        }

        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void HandleIsGroundedChange(bool isGrounded)
    {
        anim.SetBool("Grounded", isGrounded);

        if (isGrounded && IsJumping)
        {
            anim.SetBool("Jump", false);
            IsJumping = false;
        }
    }
    #endregion
}