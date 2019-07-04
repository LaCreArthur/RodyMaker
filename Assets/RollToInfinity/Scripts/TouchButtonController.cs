using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// this script sets up two buttons on the low half  of the screen to control the player by touch
/// click on the low left part of the screen to move the player to the left and vise versa
///</summary>

public class TouchButtonController : MonoBehaviour {

	public GameObject player;

	void Start () {
		GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 2);
	}
	
	public void OnClick(bool isRight) {
		float x = (isRight)? 1.0f : -1.0f;
		Vector3 movement = new Vector3(x, 0.0f, 0.0f);
		player.GetComponent<Rigidbody>().AddForce(movement * player.GetComponent<PlayerController>().speed);
	}
}