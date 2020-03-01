using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_reflectorShield : Tile
{
    public AudioClip pickupSound;
    public AudioClip activateSound;

    public float shieldDuration = 0;
    public bool shieldActive = false;
    public float shieldCooldown = 0;

    public BoxCollider2D onGroundCollider;
    public BoxCollider2D activatedCollider;

	public Sprite onGroundSprite; // what it looks like on the ground
    public Sprite shieldActivatedSprite; // the shield object spawned
    public Sprite shieldEquipped; // what the shield equipped looks like
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void useAsItem(Tile tileUsingUs) {
		// We can't swing if we're already swinging.
		if (shieldCooldown >= 0 || _tileHoldingUs != tileUsingUs || shieldActive) {
			return;
		}

		AudioManager.playAudio(activateSound);

		shieldActive = true;
        shieldDuration = .5f;
        shieldCooldown = 2.0f;
        activatedCollider.enabled = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (_tileHoldingUs != null) {
			tileName = string.Format("Energy Shield (HP: {0})", health);
		}

        if (shieldActive){
            _sprite.sprite = shieldActivatedSprite;
            shieldDuration -= Time.deltaTime;

            if (shieldDuration <= 0){
                shieldActive = false;
                activatedCollider.enabled = false;
                _sprite.sprite = shieldEquipped;
            }
        }

        shieldCooldown -= Time.deltaTime;
    }

    public override void pickUp(Tile tilePickingUsUp) {
		base.pickUp(tilePickingUsUp);
		if (_tileHoldingUs != null) {
			AudioManager.playAudio(pickupSound);
            _sprite.sprite = shieldEquipped;
            onGroundCollider.enabled = false;
            activatedCollider.enabled = false;
            activatedCollider.size = tilePickingUsUp.GetComponent<BoxCollider2D>().size * 1.5f;
            Physics2D.IgnoreCollision(tilePickingUsUp.GetComponent<BoxCollider2D>(), activatedCollider);
		}
	}

    // Can't drop us while we're swinging.
	public override void dropped(Tile tileDroppingUs) {
		base.dropped(tileDroppingUs);

        if (_tileHoldingUs == null) {
        	activatedCollider.enabled = false;
			onGroundCollider.enabled = true;
			_sprite.sprite = onGroundSprite;

			transform.parent = tileDroppingUs.transform.parent;
			transform.position = tileDroppingUs.transform.position;
        }
	}

}
