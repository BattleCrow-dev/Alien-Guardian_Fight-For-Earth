using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;

    private float horizontalInput;
    private bool isJump;

    private Rigidbody2D rb;
    private Animator anim;

    private Transform groundCheck;
    private ParticleSystem particles;
    private Transform ui;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.GetChild(0);
        particles = transform.GetChild(2).GetComponent<ParticleSystem>();
        ui = transform.GetChild(3);
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            anim.SetInteger(GlobalStringVariables.ANIMATION_STATE_NAME, 2);
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            particles.Emit(1);
            anim.SetInteger(GlobalStringVariables.ANIMATION_STATE_NAME, 1);
        }
        else
        {
            anim.SetInteger(GlobalStringVariables.ANIMATION_STATE_NAME, 0);
        }
        
    }

    private void FixedUpdate()
    {
        if (isJump && IsGrounded())
        {
            AudioController.Instance.PlayJumpSound();
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            isJump = false;
        }

        rb.velocity = new Vector2(horizontalInput * _moveSpeed, rb.velocity.y);

        ui.localScale = new Vector3(horizontalInput == 0 ? ui.localScale.x : horizontalInput >= 0 ? 1f : -1f, 1f, 1f);
        particles.transform.localScale = new Vector3(horizontalInput == 0 ? particles.transform.localScale.x : horizontalInput >= 0 ? 1f : -1f, 1f, 1f);
        transform.localScale = new Vector3(horizontalInput == 0 ? transform.localScale.x : horizontalInput >= 0 ? 1f : -1f, 1f, 1f);

        isJump = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircleAll(groundCheck.position, 0.3f, LayerMask.GetMask("Ground")).Length > 0;
    }

    public void SetHorizontalInput(float horizontalInput)
    {
        this.horizontalInput = horizontalInput;
    }
    public void SetIsJump(bool isJump)
    {
        if(!this.isJump)
            this.isJump = isJump;
    }
    public bool GetFlipX()
    {
        return transform.localScale.x > 0;
    }
}
