using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// This very powerfull script control the univers
///</summary>

public class Rotator : MonoBehaviour {

	void Update () {
		if (transform.tag == "Pick Up")	
			transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);		
		else if (transform.tag == "Heart")
			transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);		
	}
}
