using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 2;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(Collider2D other)
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y);
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
}
