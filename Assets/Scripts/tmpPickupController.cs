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
    bool AddToInventory(GameObject pickup)
    {
        if(inventory == null)
        {
            Debug.Log("Adding to inventory");
            inventory = pickup;
            HUDInvImage.sprite = pickup.GetComponent<tmpPickup>().icon;
            return true;
        }

        return false;
    }

    /**
     * Throws the currently held item infront of the player
     */
    public void ThrowItem()
    {
        if(inventory == null)
        {
            return;
        }

        GameObject ball = Instantiate(inventory);
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ballRB.useGravity = true;
        ballRB.AddForce(Vector3.forward);

        inventory = null;
    }
}
