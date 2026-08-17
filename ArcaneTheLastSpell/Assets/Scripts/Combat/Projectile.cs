using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 3f;

    [Header("Audio")]
    [SerializeField] private AudioClip clipDisparo;
    [SerializeField] private AudioClip clipImpacto;
    [SerializeField] private float volumenAudio = 0.7f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void Start()
    {
        if (clipDisparo != null)
        {
            AudioSource.PlayClipAtPoint(clipDisparo, transform.position, volumenAudio);
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Proyectil impactó a {other.name} por {damage} de daño");
            ReproducirImpacto();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            ReproducirImpacto();
            Destroy(gameObject);
        }
    }

    public void SetDirection(float direction)
    {
        SetDirection(new Vector2(direction, 0f));
    }

    private void ReproducirImpacto()
    {
        if (clipImpacto != null)
        {
            AudioSource.PlayClipAtPoint(clipImpacto, transform.position, volumenAudio);
        }
    }
}
