using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Item {
    // Count isn't an item type, it is simply used to track how many items there are
    public enum Type { THROWABLE, SUPER_BOOST, Count }
}

public class PlayerItemController : MonoBehaviour {

    public GameObject throwableItem;
	private PlayerStats stats;

    public ModelController playerAnimator;
    public Transform throwingHand;

    private Item.Type currentItem;
    private float chargeLeft = 0; // How much usage is left in this powerup
    private float nextUsableTime = 0.0f;
    private PlayerMovement playerMovement;
    private PlayerParticleController particleController;

    private int itemsActivated = 0;

    void Start() {
        playerMovement = GetComponentInParent<PlayerMovement>();
        particleController = GetComponentInChildren<PlayerParticleController>();
		stats = playerMovement.stats;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw(playerMovement.GetPlayerInput().activate) > 0 && CanUseItem()) {
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
		if ( item != null && chargeLeft <= 0) {
            currentItem = item.type;
            chargeLeft = stats.TOTAL_ITEM_CHARGE;
            item.Pickup(gameObject);
		}
	}

    void ActivatePowerup() {
        particleController.playItemParticleSystem(currentItem);
        switch(currentItem) {
            case Item.Type.THROWABLE:
                IEnumerator c = ThrowItem();
                StartCoroutine(c);
				chargeLeft -= stats.TOTAL_ITEM_CHARGE / stats.THROW_USES;
                break;
            case Item.Type.SUPER_BOOST:
				SetItemCooldown(stats.SUPER_BOOST_COOLDOWN);
				playerMovement.Boost(stats.SUPER_BOOST_POWER);
				chargeLeft -= stats.TOTAL_ITEM_CHARGE / stats.SUPER_BOOST_USES;
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

    IEnumerator ThrowItem() {
		SetItemCooldown(stats.THROW_COOLDOWN);

        GameObject throwable = Instantiate(throwableItem);
        throwable.transform.parent = throwingHand;
        throwable.transform.localPosition = Vector3.zero;
        throwable.GetComponent<Rigidbody>().isKinematic = true;
        throwable.GetComponent<SphereCollider>().enabled = false;
        playerAnimator.Throw();
        yield return new WaitForSeconds(0.5f);
        throwable.GetComponent<Rigidbody>().isKinematic = false;
        throwable.GetComponent<SphereCollider>().enabled = true;
        throwable.transform.SetParent(null);
        throwable.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
