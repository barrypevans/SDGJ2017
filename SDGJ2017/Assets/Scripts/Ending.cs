using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : Interactable {

    protected override void Interact()
    {
        GameManager.Instance.EndGame();
    }

}
