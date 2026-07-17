using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(float direction)
    {
        if (rb == null)
        {
            Debug.LogError("Falta un Rigidbody2D en el proyectil.");
            return;
        }

        rb.velocity = new Vector2(direction * speed, 0f);

        if (spriteRenderer != null)
            spriteRenderer.flipX = direction < 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        Destroy(gameObject);
    }
}