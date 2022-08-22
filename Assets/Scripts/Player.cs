using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Character Values")]
    public float walkSpeed = 2.6f;
    public float jumpHoldDuration = 0.2f;
    public float jumpSpeed = 3.9f;
    public BoxCollider2D _hurtbox;
    public bool onGround, onOneWayGround, inWater, idle, jumping, canJump;
    [SerializeField] LayerCheck _groundCheck;

    private Vector2 _deafultHurtboxSize;
    private Vector2 inputVector;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _bodyCollider;
    private Animator _animator;
    private bool _direction, _lastDirection, _lastOnGround;
    private static int _groundLayerMask, _oneWayGroundLayerMask;
    private float _jumpTimer;
    private float _walkTime = 0;
    private float _lastGroundedAtTime = -1f;
    private static readonly int OnGroundState       = Animator.StringToHash("OnGround");
    private static readonly int IdleState           = Animator.StringToHash("Idle");
    private static readonly int HorizontalMovement  = Animator.StringToHash("HorizontalMovement");
    private static readonly int VerticalMovement    = Animator.StringToHash("VerticalMovement");
    private static readonly int TurnState           = Animator.StringToHash("Turn");

    private void Awake()
    {
        _groundLayerMask = LayerMask.GetMask("Ground");
        _oneWayGroundLayerMask = LayerMask.GetMask("One-Way Ground");
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        // Application.targetFrameRate = 60;
    }

    private void Start()
    {

    }

    void Reset()
    {
       
    }

    private void FixedUpdate()
    {
        // walk direction
        _transform.localScale = new Vector2(!_lastDirection ? 0.5f : -0.5f, 0.5f);

        if (onGround)
            _rigidbody.velocity = new Vector2(inputVector.x * walkSpeed, _rigidbody.velocity.y);
        else if (!onGround)
            _rigidbody.velocity = new Vector2(inputVector.x * walkSpeed * 0.5f, _rigidbody.velocity.y);
        
        // timed jump
        // if grounded reset the last grounded time
        if (onGround || onOneWayGround)
            _lastGroundedAtTime = Time.time;
            
        // get the jump button and check if the last grounded time isn't near the current time
        // plus the time the jump button is held
        if (Input.GetButton("Jump") 
        && Time.time < _lastGroundedAtTime + jumpHoldDuration && canJump)
            // _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);
            _rigidbody.angularVelocity = 0f;
            _rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        if (!Input.GetButton("Jump") && !onGround && _rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
        

        _lastOnGround = onGround || onOneWayGround;
    }
    
    private void Update()
    {
        // get the horizontal and vertical axes into an input vector
        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // update the players state
        onGround = _groundCheck.ground;
        onOneWayGround = _groundCheck.oneWayGround;
        inWater = _groundCheck.water;
        idle = Mathf.Abs(_rigidbody.velocity.x) < 0.1f; 

        // if the player is ground and the jump counter is less than the apex of the jump
        // or the player released the jump button 
        if ((onGround || onOneWayGround) && _jumpTimer < 0.2 || Input.GetButtonUp("Jump"))
        // the player can jump and the jump counter is reset
        {
            canJump = true;
            _jumpTimer = 0f;
        } 
        // increase the jump counter as long as the jump button is pressed
        if (Input.GetButton("Jump")) 
            _jumpTimer += Time.deltaTime;

        // if the jump button has been pressed more than the apex of jump, then the player can't jump
        if (_jumpTimer > 0.2)
            canJump = false;

        // if (Input.GetButtonDown("Jump") && onGround)

        _lastDirection = _direction == _lastDirection ? _lastDirection : _direction;
        FlipSprite();
    }

    private void LateUpdate()
    {
        UpdateAnimator();
    }

    // update all the animator values
    private void UpdateAnimator()
    {
        _animator.SetBool(OnGroundState, onGround || onOneWayGround);
        _animator.SetBool(IdleState, idle || Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1);
        _animator.SetFloat(HorizontalMovement, Mathf.Abs(_rigidbody.velocity.x));
        _animator.SetFloat(VerticalMovement, _rigidbody.velocity.y);
        // turn the character based on _direction and _lastDirection
        if (_lastDirection != _direction && onGround) _animator.SetTrigger(TurnState);
    }

    private void FlipSprite()
    {
        // based on player input not velocity
        if (Input.GetAxis("Horizontal") > 0)
        {
            _direction = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _direction = true;
        }
    }
}