using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : Interactable
{

    private Vector3 _initialPosition;
    private bool _collected;
    [SerializeField]
    private PowerupType _powerupType;
    public Sprite desaturationedPowerup;

    private void Start()
    {
        if (_powerupType == PowerupType.Dash)
        {
            _collected = GameManager.Instance.FirstRunHasDash ;
            if(_collected)
            GetComponent<SpriteRenderer>().sprite = desaturationedPowerup;
        }
        else if (_powerupType == PowerupType.Gloves)
        {
            _collected = GameManager.Instance.FirstRunHasGloves;
            if (_collected)
                GetComponent<SpriteRenderer>().sprite = desaturationedPowerup;
        }

        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!_collected)
            transform.position = _initialPosition + new Vector3(0, (Mathf.Sin(Time.time * 2) + 1.0f) * .5f, 0);
        else
            transform.position = Vector3.Lerp(transform.position, _initialPosition, .1f);
    }


    protected override void Interact()
    {
        
        if (_powerupType == PowerupType.Dash)
            GameManager.Instance.HasDash = true;
        else if (_powerupType == PowerupType.Gloves)
            GameManager.Instance.HasGloves = true;
        _collected = true;
        GetComponent<SpriteRenderer>().sprite = desaturationedPowerup;
    }
}
[System.Serializable]
public enum PowerupType
{
    Dash,
    Gloves,
    Shield,

}

