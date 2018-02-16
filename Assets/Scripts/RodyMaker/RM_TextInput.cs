using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RM_TextInput : MonoBehaviour {

	private InputField inputField;
	public RM_GameManager gm;
	void Awake() {
		inputField = gameObject.GetComponent<InputField>();
		inputField.interactable = false;
	}

	public void OnEnd(string input) {
		if (input == "title"){
			gm.titleText = inputField.text;
			Debug.Log("titleText is now : " + gm.titleText);
			gm.title.GetComponent<Text>().text = inputField.text;
		}
		if (input == "introText"){

			string[] dials = gm.introText.Split('"');
			string newDial = "";
			if (inputField.text.Length > 0)
				newDial = '"' + inputField.text + '"';
				
			switch(gm.dialLayout.GetComponent<RM_DialLayout>().activeDial){
				case 1:
					if (dials.Length > 6) // 3 dials are set
						gm.introText = newDial + " " +
							   "\"" + dials[3] + "\" " + 
							   "\"" + dials[5] + "\"";
					else if (dials.Length > 4) // 2 dials
						gm.introText = newDial + " " +
							   "\"" + dials[3] + "\"";
					else 
						gm.introText = newDial;
					break;
				case 2: 
					if (dials.Length > 6) // 3 dials are set
						gm.introText = "\"" + dials[1] + "\" " + 
											   newDial + " " + 
									   "\"" + dials[5] + "\"";
					else // must be 2 dials 
						gm.introText = "\"" + dials[1] + "\" " + 
									  		   newDial;
					break;
				case 3:
					gm.introText = "\"" + dials[1] + "\" \"" + 
										  dials[3] + "\" " + 
										   newDial;
					break;
				default: break;
			}

			Debug.Log("introText is now : " + gm.introText);
			gm.introTextObj.GetComponent<Text>().text = gm.introText;
		}
		if (input == "objText"){

			RM_ObjLayout objLayoutScript = gm.objLayout.GetComponent(typeof (RM_ObjLayout)) as RM_ObjLayout;
			// Debug.Log(objLayoutScript.activeObj);

			switch (objLayoutScript.activeObj){
				case 1: gm.objText = inputField.text; 
						gm.objTextObj.GetComponent<Text>().text = inputField.text;
						Debug.Log("objText is now : " + gm.objText);
						break;
				case 2: gm.ngpText = inputField.text; 
						gm.ngpTextObj.GetComponent<Text>().text = inputField.text;
						Debug.Log("ngpText is now : " + gm.ngpText);
						break;
				case 3: gm.fswText = inputField.text; 
						gm.fswTextObj.GetComponent<Text>().text = inputField.text;
						Debug.Log("fswText is now : " + gm.fswText);
						break;
				default : break;
			}
		}
	}

	void Update() {
	}

}
