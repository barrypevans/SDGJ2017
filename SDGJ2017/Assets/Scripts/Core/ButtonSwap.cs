using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour {

    Sprite init, hover;
    public string hoverSpriteName;
	void Start () {
        init = GetComponent<Image>().sprite;
        hover = Resources.Load<Sprite>("buttonsprites/" + hoverSpriteName);
	}

    void OnMouseEnter()
    {
        GetComponent<Image>().sprite = hover;
    }
    void OnMouseExit()
    {
        GetComponent<Image>().sprite = init;
    }
    
    
}
