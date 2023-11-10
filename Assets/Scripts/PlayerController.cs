using System;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbingSpeed;
    private BoxCollider2D feetCollider;
    private CapsuleCollider2D bodyCollider;
    //[SerializeField] private Transform bulletTransformObject;
    //[SerializeField] private GameObject bullet;
    private bool _isAlive = true;
    [SerializeField] private AudioClip deathSong;
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }
        Move();
        FlipSprite();
        Jump();
        Climbing();
        Shot();
        Die();
    }
    private void Move()
    {
        if (!_isAlive)
        {
            return;
        }
        //if(!(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))){_playerAnimator.SetBool("isRunning",false);;}
        float inputX = Input.GetAxisRaw("Horizontal") ;
        Vector2 horizontalVector = new Vector2(inputX * playerSpeed, _playerRb.velocity.y);
        _playerRb.velocity = horizontalVector;
        bool _hasSpeedX = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;
        _playerAnimator.SetBool("isRunning",_hasSpeedX);
    }
    private void Climbing()
    {
        if (!_isAlive)
        {
            return;
        }
        if (!(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))))
        {
            _playerRb.gravityScale =3;
            _playerAnimator.SetBool("isClimbing",false);
            return;
        }
        float inputY = Input.GetAxisRaw("Vertical");
        _playerRb.velocity = new Vector2(_playerRb.velocity.x, inputY * climbingSpeed);
        _playerRb.gravityScale = 0;
        bool hasSpeedY = Mathf.Abs(_playerRb.velocity.y) > Mathf.Epsilon;
        _playerAnimator.SetBool("isClimbing",hasSpeedY);
    }
    private void FlipSprite()
    {
        if (!_isAlive)
        {
            return;
        }
        bool hasSpeedX = Mathf.Abs(_playerRb.velocity.x) > Mathf.Epsilon;
        if (hasSpeedX)
        {
            transform.localScale = new Vector2( Mathf.Sign(_playerRb.velocity.x),1f);
        }
    }
    private void Jump()
    {
        if (!_isAlive)
        {
            return;
        }
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerRb.velocity += new Vector2(0, jumpSpeed);
            bool hasSpeedY = Mathf.Abs(_playerRb.velocity.y) > Mathf.Epsilon;
            _playerAnimator.SetBool("isJumping",hasSpeedY);
        }
    }

    private void Die()
    {
        
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Lava")))
        {
            GetComponent<AudioSource>().PlayOneShot(deathSong);
            _isAlive = false;
            _playerRb.velocity = new Vector2(20, 20);
            CameraShaker.Instance.ShakeOnce(4f, 2f, .5f, 1.5f);
            _playerAnimator.SetTrigger("Death");
            //Invoke("DeathTime",1f);
            StartCoroutine("DeathTime");
            FindObjectOfType<PlayerStateManager>().ProcessOfPlayerDeath();
        }
        
    }

    private void Shot()
    {

        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(bullet, bulletTransformObject.position, transform.rotation);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Ladder"))
        {
            _playerAnimator.SetBool("isJumping",false);
        }
        
    }


    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(1f);
        bodyCollider.gameObject.SetActive(false);
    }
}
