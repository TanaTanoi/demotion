using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonScript : MonoBehaviour {

    public string[] options;

    private int index = 0;
    private Text buttonText;

    private void Start()
    {
        if(options == null)
        {
            Debug.LogError("There are no options for this button: " + gameObject.name);
        }

        buttonText = GetComponentInChildren<Text>();
        index = 0;
    }

    public void OnClick()
    {
        buttonText.text = options[index++];
        if (index >= options.Length)
            index = 0;
    }
}
