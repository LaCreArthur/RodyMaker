using UnityEngine;
using System.Collections;

public class BtnHelp : MonoBehaviour {

	public GameObject panelHelp;
	public void OnClick() {
		if (panelHelp.activeSelf)  panelHelp.SetActive(false);
		else panelHelp.SetActive(true);
	}
}
