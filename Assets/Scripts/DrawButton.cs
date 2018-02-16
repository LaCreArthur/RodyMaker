using UnityEngine;
using UnityEngine.UI;

public class DrawButton : MonoBehaviour {

	public Button button;


	void Awake () {
		button.interactable = false;
	}
		
	public void printClick() {
		Debug.Log (button.name + " is clicked");
	}
}
