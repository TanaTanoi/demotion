using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemController : MonoBehaviour {

    public enum ItemType { THROWABLE }

    public GameObject throwableItem;

    public Animator playerAnimator;
    public Transform throwingHand;

    private ItemType currentItem;
    private float chargeLeft = 0; // How much usage is left in this powerup
    private float nextUsableTime = 0.0f;

    private int itemsActivated = 0;
    public TextMesh itemDisplayText;
    public Image itemDisplayImage;

    private PlayerInput playerIn;

	// Use this for initialization
	void Start () {
        SetDisplayToCharge();
        playerIn = GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw(playerIn.activate) > 0 && CanUseItem()) {
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
            chargeLeft = 5.0f;
            item.Pickup(gameObject);
            SetDisplayToCharge();
		}
	}

    void ActivatePowerup() {
        switch(currentItem) {
            case ItemType.THROWABLE:
                IEnumerator c = ThrowItem();
                StartCoroutine(c);
                chargeLeft -= 1.0f;
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

    private void SetDisplayToCharge() {
        if(chargeLeft > 0) {
            itemDisplayImage.enabled = true;
            itemDisplayText.text = chargeLeft + "";
        } else {
            itemDisplayImage.enabled = false;
            itemDisplayText.text = "";
        }
    }

    IEnumerator ThrowItem() {
        SetItemCooldown(0.6f);

        GameObject throwable = Instantiate(throwableItem);
        throwable.transform.parent = throwingHand;
        throwable.transform.localPosition = Vector3.zero;
        throwable.GetComponent<Rigidbody>().isKinematic = true;
        throwable.GetComponent<SphereCollider>().enabled = false;
        playerAnimator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.5f);
        SetDisplayToCharge();
        throwable.GetComponent<Rigidbody>().isKinematic = false;
        throwable.GetComponent<SphereCollider>().enabled = true;
        throwable.transform.SetParent(null);
        throwable.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
