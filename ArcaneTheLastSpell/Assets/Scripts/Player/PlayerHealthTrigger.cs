using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTrigger : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Arcane recibió daño de contacto");
        }
    }
} 

