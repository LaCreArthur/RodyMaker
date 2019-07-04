using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RA_Triggers : MonoBehaviour {

	public Sprite joypadButtonLeft, joypadButtonRight, joypadStickLeft, joypadStickRight;
	public GameObject joypad;
	public float releaseSpeed = 1f;
	public RA_SoundManager sm;
	
	bool isJoypadClicked = false;
	float t = 0;
	Sprite joypadDefault;

	void Start()  {
		joypadDefault = joypad.GetComponent<Image>().sprite;
	}

	void Update() {
		if (isJoypadClicked) {
			if (Input.GetMouseButton(0)) {
				t = 0;
			}
			else if (t < 1) {
				t += Time.deltaTime * releaseSpeed;
			}
			else {
				isJoypadClicked = false;
				joypad.GetComponent<Image>().sprite = joypadDefault;
				t = 0;
			}
		}
	}

	public void OnButtonLeftClick() {
		joypad.GetComponent<Image>().sprite = joypadButtonLeft;
		isJoypadClicked = true;
		sm.OnJoyLeftClick();
		
	}

	public void OnButtonRightClick() {
		joypad.GetComponent<Image>().sprite = joypadButtonRight;
		isJoypadClicked = true;
		sm.OnJoyRightClick();
	}

	public void OnJoypadClick(){
		isJoypadClicked = true;
		Debug.Log("clicked on joypad");
	}
}
