using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{

    private bool _direction;
    private Rigidbody2D _rigidbody;
    private float _floorRayOffset = -1.34f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        Flip();
    }

    private void Flip()
    {
        _direction = !_direction;
        var variation = _direction ? -1 : 1;
        _rigidbody.velocity = new Vector2(variation * 10, _rigidbody.velocity.y);
        _floorRayOffset *= -1;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _direction = Random.Range(.0f, 1.0f) > .5f;
        _rigidbody.velocity = new Vector2(10, _rigidbody.velocity.y);
    }

    private void Update()
    {

        var floorRay = Physics2D.Raycast(transform.position + new Vector3(_floorRayOffset,0,0), Vector2.down,1);
     //   Debug.DrawRay(transform.position + new Vector3(_floorRayOffset, 0, 0), Vector2.down, Color.red, 2);
        if (!floorRay)
        {
            Flip();
        }
    }
}
