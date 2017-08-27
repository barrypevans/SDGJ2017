using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Sprite init, hover;
    public string hoverSpriteName;
	void Start () {
        init = GetComponent<Image>().sprite;
        hover = Resources.Load<Sprite>("buttonsprites/" + hoverSpriteName);
	}
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = init;
    }
}
