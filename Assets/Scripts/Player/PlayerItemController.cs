using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Item {
    // Count isn't an item type, it is simply used to track how many items there are
    public enum Type { STICKY_THROWABLE, BANANA_THROWABLE, SUPER_BOOST, Count }
}

public class PlayerItemController : MonoBehaviour {

	private PlayerStats stats;

	public PlayerModelController playerAnimator;
    public Transform throwingHand;

	public PowerupModelsAtlas atlas;

    private Item.Type currentItem;
    private float chargeLeft = 0; // How much usage is left in this powerup
    private float nextUsableTime = 0.0f;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private PlayerParticleController particleController;

	public FMODUnity.StudioEventEmitter fireSound;

    private int itemsActivated = 0;

    void Start() {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerInput = playerMovement.GetPlayerInput();
        particleController = GetComponentInChildren<PlayerParticleController>();
		stats = playerMovement.stats;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw(playerInput.activate) > 0 && CanUseItem()) {
            ActivatePowerup();
            itemsActivated++;
        }
    }

    public int ItemsActivated() {
        return itemsActivated;
    }
    
	// On collision with a powerup, add it
	void OnTriggerEnter(Collider other) {
		ItemController item = other.gameObject.GetComponent<ItemController>();
		if (item != null) {
			currentItem = item.type;
			chargeLeft = stats.TOTAL_ITEM_CHARGE;
			item.Pickup (gameObject);
		}
	}

    void ActivatePowerup() {
        particleController.playItemParticleSystem(currentItem);
        switch(currentItem) {
		case Item.Type.STICKY_THROWABLE:
			GameObject sticky = Instantiate (atlas.snowballThrowable);
			sticky.AddComponent<PowerupController> ().type = Powerup.Type.STICKY;
			IEnumerator stickyC = ThrowItem(sticky);
			StartCoroutine(stickyC);
				chargeLeft -= stats.TOTAL_ITEM_CHARGE / stats.THROW_USES;
                break;
		case Item.Type.BANANA_THROWABLE:
			GameObject banana = Instantiate (atlas.bananaThrowable);
			banana.AddComponent<PowerupController> ().type = Powerup.Type.BANANA;
			banana.AddComponent<Rigidbody> ().isKinematic = false;
			IEnumerator bananaC = ThrowItem(banana);
			StartCoroutine(bananaC);
			chargeLeft -= stats.TOTAL_ITEM_CHARGE / stats.THROW_USES;
			break;
		case Item.Type.SUPER_BOOST:
			SetItemCooldown (stats.SUPER_BOOST_COOLDOWN);
			playerMovement.Boost (stats.SUPER_BOOST_POWER);
			chargeLeft -= stats.TOTAL_ITEM_CHARGE / stats.SUPER_BOOST_USES;
			fireSound.Play ();
            break;
        }
    }

    private bool CanUseItem() {
        return nextUsableTime < Time.time && chargeLeft > 0;
    }

    // The cooldown (duration until next use) of the currently held item
    void SetItemCooldown(float cooldown) {
        nextUsableTime = Time.time + cooldown;
    }

	// Throws the given item (assumes it is instanciated)
	IEnumerator ThrowItem(GameObject throwable) {
		SetItemCooldown(stats.THROW_COOLDOWN);
        throwable.transform.parent = throwingHand;
        throwable.transform.localPosition = Vector3.zero;
		throwable.transform.position = throwingHand.position;
        throwable.GetComponent<Rigidbody>().isKinematic = true;
		throwable.GetComponent<Collider>().enabled = false;
        playerAnimator.Throw();
        yield return new WaitForSeconds(0.5f);
        throwable.GetComponent<Rigidbody>().isKinematic = false;
        throwable.transform.SetParent(null);
        throwable.GetComponent<Rigidbody>().AddForce(transform.forward * 700);
        yield return new WaitForSeconds(0.2f);
		throwable.GetComponent<Collider>().enabled = true;
		throwable.AddComponent<ThrowableItem> ();
    }
}
