using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtility : MonoBehaviour
{

    public GameObject ShieldObject;
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "Leathal")
        {
            if (GameManager.Instance.HasShield && GameManager.Instance.ShieldIntact)
            {
                GameManager.Instance.ShieldIntact = false;


            }
            else
            {
                GameManager.Instance.DoRespawn();
                
            }
        }

    }

    public void Update()
    {

        ShieldObject.SetActive(GameManager.Instance.HasShield && GameManager.Instance.ShieldIntact);
    }
}
