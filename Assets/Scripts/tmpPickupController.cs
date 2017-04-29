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
        Debug.Log("PIKCUP?");
        if(other.CompareTag("Pickup"))
        {
            AddToInventory(other.transform.gameObject);
            Destroy(other.transform.gameObject);
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
            inventory = pickup;
            return true;
        }

        return false;
    }
}
