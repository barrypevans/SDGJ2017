using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Locomotion : MonoBehaviour
{
    [SerializeField]
    private float RisingGravity = 10;
    [SerializeField]
    private float FallingGravity = 15;
    [SerializeField]
    private float JumpStrength = 25;
    [SerializeField]
    private float RunAcceleration = .7f;
    [SerializeField]
    private float RunSpeedCap = 12;
    private const float FrictionForce = -2f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _isGrounded;
    private bool _isFalling;
    private bool _isRising;
    private bool _isPivoting;
    private bool _canJump = true;
    private bool _canWallJump = false;
    private int _wallDirection;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdatePhysicsInputs();
        UpdateAnimatorState();
    }

    private void FixedUpdate()
    {
        UpdatePhysics();
    }

    private void UpdateAnimatorState()
    {
        _isFalling = _rigidbody.velocity.y < .01f && !_isGrounded;
        _isRising = _rigidbody.velocity.y > .01f;
        _isPivoting = Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(InputService.GetHorizontal());
        _isPivoting = _isGrounded && _isPivoting;

        _animator.SetBool("is-grounded", _isGrounded);
        _animator.SetBool("is-falling", _isFalling);
        _animator.SetBool("is-rising", _isRising);
        _animator.SetBool("is-pivoting", _isPivoting);
        _animator.SetBool("can-wall-jump", _canWallJump);
    }

    private void UpdatePhysics()
    {

        var horizontalInput = InputService.GetHorizontal();
        var xVel = _rigidbody.velocity.x + horizontalInput * RunAcceleration;

        if (_isPivoting)
            xVel += Mathf.Sign(_rigidbody.velocity.x) * FrictionForce;

        //add friction
        if ((horizontalInput == 0) && _isGrounded)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) >= Mathf.Abs(FrictionForce))
                xVel += Mathf.Sign(_rigidbody.velocity.x) * FrictionForce;
            else if (Mathf.Abs(_rigidbody.velocity.x) < Mathf.Abs(FrictionForce))
                xVel = 0;
        }

        //clamp velocity
        _rigidbody.velocity = new Vector2(Mathf.Clamp(xVel, -RunSpeedCap, RunSpeedCap), _rigidbody.velocity.y);

    }

    private void UpdatePhysicsInputs()
    {
        //Do Jumping

        if (InputService.JumpPressed())
        {
            if (!_canWallJump)
            {
                if (_canJump && _isGrounded)
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpStrength);
            }
            else
            {
                _rigidbody.velocity = new Vector2(20000 * _wallDirection, JumpStrength);
                Debug.Log("do wall jump");
            }
        }

        //swith gravity based on jumpstate
        if (!InputService.JumpHold() || _isFalling)
            _rigidbody.gravityScale = FallingGravity;
        else if (InputService.JumpHold())
            _rigidbody.gravityScale = RisingGravity;
    }

    //update Grounded state
    private void OnSensorStay_Grounded()
    {
        _isGrounded = true;
    }

    private void OnSensorExit_Grounded()
    {
        _isGrounded = false;
    }

    private void OnSensorStay_WallJump(Collider2D collider)
    {
        _canWallJump = true && !_isGrounded;
        _wallDirection = (int)Mathf.Sign(  transform.position.x- collider.transform.position.x );
    }

    private void OnSensorExit_WallJump()
    {
        _canWallJump = false;
        _wallDirection = 0;
    }
}
