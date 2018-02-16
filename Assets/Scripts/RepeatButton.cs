using UnityEngine;
using UnityEngine.UI;

public class RepeatButton : MonoBehaviour {

	public Button button;

	public void printClick() {
		Debug.Log (button.name + " is clicked");
	}
}
	