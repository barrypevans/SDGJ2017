using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    public Transform target;
    Vector2 initial;
    Vector2 final;
    public float duration = 2f;

	void Start () {
        initial = transform.position;
        final = target.position;
        StartCoroutine(Cycle());
	}
	
    IEnumerator Cycle()
    {
        bool moveToTarget = true;
        float t;
        while (true)
        {
            t = 0f;
            while (t < duration)
            {
                if (moveToTarget)
                    transform.position = Vector2.Lerp(initial, final, t / duration);
                else
                    transform.position = Vector2.Lerp(final, initial, t / duration);
                t += Time.deltaTime;

                yield return null;
            }
            moveToTarget = !moveToTarget;
            yield return null;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            collision.gameObject.transform.parent = null;
    }
}
