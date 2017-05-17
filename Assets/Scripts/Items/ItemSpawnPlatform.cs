using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPlatform : MonoBehaviour {
    public ParticleSystem spawnEffect;

    private Vector3 spawnPosition;
    private GameObject currentItem;
    
	void Start () {
        spawnPosition = transform.GetChild(0).transform.position;
	}

    public bool HasItem() {
        return currentItem != null;
    }

    public bool SpawnItem(Item.Type type) {
        if(HasItem()) {
            return false;
        } else {
            currentItem = CreateItem(type);
            currentItem.transform.position = spawnPosition;
            return true;
        }
    }

    private GameObject CreateItem(Item.Type type) {
        spawnEffect.Play();
        switch(type) {
            case Item.Type.SUPER_BOOST:
                // change once we have different models for them etc
                GameObject boost = GameObject.CreatePrimitive(PrimitiveType.Cube);
                boost.AddComponent<ItemController>().type = type;
                return boost;
            case Item.Type.STICKY_THROWABLE:
                // change once we have different models for them etc
                GameObject throwable = GameObject.CreatePrimitive(PrimitiveType.Cube);
                throwable.AddComponent<ItemController>().type = type;
                return throwable;
            default:
                return null;
        }
    }
}
