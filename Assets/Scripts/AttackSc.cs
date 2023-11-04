using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSc : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private float wayOfBullet;
    private PlayerController _playerController;
    private Rigidbody2D rb;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        wayOfBullet = _playerController.transform.localScale.x * bulletSpeed;
    }

    private void Update()
    {
        Shot();
    }

    private void Shot()
    {
        rb.velocity = new Vector2(wayOfBullet, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Dooby"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
