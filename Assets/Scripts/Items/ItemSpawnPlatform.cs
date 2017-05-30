using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPlatform : MonoBehaviour {
    private ParticleSystem spawnEffect;
    private Vector3 spawnPosition;
    private GameObject currentItem;
    
	public GameObject superboostProp;
	public GameObject bananaProp;

	void Start () {
        spawnPosition = transform.GetChild(0).transform.position;
        spawnEffect = GetComponentInChildren<ParticleSystem>();
	}

    public bool HasItem() {
        return currentItem != null;
    }

    public bool SpawnItem(Item.Type type) {
        if(HasItem()) {
            return false;
        } else {
            currentItem = CreateItem(type);
			currentItem.transform.localScale = Vector3.one * 2;
            currentItem.transform.position = spawnPosition;
            return true;
        }
    }

    private GameObject CreateItem(Item.Type type) {
        spawnEffect.Play();
        switch(type) {
            case Item.Type.SUPER_BOOST:
                // change once we have different models for them etc
			GameObject boost = Instantiate(superboostProp);
            boost.AddComponent<ItemController>().type = type;
            return boost;
		case Item.Type.STICKY_THROWABLE:
                // change once we have different models for them etc
				GameObject throwable = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				throwable.transform.localScale = Vector3.one * 0.5f;
                throwable.AddComponent<ItemController>().type = type;
                return throwable;
		case Item.Type.BANANA_THROWABLE:
			GameObject banana = Instantiate(bananaProp);
			banana.AddComponent<ItemController>().type = type;
			return banana;
            default:
                return null;
        }
    }
}
