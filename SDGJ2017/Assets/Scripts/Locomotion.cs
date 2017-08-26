using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Locomotion : MonoBehaviour
{
    private const float RisingGravity = 10;
    private const float FallingGravity = 15;
    private const float JumpStrength = 25;
    private const float RunAcceleration = .7f;
    private const float RunSpeedCap = 12;
    private const float FrictionForce = -.3f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _isGrounded;
    private bool _isFalling;
    private bool _isRising;
    private bool _isPivoting;
    private bool _canJump = true;

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

    }

    private void UpdatePhysics()
    {

        var horizontalInput = InputService.GetHorizontal();
        var xVel = _rigidbody.velocity.x + horizontalInput * RunAcceleration;

        //add friction
        if (horizontalInput == 0)
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
        if (_canJump && _isGrounded)
            if (InputService.JumpPressed())
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpStrength);

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
}
