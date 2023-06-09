using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float _speed;
    Rigidbody2D rb;
    Animator anim;
    private bool _isGrounded;
    private bool _playerIsMoving;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private int _jumpForce;

    private bool _isTouchingFront;
    [SerializeField] private Transform _frontCheck;
    private bool _wallSliding;
    [SerializeField] private float _wallSlidingSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);
        _isTouchingFront = Physics2D.OverlapCircle(_frontCheck.position,_checkRadius, _whatIsGround);
    }

    private void PlayerMovement()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_speed*Time.deltaTime,0,0);
            transform.localScale = new Vector3(1,1,1);
            _playerIsMoving = true;
            anim.SetBool("IsRunning", true);
            
        } else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_speed *Time.deltaTime, 0, 0);
            transform.localScale = new Vector3(-1,1,1);
            _playerIsMoving = true;
            anim.SetBool("IsRunning", true);
        } else
        {
            _playerIsMoving = false;
            anim.SetBool("IsRunning", false);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded == true || Input.GetKeyDown(KeyCode.W) && _isGrounded == true || Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            rb.velocity = Vector2.up * _jumpForce;
            anim.SetBool("IsJumping", true);
        } else if(rb.velocity.y == 0)
        {
            anim.SetBool("IsJumping", false);
        }

        if(_isTouchingFront == true && _isGrounded == false && _playerIsMoving)
        {
            _wallSliding = true;
        } else
        {
            _wallSliding = false;
        }

        if(_wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }

        
    }
}
