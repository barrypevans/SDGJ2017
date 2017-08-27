using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraRestrictionVolume : MonoBehaviour {

    private CameraController _camera;
    private BoxCollider2D _box;
    void Start () {
        _camera = GameObject.Find("main-camera").GetComponent<CameraController>();
        _box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _camera.SetRestrictions(new Rect(_box.bounds.min, _box.bounds.size));
        }
    }
}
