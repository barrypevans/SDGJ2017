using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    private GameObject _player;
    private Camera _camera;
    private Rect _restrictions;
    private bool _doFollow = true;
    private bool _suspendMovment;

    public bool UseRestrictions = false;

    private void Start()
    {
        _player = GameObject.Find("player");
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if(!_suspendMovment)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        var screenSpacePos = _camera.WorldToScreenPoint(_player.transform.position);
        var normalScreenSpacePos = new Vector2(screenSpacePos.x / (float)Screen.width, screenSpacePos.y / (float)Screen.height);
        normalScreenSpacePos.x -= .5f;
        normalScreenSpacePos.x *= 2;

        var xVel = _player.GetComponent<Rigidbody2D>().velocity.x;

        if (Mathf.Abs(normalScreenSpacePos.x) > .3f)
        {
            _doFollow = true;
        }

        else if (Mathf.Abs(xVel) < .01f)
        {
            _doFollow = false;
        }

        float targetX = transform.position.x;
        if (_doFollow)
        {
            targetX = Mathf.Lerp(transform.position.x, _player.transform.position.x + Mathf.Sign(xVel) * 5, .07f);

        }

        Vector3 lerpPos = Vector2.Lerp(transform.position, new Vector2(targetX, _player.transform.position.y + 5), .07f);
        lerpPos.x = targetX;
        if(UseRestrictions)
            lerpPos = new Vector3(Mathf.Clamp(lerpPos.x, _restrictions.xMin+_camera.orthographicSize*2, _restrictions.xMax-_camera.orthographicSize * 2), Mathf.Clamp(lerpPos.y, _restrictions.yMin, _restrictions.yMax),-10);
        lerpPos.z = -10;
        transform.position = lerpPos;
        
    }

    public void SuspendMovment()
    {
        _suspendMovment = true;
    }
    public void ResumeMovment()
    {
        _suspendMovment = false;
    }

    public void SetRestrictions(Rect restriction)
    {
        _restrictions = restriction;
    }

}
