using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tileable : MonoBehaviour {

    public Texture2D BottomTile;
    public bool TileOnY = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(TileOnY)
        GetComponent<Renderer>().material.SetVector("_UVScale", transform.localScale);
        else
            GetComponent<Renderer>().material.SetVector("_UVScale",new Vector2( transform.localScale.x,1));
        if (null!= BottomTile)
            GetComponent<Renderer>().material.SetTexture("_TiledTexture", BottomTile);
    }
}
