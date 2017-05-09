using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour {

    public void ShrinkTowardsAndDestory(Transform other) {
        IEnumerator c = ShrinkTowards(other);
        StartCoroutine(c);
    }

    IEnumerator ShrinkTowards(Transform otherTransform) {
        GetComponent<Collider>().enabled = false;
        while(transform.localScale.x > 0.05f) {
            transform.localScale = transform.localScale * 0.7f;
            transform.position += ((otherTransform.position - transform.position) * 0.1f);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
