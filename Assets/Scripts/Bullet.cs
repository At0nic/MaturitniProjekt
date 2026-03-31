using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 1;
    public string ownerTag; // set to "Player" or "Enemy" so bullets don't hit their own team

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't hit whoever fired the bullet
        if (collision.gameObject.CompareTag(ownerTag)) return;

        // Try to apply damage
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Spawn hit effect and destroy bullet
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}