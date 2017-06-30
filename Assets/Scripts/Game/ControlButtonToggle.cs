using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonToggle : MonoBehaviour {


	public Text buttonText;
	public int playerNumber;
	public InputType input;

	private string keyboard = "Keyboard";
	private string controller = "Controller";

	public void Toggle() {
		if (buttonText.text == keyboard) {
			input = InputType.Controller;
			buttonText.text = controller;
		} else {
			input = InputType.Keyboard;
			buttonText.text = keyboard;
		}
	}
}
