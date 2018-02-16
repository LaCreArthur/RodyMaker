using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour {

	public Button button;

	public void onClick() {
		Debug.Log (button.name + " is clicked");
	}
}
