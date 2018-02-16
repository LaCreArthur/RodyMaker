using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RM_ObjectsLayout : RM_Layout {

	public void RM_ReturnClick(){
		Debug.Log("ObjectsReturn button clicked");
		SetLayouts(gm.mainLayout);
		UnsetLayouts(gm.introTextObj, gm.title, gm.objectsLayout);
	}

	public void RM_ObjClick(int obj){
		Debug.Log("Obj button clicked");
		SetLayouts(gm.objLayout);
		UnsetLayouts(gm.introTextObj, gm.objectsLayout);

		RM_ObjLayout objLayoutScript = gm.objLayout.GetComponent(typeof (RM_ObjLayout)) as RM_ObjLayout;
		objLayoutScript.activeObj = obj;
		// for the synthetiser to know it is an obj dial 
        objLayoutScript.isObj = true;

		switch (obj) {
			case 1: 
				objLayoutScript.objInputField.text = gm.objText;
				objLayoutScript.zoneNear = gm.objNear;
				objLayoutScript.zone = gm.obj;
				objLayoutScript.phonems = gm.objDial;
				gm.objNear.SetActive(true);
				break;
			case 2: 
				objLayoutScript.objInputField.text = gm.ngpText; 
				objLayoutScript.zoneNear = gm.ngpNear;
				objLayoutScript.zone = gm.ngp;
				objLayoutScript.phonems = gm.ngpDial;
				gm.ngpNear.SetActive(true);
				break;
			case 3: 
				objLayoutScript.objInputField.text = gm.fswText; 
				objLayoutScript.zoneNear = gm.fswNear;
				objLayoutScript.zone = gm.fsw;
				objLayoutScript.phonems = gm.fswDial;
				gm.fswNear.SetActive(true);
				break;
			default: break;
		}
	}
}
