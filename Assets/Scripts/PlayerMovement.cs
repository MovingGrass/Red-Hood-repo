using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Audio Sources")]
    public AudioSource jumpAudioSource;
    public AudioSource walkAudioSource;
    public AudioSource collectAudioSource;
    public AudioSource damageAudioSource;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveHorizontal;
    private bool isJumping = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Ensure all audio sources are set
        if (jumpAudioSource == null || walkAudioSource == null || 
            collectAudioSource == null || damageAudioSource == null)
        {
            Debug.LogError("One or more AudioSources are not set on the player!");
        }
    }

    void Update()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
            return;

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        UpdateAnimations();
        UpdateFacingDirection();
        UpdateSounds();
    }

    void FixedUpdate()
    {
        if (PauseManager.Instance != null && PauseManager.Instance.IsPaused)
            return;

        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        jumpAudioSource.Play();
    }

    void UpdateAnimations()
    {
        animator.SetBool("IsWalking", moveHorizontal != 0);
        animator.SetBool("IsJumping", isJumping && rb.velocity.y > 0);
        animator.SetBool("IsFalling", !isGrounded && rb.velocity.y < 0);

        if (isGrounded && (isJumping || animator.GetBool("IsFalling")))
        {
            isJumping = false;
            animator.SetTrigger("Land");
        }
    }

    void UpdateFacingDirection()
    {
        if (moveHorizontal != 0)
        {
            transform.rotation = Quaternion.Euler(0, moveHorizontal > 0 ? 0 : 180, 0);
        }
    }

    void UpdateSounds()
    {
        if (moveHorizontal != 0 && isGrounded)
        {
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            walkAudioSource.Stop();
        }
    }

    public void PlayCollectSound()
    {
        collectAudioSource.Play();
    }

    public void PlayDamageSound()
    {
        damageAudioSource.Play();
    }
}
