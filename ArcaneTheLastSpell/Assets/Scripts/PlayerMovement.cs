using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 11f;

    [Header("Detección del suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ataque")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float horizontalInput;
    private bool jumpRequested;
    private float firePointDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (firePoint != null)
        {
            firePointDistance = Mathf.Abs(firePoint.localPosition.x);
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        bool grounded = IsGrounded();

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", grounded);

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            UpdateFirePointPosition();
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            UpdateFirePointPosition();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.velocity.y
        );

        if (jumpRequested && IsGrounded())
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                jumpForce
            );
        }

        jumpRequested = false;
    }

    private void UpdateFirePointPosition()
    {
        if (firePoint == null)
        {
            return;
        }

        Vector3 newPosition = firePoint.localPosition;

        newPosition.x = spriteRenderer.flipX
            ? -firePointDistance
            : firePointDistance;

        firePoint.localPosition = newPosition;
    }

    private void Attack()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning(
                "Falta asignar Projectile Prefab o Fire Point."
            );

            return;
        }

        animator.SetTrigger("Attack");

        GameObject newProjectiles = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );
          Projectiles projectiles =
        newProjectiles.GetComponent<Projectiles>();
        if (projectiles == null)
        {
            Debug.LogError(
                "El prefab Projectile no tiene agregado el script Projectile."
            );

            return;
        }

        float direction = spriteRenderer.flipX ? -1f : 1f;

        projectiles.SetDirection(direction);
    }

    private bool IsGrounded()
    {
        if (groundCheck == null)
        {
            return false;
        }

        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}