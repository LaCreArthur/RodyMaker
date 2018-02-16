using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RM_Layout : MonoBehaviour {

	protected RM_GameManager gm;

	void Awake() {
		gm = GameObject.Find("GameManager").GetComponent<RM_GameManager>();
	}

	protected void SetLayouts(params GameObject[] layouts) {
		foreach (GameObject layout in layouts) {
			layout.SetActive(true);
		}
	}

	protected void UnsetLayouts(params GameObject[] layouts) {
		foreach (GameObject layout in layouts) {
			layout.SetActive(false);
		}
	}
	
}
