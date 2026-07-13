using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Configuración de hechizo")]
    public float cooldown = 0.5f;

    private PlayerMovement playerMovement;
    private float cooldownTimer = 0f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = cooldown;
        }
    }

    void Shoot()
    {
        Vector2 direction = playerMovement.LastMoveDirection;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetDirection(direction);
    }
}
