using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoobyDoobyEnemy : MonoBehaviour
{
    private Rigidbody2D _doobyRb;
    [SerializeField] private float enemySpeed;


    void Start()
    {
        _doobyRb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _doobyRb.velocity = new Vector2(enemySpeed, 0);
    }

    private void FlipDoobyDooby()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_doobyRb.velocity.x)), 1);
    }

    

    private void OnTriggerExit2D(Collider2D other)
    {
        FlipDoobyDooby();
        enemySpeed = -enemySpeed;
    }
}
