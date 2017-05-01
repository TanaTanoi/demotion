using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tmpPickupController : MonoBehaviour {



    // Inventory is one slot only
    private GameObject inventory;

    public Image HUDInvImage;

    private void Start()
    {
        inventory = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup"))
        {
            if (AddToInventory(other.transform.gameObject))
            {
               Destroy(other.transform.gameObject);
            }
        }
    }

    /**
     * Attempts to add the pickup to the inventory.
     * Returns true if it was successfull (nothing else in the inv)
     * Else return false
     */
    bool AddToInventory(GameObject item)
    {
        if(inventory == null)
        {
            Debug.Log("Adding to inventory");
            tmpPickup pickup = item.GetComponent<tmpPickup>();
            inventory = pickup.ball;
            HUDInvImage.sprite = pickup.icon;
            HUDInvImage.color = new Color(255,255,255,255);
            return true;
        }

        return false;
    }

    /**
     * Throws the currently held item infront of the player
     */
    public void DropItem()
    {
        if(inventory == null)
        {
            return;
        }
        Debug.Log("Throwing a thing!");
        GameObject ball = Instantiate(inventory, new Vector3(0, 7, 2), Quaternion.identity);
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ballRB.AddForce(Vector3.forward);

        inventory = null;
        HUDInvImage.sprite = null;
        HUDInvImage.color = new Color(255,255,255,15);
    }
}
