using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipScript : MonoBehaviour {

    public enum TooltipType { FOLLOW, POINT, ONLY_TEXT }
    public TooltipType type;

    public GameObject subject;
    public Image arrow;
    public bool StartOnScreen = true; // Does it start on screen? Otherwise it will appear at this location once activated
  

    private string text;
    private Text textbox;
    private Vector3 followOffset;
    private float originalHeight;

    private Texture2D t;

    private bool active = true;

	void Start () {
        textbox = GetComponentInChildren<Text>();
        text = textbox.text;
        if(type == TooltipType.FOLLOW) {
            followOffset = transform.position - subject.transform.position;
        }
        if(type == TooltipType.ONLY_TEXT) {
            arrow.enabled = false;
        }
        if(!StartOnScreen) {
            active = false;
            originalHeight = transform.position.y;
            transform.position += Vector3.down * 10;
        }
        t = new Texture2D(1, 1);
        t.SetPixel(0, 0, Color.red);
        t.Apply();
	}
	
	// Update is called once per frame
	void Update () {
        if (subject == null && type != TooltipType.ONLY_TEXT) {
            Disappear();
        } else if(active) {
		    if( type == TooltipType.FOLLOW) {
                transform.position = followOffset + subject.transform.position;
            } else if(type == TooltipType.POINT) {
                Vector3 delta = subject.transform.position - transform.position;
                arrow.transform.rotation = Quaternion.FromToRotation(Vector3.down, delta);
            }
        }
	}

    public void SetText(string newText) {
        if(textbox == null) {
        textbox = GetComponentInChildren<Text>();

        }
        text = newText;
        textbox.text = text;
    }

    public void Appear() {
        IEnumerator c = AppearRoutine();
        StartCoroutine(c);
    }

    public void Disappear() {
        IEnumerator c = DisappearDestroyRoutine();
        StartCoroutine(c);
    }

    IEnumerator DisappearDestroyRoutine() {
        active = false;
        while(transform.position.y > -3) {
            transform.position = transform.position + Vector3.down * 0.1f;
            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator AppearRoutine() {
        while(transform.position.y < followOffset.y) {
                transform.position = transform.position + Vector3.up * 0.3f;
            yield return null;
        }
        active = true;
    }
}
