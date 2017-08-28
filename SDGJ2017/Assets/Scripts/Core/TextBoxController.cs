using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour {


    Vector2 basePos, endPos;
    Vector2 baseScale, endScale;
    public GameObject tipSprite;

	void Start () {
        basePos = tipSprite.transform.position + new Vector3(0, -4, 0);
        endPos = tipSprite.transform.position; 
        baseScale = Vector2.zero;
        endScale = tipSprite.transform.localScale;

        tipSprite.transform.localScale = baseScale;
	}

    Coroutine handlerCoroutine;

    IEnumerator transitionUp()
    {
        Vector2 holderPosition = new Vector2(tipSprite.transform.position.x, transform.position.y);
        Vector2 holderScale = new Vector2(tipSprite.transform.localScale.x, transform.localScale.y);
        float t = 0f;
        while (t < 1f)
        {
            tipSprite.transform.position = Vector2.Lerp(holderPosition, endPos, t);
            tipSprite.transform.localScale = Vector2.Lerp(holderScale, endScale, t);
            t += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    IEnumerator transitionDown()
    {
        Vector2 holderPosition = new Vector2(tipSprite.transform.position.x, tipSprite.transform.position.y);
        Vector2 holderScale = new Vector2(tipSprite.transform.localScale.x, tipSprite.transform.localScale.y);
        float t = 0f;
        while (t < 1f)
        {
            tipSprite.transform.position = Vector2.Lerp(holderPosition, basePos, t);
            tipSprite.transform.localScale = Vector2.Lerp(holderScale, baseScale, t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player")
        {
            if (null == handlerCoroutine)
                handlerCoroutine = StartCoroutine(transitionUp());
            else
            {
                StopCoroutine(handlerCoroutine);
                handlerCoroutine = StartCoroutine(transitionUp());
            }
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            if (null == handlerCoroutine)
                handlerCoroutine = StartCoroutine(transitionDown());
            else
            {
                StopCoroutine(handlerCoroutine);
                handlerCoroutine = StartCoroutine(transitionDown());
            }
        }
    }
}
