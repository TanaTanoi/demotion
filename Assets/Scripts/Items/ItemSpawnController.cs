using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Keeps one item on the map at all times, with a cooldown after the item was taken.
public class ItemSpawnController : MonoBehaviour {

    private ItemSpawnPlatform[] spawners;
    [Tooltip ("Minimum time before an item can spawn, including after an item has been picked up")]
    private const float minimumCooldown = 5f;
    [Tooltip ("Time between how often to check. Acts like a positive tolerance or upper bound on spawn cooldown")]
    private const float checkFrequency = 4f;

    private float nextSpawnTime;

	// Use this for initialization
	void Start () {
        spawners = GameObject.FindObjectsOfType<ItemSpawnPlatform>();
        InvokeRepeating("CheckAndAssign", minimumCooldown, checkFrequency);
        nextSpawnTime = Time.time;
	}

    public void CheckAndAssign() {
        if (!ItemPresent() && Time.time > nextSpawnTime) {
            AssignItemAtRandom();
        }
    }

    public void AssignItemAtRandom() {
        nextSpawnTime = Time.time + minimumCooldown;
        int index = Random.Range(0, spawners.Length);
        Item.Type type = RandomItemType();
        AssignItem(index, type);
    }

    public bool AssignItem(int index, Item.Type type) {
        if(index > spawners.Length) {
            return false;
        }
        return spawners[index].SpawnItem(type);
    }

    private Item.Type RandomItemType() {
         return (Item.Type) Random.Range(0, (int)Item.Type.Count);
    }

    private bool ItemPresent() {
        foreach(ItemSpawnPlatform spawner in spawners) {
            if(spawner.HasItem()) {
                return true;
            }
        }
        return false;
    }
}
