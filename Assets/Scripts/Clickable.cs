using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour {

	public MenuManager mm;
	public Toggle toggle;
	public int index; 
	public bool isEnabled;
	public bool isScene;
	void Update() {
		if (isScene && toggle.isOn) {
			mm.sceneToLoad = index;
		}
		else if (!isScene && toggle.isOn) {
			mm.actionToLoad = index;
		}
	}
}
