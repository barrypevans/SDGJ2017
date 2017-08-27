using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtility : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Leathal")
        {
            if (GameManager.Instance.HasShield && GameManager.Instance.ShieldIntact)
            {
                GameManager.Instance.ShieldIntact = false;
                //break shield here ^
            }
            else
            {
                GameManager.Instance.DoRespawn();
                GameManager.Instance.ShieldIntact = true;
            }
        }
   
    }
}
