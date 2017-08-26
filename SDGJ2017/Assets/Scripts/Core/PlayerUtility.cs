using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtility : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Leathal")
            GameManager.Instance.DoRespawn();
    }
}
