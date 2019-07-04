using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

///<summary>
/// Script to controle the camera and the postprocess attached to it
/// Follow the player and change the color through time
///</summary>

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float smooth;
	// speed of the color variation
	public float colorSpeed;

	public float rotationX;
	public Vector2 sceneSize;
	PostProcessingBehaviour postProcess;
	Vector3 offset;

	
	void Start () {
		offset = transform.position - player.transform.position;
		postProcess = GetComponent<PostProcessingBehaviour>();
	}


	// LateUpdate is called once per frame after object calculation
	void LateUpdate () {
		// Follow the player with an offset
		Vector3 newPos = new Vector3 (player.transform.position.x / 3.0f, player.transform.position.y, player.transform.position.z);
		transform.position = newPos + offset;

		Quaternion newRot = Quaternion.Euler(rotationX, 0.0f, player.transform.position.x);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * smooth);

		// Update the post processing hueShift
		ColorGradingModel.Settings colorsettings = postProcess.profile.colorGrading.settings;
		colorsettings.basic.hueShift += Time.deltaTime * colorSpeed;
		postProcess.profile.colorGrading.settings = colorsettings;
	}

}
