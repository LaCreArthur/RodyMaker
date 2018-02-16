using UnityEngine;
using UnityEngine.UI;

public class NgpButton : MonoBehaviour {

	public GameObject ngpButton;
	public Button button;

	void Awake () {
		ngpButton.SetActive (false);
	}
		
	public void printClick() {
		Debug.Log (button.name + " is clicked");
	}
}
