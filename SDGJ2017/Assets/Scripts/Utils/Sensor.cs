using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Sensor : MonoBehaviour
{

    public LayerMask mask;

    [SerializeField]
    private string _sensorName = "";

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (mask == (mask | (1 << collider.gameObject.layer))) return;
        SendMessageUpwards("OnSensorEnter_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
        collider.SendMessage("BoadcastSensorEnter_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (mask == (mask | (1 << collider.gameObject.layer))) return;
        SendMessageUpwards("OnSensorExit_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
        collider.SendMessage("BoadcastSensorExit_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (mask == (mask | (1 << collider.gameObject.layer))) return;
        SendMessageUpwards("OnSensorStay_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
        collider.SendMessage("BoadcastSensorStay_" + _sensorName, collider, SendMessageOptions.DontRequireReceiver);
    }
}
