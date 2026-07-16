using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Reinicia el nivel actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}