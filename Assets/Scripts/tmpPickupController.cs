using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class tmpPickupController : MonoBehaviour {



    // Inventory is one slot only
    public GameObject inventory = null;

    public Image HUDInvImage;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickup"))
        {
            
            if (AddToInventory(other.gameObject))
            {
                other.transform.gameObject.SetActive(false);
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
            inventory = item;
            HUDInvImage.sprite = item.GetComponent<tmpPickup>().icon;
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
            Debug.Log("Cannot drop nothing");
            return;
        }
        Debug.Log("Dropping a thing!");
        inventory.transform.position = transform.position + new Vector3(0.2f, 0.0f, 2.0f);
        inventory.SetActive(true);
        Rigidbody invRB = inventory.GetComponent<Rigidbody>();
        invRB.AddForce(Vector3.forward);

        inventory = null;
        HUDInvImage.sprite = null;
        HUDInvImage.color = new Color(255,255,255,15);
    }
}
