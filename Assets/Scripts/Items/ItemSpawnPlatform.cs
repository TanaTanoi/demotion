using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPlatform : MonoBehaviour {
    private ParticleSystem spawnEffect;
    private Vector3 spawnPosition;
    private GameObject currentItem;
    
	public PowerupModelsAtlas atlas;

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
			currentItem.transform.localScale = currentItem.transform.localScale * 2;
            currentItem.transform.position = spawnPosition;
            return true;
        }
    }

    private GameObject CreateItem(Item.Type type) {
        spawnEffect.Play();
        switch(type) {
        case Item.Type.SUPER_BOOST:
			GameObject boost = Instantiate(atlas.fireBoostItem);
            boost.AddComponent<ItemController>().type = type;
            return boost;
		case Item.Type.STICKY_THROWABLE:
			GameObject throwable = Instantiate(atlas.snowballItem);
            throwable.AddComponent<ItemController>().type = type;
            return throwable;
		case Item.Type.BANANA_THROWABLE:
			GameObject banana = Instantiate(atlas.bananaItem);
			banana.AddComponent<ItemController>().type = type;
			return banana;
            default:
                return null;
        }
    }
}
