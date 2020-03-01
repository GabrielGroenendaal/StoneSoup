using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_tile2 : Tile
{
    // Variables
    public Collider2D onGroundCollider;
	public Collider2D heldCollider;

	public Sprite heldSprite;
	public Sprite onGroundSprite;
    
    public override Collider2D mainCollider {
		get { return onGroundCollider; }
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
