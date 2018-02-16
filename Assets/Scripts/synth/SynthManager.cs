using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynthManager : MonoBehaviour {

	public SoundManager sm;
	public InputField input;
	public Slider pitchSlider;
	[HideInInspector]
	public RM_GameManager gm;
	[HideInInspector]
	public RM_DialLayout dialLayoutScript;
	[HideInInspector]
	public RM_ObjLayout objLayoutScript;

	void Awake() {
		gm = GameObject.Find("GameManager").GetComponent(typeof(RM_GameManager)) as RM_GameManager;

        dialLayoutScript = gm.dialLayout.GetComponent(typeof(RM_DialLayout)) as RM_DialLayout;

		objLayoutScript = gm.objLayout.GetComponent(typeof (RM_ObjLayout)) as RM_ObjLayout;

		if (dialLayoutScript.isDial) {
			input.text = dialLayoutScript.phonems;
			pitchSlider.interactable = true;
			pitchSlider.value = dialLayoutScript.pitch;
		}
		if (objLayoutScript.isObj) {
			input.text = objLayoutScript.phonems;
			pitchSlider.interactable = false;
		}
	}
}
