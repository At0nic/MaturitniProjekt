using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public static bool playerIsDead = false;
    
    [Header("Death Sprites")]
    public SpriteRenderer bodyRenderer;
    public Sprite bodyDeathSprite;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            playerIsDead = true;
        }
        
        if (bodyRenderer != null && bodyDeathSprite != null)
            bodyRenderer.sprite = bodyDeathSprite;

        foreach (MonoBehaviour script in GetComponentsInChildren<MonoBehaviour>())
        {
            if (script != this) script.enabled = false;
        }

        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}