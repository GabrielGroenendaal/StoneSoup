using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_directionTile : Tile
{
    // Variables
    public Collider2D onGroundCollider;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;
    private int direction;

    private Vector2 pushForce;
    public float forceVelocity;
    public AudioClip pushSound;
    public GameObject tileDetector;
    
    public override Collider2D mainCollider {
		get { return onGroundCollider; }
	}
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(1,4);

        if (direction == 1) {
            _sprite.sprite = upSprite;
            pushForce = new Vector2(0, forceVelocity);
        }

        else if (direction == 2){
            _sprite.sprite = rightSprite;
            pushForce = new Vector2(forceVelocity, 0);
        }

        else if (direction == 3){
            _sprite.sprite = downSprite;
            pushForce = new Vector2(0, -1 * forceVelocity);
        }
        else if (direction == 4){
            _sprite.sprite = leftSprite;
            pushForce = new Vector2(-1 * forceVelocity, 0);
        }

        BoxCollider2D borders = tileDetector.GetComponent<BoxCollider2D>();
        borders.size = _sprite.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When we collide with something in the air, we try to deal damage to it.
    public override void tileDetected(Tile otherTile) {
        //float tileDistance = Vector3.Distance(this.transform.position,otherTile.transform.position);
        float x_Distance = Mathf.Abs(this.transform.position.x - otherTile.transform.position.x);
        float y_Distance = Mathf.Abs(this.transform.position.y - otherTile.transform.position.y);

        if (x_Distance < TILE_SIZE *.9 || y_Distance < TILE_SIZE * .9) {
		    otherTile.addForce(pushForce);
            AudioManager.playAudio(pushSound);
            Debug.Log("push");
        }
        Debug.Log("saw this tile: " + otherTile);
    }
	public virtual void OnCollisionEnter2D(Collision2D collision) {
		Tile otherTile = collision.gameObject.GetComponent<Tile>();
		otherTile.addForce(pushForce);
        AudioManager.playAudio(pushSound);
        Debug.Log("push");
    }
}
