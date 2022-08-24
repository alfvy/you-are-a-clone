using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clone : MonoBehaviour
{
    [Header("Character Values")]
    [SerializeField] private bool _controlled;
    public bool controlled {
        get => _controlled; 
        set{
            if(value) GameManager.VirtualCamera.Follow = transform;
            _controlled = value;
        }
    }
    public float walkSpeed = 2.6f;
    public float jumpHoldDuration = 0.2f;
    public float jumpSpeed = 3.9f;
    public BoxCollider2D _hurtbox;
    public bool onGround, onOneWayGround, inWater, idle, jumping, canJump;
    [SerializeField] LayerCheck _groundCheck;
    public GameObject clonePrefab;
    public Vector2 inputVector;
    public Rigidbody2D rb;
    public TextMeshPro text;

    private CloneManager _cloneManager;
    private Vector2 _deafultHurtboxSize;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _bodyCollider;
    private Animator _animator;
    private bool _lastOnGround;
    private static int _groundLayerMask, _oneWayGroundLayerMask;
    private float _jumpTimer, _direction, _lastDirection;
    private float _walkTime = 0;
    private float _lastGroundedAtTime = -1f;
    // private static readonly int OnGroundState       = Animator.StringToHash("OnGround");
    // private static readonly int IdleState           = Animator.StringToHash("Idle");
    // private static readonly int HorizontalMovement  = Animator.StringToHash("HorizontalMovement");
    // private static readonly int VerticalMovement    = Animator.StringToHash("VerticalMovement");

    int Walking = Animator.StringToHash("Walking");
    int Jumping = Animator.StringToHash("Jumping");
    int Falling = Animator.StringToHash("Falling");
    int FallingWithMovement = Animator.StringToHash("Falling with Velocity");
    int Idle = Animator.StringToHash("Idle");

    private void Awake()
    {
        _groundLayerMask = LayerMask.GetMask("Ground");
        _oneWayGroundLayerMask = LayerMask.GetMask("One-Way Ground");
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        rb = _rigidbody;
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
        _cloneManager = FindObjectOfType<CloneManager>();
        text = GetComponentInChildren<TextMeshPro>();
        // Application.targetFrameRate = 60;
    }

    private void Start()
    {
        _direction = 1;
        _lastDirection = 1;
    }

    void Reset()
    {
       
    }

    private void FixedUpdate()
    {
        if(!controlled) return;
        // walk direction
        _transform.localScale = new Vector2(_lastDirection, 1);

        if (onGround)
            _rigidbody.velocity = new Vector2(inputVector.x * walkSpeed, _rigidbody.velocity.y);
        else if (!onGround)
            _rigidbody.velocity = new Vector2(inputVector.x * walkSpeed * 0.75f, _rigidbody.velocity.y);
        
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
        if (!Input.GetButton("Jump") && !onGround || _rigidbody.velocity.y < 0)
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.deltaTime;
        

        _lastOnGround = onGround || onOneWayGround;
    }
    
    private void Update()
    {
        // update the players state
        onGround = _groundCheck.ground;
        onOneWayGround = _groundCheck.oneWayGround;
        inWater = _groundCheck.water;
        idle = Mathf.Abs(_rigidbody.velocity.x) < 0.1f; 

        if(!controlled) return;

        // get the horizontal and vertical axes into an input vector
        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // increase the jump counter as long as the jump button is pressed
        if (Input.GetButton("Jump")) 
            _jumpTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            _cloneManager.AddClone(Instantiate(clonePrefab,
            transform.position + (Vector3.right * _direction), Quaternion.identity));

        // if the player is ground and the jump counter is less than the apex of the jump
        // or the player released the jump button 
        if ((onGround || onOneWayGround) && _jumpTimer < 0.2 || Input.GetButtonUp("Jump"))
        // the player can jump and the jump counter is reset
        {
            canJump = true;
            _jumpTimer = 0f;
        } 

        // if the jump button has been pressed more than the apex of jump, then the player can't jump
        if (_jumpTimer > 0.2)
            canJump = false;

        // if (Input.GetButtonDown("Jump") && onGround)

        _lastDirection = _direction == _lastDirection ? _lastDirection : _direction;
        FlipSprite();
    }

    int currentState;


    private void LateUpdate()
    {
        var state = Animate();
        if(state == 0 || state == currentState) return;
        _animator.CrossFade(state, 0, 0, 0, 0);
        currentState = state;
    }


    private int Animate()
    {
        if(onGround && idle) 
            return Idle;
        if(onGround && !idle) 
            return Walking;
        if(!onGround && Mathf.Abs(_rigidbody.velocity.x) < 0.1 && _rigidbody.velocity.y < 0) 
            return Falling;
        if(!onGround && Mathf.Abs(_rigidbody.velocity.x) > 0.1 && _rigidbody.velocity.y < 0)
            return FallingWithMovement;
        if(!onGround && _rigidbody.velocity.y > 0) 
            return Jumping;

        return 0;
    }

    private void FlipSprite()
    {
        // based on player input not velocity
        if (Input.GetAxis("Horizontal") > 0)
        {
            _direction = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _direction = -1;
        }
    }
}