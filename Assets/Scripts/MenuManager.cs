﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SFB;

public class MenuManager : MonoBehaviour {

	public GameObject[] buttons;
	public GameObject[] scenes;

	public int sceneToLoad = 2;
	public int actionToLoad = 0;

	private string gamePath;

	public void ClickButton(GameObject button) {
		Debug.Log(button.name);
	}
	
	public void ClickScene(GameObject scene) {
		Debug.Log(scene.name);
	}
	
	void Start () {
		Cursor.visible = false;
		gamePath = PlayerPrefs.GetString("gamePath");
		Debug.Log("Menu : gamePath : " + gamePath);
		StartCoroutine(init());
	}
	
	IEnumerator init() {
		string spritePath = gamePath + "\\Sprites\\";

		for (int i = 0; i<16; i++) {
			GameObject image = scenes[i].transform.GetChild(0).gameObject;
			//Debug.Log("load miniature : " + spritePath + (i+1) + ".1.png");
			image.GetComponent<Image>().sprite = 
			RM_SaveLoad.LoadSprite(spritePath + (i+1) + ".1.png",61,25);
            //Debug.Log(spritePath+i+".1.png");
        }

		foreach (GameObject button in buttons) {
			yield return new WaitForSeconds(0.2f);
			button.SetActive (true);
		}

		for (int i = 3; i<16 ; i+=4) {
			GameObject scene = scenes[i];
			yield return new WaitForSeconds(0.2f);
			scene.SetActive (true);
		}
		for (int i = 14; i>0 ; i-=4) {
			GameObject scene = scenes[i];
			yield return new WaitForSeconds(0.2f);
			scene.SetActive (true);
		}
		for (int i = 1; i<14 ; i+=4) {
			GameObject scene = scenes[i];
			yield return new WaitForSeconds(0.2f);
			scene.SetActive (true);
		}
		for (int i = 12; i>=0 ; i-=4) {
			GameObject scene = scenes[i];
			yield return new WaitForSeconds(0.2f);
			scene.SetActive (true);
		}
		Cursor.visible = true;
	}

	public void OnNext() {
		switch(actionToLoad) {
			case 0: // Bouton scene
				SceneManager.LoadScene(sceneToLoad);
				break;
			case 1: // Bouton Draw
				PlayerPrefs.SetInt("scene",1);
				SceneManager.LoadScene(20);
				break;
			case 2: // Bouton intro
				LoadGame();
				Debug.Log(gamePath);
				Debug.Log(PlayerPrefs.GetString("gamePath"));
				break;
			default: break;
		}
	}

	private void LoadGame(){
		string[] folderPath = StandaloneFileBrowser.OpenFolderPanel("Dossier de ton jeu...", Application.dataPath + "\\", false);
		if (folderPath.Length == 0)
        {
            return;
        }
        gamePath = folderPath[0];
        PlayerPrefs.SetInt("customGame", 1);
		PlayerPrefs.SetString("gamePath", gamePath);
		Debug.Log("LoadGame (gamePath): " + gamePath);
		SceneManager.LoadScene(0);
	}
}
