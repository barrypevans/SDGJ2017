using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoddessDialogSwappwer : MonoBehaviour
{

    public Sprite replacment;

    void OnEnable()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForEndOfFrame();

        if (GameManager.Instance._isSecondRun)
            GetComponent<SpriteRenderer>().sprite = replacment;
    } 

}
